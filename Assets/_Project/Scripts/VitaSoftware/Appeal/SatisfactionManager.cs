using System;
using UnityEngine;
using VitaSoftware.General;
using VitaSoftware.Logistics;
using VitaSoftware.Shop;

namespace VitaSoftware.Appeal
{
    [CreateAssetMenu(fileName = "New Satisfaction Manager", menuName = "VitaSoftware/SatisfactionManager", order = 0)]
    public class SatisfactionManager : ScriptableManager
    {
        [SerializeField] private float startingSatisfaction = 5;
        [SerializeField] private float maxSatisfaction = 10;
        [SerializeField] private float tooCheapPenalty, differentItemPenalty, tooExpensivePenalty;
        [SerializeField] private float goodChoiceBonus;

        public float CurrentSatisfaction { get; private set; }
        public float MaxSatisfaction => maxSatisfaction;

        public event Action SatisfactionUpdated;

        public void LowerSatisfaction(float amount)
        {
            CurrentSatisfaction -= amount;
            CurrentSatisfaction = Mathf.Max(1, CurrentSatisfaction);
            SatisfactionUpdated?.Invoke();
        }

        public void RaiseSatisfaction(float amount)
        {
            CurrentSatisfaction += amount;
            CurrentSatisfaction = Mathf.Min(10, CurrentSatisfaction);
            SatisfactionUpdated?.Invoke();
        }

        public override void Initialise()
        {
            CurrentSatisfaction = startingSatisfaction;
        }

        public void CalculateSatisfaction(Order actualOrder, Order requestedOrder)
        {
            UpdateSatisfactionForItem(actualOrder.gravestone, requestedOrder.gravestone);
            UpdateSatisfactionForItem(actualOrder.coffin, requestedOrder.coffin);

            void UpdateSatisfactionForItem(PurchasableItem actualItem, PurchasableItem requestedItem)
            {
                if (actualItem.Price < requestedItem.Price)
                    LowerSatisfaction(tooCheapPenalty);
                else if (actualItem.Price > requestedItem.Price)
                    LowerSatisfaction(tooExpensivePenalty);
                else if (actualItem.Label != requestedItem.Label)
                    LowerSatisfaction(differentItemPenalty);
                else
                    RaiseSatisfaction(goodChoiceBonus);
            }
        }
    }
}