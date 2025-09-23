using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    public class CameraTracker : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _trackSpeed;
        [SerializeField] private Vector3 _defaultDirection;
        [SerializeField] private float _defaultDistance;
        [SerializeField] private bool _fixedUpdate;

        private Transform _target;
        private Quaternion _rotation;
        private Vector3 _direction;
        private float _distance;

        private Quaternion _defaultRotation;

        private bool _isTtrackTargetRotation;

        private void Start()
        {
            _defaultRotation = transform.rotation;
            SetDefaultMode();
        }

        public void SetDefaultMode()
        {
            _target = _player.transform;
            _rotation = _defaultRotation;
            _direction = _defaultDirection;
            _distance = _defaultDistance;
            _isTtrackTargetRotation = false;
        }

        public void SetToNpcMode(QuestNPC npc)
        {
            _target = npc.CameraPoint;
            _distance = 0f;
            _isTtrackTargetRotation = true;
        }

        private void Update()
        {
            if (!_fixedUpdate)
                Track(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (_fixedUpdate)
                Track(Time.fixedDeltaTime);
        }

        private void Track(float deltaTime)
        {
            var targetRotation = _isTtrackTargetRotation ? _target.rotation : _rotation;
            var position = Vector3.Slerp(transform.position, GetTrackPosition(), _trackSpeed * deltaTime);
            var rotation = Quaternion.Slerp(transform.rotation, targetRotation, _trackSpeed * deltaTime);

            transform.position = position;
            transform.rotation = rotation;
        }

        private Vector3 GetTrackPosition()
        {
            return _target.position + _direction * _distance;
        }
    }
}
