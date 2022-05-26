using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        
        public event Action<BaseCustomer> CustomerWaiting;
        public event Action OrdersAvailable;
        public event Action GravestonesPlaced;

        private List<Order> itemsOrdered;

        public List<Order> OrderWishes { get; private set; }
        public List<Order> OrdersToPlace { get; set; }

        private void Awake()
        {
            OrderWishes = new();
            OrdersToPlace = new();
            wallet.Initialise();
            orderManager.Initialise();
            //TODO: move to central place that initialises/resets all scriptable managers
        }

        private void OnEnable()
        {
            ShopCounter.CustomerArrived += OnCustomerArrived;
            deliveryManager.OrdersArrived += OnOrdersArrived;
        }

        private void OnDisable()
        {
            ShopCounter.CustomerArrived -= OnCustomerArrived;
            deliveryManager.OrdersArrived -= OnOrdersArrived;
        }

        private void OnCustomerArrived(BaseCustomer customer)
        {
            Debug.Log("Customer arrived");
            CustomerWaiting?.Invoke(customer);
            //HandleCustomer(customer);//TODO: make interactable
        }

        public void HandleCustomer(BaseCustomer customer)
        {
            Debug.Log("Handling customer");
            StartCoroutine(ProcessCustomer(customer));
        }

        private IEnumerator ProcessCustomer(BaseCustomer customer)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            customer.Handle();
            HandledCustomer();
            
            OrdersAvailable?.Invoke();//TODO: move to order manager?
        }

        public void AddOrder(Order order, int quantity, int index)
        {
            OrderWishes.Remove(OrderWishes[index]);//TODO: satisfaction
            orderManager.PlaceOrder(order, quantity);
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
            
            itemsOrdered = orderManager.ProcessOrders();

            deliveryManager.DeliverGravestones(itemsOrdered);
        }
        
        private void OnOrdersArrived(List<Order> gravestones)
        {
            OrdersToPlace.AddRange(gravestones);
            for (var index = 0; index < gravestones.Count; index++)
            {
                var gravestone = gravestones[index];
                itemsOrdered.Remove(gravestone);
            }
        }

        public void PlaceGravestones()//TODO: placeable instead of gravestone
        {
            var removeFromStock = new List<Order>();
            for (int i = 0; i < OrdersToPlace.Count; i++)
            {
                if (graveyardManager.TryGetNextSpot(out var position, -1))
                {
                    Instantiate(OrdersToPlace[i].gravestone.GravestonePrefab, position, Quaternion.identity);
                    removeFromStock.Add(OrdersToPlace[i]);
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
                OrdersToPlace.Remove(config);
            }
            
            GravestonesPlaced?.Invoke();
        }

        public void PlaceGravestone(GravestoneConfig config, int index)
        {
            if (graveyardManager.TryPlaceGravestone(config.GravestonePrefab, index))
            {
                
            }
            else
            {
                Debug.Log("No more room");
                return;
            }
        }

        public void PlaceGraveDecorations()
        {
            
        }

        private void HandledCustomer()
        {
            queueManager.CustomerHandled(); //TODO: Improve
        }
    }
}