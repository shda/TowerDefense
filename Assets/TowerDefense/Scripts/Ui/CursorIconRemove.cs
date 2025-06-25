using System;
using UnityEngine;

namespace TowerDefense.Scripts.Ui
{
    public class CursorIconRemove : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void SetSpriteRenderToSizeCollider(Collider2D targetCollider)
        {
            var ppu = spriteRenderer.sprite.pixelsPerUnit;
            var colliderSizeInUnits = targetCollider.bounds.size;
            var spriteSizeInPixels = spriteRenderer.sprite.rect.size;
            var desiredScale = new Vector2(
                (colliderSizeInUnits.x * ppu) / spriteSizeInPixels.x,
                (colliderSizeInUnits.y * ppu) / spriteSizeInPixels.y
            );
            spriteRenderer.transform.localScale = desiredScale;
            spriteRenderer.transform.position = targetCollider.bounds.center;
        }
        
        public void HideSelectRect() => spriteRenderer.enabled = false;
        public void ShowSelectRect() => spriteRenderer.enabled = true;
    }
}