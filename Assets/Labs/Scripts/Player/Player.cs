using UnityEngine;
using UnityEngine.AI;

namespace Gameplay
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        [Space]

        [SerializeField] private float _interactionRange;

        [Space]

        [SerializeField] private CameraTracker _cameraTracker;

        private PlayerStateHandler _stateHandler;
        private PlayerMovement _movement;
        private PlayerInteraction _interaction;
        private PlayerAnimation _animation;

        private PlayerInput _input;

        public PlayerAnimation Animation => _animation;
        public PlayerMovement Movement => _movement;
        public PlayerInteraction Interaction => _interaction;
        public PlayerInput Input => _input;

        private void Awake()
        {
            var itemPickingUp = GetComponent<ItemPickingUp>();
            var navMeshAgent = GetComponent<NavMeshAgent>();
            _input = new KeyboardPlayerInput();

            _movement = new PlayerMovement(_input, navMeshAgent, transform);
            _interaction = new PlayerInteraction(itemPickingUp, transform, _interactionRange);
            _animation = new PlayerAnimation(_animator);

            _stateHandler = new PlayerStateHandler(this);
        }

        private void Update()
        {
            _stateHandler.Update();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<QuestNPC>(out var npc))
            {
                npc.OnPlayerEnter(this);
                _cameraTracker.SetToNpcMode(npc);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<QuestNPC>(out var npc))
            {
                npc.OnPlayerExit();
                _cameraTracker.SetDefaultMode();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _interactionRange);
        }
    }
}
