using System;
using UnityEngine;
using VitaSoftware.Control;

namespace VitaSoftware.Shop
{
    public class ShopCounter : MonoBehaviour
    {
        public static event Action<Customer> CustomerArrived;
        public static event Action<Customer> CustomerLeft;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.parent.TryGetComponent<Customer>(out var customer))
            {
                CustomerArrived?.Invoke(customer);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.transform.parent.TryGetComponent<Customer>(out var customer))
            {
                CustomerLeft?.Invoke(customer);
            }
        }
    }
}