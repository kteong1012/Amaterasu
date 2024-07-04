using Cysharp.Threading.Tasks;
using Game.Log;
using Nino;
using Nino.Core;
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

        private string _playerDataPath => $"{Application.persistentDataPath}/PlayerData/{AppInfo.CSharpVersion}/{LoginChannel}/{PlayerId}.pdt";

        private Dictionary<Type, PlayerData> _cachePlayerDataMap = new();
        #endregion

        #region Life Cycle
        protected override async UniTask Awake()
        {
            var loginService = GameService<LoginService>.Instance;
            await InitPlayerData(loginService.LoginChannel, loginService.PlayerId);
            LoadAll();
        }

        protected override void OnDestroy()
        {
            SaveAll();
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

            if (!File.Exists(_playerDataPath))
            {
                return;
            }
            var data = File.ReadAllBytes(_playerDataPath);

            var types = TypeManager.Instance.GetTypes().Where(IsPlayerDataClass);

            HotUpdate_Nino.Deserializer.Deserialize(data, out List<PlayerData> playerDatas);

            foreach (var playerData in playerDatas)
            {
                var type = playerData.GetType();
                _cachePlayerDataMap.Add(type, playerData);
            }
        }

        public void SaveAll()
        {
            var dirPath = Path.GetDirectoryName(_playerDataPath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            var playerDatas = _cachePlayerDataMap.Values.ToList();
            var data = HotUpdate_Nino.Serializer.Serialize(playerDatas);

            File.WriteAllBytes(_playerDataPath, data);
        }

        public T GetPlayerData<T>() where T : PlayerData
        {
            var type = typeof(T);
            if (!_cachePlayerDataMap.TryGetValue(type, out var playerData))
            {
                playerData = Activator.CreateInstance(type) as PlayerData;
                _cachePlayerDataMap.Add(type, playerData);
            }
            return playerData as T;
        }
        #endregion

        #region Private Methods

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
            if (type.GetCustomAttribute<NinoTypeAttribute>() == null)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
