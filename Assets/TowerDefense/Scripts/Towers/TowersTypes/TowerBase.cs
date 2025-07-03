using BlockBlast.Scripts.Common.Extensions;
using TowerDefense.Scripts.Game;
using TowerDefense.Scripts.Ui.DownMenu;
using UnityEngine;

namespace TowerDefense.Scripts.Towers.TowersTypes
{
    public abstract class TowerBase : MonoBehaviour, IRemoveLevelObject
    {
        [SerializeField] protected TowerComponents towerComponents;
        [SerializeField] protected float detectionDelay = 0.5f;
        [SerializeField] protected float fireDelay = 0.5f;
        private readonly int _maxLevel = 4;

        private readonly float _modByLevel = 0.2f;
        private float _currentFireTime;
        private float _currentTime;
        private int _level = 1;
        public TowerComponents TowerComponents => towerComponents;

        private void Start()
        {
            _currentTime = float.MaxValue;
            _currentFireTime = float.MaxValue;
        }

        protected virtual void Update()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= detectionDelay)
            {
                _currentTime = 0;
                CallDetect();
            }

            _currentFireTime += Time.deltaTime;
            var deley = fireDelay - fireDelay * (_level - 1) * _modByLevel;
            //UnityEngine.Debug.Log(deley);
            if (_currentFireTime >= deley)
            {
                _currentFireTime = 0;
                CallFire();
            }
        }

        public Collider2D Collider => towerComponents.Collider;

        public void Remove(LevelObjects levelObjects)
        {
            levelObjects.Towers.Remove(this);
            gameObject.Hide();
            Destroy(gameObject);
        }

        protected abstract void CallDetect();
        protected abstract void CallFire();

        protected float ModifierByLevel()
        {
            var modifier = 1f;
            modifier += (_level - 1) * _modByLevel;
            return modifier;
        }

        public bool CheckCanUpdate(TowerBase tower)
        {
            if (_level < _maxLevel)
                return true;
            return false;
        }

        public void UpLevel()
        {
            _level++;
            towerComponents.LevelLabel.text = _level.ToString();
            towerComponents.EnemyDetector.SetMultiplierRadius(ModifierByLevel());
        }
    }
}