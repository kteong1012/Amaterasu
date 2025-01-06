using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Jing.TurbochargedScrollList
{
    /// <summary>
    /// 滚动列表项
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class ScrollListItem : MonoBehaviour
    {
        private RectTransform _rectTransform;
        public RectTransform rectTransform
        {
            get
            {
                if (_rectTransform == null)
                {
                    _rectTransform = GetComponent<RectTransform>();
                }
                return _rectTransform;
            }
        }

        /// <summary>
        /// Item的索引位置
        /// </summary>
        public int index { get; internal set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object data { get; internal set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public float width
        {
            get { return rectTransform.rect.width; }
            set
            {
                if (rectTransform.rect.width != value)
                {
                    rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value);
                }
            }
        }

        /// <summary>
        /// 高度
        /// </summary>
        public float height
        {
            get { return rectTransform.rect.height; }
            set
            {
                if (rectTransform.rect.height != value)
                {
                    rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, value);
                }
            }
        }

        public override string ToString()
        {
            return $"[Index = {index}, data = {data}, width = {width}, height = {height}]";
        }

        public virtual void SetSelect(bool select)
        {
        }

        public virtual Button SelectButton => null;
    }
}