using TowerDefense.Scripts.TileMap;
using TowerDefense.Scripts.Towers;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefense.Scripts.Ui
{
    public class TowerDragHandler : MapDragHandler
    {
        private readonly TowerBase _towerPrefab;
        private TowerBase _currentTower;
        public override Sprite Sprite => _towerPrefab.SpriteRenderer.sprite;
        
        public TowerDragHandler(TowerBase towerPrefab)
        {
            _towerPrefab =  towerPrefab;
        }
        
        public override void OnUpdateWorldPosition(Vector3 worldPosition)
        {
            if (_currentTower == null)
            {
                _currentTower = Object.Instantiate(_towerPrefab);
                _currentTower.gameObject.SetActive(true);
            }

            UpdatePositionTower(_currentTower , worldPosition);
        }

        public override void OnPress(bool isPressed)
        {
            if (!isPressed)
            {
                if (!IsCanInsertTower(_currentTower))
                {
                    Object.DestroyImmediate(_currentTower.gameObject);
                }
                else
                {
                    _levelObjects.Towers.Add(_currentTower);
                }

                _currentTower = null;
                UpdateAllPaths();
            }
        }
        
        private void UpdateAllPaths()
        {
            foreach (var enemy in _levelObjects.Enemies)
            {
                enemy.MovingToNavigationMas.Repath();
            }
        }
        
        private bool IsCanInsertTower(TowerBase tower)
        {
            if (!IsInsideMap(tower.transform.position) || 
                !IsLimitations(tower.transform.position) ||
                IsCrossObstacleOrTower(tower) ||
                !IsCanFindPathToBase())
                return false;
            return true;
        }
        
        private bool IsCanFindPathToBase()
        {
            var enemyPoint = _levelController.FirstTargetTile.WorldPosition;
            var target = _levelController.PlayerBase.WorldPosition;

            NavMesh.CalculatePath(enemyPoint, target, NavMesh.AllAreas, _navMeshPath);
            
            if (_navMeshPath.status == NavMeshPathStatus.PathPartial)
            {
                UnityEngine.Debug.Log("Path is partially blocked!");
                return false;
            }
            
            if (_navMeshPath.status == NavMeshPathStatus.PathInvalid)
            {
                UnityEngine.Debug.Log("No valid _navMeshPath exists!");
                return false;
            }
            
            UnityEngine.Debug.Log("Path is ok!");
            return true;
        }

        private bool IsLimitations(Vector3 worldPoint)
        {
            var tileMapLevel = _levelController.CurrentMap;
            return tileMapLevel.PointInLimitions(worldPoint);
        }
        
        private bool IsInsideMap(Vector3 worldPoint)
        {
            var layerObstacles = _levelController.CurrentMap.LayerObstacles;
            var point = layerObstacles.ConvertWorldToIndexZeroLeftBottomCorner(worldPoint);
            if (point.x < 0 || point.y < 0)
                return false;
            
            if(point.x > layerObstacles.size.x - 2 || point.y > layerObstacles.size.y - 2)
                return false;
            return true;
        }

        private bool IsCrossObstacleOrTower(TowerBase tower)
        {
            bool isCross = false;

            var colliders = Physics2D.OverlapBoxAll(tower.transform.position, tower.Collider.bounds.size, 0);
            if (colliders != null)
            {
                foreach (var cd in colliders)
                {
                    if (cd.gameObject == tower.gameObject)
                        continue;
                    
                    int objLayer = cd.gameObject.layer;
                    if ((1 << objLayer & _layerMask) != 0)
                    {
                        isCross = true;
                        break;
                    }
                }
            }

            return isCross;
        }
        
        private void UpdatePositionTower(TowerBase tower, Vector3 position)
        {
            var layerObstacles = _levelController.CurrentMap.LayerObstacles;
            tower.transform.position = layerObstacles.FindNearestPointToGrid(position);
            tower.PointToGrid = layerObstacles.WorldToCell(tower.transform.position);
            tower.PointToGrid -= new Vector3Int(1, 1);
            var isCross = !IsCanInsertTower(tower);
            tower.WrongPlaceSprite.gameObject.SetActive(isCross);
        }
    }
}