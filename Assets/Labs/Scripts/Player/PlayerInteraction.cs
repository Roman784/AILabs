using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

namespace Gameplay
{
    public class PlayerInteraction
    {
        private ItemInHands _itemInHands;
        private Inventory _inventory;
        private Transform _transform;
        private float _interactionRange;

        public ItemInHands ItemInHands => _itemInHands;

        public PlayerInteraction(ItemInHands itemInHands, Inventory inventory, Transform transform, float interactionRange)
        {
            _itemInHands = itemInHands;
            _inventory = inventory;
            _transform = transform;
            _interactionRange = interactionRange;
        }

        public bool TryPickUpItem()
        {
            var itemLayer = 1 << LayerMask.NameToLayer("Item");
            Collider[] colliders = Physics.OverlapSphere(_transform.position, _interactionRange, itemLayer);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent<Item>(out var item) && item != _itemInHands.CurrentItem)
                {
                    if (_inventory.TryAddItem(item.Config))
                    {
                        Object.Destroy(item.gameObject);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool TryCreateItem(ItemConfig config)
        {
            var droppedItemPosition = _transform.position + _transform.forward * 0.5f;
            var item = Object.Instantiate(config.Prefab);
            item.transform.position = droppedItemPosition;
            return true;
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