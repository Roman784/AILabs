using UnityEngine;
using UnityEngine.AI;

namespace Gameplay
{
    public class NPCMovement
    {
        private NavMeshAgent _navMeshAgent;

        public bool IsPositionReached => _navMeshAgent.remainingDistance < 0.1f;

        public NPCMovement(NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
        }

        public void MoveTo(Vector3 position)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(position);
        }

        public void StopMovement()
        {
            _navMeshAgent.isStopped = true;
        }
    }
}
