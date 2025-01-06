using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public delegate void DroppableObjectHandler(DroppableObject dropObj, DraggableObject dragObj, PointerEventData eventData);
    public class DroppableObject : MonoBehaviour, IDroppableObject
    {
        private DroppableObjectHandler _onDrop;
        private DroppableObjectHandler _onDraggableObjectEnter;
        private DroppableObjectHandler _onDraggableObjectExit;

        public void AddOnDropListener(DroppableObjectHandler handler)
        {
            _onDrop += handler;
        }

        public void RemoveOnDropListener(DroppableObjectHandler handler)
        {
            _onDrop -= handler;
        }

        public void AddOnDraggableObjectEnterListener(DroppableObjectHandler handler)
        {
            _onDraggableObjectEnter += handler;
        }

        public void RemoveOnDraggableObjectEnterListener(DroppableObjectHandler handler)
        {
            _onDraggableObjectEnter -= handler;
        }

        public void AddOnDraggableObjectExitListener(DroppableObjectHandler handler)
        {
            _onDraggableObjectExit += handler;
        }

        public void RemoveOnDraggableObjectExitListener(DroppableObjectHandler handler)
        {
            _onDraggableObjectExit -= handler;
        }

        public void ClearOnDropListeners()
        {
            _onDrop = null;
        }

        public void ClearOnDraggableObjectEnterListeners()
        {
            _onDraggableObjectEnter = null;
        }


        protected virtual bool CheckDraggableObject(DraggableObject dragObj) => true;

        private CursorType _lastCursorType;

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            var dragObj = SSS.Get<UIService>().Target;
            if (dragObj == null)
            {
                return;
            }
            if (!CanDrop(dragObj))
            {
                dragObj.OnEndDrag(false, eventData);
                return;
            }
            _onDrop?.Invoke(this, dragObj, eventData);
            dragObj.OnEndDrag(true, eventData);
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            //var dragObj = UIDragManager.Target;
            //if (dragObj == null)
            //{
            //    return;
            //}
            //if (!CanDrop(dragObj))
            //{
            //    _lastCursorType = GameCursor.CurrentCursorType;
            //    GameCursor.SetCursorType(CursorType.State_Invalid);
            //    return;
            //}
            //_onDraggableObjectEnter?.Invoke(this, dragObj, eventData);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            //var dragObj = UIDragManager.Target;
            //if (dragObj == null)
            //{
            //    return;
            //}
            //if (!CanDrop(dragObj))
            //{
            //    GameCursor.SetCursorType(_lastCursorType);
            //    return;
            //}
            //_onDraggableObjectExit?.Invoke(this, dragObj, eventData);
        }

        private bool CanDrop(DraggableObject dragObj)
        {
            if (!CheckDraggableObject(dragObj))
            {
                return false;
            }
            return true;
        }
    }
}
