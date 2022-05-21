using System;
using UnityEngine;

namespace VitaSoftware
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private ShopManager shopManager;
        [SerializeField] private GameObject waitingCustomerDisplay;

        private Customer nextCustomer;
        
        private void OnEnable()
        {
            shopManager.CustomerWaiting += OnCustomerWaiting;
        }

        private void Awake()
        {
            ToggleCustomerDisplay(false);
        }

        private void OnCustomerWaiting(Customer customer)
        {
            ToggleCustomerDisplay(true);
            nextCustomer = customer;
        }

        public void HandleCustomer()
        {
            if (nextCustomer == null)
            {
                Debug.LogError("Next customer was null");
                return;
            }

            shopManager.HandleCustomer(nextCustomer);
            nextCustomer = null;
            ToggleCustomerDisplay(false);
        }

        private void ToggleCustomerDisplay(bool state)
        {
            waitingCustomerDisplay.SetActive(state);
        }
    }
}