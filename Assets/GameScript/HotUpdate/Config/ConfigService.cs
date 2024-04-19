using Cysharp.Threading.Tasks;
using Game.Log;
using Luban;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

namespace Game.Cfg
{

    [GameService(GameServiceLifeSpan.Game)]
    public partial class ConfigService : GameService
    {
        private List<AssetHandle> _assetHandles = new List<AssetHandle>();
        public override async UniTask Init()
        {
            GameLog.Info("====��ʼ������====");
            await LoadAll(Loader);
            _assetHandles.ForEach(handle => handle.Release());
            GameLog.Info("====��ʼ���������====");
        }

        private async UniTask<ByteBuf> Loader(string tableName)
        {
            GameLog.Debug($"�������ñ� {tableName}");
            var handle = YooAssets.LoadAssetAsync($"{tableName}");
            await handle.ToUniTask();
            var textAsset = handle.GetAssetObject<TextAsset>();
            var bytes = textAsset.bytes;
            _assetHandles.Add(handle);
            return new ByteBuf(bytes);
        }
    }
}
