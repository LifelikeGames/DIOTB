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

        private void OnEnable()
        {
            deliveryManager.GravestonesArrived += OnGravestonesArrived;
            shopManager.GravestonesPlaced += OnGravestonesPlaced;
        }

        private void OnGravestonesArrived(List<GravestoneConfig> gravestones)
        {
            UpdateCrates(gravestones);
        }

        private void UpdateCrates(List<GravestoneConfig> gravestones)
        {
            var currentStock = gravestones.Count;

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
            ClearStock();//TODO: handle leftovers
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