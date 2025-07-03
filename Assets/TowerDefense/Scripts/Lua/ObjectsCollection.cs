using System;
using UnityEngine;

namespace TowerDefense.Scripts.Lua
{
    [CreateAssetMenu(menuName = "Create TowerDefense/ObjectsCollection", fileName = "ObjectsCollection", order = 0)]
    public class ObjectsCollection : ScriptableObject
    {
        [SerializeField] private GameObject[] objects;

        public GameObject[] Objects => objects;

        public GameObject GetObject(string nameObject)
        {
            foreach (var obj in objects)
                if (obj.name.Equals(nameObject, StringComparison.OrdinalIgnoreCase))
                    return obj;

            Debug.LogError($"Object with name {nameObject} not found in ObjectsCollection.");
            return null;
        }
    }
}