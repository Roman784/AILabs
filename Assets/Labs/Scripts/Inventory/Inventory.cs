using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Gameplay
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        [Space]

        [SerializeField] private InventorySlot[] _ordinarySlots;
        [SerializeField] private InventorySlot _toolSlot;
        [SerializeField] private InventorySlot _armorSlot;

        private const float MIN_DISTANCE_TO_PLACE = 128f;

        private List<InventorySlot> _allSlots;

        public UnityEvent<ItemConfig> OnDropItemSignal { get; private set; } = new();
        public UnityEvent<ItemConfig> OnSetToolItemSignal { get; private set; } = new();
        public UnityEvent<ArmorItemConfig> OnSetArmorItemSignal { get; private set; } = new();

        private void Start()
        {
            _allSlots = new List<InventorySlot>();
            _allSlots.AddRange(_ordinarySlots);
            _allSlots.Add(_toolSlot);
            _allSlots.Add(_armorSlot);

            foreach (var slot in _allSlots)
            {
                slot.OnPointerUpSignal.AddListener((upPosition) =>
                {
                    HandleSlotDragging(slot, upPosition);
                });
            }
        }

        public bool TryAddItem(ItemConfig config)
        {
            foreach (var slot in _ordinarySlots)
            {
                if (slot.HasItem) continue;

                slot.SetItem(config);
                return true;
            }
            return false;
        }

        public bool TryRemoveItem(int id)
        {
            foreach (var slot in _allSlots)
            {
                if (slot.Config == null) continue;
                if (slot.Config.Id == id)
                {
                    slot.RemoveItem();
                    return true;
                }
            }
            return false;
        }

        private void HandleSlotDragging(InventorySlot slot, Vector2 pointerUpPosition)
        {
            var nearestSlotItem = GetNearestSlot(pointerUpPosition);
            var nearestSlot = nearestSlotItem.Item1;
            var distance = nearestSlotItem.Item2;

            if (nearestSlot != null && nearestSlot != slot && distance <= MIN_DISTANCE_TO_PLACE)
            {
                if (CheckForArmorSlots(slot, nearestSlot))
                {
                    slot.ResetDragging();
                    return;
                }

                if (TryReplaceItem(slot, nearestSlot))
                {
                    if (slot == _toolSlot || nearestSlot == _toolSlot)
                        OnSetToolItemSignal.Invoke(_toolSlot.Config);
                    if (slot == _armorSlot || nearestSlot == _armorSlot)
                        OnSetArmorItemSignal.Invoke((ArmorItemConfig)_armorSlot.Config);
                }
            }
            else
            {
                OnDropItemSignal.Invoke(slot.Config);
                slot.RemoveItem();
            }


            slot.ResetDragging();
        }

        private (InventorySlot, float) GetNearestSlot(Vector2 position)
        {
            InventorySlot nearestSlot = null;
            var nearestDistance = float.MaxValue;

            foreach (var slot in _allSlots)
            {
                var distance = Vector2.Distance(position, slot.CenterPosition);
                if (distance < nearestDistance)
                {
                    nearestSlot = slot;
                    nearestDistance = distance;
                }
            }

            return (nearestSlot, nearestDistance);
        }

        private bool TryReplaceItem(InventorySlot slot1, InventorySlot slot2)
        {
            var config1 = slot1.Config;
            var config2 = slot2.Config;

            if (config2 != null)
                slot1.SetItem(config2);
            else
                slot1.RemoveItem();

            if (config1 != null)
                slot2.SetItem(config1);
            else
                slot2.RemoveItem();

            return true;
        }

        private bool CheckForArmorSlots(InventorySlot slot1, InventorySlot slot2)
        {
            return !slot1.IsArmor && slot2 == _armorSlot ||
                   slot1 == _armorSlot && slot2.HasItem && !slot2.IsArmor;
        }
    }
}
