using System.Collections.Generic;
using UnityEngine;

namespace VitaSoftware.Graveyard
{
    [CreateAssetMenu(fileName = "New Graveyard Manager", menuName = "VitaSoftware/GraveyardManager", order = 0)]
    public class GraveyardManager : ScriptableObject
    {
        private Queue<Transform> spotQueue;
        
        public void Initialise(IEnumerable<Transform> spots)
        {
            spotQueue = new Queue<Transform>(spots);
        }
        
        public bool TryGetNextSpot(out Vector3 position)
        {
            if (spotQueue.Count > 0)
            {
                position = spotQueue.Dequeue().position;
                return true;
            }

            position = Vector3.zero;
            return false;
        }
    }
}