using System;
using UnityEngine;

namespace VitaSoftware
{
    public class DeliveryZone : MonoBehaviour
    {
        [SerializeField] private DeliveryManager deliveryManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.parent.TryGetComponent<DeliveryTruck>(out var truck))
            {
                deliveryManager.HandleTruckArrived(truck.Load);
            }
        }
    }
}