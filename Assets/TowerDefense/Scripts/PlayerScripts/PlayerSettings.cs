using UnityEngine;

namespace TowerDefense.Scripts.PlayerScripts
{
    [CreateAssetMenu(menuName = "Create TowerDefense/PlayerSettings", fileName = "PlayerSettings", order = 0)]
    public class PlayerSettings : ScriptableObject
    {
        public float StartHealth = 50f;
        public float UpHealth = 5f;
        public float DelayRecoveryHealth = 1f;

        public float MoveSpeed = 5.0f;
    }
}