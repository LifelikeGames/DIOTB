using TMPro;
using UnityEngine;

namespace VitaSoftware.Economy
{
    public class WalletUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI walletText;
        [SerializeField] private Wallet wallet;

        private void OnEnable()
        {
            wallet.MoneyUpdated += OnMoneyUpdated;
        }

        private void OnMoneyUpdated(float newCurrentMoney)
        {
            walletText.text = $"${newCurrentMoney}";
        }
    }
}