using System.Collections;
using UnityEngine;
using VitaSoftware.Appeal;
using VitaSoftware.Shop;
using Random = UnityEngine.Random;

namespace VitaSoftware.Control
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject customerPrefab;
        [SerializeField] private GameObject specialCustomerPrefab;
        [SerializeField] private QueueManager queueManager;
        [SerializeField] private SatisfactionManager satisfactionManager;
        [SerializeField] private float spawnFrequencyMultiplier = 1;
        [SerializeField] private int specialSpawnEvery = 10;

        private int customerCount;
        
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
                    var customer = Instantiate(customerCount == specialSpawnEvery?specialCustomerPrefab:customerPrefab, transform.position, Quaternion.identity);
                    customer.transform.parent = transform;
                    customerCount++;
                }
                yield return new WaitForSeconds(Random.Range(satisfactionManager.CurrentSatisfaction * spawnFrequencyMultiplier, satisfactionManager.CurrentSatisfaction*2 * spawnFrequencyMultiplier));
            }
        }
    }
}