using UnityEngine;
using UnityEngine.AI;

namespace Gameplay
{
    public class PlayerMovement
    {
        private NavMeshAgent _navMeshAgent;
        private Transform _transform;

        public PlayerMovement(NavMeshAgent navMeshAgent, Transform transform)
        {
            _navMeshAgent = navMeshAgent;
            _transform = transform;
        }

        public void Move(PlayerInput input)
        {
            var movementInput = input.GetMovementInput();
            var direction = new Vector3(movementInput.x, 0f, movementInput.y);
            var position = _transform.position + direction;

            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(position);
        }

        public void StopMovement()
        {
            _navMeshAgent.isStopped = true;
        }
    }
}
