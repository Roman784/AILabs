using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gameplay
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private ItemDragAndDrop _itemDragAndDropPrefab;
        [SerializeField] private Image _iconView;
        [SerializeField] private Transform _centerPoint;

        private ItemConfig _config;

        public ItemConfig Config => _config;
        public bool HasItem => _config != null;
        public Vector2 CenterPosition => _centerPoint.position;
        public bool IsArmor => _config != null && _config is ArmorItemConfig;
        public UnityEvent<Vector2> OnPointerUpSignal { get; private set; } = new();

        private void Start()
        {
            _itemDragAndDropPrefab.OnPointerUpSignal.AddListener((upPosition) => OnPointerUpSignal.Invoke(upPosition));
        }

        public void SetItem(ItemConfig config)
        {
            _config = config;

            _iconView.gameObject.SetActive(true);
            _iconView.sprite = config.Icon;
        }

        public void RemoveItem()
        {
            _iconView.gameObject.SetActive(false);
            _config = null;
        }

        public void ResetDragging()
        {
            _itemDragAndDropPrefab.ResetDragging(CenterPosition);
        }
    }
}
