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
        
        public void PlaceOrder(Order order, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                currentOrders.Add(order);    
            }
        }

        public float GetPriceOfAllOrders()
        {
            return currentOrders.Select(x => x.gravestone).Sum(x => x.Price);
        }

        public List<Order> ProcessOrders()
        {
            var orderedGravestones = currentOrders.ToList();
            currentOrders.Clear();
            return orderedGravestones;
        }
    }
}