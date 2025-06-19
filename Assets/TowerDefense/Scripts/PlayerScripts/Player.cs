using TowerDefense.Scripts.Enemies;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public Health Health { get; set; }

        [Inject]
        public void Construct(Health health)
        {
            Health = health;
            Reset();
        }

        public void Reset()
        {
            //
            Health.CurrentHealth.Value = 100;
            Health.MaxHealth.Value = 100;
        }
    }
}