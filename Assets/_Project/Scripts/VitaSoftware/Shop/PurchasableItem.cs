using UnityEngine;

namespace VitaSoftware.Shop
{
    public abstract class PurchasableItem : ScriptableObject
    {
        [SerializeField] protected float price;
        [SerializeField] private Sprite sprite;
        
        public float Price => price;
        public Sprite Sprite => sprite;
    }
}