using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class Stats : MonoBehaviour
    {
        [SerializeField] private TMP_Text _damageView;
        [SerializeField] private TMP_Text _armorView;

        private int _damage;
        private int _armor;

        private void Start()
        {
            UpdateView();
        }

        public void SetDamage(int damage)
        {
            _damage = damage;
            UpdateView();
        }

        public void SetArmor(int armor)
        {
            _armor = armor;
            UpdateView();
        }

        private void UpdateView()
        {
            _damageView.text = $"Dmg: {_damage}";
            _armorView.text = $"Arm: {_armor}";
        }
    }
}
