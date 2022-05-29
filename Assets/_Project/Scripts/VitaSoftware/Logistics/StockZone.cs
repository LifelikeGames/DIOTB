using System.Collections.Generic;
using UnityEngine;
using VitaSoftware.Shop;

namespace VitaSoftware.Logistics
{
    public class StockZone : MonoBehaviour
    {
        [SerializeField] private DeliveryManager deliveryManager;
        [SerializeField] private ShopManager shopManager;
        [SerializeField] private GameObject[] deliveries;

        private int currentStock;

        private void OnEnable()
        {
            deliveryManager.OrdersArrived += OnOrdersArrived;
            shopManager.GravestonesPlaced += OnGravestonesPlaced;
        }

        private void OnOrdersArrived(List<Order> orders)
        {
            currentStock += orders.Count;
            UpdateCrates();
        }

        public void UpdateCrates()
        {
            ClearStock();

            switch (currentStock)
            {
                case >= 10:
                    deliveries[0].SetActive(true);
                    deliveries[1].SetActive(true);
                    deliveries[2].SetActive(true);
                    break;
                case >= 5:
                    deliveries[0].SetActive(true);
                    deliveries[1].SetActive(true);
                    break;
                case > 0:
                    deliveries[0].SetActive(true);
                    break;
            }
        }

        private void OnGravestonesPlaced()
        {
            currentStock--;
            UpdateCrates();
        }

        private void Awake()
        {
            ClearStock();
        }

        private void ClearStock()
        {
            foreach (var delivery in deliveries)
            {
                delivery.SetActive(false);
            }
        }
    }
}