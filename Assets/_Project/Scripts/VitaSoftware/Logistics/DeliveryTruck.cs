using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VitaSoftware.Audio;
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
        [SerializeField] private SoundManager soundManager;

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
                SetEngineState(false);
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
                SetReverseState(false);
                yield return new WaitForSeconds(unloadTime);
                unloading = false;
                sign = 1;
                SetEngineState(false);
                SetEngineState(true);
            }
        }

        public void Dispatch(List<Order> pendingOrders)
        {
            SetEngineState(true);
            SetReverseState(true);
            Waiting = false;
            Load = new(pendingOrders);
        }

        private void SetEngineState(bool value)
        {
            if(!soundManager.IsSoundEnabled) return;
                
            if(value)
                engine.Play();
            else 
                engine.Stop();
        }

        private void SetReverseState(bool value)
        {
            if (!soundManager.IsSoundEnabled) return;
            
            if(value)
                beep.Play();
            else
                beep.Stop();
        }
    }
}