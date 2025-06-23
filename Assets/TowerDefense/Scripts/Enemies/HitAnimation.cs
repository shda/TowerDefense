using DG.Tweening;
using UnityEngine;

namespace TowerDefense.Scripts.Enemies
{
    public class HitAnimation : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer spriteRenderer;
        
        private Sequence _seq;
        public void Play()
        {
            if(_seq != null)
                return;
            
            _seq = DOTween.Sequence();
            var duration = 0.1f;
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
            _seq.Append(spriteRenderer.DOColor(new Color(0.5f,0.5f,0.5f), duration));
            _seq.Append(spriteRenderer.DOColor(new Color(1,1,1), duration));

            _seq = null;
        }
    }
}