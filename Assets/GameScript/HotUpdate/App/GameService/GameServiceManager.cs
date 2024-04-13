using Cysharp.Threading.Tasks;
using Game.Log;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Game
{
    public class GameServiceManager
    {
        private Dictionary<GameServiceLifeSpan, List<Type>> _serviceTypes = new Dictionary<GameServiceLifeSpan, List<Type>>();
        private Dictionary<GameServiceLifeSpan, Dictionary<Type, GameService>> _services = new Dictionary<GameServiceLifeSpan, Dictionary<Type, GameService>>();


        public GameServiceManager()
        {
            var assembly = typeof(GameService).Assembly;
            foreach (var type in assembly.GetTypes())
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

                    if(attribute.LifeSpan == GameServiceLifeSpan.None)
                    {
                        throw new Exception($"GameService {type.Name} 的 LifeSpan 不能为 None");
                    }

                    if (!_serviceTypes.ContainsKey(attribute.LifeSpan))
                    {
                        _serviceTypes.Add(attribute.LifeSpan, new List<Type>());
                    }

                    _serviceTypes[attribute.LifeSpan].Add(type);
                }
            }
        }

        public async UniTask CreateServices(GameServiceLifeSpan lifeSpan)
        {
            var types = GetServiceTypes(lifeSpan);
            if (types == null)
            {
                GameLog.Info($"没有找到 {lifeSpan} 的服务");
                return;
            }

            if (_services.TryGetValue(lifeSpan, out var services))
            {
                GameLog.Warning($"已经存在 {lifeSpan} 的服务");
                return;
            }
            GameLog.Debug($"创建 {lifeSpan} 的服务");

            var serviceDict = new Dictionary<Type, GameService>();

            foreach (var type in types)
            {
                var service = Activator.CreateInstance(type) as GameService;
                serviceDict.Add(type, service);
            }

            // 注入GameService类型的字段和属性
            foreach (var service in serviceDict.Values)
            {
                var members = service.GetType().GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                foreach (var member in members)
                {
                    var attribute = member.GetCustomAttribute<GameServiceAttribute>();
                    if (attribute == null)
                    {
                        continue;
                    }
                    // 此处特殊判断
                    if (lifeSpan == GameServiceLifeSpan.Game && attribute.LifeSpan == GameServiceLifeSpan.Login)
                    {
                        throw new Exception($"GameService {service.GetType().Name}.{member.Name} Game 不能依赖于 Login 生命周期的服务");
                    }

                    var fieldType = member.GetFieldType();
                    if (!serviceDict.ContainsKey(fieldType))
                    {
                        throw new Exception($"GameService {service.GetType().Name} 的 {member.Name} 字段类型 {fieldType.Name} 没有找到对应的服务");
                    }

                    var value = _services[lifeSpan][fieldType];
                    member.SetValue(service, value);
                }
            }

            await UniTask.WhenAll(serviceDict.Values.Select(service => service.Init()));
            _services.Add(lifeSpan, serviceDict);
        }

        public void ReleaseServices(GameServiceLifeSpan lifeSpan)
        {
            if (!_services.TryGetValue(lifeSpan, out var services))
            {
                return;
            }

            foreach (var service in services.Values)
            {
                service.Release();
            }
            services.Clear();
            _services.Remove(lifeSpan);
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
            foreach (var services in _services.Values)
            {
                foreach (var service in services.Values)
                {
                    service.Release();
                }
            }
            _services.Clear();
        }

        private List<Type> GetServiceTypes(GameServiceLifeSpan lifeSpan)
        {
            if (_serviceTypes.TryGetValue(lifeSpan, out var types))
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
