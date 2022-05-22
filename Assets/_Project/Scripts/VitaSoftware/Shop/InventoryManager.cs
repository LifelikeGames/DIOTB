using System.Linq;
using UnityEngine;

namespace VitaSoftware.Shop
{
    [CreateAssetMenu(fileName = "New Inventory Manager", menuName = "VitaSoftware/InventoryManager", order = 0)]
    public class InventoryManager : ScriptableObject
    {
        [SerializeField] private GravestoneConfig[] gravestoneConfigs;

        public GravestoneConfig GetGravestoneForBudget(float budget)
        {
            return gravestoneConfigs.Where(x=>x.Price<=budget).OrderByDescending(x=>x.Price).FirstOrDefault();
        }
    }
}