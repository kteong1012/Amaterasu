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
        private static AssetHandle _handle;
        public TextMeshProUGUI text;

        public static async void Create(Transform parent, string text)
        {
            if (_handle == null)
            {
                _handle = YooAssets.LoadAssetSync<GameObject>("UI3D_DamageText");
            }

            var go = _handle.InstantiateSync(parent);
            go.transform.localPosition = Vector3.zero;
            go.name = $"UI3D_DamageText_{text}";
            var damageText = go.GetComponent<UI3D_DamageText>();
            damageText.text.text = text;

            //become to a wild panel and play animation
            var newParent = MainCamera.Instance.worldCanvasRoot;
            go.transform.SetParent(newParent, true);
            await go.transform.DOLocalMoveY(go.transform.localPosition.y + 150f, 1).SetEase(Ease.OutQuad).ToUniTask();
            Destroy(go);
        }
    }
}