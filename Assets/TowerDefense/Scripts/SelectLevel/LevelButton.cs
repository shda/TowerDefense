using System;
using Lua.Unity;
using TMPro;
using UnityEngine;

namespace TowerDefense.Scripts.SelectLevel
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;
        private LuaAsset luaLevel;
        private int LevelNumber { get; set; }
        
        public Action OnClick;

        public void OnClickButton()
        {
            OnClick?.Invoke();
        }
        
        public void SetLevel(int index , string labelText)
        {
            LevelNumber = index;
            label.text = labelText;
        }
    }
}