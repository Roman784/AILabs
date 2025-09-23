using UnityEngine;

namespace Gameplay
{
    public class ForceApplicator
    {
        protected Rigidbody _rigidbody;
        protected Vector3 _forceAxis;

        public float MaxForce { get; protected set; }

        public ForceApplicator(Rigidbody rigidbody, Vector3 forceAxis, float maxForce)
        {
            _rigidbody = rigidbody;
            _forceAxis = forceAxis;
            MaxForce = maxForce;
        }

        public virtual void ApplyForcPercentage(float percentage) { }

        protected Vector3 CalculateForce(float percentMaxForce)
        {
            return _forceAxis * (MaxForce * percentMaxForce);
        }
    }
}
