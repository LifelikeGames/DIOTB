using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VitaSoftware.Appeal;
using VitaSoftware.Logistics;
using VitaSoftware.Notifications;

namespace VitaSoftware.Shop
{
    //TODO: require gravestone purchase for each order??
    //TODO: clear order list on the left after orders are delivered
    public class PurchaseWindowUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI ordersListText;
        [SerializeField] private ItemUI itemUIPrefab;
        [SerializeField] private PurchasableItem[] purchasableItems;
        [SerializeField] private Transform purchaseGridParent;
        [SerializeField] private ShopManager shopManager;
        [SerializeField] private GameObject purchaseWindowDisplay;
        [SerializeField] private TextMeshProUGUI placedOrdersListText;
        [SerializeField] private SatisfactionManager satisfactionManager;
        [SerializeField] private NotificationManager notificationManager;

        private bool orderAdded;

        private int currentOrderId = -1;
        private Order currentRequestedOrder;
        private List<PurchasableItem> addedConfigs;

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
            addedConfigs = new ();
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

            ordersListText.text += $"Order {currentRequestedOrder.id}\r\n\r\nRequested gravestone: {currentRequestedOrder.gravestone.name}\r\nRequestedCoffing: {currentRequestedOrder.coffin.name}"; //TODO: add formatting to SO
        }

        public GravestoneConfig GravestoneForCurrentOrder { get; private set; }
        public CoffinConfig CoffinForCurrentOrder { get; private set; }
        public void AddToOrder(PurchasableItem config)
        {
            if (currentOrderId < 0)
            {
                Debug.LogError("No order selected");
                return;
            }

            if (config is GravestoneConfig gravestoneConfig)
            {
                if (GravestoneForCurrentOrder != null)//TODO: allow replacing for current order
                {
                    Debug.LogWarning("Gravestone already added to order");
                    return;
                }
                GravestoneForCurrentOrder = gravestoneConfig;
            }
            else
            {
                if (CoffinForCurrentOrder != null)
                {
                    Debug.LogWarning("Coffin already added to order");
                    return;
                }
                CoffinForCurrentOrder = (CoffinConfig) config;
            }
            UpdateAddedOrders(); 
            
        }

        public void FinaliseOrder()
        {
            if (GravestoneForCurrentOrder == null || CoffinForCurrentOrder == null)
            {
                notificationManager.RequestNotification("A coffin and a gravestone are required");//TODO: add default coffin/gravestone?
                Debug.LogWarning("Coffin and gravestone are required");
                return;
            }
            var actualOrder = new Order(GravestoneForCurrentOrder, CoffinForCurrentOrder, currentRequestedOrder.id);

            satisfactionManager.CalculateSatisfaction(actualOrder, currentRequestedOrder);
            shopManager.AddOrder(actualOrder, 1, currentOrderId);
            orderAdded = true;
            
            addedConfigs.Add(GravestoneForCurrentOrder);
            addedConfigs.Add(CoffinForCurrentOrder);
            UpdateAddedOrders();

            if (shopManager.OrderWishes.Count > 0)
                GetNextOrder();
            else
            {
                ordersListText.text = "All orders handled!";
                currentOrderId = -1;
            }

            CoffinForCurrentOrder = null;
            GravestoneForCurrentOrder = null;
        }

        private void UpdateAddedOrders()
        {
            placedOrdersListText.text = "Current order:\r\n";
            placedOrdersListText.text += "Gravestone: " + (GravestoneForCurrentOrder == null? "None" : GravestoneForCurrentOrder.name) + Environment.NewLine;
            placedOrdersListText.text += "Coffin: " + (CoffinForCurrentOrder == null? "None" : CoffinForCurrentOrder.name) + Environment.NewLine+Environment.NewLine;
            placedOrdersListText.text += "Orders added:\r\n\r\n";

            foreach (var item in purchasableItems)
            {
                placedOrdersListText.text += item.name + " - " + addedConfigs.Count(x => x == item) + Environment.NewLine;
            }
        }

        public void PlaceOrder()
        {
            if (!orderAdded)
            {
                notificationManager.RequestNotification("At least one order must be finalised");
                return;
            }
            
            shopManager.PurchaseOrders();
            purchaseWindowDisplay.SetActive(false);
        }
        
        public void Cancel()
        {
            purchaseWindowDisplay.SetActive(false);
        }
    }
}