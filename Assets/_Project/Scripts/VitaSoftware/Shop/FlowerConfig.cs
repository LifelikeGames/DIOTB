using UnityEngine;

namespace VitaSoftware.Shop
{
    [CreateAssetMenu(fileName = "New Flower Config", menuName = "VitaSoftware/FlowerConfig", order = 0)]
    public class FlowerConfig : ScriptableObject
    {
        [SerializeField] private Color colour;
    }
}