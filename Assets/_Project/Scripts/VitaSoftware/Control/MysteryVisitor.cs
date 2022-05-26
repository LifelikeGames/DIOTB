using System;
using UnityEngine;
using VitaSoftware.Shop;

namespace VitaSoftware.Control
{
    public class MysteryVisitor : BaseCustomer
    {
        public static event Action<MysteryVisitor> MysteryVisitorArrived;

        private bool triggered;

        public override void Handle()
        {
            if (triggered) return;
            Debug.Log("A mystery visitor has arrived");
            MysteryVisitorArrived?.Invoke(this);
            triggered = true;
        }
    }
}