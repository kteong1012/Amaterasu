using Cysharp.Threading.Tasks;
using Game.Log;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Game
{
    public class GameServiceManager
    {
        private Dictionary<GameServiceDomain, List<Type>> _serviceTypes = new Dictionary<GameServiceDomain, List<Type>>();
        private Dictionary<GameServiceDomain, Dictionary<Type, GameService>> _services = new Dictionary<GameServiceDomain, Dictionary<Type, GameService>>();


        public GameServiceManager()
        {
            foreach (var type in TypeManager.Instance.GetTypes())
            {
                if (type.IsAbstract)
                {
                    continue;
                }

                if (type.IsSubclassOf(typeof(GameService)))
                {
                    var attribute = type.GetCustomAttribute<GameServiceAttribute>();
                    if (attribute == null)
                    {
                        throw new Exception($"GameService {type.Name} 必须添加 GameServiceAttribute 属性");
                    }

                    if (attribute.Domain == GameServiceDomain.None)
                    {
                        throw new Exception($"GameService {type.Name} 的 Domain 不能为 None");
                    }

                    if (!_serviceTypes.ContainsKey(attribute.Domain))
                    {
                        _serviceTypes.Add(attribute.Domain, new List<Type>());
                    }

                    _serviceTypes[attribute.Domain].Add(type);
                }
            }
        }

        public async UniTask StartServices(GameServiceDomain domain)
        {
            var types = GetServiceTypes(domain);
            if (types == null)
            {
                GameLog.Info($"没有找到 {domain} 的服务");
                return;
            }

            if (_services.TryGetValue(domain, out var services))
            {
                GameLog.Warning($"已经存在 {domain} 的服务");
                return;
            }
            GameLog.Debug($"创建 {domain} 的服务");

            var serviceDict = new Dictionary<Type, GameService>();

            foreach (var type in types)
            {
                var service = Activator.CreateInstance(type) as GameService;
                serviceDict.Add(type, service);
            }
            _services.Add(domain, serviceDict);

            // 注入GameService类型的字段和属性
            foreach (var service in serviceDict.Values)
            {
                var members = service.GetType().GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                foreach (var member in members)
                {
                    if (member.MemberType != MemberTypes.Field && member.MemberType != MemberTypes.Property)
                    {
                        continue;
                    }
                    var attribute = member.GetFieldType().GetCustomAttribute<GameServiceAttribute>();
                    if (attribute == null)
                    {
                        continue;
                    }
                    // 此处特殊判断
                    if (domain == GameServiceDomain.Game && attribute.Domain == GameServiceDomain.Login)
                    {
                        throw new Exception($"GameService {service.GetType().Name}.{member.Name} Game 不能依赖于 Login 生命周期的服务");
                    }

                    var fieldType = member.GetFieldType();
                    GameService value = null;
                    foreach (var serviceType in _services.Values)
                    {
                        if (serviceType.ContainsKey(fieldType))
                        {
                            value = serviceType[fieldType];
                            member.SetValue(service, value);
                            break;
                        }
                    }
                    if (value == null)
                    {
                        throw new Exception($"GameService {service.GetType().Name} 的 {member.Name} 字段类型 {fieldType.Name} 没有找到对应的服务");
                    }

                    member.SetValue(service, value);
                }
            }

            await serviceDict.Values.Select(service => service.Init());
            await serviceDict.Values.Select(service => service.PostInit());
        }

        public void StopServices(GameServiceDomain domain)
        {
            if (!_services.TryGetValue(domain, out var services))
            {
                return;
            }

            foreach (var service in services.Values)
            {
                service.Destroy();
            }
            services.Clear();
            _services.Remove(domain);
        }

        public void Update()
        {
            foreach (var services in _services.Values)
            {
                foreach (var service in services.Values)
                {
                    service.Update();
                }
            }
        }

        public void Release()
        {
            var orderedServices = _services.OrderByDescending(s => (int)s.Key);
            foreach (var services in orderedServices)
            {
                foreach (var service in services.Value.Values)
                {
                    service.Destroy();
                }
            }
            _services.Clear();
        }

        private List<Type> GetServiceTypes(GameServiceDomain domain)
        {
            if (_serviceTypes.TryGetValue(domain, out var types))
            {
                return types;
            }

            return null;
        }

        public T GetService<T>() where T : GameService
        {
            foreach (var services in _services.Values)
            {
                if (services == null)
                {
                    continue;
                }
                if (services.TryGetValue(typeof(T), out var service))
                {
                    return service as T;
                }
            }

            return null;
        }
    }
}
