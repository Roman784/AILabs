using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay
{
    public class SteeringWheel : MonoBehaviour, IDragHandler
    {
        [SerializeField] private Transform _view;
        [SerializeField] private float _sensity;
        [SerializeField] private float _clampedRotation;

        private float _currentRotationZ;

        public float Direction => -_currentRotationZ / _clampedRotation;

        public void OnDrag(PointerEventData eventData)
        {
            var delta = eventData.delta.x;
            _currentRotationZ -= delta * _sensity;
            _currentRotationZ = Mathf.Clamp(_currentRotationZ, -_clampedRotation, _clampedRotation);

            _view.rotation = Quaternion.Euler(0f, 0f, _currentRotationZ);
        }
    }
}
