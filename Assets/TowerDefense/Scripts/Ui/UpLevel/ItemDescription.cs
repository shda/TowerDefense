using NaughtyAttributes;
using UnityEngine;

namespace TowerDefense.Scripts.Ui.UpLevel
{
    public class ItemDescription : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        [SerializeField] [ResizableTextArea] private string description;
        
        public Sprite Icon => icon;
        public string Description => description;
    }
}