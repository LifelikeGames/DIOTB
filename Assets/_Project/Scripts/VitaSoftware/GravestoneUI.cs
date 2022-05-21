using System;
using UnityEngine;

namespace VitaSoftware
{
    public class GravestoneUI : MonoBehaviour
    {
        [SerializeField] private GameObject graveStoneDisplay;
        [SerializeField] private ShopManager shopManager;

        private void Awake()
        {
            ToggleGravestoneDisplay(false);
        }

        private void OnEnable()
        {
            shopManager.OrdersAvailable += OnOrdersAvailable;
        }

        private void OnOrdersAvailable()
        {
            ToggleGravestoneDisplay(true);
        }

        private void ToggleGravestoneDisplay(bool state)
        {
            graveStoneDisplay.SetActive(state);
        }

        public void PlaceGraves()
        {
            shopManager.PlaceGravestones();
        }

        public void OrderGraves()
        {
            shopManager.ProcessOrders();
        }
    }
}