using NavMeshPlus.Components;
using TowerDefense.Scripts.Input;
using TowerDefense.Scripts.Towers;
using TowerDefense.Scripts.Ui;
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
        private MapDragHandler _dragHandler;
        private LevelObjects _levelObjects;
        
        [Inject]
        public void Construct(
            InputSwitcher inputSwitcher , 
            LevelObjects levelObjects)
        {
            _navMeshPath = new(); 
            _levelObjects = levelObjects;
            _inputSwitcher = inputSwitcher;
            _inputSwitcher.SetChangeMap(this);
        }

        public void StartDrag(MapDragHandler dragHandler)
        {
            _dragHandler = dragHandler;
            _dragHandler.Init(
                levelController , 
                layerObstaclesTag , 
                _levelObjects , 
                _navMeshPath);
            _inputSwitcher.SwitchToChangeMapHandlerControl();
            Time.timeScale = 0.05f;
        }

        public void OnPress(bool isPressed)
        {
            _dragHandler.OnPress(isPressed);
            if (!isPressed)
            {
                _inputSwitcher.SwitchToPlayerControl();
                Time.timeScale = 1.0f;
            }
        }

        public void OnPointWorld(Vector3 worldPosition)
        {
            _dragHandler.OnUpdateWorldPosition(worldPosition);
        }

        public void OnPointMoving(Vector2 direction)
        {
        }
    }
}