using BlockBlast.Scripts.Common.Extensions;
using TowerDefense.Scripts.Towers.Detectors;
using TowerDefense.Scripts.Ui;
using UnityEngine;
using UnityEngine.AI;


namespace TowerDefense.Scripts.Towers
{
    public abstract class TowerBase : MonoBehaviour , IRemoveLevelObject
    {
        [SerializeField] protected SpriteRenderer wrongPlaceSprite;
        [SerializeField] protected BoxCollider2D boxCollider2D;
        [SerializeField] protected EnemyDetector enemyDetector;
        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] protected NavMeshObstacle  navMeshObstacle; 
        [SerializeField] protected float detectionDelay = 0.5f;
        [SerializeField] protected float fireDelay = 0.5f;
        
        public Vector3Int PointToGrid { get; set; }
        public NavMeshObstacle NavMeshObstacle => navMeshObstacle;
        public SpriteRenderer WrongPlaceSprite => wrongPlaceSprite;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        public Collider2D Collider => boxCollider2D;
        
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