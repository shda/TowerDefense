using BlockBlast.Scripts.Common.Extensions;
using UnityEngine;

namespace TowerDefense.Scripts.Ui.DownMenu
{
    public class RemoverDragHandler : MapDragHandler
    {
        public override Sprite Sprite => _spriteItem;
        
        private readonly Sprite _spriteItem;
        private readonly SpriteResizeToCollider _spriteResizeToCollider;
        
        private IRemoveLevelObject _selectRemoveObject;

        public RemoverDragHandler(Sprite spriteItem , SpriteResizeToCollider spriteResizeToCollider)
        {
            _spriteResizeToCollider = spriteResizeToCollider;
            _spriteItem = spriteItem;
        }
        
        public override void OnUpdateWorldPosition(Vector3 worldPosition)
        {
            _selectRemoveObject = null;
            
            _spriteResizeToCollider.Show();
            _spriteResizeToCollider.HideSelectRect();
            _spriteResizeToCollider.transform.position = worldPosition;

            var raycastAll = Physics2D.RaycastAll(worldPosition, Vector2.zero);
            foreach (var obstacle in raycastAll)
            {
                _selectRemoveObject = obstacle.transform.GetComponent<IRemoveLevelObject>();
                if (_selectRemoveObject != null)
                {
                    var targetCollider = _selectRemoveObject.Collider;
                    _spriteResizeToCollider.SetSpriteRenderToSizeCollider(targetCollider);
                    _spriteResizeToCollider.ShowSelectRect();
                    break;
                }
            }
        }

        public override void OnPress(bool isPressed)
        {
            _spriteResizeToCollider.Hide();
            
            UnityEngine.Debug.Log(isPressed);

            if (_selectRemoveObject != null)
            {
                _selectRemoveObject.Remove(_levelObjects);
                _selectRemoveObject = null;
                
                OnSuccess?.Invoke(this);
            }
        }
    }
}