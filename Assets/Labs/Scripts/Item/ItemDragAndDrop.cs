using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Gameplay
{
    public class ItemDragAndDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Canvas _canvas;

        private Transform _originalParent;
        private int _originalChildIndex;
        private bool _isDragging;
        private Vector2 _dragOffset;
        private RectTransform _canvasRect;

        public UnityEvent<Vector2> OnPointerUpSignal { get; private set; } = new();

        private void Start()
        {
            _canvasRect = _canvas.GetComponent<RectTransform>();
        }

        private void Update()
        {
            Drag();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _originalChildIndex = _rectTransform.GetSiblingIndex();
            _originalParent = _rectTransform.parent;

            _rectTransform.SetParent(_canvas.transform);
            _rectTransform.SetAsLastSibling();

            var localPoint = GetCanvasPosition(eventData.position);
            _dragOffset = localPoint - _rectTransform.anchoredPosition;

            _isDragging = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_isDragging) return;
            _isDragging = false;

            _rectTransform.SetParent(_originalParent);
            _rectTransform.SetSiblingIndex(_originalChildIndex);

            OnPointerUpSignal.Invoke(_rectTransform.position);
        }

        public void ResetDragging(Vector2 position)
        {
            _rectTransform.transform.position = position;
        }

        private void Drag()
        {
            if (!_isDragging) return;

            var localPoint = GetCanvasPosition(Input.mousePosition);
            _rectTransform.anchoredPosition = localPoint - _dragOffset;
        }

        private Vector2 GetCanvasPosition(Vector3 position)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvasRect,
                position,
                _canvas.worldCamera,
                out var localPoint
            );

            return localPoint;
        }
    }
}
