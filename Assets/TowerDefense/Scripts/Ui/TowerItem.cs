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
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private TowerBase selectTower;
        
        public TowerBase SelectTower => selectTower;
        public Action<TowerItem> OnSelect;

        private float _dragValue = 0;

        private void Start()
        {
            scrollRect = GetComponentInParent<ScrollRect>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragValue = 0;
            scrollRect.OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _dragValue = eventData.delta.y;
            if (_dragValue > drawValueToSelect)
            {
                _dragValue = 0;
                OnSelect?.Invoke(this);
            }

            scrollRect.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            scrollRect.OnEndDrag(eventData);
        }
    }
}