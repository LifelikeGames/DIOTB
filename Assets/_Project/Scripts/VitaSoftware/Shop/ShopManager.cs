using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VitaSoftware.Control;
using VitaSoftware.Economy;
using VitaSoftware.Graveyard;
using VitaSoftware.Logistics;
using Random = UnityEngine.Random;

namespace VitaSoftware.Shop
{
    //TODO: Split up into further managers
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private QueueManager queueManager;
        [SerializeField] private GraveyardManager graveyardManager;
        [SerializeField] private GameObject gravestonePrefab;
        [SerializeField] private GameObject cratePrefab;
        [SerializeField] private Wallet wallet;
        [SerializeField] private InventoryManager inventoryManager;
        [SerializeField] private OrderManager orderManager;
        [SerializeField] private DeliveryManager deliveryManager;
        
        public event Action<Customer> CustomerWaiting;
        public event Action OrdersAvailable;
        public event Action GravestonesPlaced;

        private List<GravestoneConfig> gravestonesToPlace;
        private List<GravestoneConfig> gravestonesOrdered;

        public List<GravestoneConfig> GravestoneWishes { get; private set; }

        private void Awake()
        {
            GravestoneWishes = new();
            gravestonesToPlace = new();
            wallet.Initialise();
            orderManager.Initialise();
            //TODO: move to central place that initialises/resets all scriptable managers
        }

        private void OnEnable()
        {
            ShopCounter.CustomerArrived += OnCustomerArrived;
            deliveryManager.GravestonesArrived += OnGravestonesArrived;
        }

        private void OnDisable()
        {
            ShopCounter.CustomerArrived -= OnCustomerArrived;
            deliveryManager.GravestonesArrived -= OnGravestonesArrived;
        }

        private void OnCustomerArrived(Customer customer)
        {
            Debug.Log("Customer arrived");
            CustomerWaiting?.Invoke(customer);
            //HandleCustomer(customer);//TODO: make interactable
        }

        public void HandleCustomer(Customer customer)
        {
            Debug.Log("Handling customer");
            StartCoroutine(ProcessCustomer(customer));
        }

        private IEnumerator ProcessCustomer(Customer customer)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            var gravestoneConfig = inventoryManager.GetGravestoneForBudget(customer.Budget);
            //Instantiate(gravestoneConfig.GravestonePrefab, position, Quaternion.identity);
            GravestoneWishes.Add(gravestoneConfig);
            //orderManager.PlaceOrder(gravestoneConfig, 1);
            wallet.EarnMoney(gravestoneConfig.Price);
            HandledCustomer(customer);
            
            OrdersAvailable?.Invoke();//TODO: move to order manager?
        }

        public void AddOrder(GravestoneConfig config, int quantity)
        {
            GravestoneWishes.Remove(GravestoneWishes.LastOrDefault());//TODO: satisfaction
            orderManager.PlaceOrder(config, quantity);
        }

        public void PurchaseOrders()
        {
            var price = orderManager.GetPriceOfAllOrders(); 
            if (price > wallet.CurrentMoney)
            {
                Debug.Log("Insufficient funds to purchase all orders");
                //TODO: allow individual purchases
                return;
            }

            if (!wallet.TrySpendMoney(price))
            {
                Debug.LogError("Insufficient funds to purchase all orders");
                return;
            }
            
            gravestonesOrdered = orderManager.ProcessOrders();

            deliveryManager.DeliverGravestones(gravestonesOrdered);
        }
        
        private void OnGravestonesArrived(List<GravestoneConfig> gravestones)
        {
            gravestonesToPlace.AddRange(gravestones);
            for (var index = 0; index < gravestones.Count; index++)
            {
                var gravestone = gravestones[index];
                gravestonesOrdered.Remove(gravestone);
            }
        }

        public void PlaceGravestones()
        {
            var removeFromStock = new List<GravestoneConfig>();
            for (int i = 0; i < gravestonesToPlace.Count; i++)
            {
                if (graveyardManager.TryGetNextSpot(out var position))
                {
                    Instantiate(gravestonesToPlace[i].GravestonePrefab, position, Quaternion.identity);
                    removeFromStock.Add(gravestonesToPlace[i]);
                }
                else
                {
                    //Add to waiting list -> cold storage
                    Debug.Log("No more room");
                    return;
                }
            }

            foreach (var config in removeFromStock)
            {
                gravestonesToPlace.Remove(config);
            }
            
            GravestonesPlaced?.Invoke();
        }

        private void HandledCustomer(Customer customer)
        {
            customer.SendHome();
            queueManager.CustomerHandled(null); //TODO: Improve
        }
    }
}