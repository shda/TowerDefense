using System;
using TowerDefense.Scripts.Towers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerDefense.Scripts.Ui
{
    public class TowerItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private float drawValueToSelect;
        [SerializeField] private Image itemImage;
        
        private ScrollRect _scrollRect;
        public TowerBase SelectTower { get; set; }
        public Action<TowerItem> OnSelect;

        private float _dragValue = 0;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragValue = 0;
            _scrollRect.OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _dragValue = eventData.delta.y;
            if (_dragValue > drawValueToSelect)
            {
                _dragValue = 0;
                OnSelect?.Invoke(this);
            }

            _scrollRect.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _scrollRect.OnEndDrag(eventData);
        }

        public void SetPrefab(TowerBase tower , ScrollRect scrollRect)
        {
            _scrollRect = scrollRect;
            itemImage.sprite = tower.SpriteRenderer.sprite;
            SelectTower = tower;
        }
    }
}