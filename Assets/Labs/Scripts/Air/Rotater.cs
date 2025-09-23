using UnityEngine;

namespace Gameplay
{
    public class Rotater
    {
        private Transform _transform;
        private Vector3 _rotationAxis;
        private float _maxDegressPerSecond;

        public Rotater(Transform transform, Vector3 rotationAxis, float maxRPM)
        {
            _transform = transform;
            _rotationAxis = rotationAxis;
            _maxDegressPerSecond = (maxRPM / 60f) * 360f;
        }

        public void Rotate(float percentMaxSpeed)
        {
            _transform.Rotate(_rotationAxis * (Time.deltaTime * (_maxDegressPerSecond * percentMaxSpeed)));
        }
    }
}
