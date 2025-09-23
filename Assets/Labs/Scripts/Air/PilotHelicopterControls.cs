using UnityEngine;

namespace Gameplay
{
    public class PilotHelicopterControls : HelicopterControls
    {
        public override void HandleInput()
        {
            Pitch = Input.GetAxis("Vertical");
            Roll = Input.GetAxis("Horizontal");
            Yaw = Mathf.InverseLerp(-1f, 1f, GetAxis(KeyCode.J, KeyCode.L));
            Collective = Mathf.InverseLerp(-1f, 1f, GetAxis(KeyCode.K, KeyCode.I));
            Throttle = 1f;
        }

        private float GetAxis(KeyCode keyMin, KeyCode keyMax)
        {
            return Input.GetKey(keyMin) ? -1f : Input.GetKey(keyMax) ? 1f : 0f;
        }
    }
}
