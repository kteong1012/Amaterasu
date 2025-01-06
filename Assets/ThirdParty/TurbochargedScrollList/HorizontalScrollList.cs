using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jing.TurbochargedScrollList
{
    /// <summary>
    /// 水平滚动列表
    /// </summary>
    public class HorizontalScrollList : BaseScrollList
    {
        public HorizontalLayoutSettings layout { get; private set; }

        public HorizontalScrollList(ScrollRect scrollRect, GameObject itemPrefab, HorizontalLayoutSettings layoutSettings = null)
        {
            if (null == layoutSettings)
            {
                layout = new HorizontalLayoutSettings();
            }
            else
            {
                layout = layoutSettings;
            }

            InitScrollView(scrollRect);            

            InitItem(itemPrefab);
        }
        protected override void ResizeContent(UpdateData updateConfig)
        {
            float w = 0;
            for (int i = 0; i < _itemModels.Count; i++)
            {
                w += (_itemModels[i].width + layout.gap);
            }
            w -= layout.gap;

            w = w + layout.paddingLeft + layout.paddingRight;

            SetContentSize(w, viewportSize.y);

            // 如果是居中或右对齐，调整内容位置
            if (layout.alignment == HorizontalLayoutSettings.Alignment.Center || layout.alignment == HorizontalLayoutSettings.Alignment.Right)
            {
                float contentWidth = content.rect.width;
                float viewportWidth = viewportSize.x;
                if (contentWidth < viewportWidth)
                {
                    float offset = (viewportWidth - contentWidth) / 2;
                    if (layout.alignment == HorizontalLayoutSettings.Alignment.Right)
                    {
                        offset = viewportWidth - contentWidth;
                    }
                    content.localPosition = new Vector3(offset, content.localPosition.y, content.localPosition.z);
                }
            }
        }

        protected override void Refresh(UpdateData updateConfig, out int lastStartIndex)
        {
            var contentWidth = content.rect.width;

            if (updateConfig.keepPaddingType == EKeepPaddingType.END)
            {
                var targetRenderStartPos = (contentWidth - updateConfig.tempLastContentRect.width) + contentRenderStartPos;
                var temp = content.localPosition;
                temp.x = -targetRenderStartPos;
                content.localPosition = temp;
            }

            contentRenderStartPos = -content.localPosition.x;

            if (contentRenderStartPos < 0)
            {
                contentRenderStartPos = 0;
            }
            else if (contentRenderStartPos > contentWidth - viewportSize.x)
            {
                contentRenderStartPos = contentWidth - viewportSize.x;
            }

            int dataIdx;
            float startPos = layout.paddingLeft;

            for (dataIdx = 0; dataIdx < _itemModels.Count; dataIdx++)
            {
                var dataRight = startPos + _itemModels[dataIdx].width;
                if (dataRight >= contentRenderStartPos)
                {
                    break;
                }

                startPos = dataRight + layout.gap;
            }
            lastStartIndex = dataIdx;

            float contentWidthLimit = viewportSize.x;
            float itemX = startPos;

            Dictionary<ScrollListItemModel, ScrollListItem> lastShowingItems = new Dictionary<ScrollListItemModel, ScrollListItem>(_showingItems);

            _showingItems.Clear();

            while (dataIdx < _itemModels.Count)
            {
                var model = _itemModels[dataIdx];

                ScrollListItem item = CreateItem(model, dataIdx, lastShowingItems);
                _showingItems[model] = item;

                var pos = Vector3.zero;
                pos.x = itemX;
                item.rectTransform.localPosition = pos;
                itemX += (item.width + layout.gap);
                dataIdx++;

                if (itemX - contentRenderStartPos >= contentWidthLimit)
                {
                    break;
                }
            }

            RecycleUselessItems(lastShowingItems);

            // 如果是居中或右对齐，调整内容位置
            if (layout.alignment == HorizontalLayoutSettings.Alignment.Center || layout.alignment == HorizontalLayoutSettings.Alignment.Right)
            {
                float totalContentWidth = itemX - layout.gap + layout.paddingLeft + layout.paddingRight;
                float viewportWidth = viewportSize.x;
                if (totalContentWidth < viewportWidth)
                {
                    float offset = (viewportWidth - totalContentWidth) / 2;
                    if (layout.alignment == HorizontalLayoutSettings.Alignment.Right)
                    {
                        offset = viewportWidth - totalContentWidth;
                    }
                    content.localPosition = new Vector3(offset, content.localPosition.y, content.localPosition.z);
                }
            }
        }

        protected override bool AdjustmentItemSize(ScrollListItem item)
        {
            if (item.width != _itemModels[item.index].width)
            {
                //Debug.Log($"item[{item.index}]的尺寸改变 {_itemModels[item.index].width} => {item.width}");
                _itemModels[item.index].width = item.width;
                return true;
            }
            return false;
        }

        public override void ScrollToItem(int index)
        {
            if (index < 0)
            {
                index = 0;
            }
            else if (index >= _itemModels.Count)
            {
                index = _itemModels.Count - 1;
            }

            float pos = layout.paddingLeft;
            for (int i = 0; i < index; i++)
            {
                pos += (_itemModels[i].width + layout.gap);
            }

            ScrollToPosition(pos);
        }

        public void ScrollToPosition(float position)
        {
            ScrollToPosition(new Vector2(position, 0));
        }
    }
}