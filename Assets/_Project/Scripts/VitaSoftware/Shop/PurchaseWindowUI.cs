using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VitaSoftware.Logistics;

namespace VitaSoftware.Shop
{
    //TODO: require gravestone purchase for each order??
    //TODO: clear order list on the left after orders are delivered
    public class PurchaseWindowUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI ordersListText;
        [SerializeField] private ItemUI itemUIPrefab;
        [SerializeField] private GravestoneConfig[] purchasableItems;
        [SerializeField] private Transform purchaseGridParent;
        [SerializeField] private ShopManager shopManager;
        [SerializeField] private GameObject purchaseWindowDisplay;
        [SerializeField] private TextMeshProUGUI placedOrdersListText;

        private bool orderAdded;

        private int currentOrderId = -1;
        private Order currentRequestedOrder;
        private List<GravestoneConfig> addedConfigs;

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
            addedConfigs = new List<GravestoneConfig>();
        }

        public void EnableAndGetOrders()
        {
            purchaseWindowDisplay.SetActive(true);
            placedOrdersListText.text = "Orders added:\r\n";
            UpdateAddedOrders();
            GetNextOrder();
        }

        private void GetNextOrder()
        {
            currentRequestedOrder = shopManager.OrderWishes.FirstOrDefault();
            ordersListText.text = "";
            if (currentRequestedOrder == null)
            {
                Debug.LogError("No orders found");
                return;
            }

            currentOrderId = shopManager.OrderWishes.IndexOf(currentRequestedOrder);

            ordersListText.text += $"Order {currentRequestedOrder.id}\r\n\r\nRequested gravestone: {currentRequestedOrder.gravestone.name}."; //TODO: add formatting to SO
        }

        public void AddToOrder(GravestoneConfig config)
        {
            if (currentOrderId < 0)
            {
                Debug.LogError("No order selected");
                return;
            }

            var actualOrder = new Order(config, currentRequestedOrder.id);
            shopManager.AddOrder(actualOrder, 1, currentOrderId);
            orderAdded = true;
            
            addedConfigs.Add(config);
            UpdateAddedOrders();

            if (shopManager.OrderWishes.Count > 0)
                GetNextOrder();
            else
            {
                ordersListText.text = "All orders handled!";
                currentOrderId = -1;
            }
        }

        private void UpdateAddedOrders()
        {
            placedOrdersListText.text = "Orders added:\r\n\r\n";

            foreach (var item in purchasableItems)
            {
                placedOrdersListText.text += item.name + " - " + addedConfigs.Count(x => x == item) + Environment.NewLine;
            }
        }

        public void PlaceOrder()
        {
            if (!orderAdded) return;
            
            shopManager.PurchaseOrders();
            purchaseWindowDisplay.SetActive(false);
        }
        
        public void Cancel()
        {
            purchaseWindowDisplay.SetActive(false);
        }
    }
}