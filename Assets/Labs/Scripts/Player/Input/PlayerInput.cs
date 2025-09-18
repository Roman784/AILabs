using UnityEngine;

namespace Gameplay
{
    public abstract class PlayerInput
    {
        private Camera _camera;

        public PlayerInput()
        {
            _camera = Camera.main;
        }

        public abstract Vector2 GetMovementInput();
        public abstract bool IsPickUpItem();
        public abstract bool IsDropItem();
        public abstract bool IsInteractWithNpc();
        public abstract bool IsMovementActive();

        protected Vector2 ConvertInputToCameraSpace(Vector2 input)
        {
            var cameraForward = _camera.transform.forward;
            var cameraRight = _camera.transform.right;

            var forwardDirection = new Vector2(cameraForward.x, cameraForward.z).normalized;
            var rightDirection = new Vector2(cameraRight.x, cameraRight.z).normalized;

            return forwardDirection * input.y + rightDirection * input.x;
        }
    }
}
