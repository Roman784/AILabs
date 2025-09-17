using UnityEngine;

namespace Gameplay
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemType _type;

        public ItemType Type => _type;
    }
}