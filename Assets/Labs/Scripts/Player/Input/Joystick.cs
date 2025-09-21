using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;

namespace Gameplay
{
    public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform _handle;
        [SerializeField] private RectTransform _background;

        [SerializeField] private float _deadZone;
        [SerializeField] private float _sensity;

        private Vector2 _axes;
        private CanvasGroup _canvasGroup;

        public Vector2 Axes => _axes.normalized;

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            Hide();
        }

        public void Show()
        {
            _canvasGroup.alpha = 1f;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0f;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            var position = RectTransformUtility.WorldToScreenPoint(null, _background.position);
            var radius = _background.sizeDelta / 2f;

            _axes = (eventData.position - position) / (radius * (1 / _sensity));
            HandleInput(_axes.magnitude, _axes.normalized);
            _handle.anchoredPosition = _axes * radius;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _axes = Vector2.zero;
            _handle.anchoredPosition = Vector2.zero;
        }

        private void HandleInput(float magnitude, Vector2 normalised)
        {
            if (magnitude > _deadZone)
            {
                if (magnitude > 1)
                    _axes = normalised;
            }
            else
            {
                _axes = Vector2.zero;
            }
        }
    }
}
