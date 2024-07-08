using Cysharp.Threading.Tasks;
using Game.Log;
using Luban;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

namespace Game.Cfg
{

    [GameService(GameServiceDomain.Game)]
    public partial class ConfigService : GameService
    {
        protected override async UniTask Start()
        {
            GameLog.Info("====初始化配置====");
            await LoadAll(Loader);
            GameLog.Info("====初始化配置完成====");
        }

        private async UniTask<ByteBuf> Loader(string tableName)
        {
            GameLog.Debug($"加载配置表 {tableName}");
            var bytes = await GameServices.ResourceService.LoadRawFileAsync(tableName);
            return new ByteBuf(bytes);
        }
    }
}
