using Unity.VisualScripting;
using UnityEngine;

namespace VitaSoftware.Shop
{
    [CreateAssetMenu(fileName = "New Gravestone Config", menuName = "VitaSoftware/GravestoneConfig", order = 0)]
    public class GravestoneConfig : PurchasableItem
    {
        [SerializeField] private GameObject gravestonePrefab;
        
        public GameObject GravestonePrefab => gravestonePrefab;
    }
}