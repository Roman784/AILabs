using UnityEngine;

namespace Gameplay
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _reductionSpeed;
        [SerializeField] private float _maxVelocity;

        [Space]

        [SerializeField] private Transform _wheelBackLeft;
        [SerializeField] private Transform _wheelBackRight;
        [SerializeField] private Transform _wheelFrontLeft;
        [SerializeField] private Transform _wheelFrontRight;

        [Space]

        [SerializeField] private TrailRenderer _tireTrack1;
        [SerializeField] private TrailRenderer _tireTrack2;

        [Space]

        [SerializeField] private SteeringWheel _steeringWheel;

        private Vector3 _movementVector;
        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            var horizontalDirection = _steeringWheel.Direction;
            var verticalDirection = GetVerticalDirection();

            Move(verticalDirection);
            RotateCar(horizontalDirection);
            ReductMovementVector();

            RotateWheel(_wheelBackLeft, horizontalDirection / -2f);
            RotateWheel(_wheelBackRight, horizontalDirection / -2f);
            RotateWheel(_wheelFrontLeft, horizontalDirection);
            RotateWheel(_wheelFrontRight, horizontalDirection);

            EmitTiraTrack(horizontalDirection, verticalDirection);
        }

        private void Move(float verticalDirection)
        {
            var velocity = _rigidbody.linearVelocity + _movementVector * _movementSpeed * verticalDirection * Time.deltaTime;

            velocity.x = Mathf.Clamp(velocity.x, -_maxVelocity, _maxVelocity);
            velocity.z = Mathf.Clamp(velocity.z, -_maxVelocity, _maxVelocity);

            Debug.Log(velocity);
            _rigidbody.linearVelocity = velocity;
        }

        private void RotateCar(float horizontalDirection)
        {
            if (Mathf.Abs(horizontalDirection) < 0.1f) return;

            var rotationY = transform.eulerAngles.y;
            rotationY += horizontalDirection * _rotationSpeed * Time.deltaTime;

            transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
        }

        private void ReductMovementVector()
        {
            _movementVector = Vector3.Lerp(_movementVector, transform.forward, _reductionSpeed * Time.deltaTime);
        }

        private void RotateWheel(Transform wheel, float t)
        {
            var rotationX = wheel.eulerAngles.x + _movementSpeed * Time.deltaTime;
            var rotationY = Mathf.Lerp(0f, 60f, Mathf.Abs(t)) * Mathf.Sign(t);
            wheel.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        }

        private void EmitTiraTrack(float horizontalDirection, float verticalDirection)
        {
            var isEmitTireTrack = Mathf.Abs(horizontalDirection * verticalDirection) > 0.5f;
            _tireTrack1.emitting = isEmitTireTrack;
            _tireTrack2.emitting = isEmitTireTrack;
        }

        private float GetVerticalDirection()
        {
            return Input.GetAxis("Vertical");
        }
    }
}
