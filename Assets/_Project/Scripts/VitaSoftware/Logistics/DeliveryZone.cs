using System.Collections;
using UnityEngine;

namespace VitaSoftware.Logistics
{
    public class DeliveryZone : MonoBehaviour
    {
        [SerializeField] private DeliveryManager deliveryManager;
        [SerializeField] private float unloadTime = 3;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<DeliveryTruck>(out var truck))
            {
                StartCoroutine(HandleTruckArrived(truck));
            }
            
            
            IEnumerator HandleTruckArrived(DeliveryTruck deliveryTruck)
            {
                yield return new WaitForSeconds(unloadTime);
                deliveryManager.HandleTruckArrived(deliveryTruck.Load);
            }
        }
    }
}