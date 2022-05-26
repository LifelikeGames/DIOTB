using UnityEngine;

namespace VitaSoftware.Shop
{
    [CreateAssetMenu(fileName = "New Coffin Config", menuName = "VitaSoftware/CoffinConfig", order = 0)]
    public class CoffinConfig : PurchasableItem
    {
        [SerializeField] private GameObject prefab;

        public GameObject Prefab => prefab;
    }
}