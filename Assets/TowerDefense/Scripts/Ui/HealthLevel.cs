using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TowerDefense.Scripts.Ui
{
    public class HealthLevel : MonoBehaviour, IPoolable<IMemoryPool>
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Image sliderLine;
        private IMemoryPool _memoryPool;
        
        public void SetLife(float currentHealth , float maxHealth)
        {
            sliderLine.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
        }
        
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

        public void UpdatePositon(RectTransform canvasRectTransform, Vector3 wordPosition)
        {

            Vector2 screenPoint = Camera.main.WorldToScreenPoint(wordPosition);
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRectTransform,
                screenPoint,
                Camera.main,
                out Vector2 localPoint
            );

            // Применяем позицию к RectTransform
            rectTransform.localPosition = localPoint;
   
            
            //Vector3 screenPos = Camera.main.WorldToScreenPoint(wordPosition);
           // rectTransform.position = screenPos;
        }
    }
}