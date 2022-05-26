using UnityEngine;

namespace VitaSoftware.Control
{
    public abstract class BaseCustomer : MonoBehaviour
    {
        [SerializeField] protected Controller controller;
        
        public void SendHome()
        {
            controller.SendHome();
        }

        public abstract void Handle();
    }
}