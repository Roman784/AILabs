using UnityEngine;

namespace Gameplay
{
    public class LinearForceApplicator : ForceApplicator
    {
        public LinearForceApplicator(Rigidbody rigidbody, Vector3 forceAxis, float maxForce) : base(rigidbody, forceAxis, maxForce)
        {
        }

        public override void ApplyForcPercentage(float percentage)
        {
            _rigidbody.AddRelativeForce(CalculateForce(percentage));
        }
    }
}
