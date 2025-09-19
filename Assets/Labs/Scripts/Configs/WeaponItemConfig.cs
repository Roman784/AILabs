using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "WeaponItemConfig", menuName = "Configs/New Weapon Item Config", order = 1)]
    public class WeaponItemConfig : ItemConfig
    {
        public int Damage;
    }
}
