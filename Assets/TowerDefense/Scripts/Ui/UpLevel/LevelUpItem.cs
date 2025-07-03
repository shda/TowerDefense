using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.Scripts.Ui.UpLevel
{
    public class LevelUpItem : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI textLabel;

        private Action<LevelUpItem> PressItem;

        public void SetInfo(Sprite sprite, string text, Action<LevelUpItem> pressItem)
        {
            if (image != null)
            {
                image.sprite = sprite;
            }

            if (textLabel != null)
            {
                textLabel.text = text;
            }

            PressItem = pressItem;
        }

        public void OnPressItem()
        {
            PressItem?.Invoke(this);
        }
    }
}