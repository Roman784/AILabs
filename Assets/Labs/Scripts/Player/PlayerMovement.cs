using UnityEngine;

namespace Gameplay
{
    public class PlayerMovement
    {
        private PlayerInput _input;
        private Transform _transform;
        private float _movementSpeed;
        private float _rotationSpeed;

        public PlayerMovement(PlayerInput input, Transform transform, 
                              float movementSpeed, float rotationSpeed)
        {
            _input = input;
            _transform = transform;
            _movementSpeed = movementSpeed;
            _rotationSpeed = rotationSpeed;
        }

        public void Move(float deltaTime)
        {
            var input = _input.GetMovementInput();
            var direction = new Vector3(input.x, 0f, input.y).normalized;
            var velocity = direction * _movementSpeed;
            var rotation = Quaternion.LookRotation(direction);

            _transform.position += velocity * deltaTime;
            _transform.rotation = Quaternion.Lerp(_transform.rotation, rotation, _rotationSpeed * deltaTime);
        }
    }
}
