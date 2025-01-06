using Game.Log;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public delegate void DragObjectHandler(DraggableObject dragObj, PointerEventData eventData);

    public abstract class DraggableObject : MonoBehaviour, IDraggableObject
    {
        private event DragObjectHandler _onBeginDrag;
        private event DragObjectHandler _onDrag;
        private event DragObjectHandler _onEndDrag;

        // 拖拽副本（半透明物体）
        [SerializeField]
        private GameObject _dragCopyObj;
        public GameObject DragCopyObj => _dragCopyObj ??= gameObject;
        public bool ShowDragCopy { get; set; } = true;
        private Vector2 _dragOffset;
        public abstract DraggableObjectType Type { get; }

        public void AddOnBeginDragListener(DragObjectHandler handler)
        {
            _onBeginDrag += handler;
        }

        public void AddOnDragListener(DragObjectHandler handler)
        {
            _onDrag += handler;
        }

        public void AddOnEndDragListener(DragObjectHandler handler)
        {
            _onEndDrag += handler;
        }

        public void RemoveOnBeginDragListener(DragObjectHandler handler)
        {
            _onBeginDrag -= handler;
        }

        public void RemoveOnDragListener(DragObjectHandler handler)
        {
            _onDrag -= handler;
        }

        public void RemoveOnEndDragListener(DragObjectHandler handler)
        {
            _onEndDrag -= handler;
        }

        public void ClearOnBeginDragListener()
        {
            _onBeginDrag = null;
        }

        public void ClearOnDragListener()
        {
            _onDrag = null;
        }

        public void ClearOnEndDragListener()
        {
            _onEndDrag = null;
        }

        protected virtual void BeginDrag(PointerEventData eventData)
        {
        }

        protected virtual void Drag(PointerEventData eventData)
        {
        }

        protected virtual void EndDrag(bool success, PointerEventData eventData)
        {


        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            SSS.Get<UIService>().Target = this;
            var uiPos = SSS.Get<UIService>().WorldToScreen(transform.position);
            _dragOffset = uiPos - eventData.position;
            var rt = GetComponent<RectTransform>();
            var halfWidth = rt.rect.width / 1f;
            var helfHeight = rt.rect.height / 1f;
            var pivot = rt.pivot;
            _dragOffset.x += halfWidth * (0.5f - pivot.x);
            _dragOffset.y += helfHeight * (0.5f - pivot.y);

            BeginDrag(eventData);
            _onBeginDrag?.Invoke(this, eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (ShowDragCopy)
            {
                var dragCopyPosition = eventData.position + _dragOffset;
                SSS.Get<UIService>().UpdateDragCopyPosition(dragCopyPosition);
            }
            Drag(eventData);
            _onDrag?.Invoke(this, eventData);
        }

        public void OnEndDrag(bool dropSuccess, PointerEventData eventData)
        {
            EndDrag(dropSuccess, eventData);
            _onEndDrag?.Invoke(this, eventData);
            SSS.Get<UIService>().Target = null;
        }
    }
}
