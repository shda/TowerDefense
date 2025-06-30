using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerDefense.Scripts.Ui.DownMenu
{
    public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private float drawValueToSelect;
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI countText;
        
        private ScrollRect _scrollRect;
        public MapDragHandler MapDragHandler { get; set; }
        public Action<DragItem> OnSelect;

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

        public void SetHandler(MapDragHandler mapDragHandler, int count, ScrollRect scrollRect)
        {
            countText.text = count.ToString();
            
            _scrollRect = scrollRect;
            itemImage.sprite = mapDragHandler.Sprite;
            MapDragHandler = mapDragHandler;
        }
    }
}