using BlockBlast.Scripts.Common.Extensions;
using TowerDefense.Scripts.Towers;
using UnityEngine;

namespace TowerDefense.Scripts.Ui.UpLevel
{
    [CreateAssetMenu(menuName = "Create TowerDefense/TowerCollection", fileName = "TowerCollection", order = 0)]
    public class TowerSettingsCollection : ScriptableObject
    {
        [SerializeField] private AddTowerSettings artilleryTower;
        [SerializeField] private AddTowerSettings gunTower;
        [SerializeField] private AddTowerSettings flyTower;
        [SerializeField] private AddTowerSettings downSpeedTower;
        [SerializeField] private AddTowerSettings poisonTower;
        [SerializeField] private AddTowerSettings otherTower;
        
        public AddTowerSettings ArtilleryTower => artilleryTower;
        public AddTowerSettings GunTower => gunTower;
        public AddTowerSettings FlyTower => flyTower;
        public AddTowerSettings DownSpeedTower => downSpeedTower;
        public AddTowerSettings PoisonTower => poisonTower;
        
        public AddTowerSettings OtherTower => otherTower;

        public AddTowerSettings[] GetShuffleTowers()
        {
            AddTowerSettings[] settings = {ArtilleryTower, GunTower, FlyTower, DownSpeedTower, PoisonTower, OtherTower};
            settings.Shuffle();

            return settings;
        }
    }
}