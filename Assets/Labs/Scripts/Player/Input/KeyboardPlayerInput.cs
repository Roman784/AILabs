using UnityEngine;

namespace Gameplay
{
    public class KeyboardPlayerInput : PlayerInput
    {
        private const string HORIZONTAL = "Horizontal";
        private const string VERTICAL = "Vertical";

        public override Vector2 GetMovementInput()
        {
            var xInput = Input.GetAxis(HORIZONTAL);
            var yInput = Input.GetAxis(VERTICAL);
            var input = new Vector2(xInput, yInput);

            return ConvertInputToCameraSpace(input);
        }

        public override bool IsDropItem()
        {
            return Input.GetKeyUp(KeyCode.Q);
        }

        public override bool IsInteractWithNpc()
        {
            return Input.GetKeyUp(KeyCode.R);
        }

        public override bool IsPickUpItem()
        {
            return Input.GetKeyUp(KeyCode.E);
        }

        public override bool IsMovementActive()
        {
            return
                Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.D) ||
                Input.GetKey(KeyCode.LeftArrow) ||
                Input.GetKey(KeyCode.RightArrow) ||
                Input.GetKey(KeyCode.UpArrow) ||
                Input.GetKey(KeyCode.DownArrow);
        }
    }
}
