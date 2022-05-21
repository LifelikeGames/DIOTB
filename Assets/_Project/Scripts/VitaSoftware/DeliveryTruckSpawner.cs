using System;
using System.Collections.Generic;
using UnityEngine;

namespace VitaSoftware
{
    public class DeliveryTruckSpawner : MonoBehaviour
    {
        [SerializeField] private DeliveryManager deliveryManager;
        [SerializeField] private DeliveryTruck truckPrefab;

        private DeliveryTruck lorry;
        private bool isOrderWaiting;
        private List<GravestoneConfig> pendingOrders;

        private void Awake()
        {
            pendingOrders = new List<GravestoneConfig>();
        }

        private void OnEnable()
        {
            deliveryManager.GravestonesOrdered += OnGravestonesOrdered;
            deliveryManager.TruckReady += OnTruckReady;
        }

        private void OnTruckReady()
        {
            if (lorry.Waiting && isOrderWaiting)
            {
                lorry.Dispatch(pendingOrders);
                pendingOrders.Clear();
            }
        }

        private void OnGravestonesOrdered(List<GravestoneConfig> gravestones)
        {
            if (lorry == null)
            {
                SpawnTruck(gravestones);
                return;
            }

            if (lorry.Waiting)
            {
                lorry.Dispatch(gravestones);
            }
            else
            {
                isOrderWaiting = true;
                pendingOrders.AddRange(gravestones);
            }
        }

        private void SpawnTruck(List<GravestoneConfig> gravestones)
        {
            lorry = Instantiate(truckPrefab, transform.position, truckPrefab.transform.rotation);
            lorry.Dispatch(gravestones);
            
        }
    }
}