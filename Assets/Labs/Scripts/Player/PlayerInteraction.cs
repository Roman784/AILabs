using UnityEngine;

namespace Gameplay
{
    public class PlayerInteraction
    {
        private ItemPickingUp _itemPickingUp;
        private Transform _transform;
        private float _interactionRange;

        public Item HoldingItem => _itemPickingUp.CurrentItem;

        public PlayerInteraction(ItemPickingUp itemPickingUp, Transform transform, float interactionRange)
        {
            _itemPickingUp = itemPickingUp;
            _transform = transform;
            _interactionRange = interactionRange;
        }

        public bool TryPickUpItem()
        {
            var itemLayer = 1 << LayerMask.NameToLayer("Item");
            Collider[] colliders = Physics.OverlapSphere(_transform.position, _interactionRange, itemLayer);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent<Item>(out var item))
                {
                    if (_itemPickingUp.TryPickUp(item))
                        return true;
                }
            }
            return false;
        }

        public bool TryDropItem()
        {
            var droppedItemPosition = _transform.position + _transform.forward * 0.5f;
            return _itemPickingUp.TryDrop(droppedItemPosition);
        }

        public bool TryInteractWithNpc()
        {
            var npcLayer = 1 << LayerMask.NameToLayer("NPC");
            Collider[] colliders = Physics.OverlapSphere(_transform.position, _interactionRange, npcLayer);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent<QuestNPC>(out var npc))
                {
                    npc.Interact(this);
                    return true;
                }
            }
            return false;
        }        
    }
}