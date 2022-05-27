using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VitaSoftware.Shop
{
    public class ItemUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private Image image;

        private PurchasableItem config;

        public void SetFields(PurchasableItem purchasableItem)
        {
            label.text = purchasableItem.Label + " - $" + purchasableItem.Price;
            image.sprite = purchasableItem.Sprite;
            config = purchasableItem;
        }
    }
}