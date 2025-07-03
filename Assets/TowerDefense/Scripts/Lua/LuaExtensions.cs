using System;
using System.Linq;
using Lua;

namespace TowerDefense.Scripts.Lua
{
    public static class LuaExtensions
    {
        public static T[] ToArray<T>(this LuaTable table)
        {
            if (table == null)
            {
                return Array.Empty<T>();
            }

            return table.Select(x => x.Value.Read<T>()).ToArray();
        }
    }
}