using NaughtyAttributes;
using UnityEngine;

namespace TowerDefense.Scripts.Ui.UpLevel
{
    public class UpLevelSettings : ScriptableObject
    {
        [ShowAssetPreview]
        [SerializeField] private Sprite icon;
        [SerializeField] [ResizableTextArea] private string description;
        
        public virtual Sprite Icon => icon;
        public virtual string Description => description;
    }
}