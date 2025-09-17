using UnityEngine;

namespace Gameplay
{
    public class ItemPickingUp : MonoBehaviour
    {
        [SerializeField] private Transform _inHandsPoint;

        private Item _currentItem;

        public Item CurrentItem => _currentItem;
        private Vector3 InHandsPosition => _inHandsPoint.position;

        public bool TryPickUp(Item item)
        {
            if (item == _currentItem) return false;

            Drop(item.transform.position);

            item.transform.position = InHandsPosition;
            item.transform.SetParent(_inHandsPoint, true);
            _currentItem = item;

            return true;
        }

        public void Drop(Vector3 position)
        {
            if (_currentItem == null) return;

            _currentItem.transform.position = position;
            _currentItem.transform.SetParent(null, true);
            _currentItem = null;
        }
    }
}