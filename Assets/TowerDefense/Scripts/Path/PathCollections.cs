using System.Collections.Generic;
using System.Linq;
using TowerDefense.Scripts.Towers;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Path
{
    public class PathCollections 
    {
        private LevelObjects _levelObjects;
        private LevelController _levelController;

        private readonly Vector3 _offset = new(0.25f, 0.25f);
        
        [Inject]
        public void Construct(
            LevelController levelController , 
            LevelObjects levelObjects)
        {
            _levelController = levelController;
            _levelObjects = levelObjects;
        }
        
        public IEnumerable<Vector3> FindPathStartEnemy()
        {
            var enemyStartPoint = _levelController.FirstTargetTile;
            return FindPath(enemyStartPoint.Position);
        }
        
        public IEnumerable<Vector3> FindPath(Vector3Int startTilePositon)
        {
            var targetPoint = _levelController.PlayerBase;
            var findPath = FindPath(startTilePositon, targetPoint.Position);
            return findPath.Select(x => _levelController.CurrentMap.LayerObstacles.GetCellCenterWorld(x) + _offset);
        }
        
        public IReadOnlyList<Vector3Int> FindPath(
            Vector3Int startTilePositon, 
            Vector3Int endTilePositon)
        {
            var list = UnityEngine.Pool.ListPool<Vector3Int>.Get();
            
            var obsticles = _levelController.CurrentMap.LayerObstacles;
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
            
            AddTowersToList(list);
            
            var findPath = FindPathFromTileToTile(startTilePositon, endTilePositon , list);
            UnityEngine.Pool.ListPool<Vector3Int>.Release(list);
            
            return findPath;
        }
        
        public IReadOnlyList<Vector3Int> FindPathFromTileToTile(
            Vector3Int startTilePositon, 
            Vector3Int endTilePositon, 
            IEnumerable<Vector3Int> otherObstacles)
        {
            var obstaclesHash = new HashSet<Vector3Int>(otherObstacles);

            var cellBounds = _levelController.CurrentMap.LayerObstacles.cellBounds;
            
            return Pathfinder.FindPath(
                startTilePositon, 
                endTilePositon,
                cellBounds.min, 
                cellBounds.max, 
                obstaclesHash , true);
        }
        
        private void AddTowersToList(List<Vector3Int> list)
        {
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


        
    }
}