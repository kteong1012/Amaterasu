using Cysharp.Threading.Tasks;
using System.Text;
using YooAsset;

namespace Game
{

    public partial class ResourceService
    {
        #region MainPackage
        public AssetHandle LoadAssetAsync<T>(string path) where T : UnityEngine.Object
        {
            return _mainPackage.LoadAssetAsync<T>(path);
        }
        public AssetHandle LoadAssetAsync(string path)
        {
            return _mainPackage.LoadAssetAsync(path);
        }
        public AssetHandle LoadAssetSync<T>(string path) where T : UnityEngine.Object
        {
            return _mainPackage.LoadAssetSync<T>(path);
        }
        #endregion

        #region RawFilePackage
        public byte[] LoadRawFileBytesSync(string path)
        {
            var handle = _rawFilePackage.LoadRawFileSync(path);
            var data = handle.GetRawFileData();
            handle.Release();
            return data;
        }
        public async UniTask<byte[]> LoadRawFileAsync(string path)
        {
            var handle = _rawFilePackage.LoadRawFileAsync(path);
            await handle.ToUniTask();
            var data = handle.GetRawFileData();
            handle.Release();
            return data;
        }
        public string LoadRawFileTextSync(string path)
        {
            var handle = _rawFilePackage.LoadRawFileSync(path);
            var text = handle.GetRawFileText();
            handle.Release();
            return text;
        }
        public async UniTask<string> LoadRawFileTextAsync(string path)
        {
            var handle = _rawFilePackage.LoadRawFileAsync(path);
            await handle.ToUniTask();
            var text = handle.GetRawFileText();
            handle.Release();
            return text;
        }
        #endregion
    }
}
