using BlockBlast.Scripts.Common.Extensions;
using TMPro;
using UnityEngine;

namespace TowerDefense.Scripts.Lua
{
    public class WindowError : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI header;
        [SerializeField] private TextMeshProUGUI label;

        public void OnPressButtonClose()
        {
            transform.Hide();
        }

        public void ShowError(string headerText, string message)
        {
            transform.Show();
            label.text = message;
            header.text = headerText;
        }
    }
}