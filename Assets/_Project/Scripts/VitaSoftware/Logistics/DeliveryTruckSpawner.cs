using System.Collections.Generic;
using UnityEngine;
using VitaSoftware.Shop;

namespace VitaSoftware.Logistics
{
    public class DeliveryTruckSpawner : MonoBehaviour
    {
        [SerializeField] private DeliveryManager deliveryManager;
        [SerializeField] private DeliveryTruck truckPrefab;

        private DeliveryTruck lorry;
        private bool isOrderWaiting;
        private List<Order> pendingOrders;

        private void Awake()
        {
            pendingOrders = new();
        }

        private void OnEnable()
        {
            deliveryManager.OrdersOrdered += OnOrdersOrdered;
            deliveryManager.TruckReady += OnTruckReady;
        }

        private void OnTruckReady()
        {
            if (lorry.Waiting && isOrderWaiting)
            {
                lorry.Dispatch(pendingOrders);
                pendingOrders.Clear();
            }
        }

        private void OnOrdersOrdered(List<Order> orders)
        {
            if (lorry == null)
            {
                SpawnTruck(orders);
                return;
            }

            if (lorry.Waiting)
            {
                lorry.Dispatch(orders);
                pendingOrders.Clear();
            }
            else
            {
                isOrderWaiting = true;
                pendingOrders.AddRange(orders);
            }
        }

        private void SpawnTruck(List<Order> orders)
        {
            lorry = Instantiate(truckPrefab, transform.position, truckPrefab.transform.rotation);
            lorry.Dispatch(orders);
        }
    }
}