using UnityEngine;

namespace TowerDefense.Scripts.PlayerScripts
{
    public class PlayerAnimations : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer robotFaceRenderer;
        [SerializeField] private Sprite spriteFire;
        [SerializeField] private Sprite spriteNoFire;

        [SerializeField] private PlayerAutoShooting playerAutoShooting;
        [SerializeField] private float timeToSwitch = 0.5f;

        private float _currentTimeToSwitch = float.MaxValue;

        private void Awake()
        {
            playerAutoShooting.OnFire = () => { _currentTimeToSwitch = 0; };
        }

        private void Update()
        {
            _currentTimeToSwitch += Time.deltaTime;
            if (_currentTimeToSwitch > timeToSwitch)
            {
                robotFaceRenderer.sprite = spriteNoFire;
            }
            else
            {
                robotFaceRenderer.sprite = spriteFire;
            }
        }
    }
}