using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VitaSoftware.Graveyard
{
    public class PlotUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coordinateLabel;
        [SerializeField] private Button button;
        [SerializeField] private Color occupiedColor;
        [SerializeField] private Image image;

        public bool IsOccupied { get; set; }

        public void SetLabel(string coordinates)
        {
            coordinateLabel.text = coordinates;
        }

        public void ItemsPlaced()
        {
            IsOccupied = true;
            image.color = occupiedColor;
        }
    }
}