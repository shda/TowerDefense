using TriInspector;
using UnityEngine;

namespace TowerDefense.Scripts.Enemies
{
    public class EnemySprite : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer spriteRenderer;
        [SerializeField] public Sprite[] sprites;
        
        private void OnEnable()
        {
            RandomSprite();
        }

        [Button]
        private void RandomSprite()
        {
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }
}