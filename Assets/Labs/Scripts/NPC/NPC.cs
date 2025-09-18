using UnityEngine;
using UnityEngine.AI;

namespace Gameplay
{
    public class NPC : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private NpcStateHandler _stateHandler;
        private NPCMovement _movement;
        private NPCAnimation _animation;
        private bool _isPlayerInRange;

        public NPCMovement Movement => _movement;
        public NPCAnimation Animation => _animation;
        public bool IsPlayerInRange => _isPlayerInRange;

        private void Awake()
        {
            _movement = new NPCMovement(GetComponent<NavMeshAgent>());
            _animation = new NPCAnimation(_animator);
            _stateHandler = new NpcStateHandler(this);
        }

        public virtual void OnPlayerEnter(Player player)
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
