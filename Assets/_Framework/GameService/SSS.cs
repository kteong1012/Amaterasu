using Cysharp.Threading.Tasks;
using Game.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Game
{
    public static class SSS
    {
        class DomainTreeNode
        {
            public GameServiceDomain Domain { get; }
            public DomainTreeNode Parent { get; private set; }
            public int Depth => Parent == null ? 0 : Parent.Depth + 1;
            private List<DomainTreeNode> _children = new List<DomainTreeNode>();


            public DomainTreeNode(GameServiceDomain domain)
            {
                Domain = domain;
            }

            public void AddChild(DomainTreeNode child)
            {
                child.Parent = this;
                _children.Add(child);
            }

            public DomainTreeNode[] GetChildren()
            {
                return _children.ToArray();
            }
        }

        private static Dictionary<GameServiceDomain, HashSet<Type>> _domainTypes;
        private static Dictionary<GameServiceDomain, bool> _domainActive = new Dictionary<GameServiceDomain, bool>();
        private static Dictionary<GameServiceDomain, List<GameService>> _services = new Dictionary<GameServiceDomain, List<GameService>>();
        private static Dictionary<GameServiceDomain, DomainTreeNode> _domainTree = new Dictionary<GameServiceDomain, DomainTreeNode>();
        public static GameServiceDomain CurrentDomain { get; private set; }

        internal static IEnumerable<GameServiceDomain> DomainActive => _domainActive.Keys;

        static SSS()
        {
            var enums = Enum.GetValues(typeof(GameServiceDomain)).Cast<GameServiceDomain>();
            _domainTypes = new Dictionary<GameServiceDomain, HashSet<Type>>();
            foreach (var domain in enums)
            {
                _domainTypes[domain] = new HashSet<Type>();
            }

            var types = TypeManager.Instance.GetTypes();
            foreach (var type in types)
            {
                // if type is abstract, skip
                if (type.IsAbstract)
                {
                    continue;
                }

                // if type is not GameService, skip
                if (!typeof(GameService).IsAssignableFrom(type))
                {
                    continue;
                }

                // if type has GameServiceAttribute, add to domainTypes
                var attribute = type.GetCustomAttribute<GameServiceAttribute>();
                if (attribute != null)
                {
                    if (!_domainTypes.TryGetValue(attribute.Domain, out var domainTypes))
                    {
                        throw new Exception($"Domain {attribute.Domain} not found in GameServiceDomain");
                    }
                    domainTypes.Add(type);
                }
            }

            // build domain tree
            // add Game as default
            GetDomainTreeNode(GameServiceDomain.Game);
            foreach (var domain in enums)
            {
                // Get GameServiceDomainParentAttribute.Parent
                var field = typeof(GameServiceDomain).GetField(domain.ToString());
                var attribute = field.GetCustomAttribute<GameServiceDomainParentAttribute>();
                if (attribute != null)
                {
                    var parentDomain = attribute.Parent;
                    var parentNode = GetDomainTreeNode(parentDomain);
                    var node = GetDomainTreeNode(domain);
                    parentNode.AddChild(node);
                }
            }
        }

        private static DomainTreeNode GetDomainTreeNode(GameServiceDomain domain)
        {
            if (!_domainTree.TryGetValue(domain, out var node))
            {
                node = new DomainTreeNode(domain);
                _domainTree[domain] = node;
            }
            return node;
        }

        public static T Get<T>() where T : GameService
        {
            foreach (var (domain, services) in _services)
            {
                if (IsDomainActive(domain))
                {
                    foreach (var service in services)
                    {
                        if (service is T t)
                        {
                            return t;
                        }
                    }
                }
            }
            GameLog.Error($"{typeof(T).Name} 的 Domain 未激活或者服务未全部启动完成");
            return null;
        }

        public static bool TryGet<T>(out T service) where T : GameService
        {
            service = Get<T>();
            return service != null;
        }

        public static bool IsDomainActive(GameServiceDomain serviceDomain)
        {
            return _domainActive.TryGetValue(serviceDomain, out var active) && active;
        }

        private static bool IsParentDomainActive(GameServiceDomain serviceDomain, bool needLog)
        {
            var node = GetDomainTreeNode(serviceDomain);
            var parentNode = node.Parent;
            if (parentNode == null)
            {
                return true;
            }
            var result = IsDomainActive(parentNode.Domain);
            if (!result && needLog)
            {
                GameLog.Error($"Domain {serviceDomain} parent domain {parentNode.Domain} is not active");
            }
            return result;
        }

        private static DomainTreeNode GetCommonAncestor(DomainTreeNode node1, DomainTreeNode node2)
        {
            while (node1 != node2)
            {
                if (node1.Depth > node2.Depth)
                {
                    node1 = node1.Parent;
                }
                else if (node2.Depth > node1.Depth)
                {
                    node2 = node2.Parent;
                }
                else
                {
                    node1 = node1.Parent;
                    node2 = node2.Parent;
                }
            }
            return node1;
        }

        public static async UniTask SetCurrentDomain(GameServiceDomain serviceDomain, bool force = false)
        {
            // check if parent domain is active
            if (!IsParentDomainActive(serviceDomain, true))
            {
                return;
            }

            var currentNode = GetDomainTreeNode(CurrentDomain);
            var newNode = GetDomainTreeNode(serviceDomain);
            if (currentNode == newNode && !force)
            {
                return;
            }

            var commonAncestor = GetCommonAncestor(currentNode, newNode);
            while (currentNode != commonAncestor)
            {
                StopDomain(currentNode.Domain);
                currentNode = currentNode.Parent;
            }

            await StartDomain(serviceDomain);
            CurrentDomain = serviceDomain;
        }

        private static async UniTask StartDomain(GameServiceDomain serviceDomain)
        {
            if (!_domainTypes.TryGetValue(serviceDomain, out var domainTypes))
            {
                return;
            }

            if (!_services.TryGetValue(serviceDomain, out var services))
            {
                services = new List<GameService>();
                _services[serviceDomain] = services;
            }

            foreach (var type in domainTypes)
            {
                var service = Activator.CreateInstance(type) as GameService;
                services.Add(service);
            }
            var initTasks = new List<UniTask>();
            foreach (var service in services)
            {
                initTasks.Add(service.__Init());
            }
            await UniTask.WhenAll(initTasks);

            _domainActive[serviceDomain] = true;

            GameLog.Debug($"Domain {serviceDomain} started, services: {string.Join(", ", services.Select(x => x.GetType().Name))}");

            var postInitTasks = new List<UniTask>();
            foreach (var service in services)
            {
                postInitTasks.Add(service.__PostInit());
            }
            await UniTask.WhenAll(postInitTasks);
        }

        private static void StopDomain(GameServiceDomain serviceDomain)
        {
            if (!_services.TryGetValue(serviceDomain, out var services))
            {
                return;
            }

            // Stop all child domains first
            var node = GetDomainTreeNode(serviceDomain);
            foreach (var child in node.GetChildren())
            {
                StopDomain(child.Domain);
            }
            foreach (var service in services)
            {
                service.__Dispose();
            }
            services.Clear();

            _domainActive[serviceDomain] = false;

            GameLog.Debug($"Domain {serviceDomain} stopped, services: {string.Join(", ", services.Select(x => x.GetType().Name))}");
        }

        public static void StopAll()
        {
            StopDomain(GameServiceDomain.Game);
        }

        public static void Update()
        {
            foreach (var services in _services.Values)
            {
                foreach (var service in services)
                {
                    service.Update();
                }
            }
        }

        public static void LateUpdate()
        {
            foreach (var services in _services.Values)
            {
                foreach (var service in services)
                {
                    service.LateUpdate();
                }
            }
        }

        public static void FixedUpdate()
        {
            foreach (var services in _services.Values)
            {
                foreach (var service in services)
                {
                    service.FixedUpdate();
                }
            }
        }
    }
}
