using UnityEngine;

namespace VitaSoftware
{
    [CreateAssetMenu(fileName = "New Gravestone Config", menuName = "VitaSoftware/GravestoneConfig", order = 0)]
    public class GravestoneConfig : ScriptableObject
    {
        [SerializeField] private float price;
        [SerializeField] private GameObject gravestonePrefab;

        public float Price => price;
        public GameObject GravestonePrefab => gravestonePrefab;
    }
}