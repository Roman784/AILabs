using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class ItemInHands : MonoBehaviour
    {
        [SerializeField] private Transform _inHandsPoint;

        private Item _currentItem;

        public Item CurrentItem => _currentItem;
        private Vector3 InHandsPosition => _inHandsPoint.position;

        public UnityEvent<ItemConfig> OnItemGivenSignal { get; private set; } = new();

        public bool TryRaiseItem(ItemConfig config)
        {
            if (_currentItem != null)
                TryRemoveItem();

            var item = Instantiate(config.Prefab);
            item.transform.position = InHandsPosition;
            item.transform.SetParent(_inHandsPoint, true);

            _currentItem = item;

            return true;
        }

        public bool TryRemoveItem()
        {
            if (_currentItem == null) return false;

            Destroy(_currentItem.gameObject);
            return true;
        }

        public bool TryGiveItem()
        {
            if(_currentItem == null) return false;

            OnItemGivenSignal.Invoke(_currentItem.Config);
            TryRemoveItem();
            return true;
        }
    }
}