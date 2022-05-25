using System.Collections.Generic;
using UnityEngine;

namespace VitaSoftware.Graveyard
{
    [CreateAssetMenu(fileName = "New Graveyard Manager", menuName = "VitaSoftware/GraveyardManager", order = 0)]
    public class GraveyardManager : ScriptableObject
    {
        private List<Transform> spots;
        
        public void Initialise(IEnumerable<Transform> spotList)
        {
            spots = new (spotList);
        }
        
        public bool TryGetNextSpot(out Vector3 position, int index)
        {
            if (spots.Count > 0)
            {
                position = spots[index].position;
                return true;
            }

            position = Vector3.zero;
            return false;
        }
    }
}