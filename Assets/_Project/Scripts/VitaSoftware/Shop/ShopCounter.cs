using System;
using UnityEngine;
using VitaSoftware.Control;

namespace VitaSoftware.Shop
{
    public class ShopCounter : MonoBehaviour
    {
        public static event Action<BaseCustomer> CustomerArrived;
        public static event Action<BaseCustomer> CustomerLeft;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.parent.TryGetComponent<BaseCustomer>(out var customer))
            {
                CustomerArrived?.Invoke(customer);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.transform.parent.TryGetComponent<BaseCustomer>(out var customer))
            {
                CustomerLeft?.Invoke(customer);
            }
        }
    }
}