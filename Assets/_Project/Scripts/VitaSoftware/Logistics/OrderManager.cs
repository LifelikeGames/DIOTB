using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VitaSoftware.General;
using VitaSoftware.Shop;

namespace VitaSoftware.Logistics
{
    [CreateAssetMenu(fileName = "New Order Manager", menuName = "VitaSoftware/OrderManager", order = 0)]
    public class OrderManager : ScriptableManager
    {
        private List<Order> currentOrders;

        public override void Initialise()
        {
            currentOrders = new List<Order>();
        }

        public void PlaceOrder(GravestoneConfig config, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                currentOrders.Add(new Order(config));    
            }
        }

        public float GetPriceOfAllOrders()
        {
            return currentOrders.Select(x => x.gravestone).Sum(x => x.Price);
        }

        public List<GravestoneConfig> ProcessOrders()
        {
            var orderedGravestones = currentOrders.Select(x => x.gravestone).ToList();
            currentOrders.Clear();
            return orderedGravestones;
        }
    }
    
    [Serializable]
    public struct Order
    {
        public GravestoneConfig gravestone;

        public Order(GravestoneConfig config)
        {
            gravestone = config;
        }
    }
}