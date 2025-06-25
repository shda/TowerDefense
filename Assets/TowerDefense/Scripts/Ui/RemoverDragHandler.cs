using BlockBlast.Scripts.Common.Extensions;
using TowerDefense.Scripts.Obstacles;
using TowerDefense.Scripts.Towers;
using UnityEngine;

namespace TowerDefense.Scripts.Ui
{
    public class RemoverDragHandler : MapDragHandler
    {
        public override Sprite Sprite => _spriteItem;
        
        private readonly Sprite _spriteItem;
        private readonly CursorIconRemove _cursorIconRemove;
        
        private IRemoveLevelObject _selectRemoveObject;

        public RemoverDragHandler(Sprite spriteItem , CursorIconRemove cursorIconRemove)
        {
            _cursorIconRemove = cursorIconRemove;
            _spriteItem = spriteItem;
        }
        
        public override void OnUpdateWorldPosition(Vector3 worldPosition)
        {
            _selectRemoveObject = null;
            
            _cursorIconRemove.Show();
            _cursorIconRemove.HideSelectRect();
            _cursorIconRemove.transform.position = worldPosition;

            var raycastAll = Physics2D.RaycastAll(worldPosition, Vector2.zero);
            foreach (var obstacle in raycastAll)
            {
                _selectRemoveObject = obstacle.transform.GetComponent<IRemoveLevelObject>();
                if (_selectRemoveObject != null)
                {
                    var targetCollider = _selectRemoveObject.Collider;
                    _cursorIconRemove.SetSpriteRenderToSizeCollider(targetCollider);
                    _cursorIconRemove.ShowSelectRect();
                    break;
                }
            }
        }

        public override void OnPress(bool isPressed)
        {
            _cursorIconRemove.Hide();
            
            UnityEngine.Debug.Log(isPressed);

            if (_selectRemoveObject != null)
            {
                _selectRemoveObject.Remove(_levelObjects);
                _selectRemoveObject = null;
            }
        }
    }
}