using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using YooAsset;

namespace Game
{
    public class UI3D_DamageText : MonoBehaviour
    {
        private AssetHandle _handle;
        public TextMeshProUGUI text;

        public static async void Create(Transform parent, string text)
        {
            var handle = YooAssets.LoadAssetSync<GameObject>("UI3D_DamageText");
            var go = handle.InstantiateSync(parent);
            go.name = $"UI3D_DamageText_{text}";
            var damageText = go.GetComponent<UI3D_DamageText>();
            damageText._handle = handle;
            damageText.text.text = text;

            //dotween
            await go.transform.DOLocalMoveY(150f, 1).SetEase(Ease.OutQuad).ToUniTask();
            Destroy(go);
        }

        private void OnDestroy()
        {
            _handle.Release();
        }
    }
}