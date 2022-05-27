using UnityEngine;
using VitaSoftware.Economy;
using VitaSoftware.General;

namespace VitaSoftware.Underworld
{
    [CreateAssetMenu(fileName = "New Underworld Manager", menuName = "VitaSoftware/UnderworldManager", order = 0)]
    public class UnderworldManager : ScriptableManager
    {
        [SerializeField] private float corpsePrice;
        [SerializeField] private Wallet wallet;
        public bool IsActive { get; set; }
        
        public override void Initialise()
        {
            IsActive = false;
        }
        
        public void SellCorpse()
        {
            wallet.EarnMoney(corpsePrice);
        }
    }
}