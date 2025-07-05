using Lua.Unity;
using UnityEngine;

namespace TowerDefense.Scripts.Lua
{
    [CreateAssetMenu(menuName = "Create TowerDefense/LuaScriptsCollection", fileName = "LuaScriptsCollection", order = 0)]
    public class LuaScriptsCollection : ScriptableObject
    {
        [SerializeField] private LuaAsset[] luaScripts;
        public LuaAsset[] LuaScripts => luaScripts;
    }
}