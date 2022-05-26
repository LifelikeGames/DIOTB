using TMPro;
using UnityEngine;

namespace VitaSoftware.Notifications
{
    public class NotificationPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI notificationText;

        public void SetNotificationText(string text)
        {
            notificationText.text = text;
        }

        public void CloseNotification()
        {
            Destroy(gameObject);
        }
    }
}