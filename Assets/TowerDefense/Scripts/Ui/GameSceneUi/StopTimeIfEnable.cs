using UnityEngine;

namespace TowerDefense.Scripts.Ui.GameSceneUi
{
    public class StopTimeIfEnable : MonoBehaviour
    {
        private float _timeScale;
        private void OnEnable()
        {
            _timeScale = Time.timeScale;
            Time.timeScale = 0;
        }

        private void OnDisable()
        {
            Time.timeScale = _timeScale;
        }
    }
}