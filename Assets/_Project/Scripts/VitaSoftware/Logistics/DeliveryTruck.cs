using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VitaSoftware.Shop;

namespace VitaSoftware.Logistics
{
    public class DeliveryTruck : MonoBehaviour
    {
        [SerializeField] private DeliveryManager deliveryManager;
        [SerializeField] private float speed = 1;
        [SerializeField] private AudioSource engine;
        [SerializeField] private AudioSource beep;
        [SerializeField] private float unloadTime =3;

        private int sign = -1;
        private bool unloading;

        public bool Waiting { get; private set; } = true;

        public List<Order> Load { get; private set; }

        private void Update()
        {
            if (Waiting || unloading) return;
            transform.position += transform.forward * (sign * (speed * Time.deltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<DeliveryZone>(out var deliveryZone))
            {
                ArrivedAtDeliveryZone();
            }
            else if (other.TryGetComponent<DeliveryTruckSpawner>(out var spawner))
            {
                engine.Stop();
                Waiting = true;
                deliveryManager.StartWaitingForOrder();
                sign = -1;
            }
        }

        private void ArrivedAtDeliveryZone()
        {
            StartCoroutine(UnloadItems());

            IEnumerator UnloadItems()
            {
                unloading = true;
                beep.Stop();
                yield return new WaitForSeconds(unloadTime);
                unloading = false;
                sign = 1;
                engine.Stop();
                engine.Play();
            }
        }

        public void Dispatch(List<Order> pendingOrders)
        {
            engine.Play();
            beep.Play();
            Waiting = false;
            Load = new(pendingOrders);
        }
    }
}