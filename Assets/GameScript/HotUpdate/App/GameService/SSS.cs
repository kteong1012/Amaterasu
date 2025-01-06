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
        private static Dictionary<GameServiceDomain, HashSet<Type>> _domainTypes;
        private static Dictionary<GameServiceDomain, bool> _domainActive = new Dictionary<GameServiceDomain, bool>();
        private static Dictionary<GameServiceDomain, List<GameService>> _services = new Dictionary<GameServiceDomain, List<GameService>>();

        static SSS()
        {
            // Collect all GameService types
            _domainTypes = new Dictionary<GameServiceDomain, HashSet<Type>>();
            var types = HotUpdateAssemblyManager.Instance.GetTypes();
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
                        domainTypes = new HashSet<Type>();
                        _domainTypes[attribute.Domain] = domainTypes;
                    }
                    domainTypes.Add(type);
                }
            }
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

        public static async UniTask StartServices(GameServiceDomain serviceDomain)
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

            var postInitTasks = new List<UniTask>();
            foreach (var service in services)
            {
                postInitTasks.Add(service.__PostInit());
            }
            await UniTask.WhenAll(postInitTasks);
        }

        public static void StopServices(GameServiceDomain serviceDomain)
        {
            if (!_services.TryGetValue(serviceDomain, out var services))
            {
                return;
            }
            foreach (var service in services)
            {
                service.__Dispose();
            }
            services.Clear();

            _domainActive[serviceDomain] = false;
        }

        public static void StopAll()
        {
            var reverseServices = _services.Values.Reverse();
            foreach (var services in reverseServices)
            {
                foreach (var service in services)
                {
                    service.__Dispose();
                }
                services.Clear();
            }
            _domainActive.Clear();
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
