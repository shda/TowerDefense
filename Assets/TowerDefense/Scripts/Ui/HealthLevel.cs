using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TowerDefense.Scripts.Ui
{
    public class HealthLevel : MonoBehaviour, IPoolable<IMemoryPool>
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Image sliderLine;
        [SerializeField] private Color colorUpHalfLife;
        [SerializeField] private Color colorDownHalfLife;
        private IMemoryPool _memoryPool;
        
        public void SetLife(float currentHealth , float maxHealth)
        {
            var value =  Mathf.Clamp(currentHealth / maxHealth, 0, 1);
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
                out Vector2 localPoint
            );
            
            rectTransform.localPosition = localPoint;
        }

        #region Spawn
        public void OnDespawned()
        {
            
        }

        public void OnSpawned(IMemoryPool p1)
        {
            _memoryPool = p1;
        }
        public void RemoveToPool()
        {
            _memoryPool.Despawn(this);
        }
        
        public class Factory : PlaceholderFactory<HealthLevel>
        {
        }
        
        public class MemoryPool : MonoPoolableMemoryPool<IMemoryPool, HealthLevel>
        {
        }
        #endregion
    }
}