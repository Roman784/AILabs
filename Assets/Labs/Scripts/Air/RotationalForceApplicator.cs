using UnityEngine;

namespace Gameplay
{
    public class RotationalForceApplicator : ForceApplicator
    {
        public float PercentMaxRPM => _rigidbody.angularVelocity.magnitude / _rigidbody.maxAngularVelocity;

        public RotationalForceApplicator(Rigidbody rigidbody, Vector3 forceAxis, float maxRPM, float maxForce) : base(rigidbody, forceAxis, maxForce)
        {
            _rigidbody.maxAngularVelocity = ((maxForce / 60f) * 360f) / 57.296f;
        }

        public override void ApplyForcPercentage(float percentage)
        {
            _rigidbody.AddRelativeTorque(CalculateForce(percentage));
        }
    }
}
