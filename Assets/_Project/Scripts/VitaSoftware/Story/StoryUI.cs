using System;
using UnityEngine;
using VitaSoftware.Control;
using VitaSoftware.Shop;
using VitaSoftware.Underworld;

namespace VitaSoftware.Story
{
    public class StoryUI : MonoBehaviour
    {
        [SerializeField] private GameObject storyDisplay;
        [SerializeField] private UnderworldManager underworldManager;
        [SerializeField] private QueueManager queueManager;

        private MysteryVisitor mysteryVisitor;

        private void Awake()
        {
            underworldManager.Initialise();
            storyDisplay.SetActive(false);
        }

        private void OnEnable()
        {
            MysteryVisitor.MysteryVisitorArrived += OnMysteryVisitorArrived;
        }

        private void OnDisable()
        {
            MysteryVisitor.MysteryVisitorArrived -= OnMysteryVisitorArrived;
        }

        private void OnMysteryVisitorArrived(MysteryVisitor visitor)
        {
            mysteryVisitor = visitor;
            storyDisplay.SetActive(true);
        }

        public void Accept()
        {
            underworldManager.IsActive = true;
            
            storyDisplay.SetActive(false);
            SendVisitorHome();
        }

        public void Refuse()
        {
            storyDisplay.SetActive(false);
            SendVisitorHome();
        }

        private void SendVisitorHome()
        {
            mysteryVisitor.SendHome();
            queueManager.CustomerHandled();
        }
    }
}