using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VitaSoftware.Shop
{
    public class ItemUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private Image image;

        private GravestoneConfig config;

        public void SetFields(GravestoneConfig purchasableItem)
        {
            label.text = purchasableItem.name + " - $" + purchasableItem.Price;
            image.sprite = purchasableItem.Sprite;
            config = purchasableItem;
        }
    }
}