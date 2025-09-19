using UnityEngine;
using UnityEngine.AI;

namespace Gameplay
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Stats _stats;

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
            var itemInHands = GetComponent<ItemInHands>();
            var navMeshAgent = GetComponent<NavMeshAgent>();
            _input = new KeyboardPlayerInput();

            _movement = new PlayerMovement(_input, navMeshAgent, transform);
            _interaction = new PlayerInteraction(itemInHands, _inventory, transform, _interactionRange);
            _animation = new PlayerAnimation(_animator);

            _stateHandler = new PlayerStateHandler(this);

            itemInHands.OnItemGivenSignal.AddListener((config) =>
            {
                if (_inventory.TryRemoveItem(config.Id))
                    _animation.SetPickUpItemAnimation();
            });

            _inventory.OnDropItemSignal.AddListener((config) =>
            {
                if (config.Id == itemInHands.CurrentItem.Config.Id)
                    itemInHands.TryRemoveItem();
                if (_interaction.TryCreateItem(config))
                    _animation.SetPickUpItemAnimation();
            });

            _inventory.OnSetToolItemSignal.AddListener((config) =>
            {
                if (config != null)
                {
                    itemInHands.TryRaiseItem(config);

                    if (config is WeaponItemConfig)
                        _stats.SetDamage(((WeaponItemConfig)config).Damage);
                    else
                        _stats.SetDamage(0);
                }
                else
                {
                    itemInHands.TryRemoveItem();
                    _stats.SetDamage(0);
                }
            });

            _inventory.OnSetArmorItemSignal.AddListener((config) => 
            {
                if (config != null)
                    _stats.SetArmor(config.Armor);
                else
                    _stats.SetArmor(0);
            });
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
