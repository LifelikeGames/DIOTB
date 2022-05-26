using System;
using UnityEngine;

namespace VitaSoftware.Notifications
{
    [CreateAssetMenu(fileName = "New Notification Manager", menuName = "VitaSoftware/NotificationManager", order = 0)]
    public class NotificationManager : ScriptableObject
    {
        public event Action<string> NotificationRequested;
        
        public void RequestNotification(string text)
        {
            NotificationRequested?.Invoke(text);
        }
    }
}