using System;
using System.Collections.Generic;
using UnityEngine;
using VitaSoftware.Shop;

namespace VitaSoftware.Logistics
{
    [CreateAssetMenu(fileName = "New Delivery Manager", menuName = "VitaSoftware/DeliveryManager", order = 0)]
    public class DeliveryManager : ScriptableObject
    {
        public event Action<List<Order>> OrdersOrdered;
        public event Action<List<Order>> OrdersArrived;
        public event Action TruckReady;

        public void DeliverGravestones(List<Order> orders)
        {
            OrdersOrdered?.Invoke(orders);
        }

        public void HandleTruckArrived(List<Order> orders)
        {
            OrdersArrived?.Invoke(orders);
        }

        public void StartWaitingForOrder()
        {
            TruckReady?.Invoke();
        }
    }
}