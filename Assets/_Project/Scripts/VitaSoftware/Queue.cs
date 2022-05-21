using System;
using UnityEngine;

namespace VitaSoftware
{
    public class Queue : MonoBehaviour
    {
        [SerializeField] private QueueManager queueManager;
        [SerializeField] private Transform[] queueSpots;

        private void Awake()
        {
            if(queueManager != null)
                queueManager.InitialiseQueue(queueSpots);
        }
    }
}
