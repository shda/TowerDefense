using UnityEngine;

namespace TowerDefense.Scripts.Lua
{
    public class LuaCodeEditor : MonoBehaviour
    {
        [SerializeField] private Font font;

        public string text;
        private Vector2 scrollPosition;

        private void OnGUI()
        {
            var width = Screen.width - 10;
            var height = Screen.height - 100;

            scrollPosition = GUILayout.BeginScrollView(scrollPosition,
                GUILayout.Width(width), GUILayout.Height(height));

            var style = new GUIStyle(GUI.skin.textField);
            style.fontSize = 20;
            style.font = font;

            text = GUILayout.TextArea(
                text,
                style,
                GUILayout.Width(width - 20),
                GUILayout.ExpandHeight(true)
            );

            // Заканчиваем область скролла
            GUILayout.EndScrollView();
        }
    }
}