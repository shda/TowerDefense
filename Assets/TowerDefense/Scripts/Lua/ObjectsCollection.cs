using System;
using UnityEngine;

namespace TowerDefense.Scripts.Lua
{
    public class ObjectsCollection<T> : ScriptableObject where T : Component
    {
        [SerializeField] private T[] objects;

        public T[] Objects => objects;

        public T GetObject(string nameObject)
        {
            foreach (var obj in objects)
            {
                if (obj.name.Equals(nameObject, StringComparison.OrdinalIgnoreCase))
                {
                    return obj;
                }
            }

            Debug.LogError($"Object with name {nameObject} not found in ObjectsCollection.");
            return null;
        }
    }
}