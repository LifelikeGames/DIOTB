using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VitaSoftware.Graveyard
{
    [CreateAssetMenu(fileName = "New Graveyard Manager", menuName = "VitaSoftware/GraveyardManager", order = 0)]
    public class GraveyardManager : ScriptableObject
    {
        private List<Transform> spots;
        private Dictionary<int, GameObject> gravestones;
        private Transform gravestoneParent;
        
        public void Initialise(IEnumerable<Transform> spotList, Transform parent)
        {
            spots = new (spotList);
            gravestoneParent = parent;
            gravestones = new();
        }
        
        public bool TryGetNextSpot(out Vector3 position, int index)
        {
            if (spots.Count > 0)
            {
                position = spots[index].transform.position;
                return true;
            }

            position = Vector3.zero;
            return false;
        }

        public void EmptySpot(int plotIndex)
        {
            Destroy(gravestones[plotIndex].gameObject);
            gravestones.Remove(plotIndex);
        }

        public bool TryPlaceGravestone(GameObject gravestonePrefab, int index)
        {
            if (TryGetNextSpot(out var position, index))
            {
                var gravestone = Instantiate(gravestonePrefab, position, Quaternion.identity);
                gravestone.transform.SetParent(gravestoneParent);
                gravestones.Add(index, gravestone);
                return true;
            }

            return false;
        }
    }
}