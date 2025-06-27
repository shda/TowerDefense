using TMPro;
using TowerDefense.Scripts.Towers;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.Scripts.Ui.UpLevel
{
    public class UpLevelItem : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI textLabel;
        
        public void SetInfo(Sprite sprite , string text)
        {
            if (image != null)
                image.sprite = sprite;
            if (textLabel != null)
                textLabel.text = text;
        }
    }
}