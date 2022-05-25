using UnityEngine;
using VitaSoftware.Graveyard;

namespace VitaSoftware.Shop
{
    public class OrderUI : MonoBehaviour
    {
        [SerializeField] private GameObject graveStoneDisplay;
        [SerializeField] private ShopManager shopManager;
        [SerializeField] private PurchaseWindowUI purchaseWindowUI;
        [SerializeField] private CemeteryOverviewUI cemeteryOverviewUI;

        private void Awake()
        {
            ToggleGravestoneDisplay(false);
        }

        private void OnEnable()
        {
            shopManager.OrdersAvailable += OnOrdersAvailable;
        }

        private void OnOrdersAvailable()
        {
            ToggleGravestoneDisplay(true);
        }

        private void ToggleGravestoneDisplay(bool state)
        {
            graveStoneDisplay.SetActive(state);
        }

        public void PlaceOrders()
        {
            cemeteryOverviewUI.PlaceOrders();
        }

        public void OrderGraveObjects()
        {
            purchaseWindowUI.EnableAndGetOrders();
            //shopManager.PurchaseOrders();
        }
    }
}