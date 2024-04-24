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
        public TextMeshProUGUI text;

        public async UniTask StartFloating(string content)
        {
            name = $"UI3D_DamageText_{content}";
            text.text = content;

            //become to a wild panel and play animation
            var newParent = MainCamera.Instance.worldCanvasRoot;
            transform.SetParent(newParent, true);
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
            await transform.DOLocalMoveY(transform.localPosition.y + 150f, 1).SetEase(Ease.OutQuad).ToUniTask();
        }
    }
}