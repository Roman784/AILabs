using UnityEngine;

namespace Gameplay
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemType _type;
        [SerializeField] private ItemConfig _config;

        public ItemType Type => _type;
        public ItemConfig Config => _config;
    }
}