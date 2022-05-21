using System;
using System.Collections.Generic;
using UnityEngine;

namespace VitaSoftware
{
    [CreateAssetMenu(fileName = "New Delivery Manager", menuName = "VitaSoftware/DeliveryManager", order = 0)]
    public class DeliveryManager : ScriptableObject
    {
        public event Action<List<GravestoneConfig>> GravestonesOrdered;
        public event Action<List<GravestoneConfig>> GravestonesArrived;
        public event Action TruckReady;

        public void DeliverGravestones(List<GravestoneConfig> gravestones)
        {
            GravestonesOrdered?.Invoke(gravestones);
        }

        public void HandleTruckArrived(List<GravestoneConfig> gravestones)
        {
            GravestonesArrived?.Invoke(gravestones);
        }

        public void StartWaitingForOrder()
        {
            TruckReady?.Invoke();
        }
    }
}