using UnityEngine;
using UnityEngine.AI;

namespace Gameplay
{
    public class NPC : MonoBehaviour
    {
        [SerializeField] private Transform _wanderCenterPoint;
        [SerializeField] private float _wanderRange;

        [Space]

        [SerializeField] private Animator _animator;

        private NpcStateHandler _stateHandler;
        private NPCMovement _movement;
        private NPCAnimation _animation;
        private bool _isPlayerInRange;

        public NPCMovement Movement => _movement;
        public NPCAnimation Animation => _animation;
        public bool IsPlayerInRange => _isPlayerInRange;
        public Vector3 WanderCenterPosition => _wanderCenterPoint.position;
        public float WanderRange => _wanderRange;

        protected void Awake()
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

        protected void Update()
        {
            _stateHandler.Update();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(WanderCenterPosition, WanderRange);
        }
    }
}
