using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "ItemConfig", menuName = "Configs/New Item Config")]
    public class ItemConfig : ScriptableObject
    {
        public int Id;
        public Sprite Icon;
        public Item Prefab;
    }
}
