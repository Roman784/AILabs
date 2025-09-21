using UnityEngine;

namespace Gameplay
{
    public class JoystickPlayerInput : KeyboardPlayerInput
    {
        private Joystick _joystick;

        public JoystickPlayerInput(Joystick joystick)
        {
            _joystick = joystick;
        }

        public override Vector2 GetMovementInput()
        {
            var input = _joystick.Axes;
            return ConvertInputToCameraSpace(input);
        }

        public override bool IsMovementActive()
        {
            return Mathf.Abs(_joystick.Axes.x) > 0 ||
                   Mathf.Abs(_joystick.Axes.y) > 0;
        }
    }
}
