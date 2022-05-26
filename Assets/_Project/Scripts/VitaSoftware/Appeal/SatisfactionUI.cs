using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VitaSoftware.Appeal
{
    public class SatisfactionUI : MonoBehaviour
    {
        [SerializeField] private Image currentSatisfactionImage;
        [SerializeField] private SatisfactionManager satisfactionManager;

        private float widthMultiplier;

        private void OnEnable()
        {
            satisfactionManager.Initialise();
            widthMultiplier = currentSatisfactionImage.rectTransform.sizeDelta.x / satisfactionManager.MaxSatisfaction; 
            UpdateSatisfactionSlider();
            satisfactionManager.SatisfactionUpdated += OnSatisfactionUpdated;
        }

        private void OnDisable()
        {
            satisfactionManager.SatisfactionUpdated -= OnSatisfactionUpdated;
        }

        private void OnSatisfactionUpdated()
        {
            UpdateSatisfactionSlider();
        }

        private void UpdateSatisfactionSlider()
        {
            currentSatisfactionImage.rectTransform.sizeDelta = new Vector2(satisfactionManager.CurrentSatisfaction * widthMultiplier,
                currentSatisfactionImage.rectTransform.sizeDelta.y);
            
        }
    }
}