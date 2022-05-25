using System;
using VitaSoftware.Shop;

namespace VitaSoftware.Logistics
{
    [Serializable]
    public class Order
    {
        public GravestoneConfig gravestone;
        public int id;
        private static int nextId = 1;

        public float Price => gravestone.Price;

        public Order(GravestoneConfig config)
        {
            gravestone = config;
            id = nextId++;
        }

        public Order(GravestoneConfig config, int id)
        {
            gravestone = config;
            this.id = id;
        }
    }
}