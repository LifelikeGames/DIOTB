using UnityEngine;

namespace VitaSoftware.Shop
{
    [CreateAssetMenu(fileName = "New Gravestone Config", menuName = "VitaSoftware/GravestoneConfig", order = 0)]
    public class GravestoneConfig : ScriptableObject
    {
        [SerializeField] private float price;
        [SerializeField] private GameObject gravestonePrefab;
        [SerializeField] private Sprite sprite;

        public float Price => price;
        public GameObject GravestonePrefab => gravestonePrefab;
        public Sprite Sprite => sprite;
    }
}