using TowerDefense.Scripts.Towers.Detectors;
using TowerDefense.Scripts.Ui;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace TowerDefense.Scripts.Towers
{
    public class TowerComponents : MonoBehaviour
    {
        [FormerlySerializedAs("towerName")] [SerializeField] private string towerId;
        [SerializeField] private SpriteRenderer wrongPlaceSprite;
        [SerializeField] private BoxCollider2D boxCollider2D;
        [SerializeField] private EnemyDetector enemyDetector;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private NavMeshObstacle  navMeshObstacle;
        
        public NavMeshObstacle NavMeshObstacle => navMeshObstacle;
        public SpriteRenderer WrongPlaceSprite => wrongPlaceSprite;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        public Collider2D Collider => boxCollider2D;
        public EnemyDetector EnemyDetector => enemyDetector;
        
        public string TowerId => towerId;
    }
}