using BlockBlast.Scripts.Common.Extensions;
using TowerDefense.Scripts.Game;
using TowerDefense.Scripts.Towers.Detectors;
using TowerDefense.Scripts.Ui;
using UnityEngine;
using UnityEngine.AI;


namespace TowerDefense.Scripts.Towers
{
    public abstract class TowerBase : MonoBehaviour , IRemoveLevelObject
    {
        [SerializeField] protected TowerComponents towerComponents;
        [SerializeField] protected float detectionDelay = 0.5f;
        [SerializeField] protected float fireDelay = 0.5f;
        public Collider2D Collider => towerComponents.Collider;
        public TowerComponents TowerComponents => towerComponents;
        
        private float _currentTime;
        private float _currentFireTime;
        
        private void Start()
        {
            _currentTime = float.MaxValue;
            _currentFireTime = float.MaxValue;
        }
        
        protected abstract void CallDetect();
        protected abstract void CallFire();
        
        protected virtual void Update()
        {
            _currentTime  += Time.deltaTime;
            if (_currentTime >= detectionDelay)
            {
                _currentTime = 0;
                CallDetect();
            }
            
            _currentFireTime += Time.deltaTime;
            if (_currentFireTime >= fireDelay)
            {
                _currentFireTime = 0;
                CallFire();
            }
        }
        
        public void Remove(LevelObjects levelObjects)
        {
            levelObjects.Towers.Remove(this);
            gameObject.Hide();
            Destroy(gameObject);
        }
    }
}