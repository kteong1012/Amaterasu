using Cysharp.Threading.Tasks;
using Game.Log;
using Nino.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Game
{
    [GameService(GameServiceDomain.Login)]
    public class PlayerDataService : GameService
    {
        #region Fields & Properties
        public LoginChannel LoginChannel { get; private set; }

        public string PlayerId { get; private set; }

        private string _PlayerDataDir => $"{Application.persistentDataPath}/PlayerData/{LoginChannel}/{PlayerId}";

        private Dictionary<Type, PlayerData> _cachePlayerDataMap = new();
        #endregion

        #region Life Cycle
        protected override void OnDestroy()
        {
            _cachePlayerDataMap.Clear();
            base.OnDestroy();
        }
        #endregion

        #region Public Methods
        public UniTask InitPlayerData(LoginChannel loginChannel, string playerId)
        {
            LoginChannel = loginChannel;
            PlayerId = playerId;
            GameLog.Info($"PlayerDataService InitPlayerData, LoginChannel: {LoginChannel}, PlayerId: {PlayerId}");


            return UniTask.CompletedTask;
        }

        public void LoadAll()
        {
            _cachePlayerDataMap.Clear();

            var types = TypeManager.Instance.GetTypes().Where(IsPlayerDataClass);

            foreach (var type in types)
            {
                var fileName = Path.Combine(_PlayerDataDir, GetPlayerDataFileName(type));
                PlayerData playerData = null;
                if (File.Exists(fileName))
                {
                    var bytes = File.ReadAllBytes(fileName);
                    playerData = Deserializer.Deserialize(type, bytes) as PlayerData;
                }
                if (playerData == null)
                {
                    playerData = Activator.CreateInstance(type) as PlayerData;
                }
                _cachePlayerDataMap.Add(type, playerData);
            }
        }

        public async UniTask SaveAll()
        {
            if (!Directory.Exists(_PlayerDataDir))
            {
                Directory.CreateDirectory(_PlayerDataDir);
            }

            var tasks = new List<UniTask>();
            foreach (var kv in _cachePlayerDataMap)
            {
                var type = kv.Key;
                var playerData = kv.Value;
                var fileName = Path.Combine(_PlayerDataDir, GetPlayerDataFileName(type));
                var bytes = Serializer.Serialize(playerData);
                var task = File.WriteAllBytesAsync(fileName, bytes).AsUniTask();
                tasks.Add(task);
            }
            await tasks;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// 类型全名，并把小数点替换成下划线
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetPlayerDataFileName(Type type)
        {
            return type.FullName.Replace(".", "_");
        }

        private static bool IsPlayerDataClass(Type type)
        {
            if (!type.IsSubclassOf(typeof(PlayerData)))
            {
                return false;
            }
            if (type.IsAbstract)
            {
                return false;
            }
            if (type.GetCustomAttribute<NinoSerializeAttribute>() == null)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
