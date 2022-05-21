using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace VitaSoftware
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private QueueManager queueManager;
        [SerializeField] private GraveyardManager graveyardManager;
        [SerializeField] private GameObject gravestonePrefab;
        [SerializeField] private Wallet wallet;
        [SerializeField] private InventoryManager inventoryManager;

        private void Awake()
        {
            wallet.Initialise();
        }

        private void OnEnable()
        {
            ShopCounter.CustomerArrived += OnCustomerArrived;
        }

        private void OnDisable()
        {
            ShopCounter.CustomerArrived -= OnCustomerArrived;
        }

        private void OnCustomerArrived(Customer customer)
        {
            Debug.Log("Customer arrived");
            HandleCustomer(customer);//TODO: make interactable
        }

        private void HandleCustomer(Customer customer)
        {
            Debug.Log("Handling customer");
            
            
            if (graveyardManager.TryGetNextSpot(out var position))
            {
                StartCoroutine(ProcessOrder(position, customer));
            }
            else
            {
                //Add to waiting list -> cold storage
                Debug.Log("No more room");
                HandledCustomer(customer);
            }
        }

        private IEnumerator ProcessOrder(Vector3 position, Customer customer)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            var gravestoneConfig = inventoryManager.GetGravestoneForBudget(customer.Budget);
            Instantiate(gravestoneConfig.GravestonePrefab, position, Quaternion.identity);
            wallet.EarnMoney(gravestoneConfig.Price);
            HandledCustomer(customer);
        }

        private void HandledCustomer(Customer customer)
        {
            customer.SendHome();
            queueManager.CustomerHandled(null); //TODO: Improve
        }
    }
}