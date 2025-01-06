using Game.Log;
using Jing.TurbochargedScrollList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using YooAsset;

namespace Game
{
    [RequireComponent(typeof(RectTransform), typeof(ScrollRect))]
    public class ScrollList : MonoBehaviour, IScrollList
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

        private ScrollRect _scrollRect;
        public ScrollRect scrollRect
        {
            get
            {
                if (_scrollRect == null)
                {
                    _scrollRect = GetComponent<ScrollRect>();
                }
                return _scrollRect;
            }
        }

        public GameObject itemPrefab;

        private BaseScrollList _scrollList;

        private void LoadItemPrefab(string itemPrefabPath)
        {
            var handle = SSS.Get<ResourceService>().LoadAssetSync<GameObject>(itemPrefabPath);
            itemPrefab = handle.InstantiateSync(scrollRect.transform);
            itemPrefab.SetActive(false);
            handle.Release();
        }

        public void SetAsHorizontal(string itemPrefabPath, RenderItemDelegate onRenderItem = null, Jing.TurbochargedScrollList.HorizontalLayoutSettings settings = null)
        {
            LoadItemPrefab(itemPrefabPath);
            SetAsHorizontal(onRenderItem, settings);
        }

        public void SetAsHorizontal(RenderItemDelegate onRenderItem = null, Jing.TurbochargedScrollList.HorizontalLayoutSettings settings = null)
        {
            if (_scrollList != null)
            {
                GameLog.Warning($"重复设置ScrollList,已经是{_scrollList.GetType().Name}类型, 无法再次设置为 HorizontalScrollLis", this);
                return;
            }
            if (itemPrefab == null)
            {
                GameLog.Error("itemPrefab 不能为空", this);
                return;
            }
            if (settings == null)
            {
                if (scrollRect.gameObject.GetComponent<HorizontalTurboLayoutSettings>() != null)
                {
                    settings = scrollRect.gameObject.GetComponent<HorizontalTurboLayoutSettings>().settings;
                }
            }
            _scrollList = new HorizontalScrollList(scrollRect, itemPrefab, settings);
            _scrollList.onRenderItem = onRenderItem;
        }

        public void SetAsVertical(string itemPrefabPath, RenderItemDelegate onRenderItem = null, VerticalLayoutSettings settings = null)
        {
            LoadItemPrefab(itemPrefabPath);
            SetAsVertical(onRenderItem, settings);
        }
        public void SetAsVertical(RenderItemDelegate onRenderItem = null, VerticalLayoutSettings settings = null)
        {
            if (_scrollList != null)
            {
                GameLog.Warning($"重复设置ScrollList,已经是{_scrollList.GetType().Name}类型, 无法再次设置为 VerticalScrollList", this);
                return;
            }
            if (itemPrefab == null)
            {
                GameLog.Error("itemPrefab 不能为空", this);
                return;
            }
            if (settings == null)
            {
                if (scrollRect.gameObject.GetComponent<VerticalTurboLayoutSettings>() != null)
                {
                    settings = scrollRect.gameObject.GetComponent<VerticalTurboLayoutSettings>().settings;
                }
            }
            _scrollList = new VerticalScrollList(scrollRect, itemPrefab, settings);
            _scrollList.onRenderItem = onRenderItem;
        }

        public void SetAsGrid(string itemPrefabPath, RenderItemDelegate onRenderItem = null, GridLayoutSettings settings = null)
        {
            LoadItemPrefab(itemPrefabPath);
            SetAsGrid(onRenderItem, settings);
        }
        public void SetAsGrid(RenderItemDelegate onRenderItem = null, GridLayoutSettings settings = null)
        {
            if (_scrollList != null)
            {
                GameLog.Warning($"重复设置ScrollList,已经是{_scrollList.GetType().Name}类型, 无法再次设置为 GridScrollList", this);
                return;
            }
            if (itemPrefab == null)
            {
                GameLog.Error("itemPrefab 不能为空", this);
                return;
            }
            if (settings == null)
            {
                if (scrollRect.gameObject.GetComponent<GridTurboLayoutSettings>() != null)
                {
                    settings = scrollRect.gameObject.GetComponent<GridTurboLayoutSettings>().settings;
                }
            }

            _scrollList = new GridScrollList(scrollRect, itemPrefab, settings);
            _scrollList.onRenderItem = onRenderItem;
        }


        public int ItemCount => TryGetScrollList().ItemCount;

        public float ContentWidth => TryGetScrollList().ContentWidth;

        public float ContentHeight => TryGetScrollList().ContentHeight;

        public RenderItemDelegate onRenderItem { get => TryGetScrollList().onRenderItem; set => TryGetScrollList().onRenderItem = value; }
        public Action onRebuildContent { get => TryGetScrollList().onRebuildContent; set => TryGetScrollList().onRebuildContent = value; }
        public Action onRefresh { get => TryGetScrollList().onRefresh; set => TryGetScrollList().onRefresh = value; }
        public ItemBeforeReuseDelegate onItemBeforeReuse { get => TryGetScrollList().onItemBeforeReuse; set => TryGetScrollList().onItemBeforeReuse = value; }

        public void RemoveAt(int index)
        {
            TryGetScrollList().RemoveAt(index);
        }

        public void Clear()
        {
            TryGetScrollList().Clear();
        }

        public bool CheckItemShowing(int index)
        {
            return TryGetScrollList().CheckItemShowing(index);
        }

        public void ScrollToItem(int index)
        {
            TryGetScrollList().ScrollToItem(index);
        }

        public void ScrollToPosition(Vector2 position)
        {
            TryGetScrollList().ScrollToPosition(position);
        }

        public Vector2 GetDistanceToEnd()
        {
            return TryGetScrollList().GetDistanceToEnd();
        }

        public void AddRange<TData>(IEnumerable<TData> collection)
        {
            TryGetScrollList().AddRange(collection);
        }

        public void Add(object data)
        {
            TryGetScrollList().Add(data);
        }

        public void Insert(int index, object data)
        {
            TryGetScrollList().Insert(index, data);
        }

        public bool Remove(object data)
        {
            return TryGetScrollList().Remove(data);
        }

        public T[] GetDatasCopy<T>()
        {
            return TryGetScrollList().GetDatasCopy<T>();
        }

        public void UpdateData<TData>(IEnumerable<TData> collection)
        {
            if (collection.IsNullOrEmpty())
            {
                Clear();
                return;
            }
            else
            {
                TryGetScrollList().UpdateDatas(collection);
                if (SelectIndex < 0)
                {
                    SelectIndex = 0;
                }
            }
        }

        private BaseScrollList TryGetScrollList()
        {
            if (_scrollList == null)
            {
                throw new Exception("请先调用 SetAsHorizontal, SetAsVertical或者 SetAsGrid 方法设置 ScrollList 类型");
            }
            return _scrollList;
        }

        public int SelectIndex
        {
            get => TryGetScrollList().SelectIndex;
            set => TryGetScrollList().SelectIndex = value;
        }
        public Action<int, object> onSelectItem
        {
            get => TryGetScrollList().onSelectItem;
            set => TryGetScrollList().onSelectItem = value;
        }
    }
}