using Cysharp.Threading.Tasks;
using Game.Log;
using UnityEngine;

namespace Game
{
    public partial class UIService
    {
        private DraggableObject _target;
        public DraggableObject Target
        {
            get => _target;
            set
            {
                if (value != null && _target == value)
                {
                    return;
                }
                _target = value;
                if (_target == null)
                {
                    if (_dragCopy != null)
                    {
                        Object.DestroyImmediate(_dragCopy);
                    }
                }
                else
                {
                    if (_target.ShowDragCopy)
                    {
                        if (_dragCopy != null)
                        {
                            Object.DestroyImmediate(_dragCopy);
                        }
                        var rect = _target.GetComponent<RectTransform>();
                        var width = rect.rect.width;
                        var height = rect.rect.height;

                        _dragCopy = Object.Instantiate(_target.DragCopyObj, _DragLayer);
                        // pivot : (0.5,0.5)  Anchor : (0.5,0.5)
                        var dragRect = _dragCopy.GetComponent<RectTransform>();
                        dragRect.anchorMin = Vector2.one / 2;
                        dragRect.anchorMax = Vector2.one / 2;
                        dragRect.pivot = Vector2.one / 2;
                        dragRect.sizeDelta = new Vector2(width, height);
                    }
                }
            }
        }
        private GameObject _dragCopy;

        public void UpdateDragCopyPosition(Vector3 position)
        {
            if (_dragCopy != null)
            {
                var worldPos = ScreenToWorld(position);
                _dragCopy.transform.position = worldPos;
                // z = 0
                _dragCopy.transform.localPosition = new Vector3(_dragCopy.transform.localPosition.x, _dragCopy.transform.localPosition.y, 0);
            }
        }

        private void UpdateDrag()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (Target != null)
                {
                    Target = null;
                }
            }
        }
    }
}
