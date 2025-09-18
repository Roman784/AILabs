using UnityEngine;
using UnityEngine.AI;

namespace Gameplay
{
    public class PlayerMovement
    {
        private PlayerInput _input;
        private NavMeshAgent _navMeshAgent;
        private Transform _transform;

        public PlayerMovement(PlayerInput input, NavMeshAgent navMeshAgent, Transform transform)
        {
            _input = input;
            _navMeshAgent = navMeshAgent;
            _transform = transform;
        }

        public void Move()
        {
            var input = _input.GetMovementInput();
            var direction = new Vector3(input.x, 0f, input.y);
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
