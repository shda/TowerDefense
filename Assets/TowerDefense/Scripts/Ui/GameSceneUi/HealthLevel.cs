using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TowerDefense.Scripts.Ui.GameSceneUi
{
    public class HealthLevel : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Image sliderLine;
        [SerializeField] private Color colorUpHalfLife;
        [SerializeField] private Color colorDownHalfLife;
        private IMemoryPool _memoryPool;

        public void SetLife(float currentHealth, float maxHealth)
        {
            var value = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
            sliderLine.fillAmount = value;

            if (value > 0.5f)
            {
                sliderLine.color = colorUpHalfLife;
            }
            else
            {
                sliderLine.color = colorDownHalfLife;
            }
        }

        public void UpdatePositon(RectTransform canvasRectTransform, Vector3 wordPosition)
        {
            var screenPoint = Camera.main.WorldToScreenPoint(wordPosition);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRectTransform,
                screenPoint,
                Camera.main,
                out var localPoint
            );

            rectTransform.localPosition = localPoint;
        }
    }
}