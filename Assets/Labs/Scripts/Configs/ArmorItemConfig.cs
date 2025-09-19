using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "ArmorItemConfig", menuName = "Configs/New Armor Item Config", order = 2)]
    public class ArmorItemConfig : ItemConfig
    {
        public int Armor;
    }
}
