using System.Collections.Generic;
using System.Linq;
using NavMeshPlus.Components;
using TowerDefense.Scripts.Debug;
using TowerDefense.Scripts.Input;
using TowerDefense.Scripts.Path;
using TowerDefense.Scripts.TileMap;
using TowerDefense.Scripts.Towers;
using TowerDefense.Scripts.Utils;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace TowerDefense.Scripts.Interactivity
{
    public class AddTowerToMapController : MonoBehaviour, IPressListener, IPointListener
    {
        [SerializeField] private LayerMask layerObstaclesTag;
        [SerializeField] private LevelController levelController;
        [SerializeField] private TowerCollection towerCollection;
        [SerializeField] private NavMeshSurface navigationSurface;
        
        private NavMeshPath _navMeshPath; 
        private InputSwitcher _inputSwitcher;

        private TowerBase _tower;
        private LevelObjects _levelObjects;
        private IReadOnlyList<Vector3Int> _findPath;
        private PathCollections _pathCollections;

        [Inject]
        public void Construct(
            InputSwitcher inputSwitcher , 
            LevelObjects levelObjects , 
            PathCollections pathCollections)
        {
            _navMeshPath = new(); 
            
            _levelObjects = levelObjects;
            _pathCollections = pathCollections;
            
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
            if (!isPressed)
            {
                if (!IsCanInsertTower(_tower))
                {
                    DestroyImmediate(_tower.gameObject);
                }
                else
                {
                    _levelObjects.Towers.Add(_tower);
                }
                
                _tower = null;
                _inputSwitcher.SwitchToPlayerControl();

                Time.timeScale = 1.0f;
                UpdateAllPaths();
            }
        }

        private void UpdateAllPaths()
        {
            foreach (var enemy in _levelObjects.Enemies)
            {
                enemy.MovingToNavigationMas.Repath();
                
             //   var startPoint = levelController.CurrentMap.LayerObstacles.WorldToCell(enemy.transform.position);
              //  var newPath = _pathCollections.FindPath(startPoint);
               // enemy.SetMovingPath(newPath);
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

        private bool IsCanInsertTower(TowerBase tower)
        {
            if (!IsInsideMap(tower))
                return false;

            if (!IsLimitions(tower))
                return false;
            
            if (IsCrossObstacleOrTower(tower))
                return false;
            
            if (!IsCanFindPathToBase())
                return false;
            
            return true;
        }

        private bool IsLimitions(TowerBase tower)
        {
            var tileMapLevel = levelController.CurrentMap;
            return tileMapLevel.PointInLimitions(tower.transform.position);
        }

        //Todo need fix
        private bool IsCanFindPathToBase()
        {
            var tileMapLevel = levelController.CurrentMap;
            var enemyPoint = levelController.FirstTargetTile.WorldPosition;
            var target = levelController.PlayerBase.WorldPosition;

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

        private void OtherPoints(List<Vector3Int> list)
        {
            GetTowerCoordinates(_tower , list);
            foreach (var tower in _levelObjects.Towers)      
            {
                GetTowerCoordinates(tower , list);
            }
        }

        private void GetTowerCoordinates(TowerBase tower , List<Vector3Int> list)
        {
            var point = tower.PointToGrid;
            //point -= new Vector3Int(1, 1);
            list.Add(new Vector3Int(point.x, point.y));
            list.Add(new Vector3Int(point.x + 1, point.y));
            list.Add(new Vector3Int(point.x + 1, point.y + 1));
            list.Add(new Vector3Int(point.x, point.y + 1));
        }

        private bool IsInsideMap(TowerBase tower)
        {
            var layerObstacles = levelController.CurrentMap.LayerObstacles;
            var point = layerObstacles.ConvertWorldToIndexZeroLeftBottomCorner(tower.transform.position);
            if (point.x < 0 || point.y < 0)
                return false;
            
            if(point.x > layerObstacles.size.x - 2 || point.y > layerObstacles.size.y - 2)
                return false;

            return true;
        }

        private bool IsCrossObstacleOrTower(TowerBase tower)
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
            var layerObstacles = levelController.CurrentMap.LayerObstacles;
            tower.transform.position = layerObstacles.FindNearestPointToGrid(position);
            tower.PointToGrid = layerObstacles.WorldToCell(tower.transform.position);
            tower.PointToGrid -= new Vector3Int(1, 1);
            var isCross = !IsCanInsertTower(tower);
            tower.WrongPlaceSprite.gameObject.SetActive(isCross);
        }

        private void OnDrawGizmos()
        {
            if(_findPath == null)
                return;
            
            GizmoHelper.DrawPath(levelController.CurrentMap , _findPath);
        }
    }
}