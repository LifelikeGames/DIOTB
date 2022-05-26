using UnityEngine;
using VitaSoftware.Appeal;
using VitaSoftware.Economy;
using VitaSoftware.Logistics;
using VitaSoftware.Shop;
using Random = UnityEngine.Random;

namespace VitaSoftware.Control
{
    public class Customer : BaseCustomer
    {
        [SerializeField] private float budget = 25; //TODO: to SO with other fields
        [SerializeField] private SatisfactionManager satisfactionManager;
        [SerializeField] private InventoryManager inventoryManager;
        [SerializeField] private ShopManager shopManager;
        [SerializeField] private Wallet wallet;

        public float Budget => budget;

        private void Awake()
        {
            budget = Random.Range(25*satisfactionManager.CurrentSatisfaction, 100*(satisfactionManager.CurrentSatisfaction/2));
            shopManager = FindObjectOfType<ShopManager>();
        }

        public override void Handle()
        {
            var gravestoneConfig = inventoryManager.GetGravestoneForBudget(budget);
            budget -= gravestoneConfig.Price;
            var coffinConfig = inventoryManager.GetCoffinForBudget(budget);
            //TODO: get random flower and other decoration wishes 

            var order = new Order(gravestoneConfig, coffinConfig);
            shopManager.OrderWishes.Add(order);

            wallet.EarnMoney(gravestoneConfig.Price);
            wallet.EarnMoney(coffinConfig.Price);
            
            SendHome();
        }
    }
}