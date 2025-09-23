using UnityEngine;

namespace Gameplay
{
    public class Helicopter : MonoBehaviour
    {
        [SerializeField] private Rigidbody _mainRotor;
        [SerializeField] private Rigidbody _body;
        [SerializeField] private Transform _tailRotor;

        private LinearForceApplicator _lift;
        private RotationalForceApplicator _mainRotorTorque;
        private RotationalForceApplicator _bodyTorque;
        private RotationalForceApplicator _bodyCounterTorque;
        private RotationalForceApplicator _pitchTorque;
        private RotationalForceApplicator _rollTorque;

        private HelicopterControls _controls;
        private Rotater _tailRotorRotater;

        private void Start()
        {
            _lift = new LinearForceApplicator(_body, Vector3.up, 100062);
            _mainRotorTorque = new RotationalForceApplicator(_mainRotor, Vector3.down, 3, 240);
            _bodyTorque = new RotationalForceApplicator(_body, Vector3.down, 120, 30000);
            _bodyCounterTorque = new RotationalForceApplicator(_body, Vector3.up, 120, 55000);
            _pitchTorque = new RotationalForceApplicator(_body, Vector3.right, 100, 25000);
            _rollTorque = new RotationalForceApplicator(_body, Vector3.back, 100, 20000);

            _controls = new PilotHelicopterControls();
            _tailRotorRotater = new Rotater(_tailRotor, Vector3.right, 500);

            _body.centerOfMass = Vector3.zero;
        }

        private void Update()
        {
            _controls.HandleInput();
        }

        private void FixedUpdate()
        {
            _mainRotorTorque.ApplyForcPercentage(_controls.Throttle);
            _bodyTorque.ApplyForcPercentage(_mainRotorTorque.PercentMaxRPM);
            _tailRotorRotater.Rotate(_mainRotorTorque.PercentMaxRPM);
            _bodyCounterTorque.ApplyForcPercentage(_mainRotorTorque.PercentMaxRPM * _controls.Yaw);
            _lift.ApplyForcPercentage(_mainRotorTorque.PercentMaxRPM * _controls.Collective);
            _pitchTorque.ApplyForcPercentage(_mainRotorTorque.PercentMaxRPM * _controls.Pitch);
            _rollTorque.ApplyForcPercentage(_mainRotorTorque.PercentMaxRPM * _controls.Roll);
        }
    }
}
