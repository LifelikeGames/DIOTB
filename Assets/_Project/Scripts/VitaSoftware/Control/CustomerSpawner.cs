using System.Collections;
using UnityEngine;
using VitaSoftware.Shop;
using Random = UnityEngine.Random;

namespace VitaSoftware.Control
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject customerPrefab;
        [SerializeField,Range(1, 60)] private float minSpawnFrequency, maxSpawnFrequency;//TODO: replace with minmax slider
        [SerializeField] private QueueManager queueManager;
        
        private void Start()
        {
            StartCoroutine(SpawnCustomer());
        }

        private IEnumerator SpawnCustomer()
        {
            while (true)
            {
                if (queueManager.HasAvailableSpot())
                {
                    var customer = Instantiate(customerPrefab, transform.position, Quaternion.identity);
                    customer.transform.parent = transform;    
                }
                yield return new WaitForSeconds(Random.Range(minSpawnFrequency, maxSpawnFrequency));
            }
        }
    }
}