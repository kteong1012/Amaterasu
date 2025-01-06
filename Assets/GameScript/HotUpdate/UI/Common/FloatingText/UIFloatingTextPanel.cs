using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game
{
    [UI2D("UIFloatingTextPanel")]
    public partial class UIFloatingTextPanel : UI2DPanel
    {
        public override UI2DPanelLayer Layer => UI2DPanelLayer.Tips;
        public override UI2DPanelOptions Options => UI2DPanelOptions.None;

        [SerializeField]
        private GameObject _textPrefab;

        private const float _minStayTime = 0.2f;
        private const float _maxStayTime = 1f;
        private GameObjectPool _pool;
        private Queue<string> _queue = new Queue<string>();
        private Dictionary<float, bool> _positions;
        private const float _textHeight = 60f;
        private const float _maxY = 250f;
        private const float _minY = -450f;

        protected override void OnCreate()
        {
            _pool = new GameObjectPool(_textPrefab);

            _positions = new Dictionary<float, bool>();

            for (float i = _maxY; i >= _minY; i -= _textHeight)
            {
                _positions.Add(i, false);
            }
        }

        private void ShowText(string message, float y, float stayTime)
        {
            AsyncMethod().Forget();
            return;

            async UniTaskVoid AsyncMethod()
            {
                var go = _pool.Get(transform);
                if (!go)
                {
                    return;
                }

                var text = go.GetComponent<TMPro.TextMeshProUGUI>();
                text.text = message;

                // dotween动画
                // 初始状态：透明度0，缩放0.5，Y轴 -500
                text.alpha = 0;
                text.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                text.transform.localPosition = new Vector3(text.transform.localPosition.x, -500f, text.transform.localPosition.z);

                // 透明度1，缩放1，Y轴 150
                var task1 = text.DOFade(1, 0.2f).ToUniTask();
                var task2 = text.transform.DOScale(1, 0.2f).ToUniTask();
                var task3 = text.transform.DOLocalMoveY(y, 0.2f).ToUniTask();
                await UniTask.WhenAll(task1, task2, task3);
                await UniTask.Delay(TimeSpan.FromSeconds(stayTime));
                await text.DOFade(0, 1f).ToUniTask();
                _pool.Release(go);

                _positions[y] = false;
            }
        }

        public void ShowText(string message)
        {
            var panel = SSS.Get<UIService>().OpenPanel<UIFloatingTextPanel>();
            panel._queue.Enqueue(message);
            panel.Next();
        }

        private void Next()
        {
            if (!_queue.TryDequeue(out var text))
            {
                return;
            }

            // queue的元素越多，staytime越短
            var stayTime = _maxStayTime - (_queue.Count * 0.2f);
            stayTime = Mathf.Clamp(stayTime, _minStayTime, _maxStayTime);

            // 获取一个未使用的位置
            var y = 0f;
            foreach (var item in _positions)
            {
                if (!item.Value)
                {
                    y = item.Key;
                    _positions[item.Key] = true;
                    break;
                }
            }

            ShowText(text, y, stayTime);
        }
    }

    public static class FloatingText
    {
        public static void Show(string message)
        {
            var panel = SSS.Get<UIService>().OpenPanel<UIFloatingTextPanel>();
            panel.ShowText(message);
        }
    }
}