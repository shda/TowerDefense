using TowerDefense.Scripts.Towers;
using UnityEngine;

namespace TowerDefense.Scripts.Ui.UpLevel
{
    public class TowerCollection : MonoBehaviour
    {
        [SerializeField] private AddTowerSettings artilleryTower;
        [SerializeField] private AddTowerSettings gunTower;
        [SerializeField] private AddTowerSettings flyTower;
        [SerializeField] private AddTowerSettings downSpeedTower;
        [SerializeField] private AddTowerSettings poisonTower;
        
        public AddTowerSettings ArtilleryTower => artilleryTower;
        public AddTowerSettings GunTower => gunTower;
        public AddTowerSettings FlyTower => flyTower;
        public AddTowerSettings DownSpeedTower => downSpeedTower;
        public AddTowerSettings PoisonTower => poisonTower;
    }
}