using UnityEngine;

namespace Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;

        [Space]

        [SerializeField] private float _interactionRange;

        private Rigidbody _rigidbody;
        private ItemPickingUp _itemPickingUp;
        private Camera _camera;

        public Item HoldingItem => _itemPickingUp.CurrentItem;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _itemPickingUp = GetComponent<ItemPickingUp>();
            _camera = Camera.main;
        }

        private void Update()
        {
            Move(Time.deltaTime);

            if (Input.GetKeyUp(KeyCode.E))
            {
                PickUpItem();
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                DropItem();
            }
            else if (Input.GetKeyUp(KeyCode.R))
            {
                InteractWithNpc();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<QuestNPC>(out var npc))
                npc.OnPlayerEnter();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<QuestNPC>(out var npc))
                npc.OnPlayerExit();
        }

        private void Move(float deltaTime)
        {
            if (!IsInputActive()) return;

            var input = ConvertInputToCameraSpace(GetInput());
            var direction = new Vector3(input.x, 0f, input.y).normalized;
            var velocity = direction * _movementSpeed;
            var rotation = Quaternion.LookRotation(direction);

            _rigidbody.AddForce(velocity, ForceMode.Force);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * deltaTime);

            LimitVelocity();
        }

        private void LimitVelocity()
        {
            var flatVelocity = new Vector3(_rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z);

            if (flatVelocity.magnitude > _movementSpeed)
            {
                var limitedVel = flatVelocity.normalized * _movementSpeed;
                _rigidbody.linearVelocity = new Vector3(limitedVel.x, _rigidbody.linearVelocity.y, limitedVel.z);
            }
        }

        private void PickUpItem()
        {
            var itemLayer = 1 << LayerMask.NameToLayer("Item");
            Collider[] colliders = Physics.OverlapSphere(transform.position, _interactionRange, itemLayer);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent<Item>(out var item))
                {
                    if (_itemPickingUp.TryPickUp(item))
                        return;
                }
            }
        }

        private void DropItem()
        {
            var droppedItemPosition = transform.position + transform.forward * 0.5f;
            _itemPickingUp.Drop(droppedItemPosition);
        }

        private void InteractWithNpc()
        {
            var npcLayer = 1 << LayerMask.NameToLayer("NPC");
            Collider[] colliders = Physics.OverlapSphere(transform.position, _interactionRange, npcLayer);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent<QuestNPC>(out var npc))
                {
                    npc.Interact(this);
                    return;
                }
            }
        }

        private Vector2 GetInput()
        {
            return new Vector2(
                Input.GetAxis("Horizontal"), 
                Input.GetAxis("Vertical"));
        }

        private bool IsInputActive()
        {
            return
                Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.D) ||
                Input.GetKey(KeyCode.LeftArrow) ||
                Input.GetKey(KeyCode.RightArrow) ||
                Input.GetKey(KeyCode.UpArrow) ||
                Input.GetKey(KeyCode.DownArrow);
        }

        protected Vector2 ConvertInputToCameraSpace(Vector2 input)
        {
            var cameraForward = _camera.transform.forward;
            var cameraRight = _camera.transform.right;

            var forwardDirection = new Vector2(cameraForward.x, cameraForward.z).normalized;
            var rightDirection = new Vector2(cameraRight.x, cameraRight.z).normalized;

            return forwardDirection * input.y + rightDirection * input.x;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _interactionRange);
        }
    }
}