using System.Collections.Generic;
using System.Linq;
using TowerDefense.Scripts.Debug;
using TowerDefense.Scripts.Input;
using TowerDefense.Scripts.Path;
using TowerDefense.Scripts.TileMap;
using TowerDefense.Scripts.Towers;
using TowerDefense.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Interactivity
{
    public class AddTowerToMapController : MonoBehaviour, IPressListener, IPointListener
    {
        [SerializeField] private LayerMask layerObstaclesTag;
        [SerializeField] private LevelController levelController;
        [SerializeField] private TowerCollection towerCollection;

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
                var startPoint = levelController.CurrentMap.LayerObstacles.WorldToCell(enemy.transform.position);
                var newPath = _pathCollections.FindPath(startPoint);
                enemy.SetMovingPath(newPath);
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
            
            if (IsCrossObstacleOrTower(tower))
                return false;
            
            if (!IsCanFindPath())
                return false;
            
            return true;
        }

        //Todo need fix
        private bool IsCanFindPath()
        {
            var tileMapLevel = levelController.CurrentMap;
            var enemyPoint = MapUtils.GetFirstTargetPoints(tileMapLevel);
            var target = MapUtils.GetTargetPoints(tileMapLevel);

            var startTile = enemyPoint.First();
            var endTile = target.First();

            var list = UnityEngine.Pool.ListPool<Vector3Int>.Get();
            
           // HashSet<Vector3Int> map = new HashSet<Vector3Int>();

            var obsticles = levelController.CurrentMap.LayerObstacles;
            var cel = obsticles.cellBounds;

            for (int x = cel.xMin; x < cel.xMax + 1; x++)
            {
                for (int y = cel.yMin; y < cel.yMax + 1; y++)
                {
                    var tile = obsticles.GetTile(new Vector3Int(x, y));
                    if (tile)
                    {
                        list.Add(new Vector3Int(x, y));
                        list.Add(new Vector3Int(x + 1, y));
                        list.Add(new Vector3Int(x + 1, y + 1));
                        list.Add(new Vector3Int(x, y + 1));
                    }
                }
            }
            
            OtherPoints(list);
            var pathfinderToTileMap = new PathfinderToTileMap(tileMapLevel);
            _findPath = pathfinderToTileMap.FindPathFromTileToTile(startTile.Position, endTile.Position , list);

            UnityEngine.Pool.ListPool<Vector3Int>.Release(list);
            return _findPath != null;
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
            UnityEngine.Debug.Log(tower.PointToGrid);
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