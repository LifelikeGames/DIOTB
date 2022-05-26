using System;
using System.Collections.Generic;
using UnityEngine;
using VitaSoftware.Control;

namespace VitaSoftware.Shop
{
    [CreateAssetMenu(fileName = "New Queue Manager", menuName = "VitaSoftware/QueueManager", order = 0)]
    public class QueueManager : ScriptableObject
    {
        private List<Transform> spotList;
        private Queue<Controller> customerQueue;
        public event Action NewSpotAvailable;

        public void InitialiseQueue(IEnumerable<Transform> spots)
        {
            spotList = new List<Transform>(spots);
            customerQueue = new Queue<Controller>();
        }

        public bool TryGetSpot(out Vector3 position, Controller controller)
        {
            if(customerQueue.Count < spotList.Count)
            {
                position = spotList[customerQueue.Count].position;
                customerQueue.Enqueue(controller);
                return true;    
            }

            position = Vector3.zero;
            return false;
        }

        public bool TryGetNextCustomer(out Controller customer)
        {
            if (customerQueue.Count > 0)
            {
                customer = customerQueue.Dequeue();
                customerQueue.Clear();
                NewSpotAvailable?.Invoke();
                return true;
            }

            customer = null;
            return false;
        }

        public bool HasAvailableSpot()
        {
            return customerQueue.Count < 5;
        }

        public void CustomerHandled()
        {
            customerQueue.Dequeue();
            customerQueue.Clear();
            NewSpotAvailable?.Invoke();
        }
    }
}