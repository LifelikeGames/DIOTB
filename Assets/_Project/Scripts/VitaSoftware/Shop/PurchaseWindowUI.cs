using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VitaSoftware.Shop
{
    public class PurchaseWindowUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI ordersListText;
        [SerializeField] private ItemUI itemUIPrefab;
        [SerializeField] private GravestoneConfig[] purchasableItems;
        [SerializeField] private Transform purchaseGridParent;
        [SerializeField] private ShopManager shopManager;
        [SerializeField] private GameObject purchaseWindowDisplay;

        private bool orderAdded;

        private void Awake()
        {
            purchaseWindowDisplay.SetActive(false);
            foreach (var purchasableItem in purchasableItems)
            {
                var itemUI = Instantiate(itemUIPrefab, purchaseGridParent);
                itemUI.SetFields(purchasableItem);
                var button = itemUI.GetComponent<Button>();
                button.onClick.AddListener(()=>AddToOrder(purchasableItem));
            }
        }

        public void EnableAndSetOrders()
        {
            purchaseWindowDisplay.SetActive(true);
            var configs = shopManager.GravestoneWishes;
            ordersListText.text = "";
            //TODO: process orders one at a time??
            foreach (var config in configs)
            {
                ordersListText.text += config.name + Environment.NewLine;//TODO: group
            }
        }

        public void AddToOrder(GravestoneConfig config)
        {
            shopManager.AddOrder(config, 1);//TODO: add quantity slider to UI
            orderAdded = true;
        }

        public void PlaceOrder()
        {
            if (!orderAdded) return;
            
            shopManager.PurchaseOrders();
            purchaseWindowDisplay.SetActive(false);
        }
    }
}