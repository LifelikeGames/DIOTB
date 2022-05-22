using UnityEngine;

namespace VitaSoftware.Shop
{
    public class OrderUI : MonoBehaviour
    {
        [SerializeField] private GameObject graveStoneDisplay;
        [SerializeField] private ShopManager shopManager;
        [SerializeField] private PurchaseWindowUI purchaseWindowUI;

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

        public void PlaceGraves()
        {
            shopManager.PlaceGravestones();
        }

        public void OrderGraves()
        {
            purchaseWindowUI.EnableAndSetOrders();
            //shopManager.PurchaseOrders();
        }
    }
}