using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.Scripts.Ui.UpLevel
{
    public class UpLevelItem : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI textLabel;
        
        private Action<UpLevelItem> PressItem;
        
        public void SetInfo(Sprite sprite , string text , Action<UpLevelItem> pressItem)
        {
            if (image != null)
                image.sprite = sprite;
            if (textLabel != null)
                textLabel.text = text;

            PressItem = pressItem;
        }

        public void OnPressItem()
        {
            PressItem?.Invoke(this);
        }
    }
}