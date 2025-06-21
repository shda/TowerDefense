using System;
using System.Linq;
using TowerDefense.Scripts.Input;
using TowerDefense.Scripts.Towers;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Interactivity
{
    public class AddTowerToMapController : MonoBehaviour, IPressListener, IPointListener
    {
        [SerializeField] private LayerMask layerObstaclesTag;
        [SerializeField] private LevelMap levelMap;
        [SerializeField] private TowerCollection towerCollection;

        private InputSwitcher _inputSwitcher;

        private TowerBase _tower;

        [Inject]
        public void Construct(InputSwitcher inputSwitcher)
        {
            _inputSwitcher = inputSwitcher;
            _inputSwitcher.SetChangeMap(this);
        }
        
        public void StartDrag(TowerBase itemSelectTower)
        {
            _tower = Instantiate(itemSelectTower);
            _tower.gameObject.SetActive(false);
            _inputSwitcher.SwitchToChangeMapHandlerControl();

            Time.timeScale = 0.1f;
        }

        public void OnPress(bool isPressed)
        {
            UnityEngine.Debug.Log(isPressed);
            if (!isPressed)
            {
                if (!IsCanInsertTower(_tower))
                {
                    DestroyImmediate(_tower.gameObject);
                }
     
                _tower = null;
                _inputSwitcher.SwitchToPlayerControl();
                
                Time.timeScale = 1.0f;
            }
        }

        public void OnPointWorld(Vector3 worldPosition)
        {
            if (_tower == null)
                return;
            _tower.gameObject.SetActive(true);
            UpdatePositionTower(_tower, worldPosition);
        }

        public void OnPointMoving(Vector2 direction)
        {
            
        }

        private Vector3 FindNearestPointToGrid(Vector3 worldPoint)
        {
            var layerObstacles = levelMap.CurrentMap.LayerObstacles;
            var size = layerObstacles.cellSize;

            var pointX = (float)Math.Round(worldPoint.x / size.x) * size.x;
            var pointY = (float)Math.Round(worldPoint.y / size.y) * size.y;
            return new Vector3(pointX, pointY, 0);
        }

        private bool IsCanInsertTower(TowerBase tower)
        {
            bool isCrossObstacleOrTower = CheckIsCrossObstacleOrTower(tower);
            return !isCrossObstacleOrTower;
        }

        private bool CheckIsCrossObstacleOrTower(TowerBase tower)
        {
            bool isCross = false;

            var colliders = Physics2D.OverlapBoxAll(tower.transform.position, tower.BoxCollider2D.size, 0);
            if (colliders != null)
            {
                foreach (var cd in colliders)
                {
                    if (cd.gameObject == tower.gameObject)
                        continue;

                    int objLayer = cd.gameObject.layer;
                    if ((1 << objLayer & layerObstaclesTag) != 0)
                    {
                       // UnityEngine.Debug.Log(cd);
                        isCross = true;
                        break;
                    }
                }
            }

            return isCross;
        }

        private void UpdatePositionTower(TowerBase tower, Vector3 position)
        {
            tower.transform.position = FindNearestPointToGrid(position);
            var isCross = CheckIsCrossObstacleOrTower(tower);
            tower.WrongPlaceSprite.gameObject.SetActive(isCross);
        }
    }
}