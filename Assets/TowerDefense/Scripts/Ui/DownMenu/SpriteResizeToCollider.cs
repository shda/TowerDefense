using UnityEngine;

namespace TowerDefense.Scripts.Ui.DownMenu
{
    public class SpriteResizeToCollider : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private bool setPositionToThis;

        public void SetSpriteRenderToSizeCollider(Collider2D targetCollider)
        {
            var ppu = spriteRenderer.sprite.pixelsPerUnit;
            var colliderSizeInUnits = targetCollider.bounds.size;
            var spriteSizeInPixels = spriteRenderer.sprite.rect.size;
            var desiredScale = new Vector2(
                colliderSizeInUnits.x * ppu / spriteSizeInPixels.x,
                colliderSizeInUnits.y * ppu / spriteSizeInPixels.y
            );
            spriteRenderer.transform.localScale = desiredScale;

            if (setPositionToThis)
                spriteRenderer.transform.position = transform.position;
            else
                spriteRenderer.transform.position = targetCollider.bounds.center;
        }


        public void HideSelectRect()
        {
            spriteRenderer.enabled = false;
        }

        public void ShowSelectRect()
        {
            spriteRenderer.enabled = true;
        }
    }
}