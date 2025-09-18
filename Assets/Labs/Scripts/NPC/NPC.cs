using UnityEngine;
using UnityEngine.AI;

namespace Gameplay
{
    public class NPC : MonoBehaviour
    {
        private NpcStateHandler _stateHandler;
        private NPCMovement _movement;
        private bool _isPlayerInRange;

        public NPCMovement Movement => _movement;
        public bool IsPlayerInRange => _isPlayerInRange;

        private void Awake()
        {
            _movement = new NPCMovement(GetComponent<NavMeshAgent>());
            _stateHandler = new NpcStateHandler(this);
        }

        public virtual void OnPlayerEnter(PlayerController player)
        {
            _isPlayerInRange = true;
        }

        public virtual void OnPlayerExit()
        {
            _isPlayerInRange = false;
        }

        private void Update()
        {
            _stateHandler.Update();
        }
    }
}
