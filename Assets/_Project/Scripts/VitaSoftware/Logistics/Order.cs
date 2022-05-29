using System;
using VitaSoftware.Shop;

namespace VitaSoftware.Logistics
{
    [Serializable]
    public class Order
    {
        public GravestoneConfig gravestone;
        public CoffinConfig coffin;
        public int id;
        private static int nextId = 1;

        public float Price => gravestone.Price;

        public Order(GravestoneConfig config)
        {
            gravestone = config;
            id = nextId++;
        }

        public Order(GravestoneConfig stone, CoffinConfig box)
        {
            gravestone = stone;
            coffin = box;
            id = nextId++;
        }

        public Order(GravestoneConfig config, CoffinConfig box, int id)
        {
            gravestone = config;
            coffin = box;
            this.id = id;
        }
    }
}