using System;
using UnityEngine;

namespace VitaSoftware.Notifications
{
    public class NotificationUI : MonoBehaviour
    {
        [SerializeField] private NotificationManager notificationManager;
        [SerializeField] private NotificationPopup notificationPrefab;
        [SerializeField] private Transform notificationParent;

        private void OnEnable()
        {
            notificationManager.NotificationRequested += OnNotificationRequested;           
        }

        private void OnDisable()
        {
            notificationManager.NotificationRequested -= OnNotificationRequested;
        }

        private void OnNotificationRequested(string text)
        {
            var popup = Instantiate(notificationPrefab, notificationParent);
            popup.SetNotificationText(text);
        }
    }
}