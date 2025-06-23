using System;
using System.Collections.Generic;
using System.Linq;
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
            if (!isPressed)
            {
                if (!IsCanInsertTower(_tower))
                {
                    DestroyImmediate(_tower.gameObject);
                }
                else
                {
                    levelMap.TowersToMap.Add(_tower);
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

        private bool IsCanInsertTower(TowerBase tower)
        {
            if (!IsTowerOnMap(tower))
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
            var tileMapLevel = levelMap.CurrentMap;
            var enemyPoint = MapUtils.GetFirstTargetPoints(tileMapLevel);
            var target = MapUtils.GetTargetPoints(tileMapLevel);

            var startTile = enemyPoint.First();
            var endTile = target.First();

            var list = UnityEngine.Pool.ListPool<Vector2Int>.Get();
            OtherPoints(list);
            var pathfinderToTileMap = new PathfinderToTileMap(tileMapLevel);
            var findPath = pathfinderToTileMap.FindPathFromTileToTile(startTile, endTile , list);

            UnityEngine.Pool.ListPool<Vector2Int>.Release(list);
            return findPath != null;
        }

        private void OtherPoints(List<Vector2Int> list)
        {
            GetTowerCoordinates(_tower , list);
            foreach (var tower in levelMap.TowersToMap)      
            {
                GetTowerCoordinates(tower , list);
            }
        }

        private void GetTowerCoordinates(TowerBase tower , List<Vector2Int> list)
        {
            var layerObstacles = levelMap.CurrentMap.LayerObstacles;
            var point = layerObstacles.ConvertWorldToIndexZeroLeftBottomCorner(tower.transform.position);
            point -= new Vector3Int(1, 1);
            list.Add(new Vector2Int(point.x, point.y));
            list.Add(new Vector2Int(point.x + 1, point.y));
            list.Add(new Vector2Int(point.x + 1, point.y + 1));
            list.Add(new Vector2Int(point.x, point.y + 1));
        }

        public bool IsTowerOnMap(TowerBase tower)
        {
            var layerObstacles = levelMap.CurrentMap.LayerObstacles;
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
            var layerObstacles = levelMap.CurrentMap.LayerObstacles;
            tower.transform.position = layerObstacles.FindNearestPointToGrid(position);
            var isCross = !IsCanInsertTower(tower);
            tower.WrongPlaceSprite.gameObject.SetActive(isCross);
        }

        private void Update()
        {
            if (_tower == null)
                return;

            //   var tileMapLevel = levelMap.CurrentMap;
            var layerObstacles = levelMap.CurrentMap.LayerObstacles;
            var point = layerObstacles.ConvertWorldToIndexZeroLeftBottomCorner(_tower.transform.position);
            point -= new Vector3Int(1, 1);
            // var point = layerObstacles.FindNearestPointToGrid(_tower.transform.position);
            //var tile = tileMapLevel.LayerObstacles.WorldToCell(point);
            //  var size = tileMapLevel.LayerObstacles.cellBounds.position;
            //  var pointToLeftBottonCorner = tile - size;
           // UnityEngine.Debug.Log(point);
            //  UnityEngine.Debug.Log(tile);
        }
    }
}