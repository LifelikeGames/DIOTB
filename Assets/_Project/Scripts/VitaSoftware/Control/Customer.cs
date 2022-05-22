using UnityEngine;
using Random = UnityEngine.Random;

namespace VitaSoftware.Control
{
    public class Customer : MonoBehaviour
    {
        [SerializeField] private float budget; //TODO: to SO with other fields
        [SerializeField] private Controller controller;

        public float Budget => budget;

        private void Awake()
        {
            budget = Random.Range(25, 100);
        }

        public void SendHome()
        {
            controller.SendHome();
        }
    }
}