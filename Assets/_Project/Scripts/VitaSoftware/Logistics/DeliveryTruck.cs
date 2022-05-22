using System.Collections.Generic;
using UnityEngine;
using VitaSoftware.Shop;

namespace VitaSoftware.Logistics
{
    public class DeliveryTruck : MonoBehaviour
    {
        [SerializeField] private DeliveryManager deliveryManager;
        [SerializeField] private float speed = 1;

        private int sign = -1;

        public bool Waiting { get; private set; } = true;

        public List<GravestoneConfig> Load { get; private set; }

        private void Update()
        {
            if (Waiting) return;
            transform.position += transform.forward * (sign * (speed * Time.deltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<DeliveryZone>(out var deliveryZone))
            {
                sign = 1;
            }
            else if (other.TryGetComponent<DeliveryTruckSpawner>(out var spawner))
            {
                Waiting = true;
                deliveryManager.StartWaitingForOrder();
                sign = -1;
            }
        }

        public void Dispatch(List<GravestoneConfig> pendingOrders)
        {
            Waiting = false;
            Load = new(pendingOrders);
        }
    }
}