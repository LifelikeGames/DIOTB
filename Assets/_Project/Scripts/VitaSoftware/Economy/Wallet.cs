using System;
using UnityEngine;
using VitaSoftware.General;

namespace VitaSoftware.Economy
{
    [CreateAssetMenu(fileName = "New Wallet", menuName = "VitaSoftware/Wallet", order = 0)]
    public class Wallet : ScriptableManager
    {
        [SerializeField] private float startingMoney;

        public event Action<float> MoneyUpdated;

        public float CurrentMoney { get; private set; }

        public void EarnMoney(float amount)
        {
            CurrentMoney += amount;
            MoneyUpdated?.Invoke(CurrentMoney);
        }

        public bool CanSpendMoney(float amount)
        {
            return CurrentMoney >= amount;
        }

        public bool TrySpendMoney(float amount)
        {
            if (CurrentMoney >= amount)
            {
                CurrentMoney -= amount;
                MoneyUpdated?.Invoke(CurrentMoney);
                return true;
            }

            Debug.LogWarning("Insufficient funds");
            return false;
        }

        public override void Initialise()
        {
            CurrentMoney = startingMoney;
            MoneyUpdated?.Invoke(CurrentMoney);
        }
    }
}