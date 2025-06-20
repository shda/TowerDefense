using TowerDefense.Scripts.Enemies;
using TowerDefense.Scripts.Towers.Detectors;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Towers
{
    public abstract class TowerBase : MonoBehaviour
    {
        [SerializeField] protected EnemyDetector enemyDetector;
        [SerializeField] protected float detectionDelay = 0.5f;
        [SerializeField] protected float fireDelay = 0.5f;
        
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
    }
}