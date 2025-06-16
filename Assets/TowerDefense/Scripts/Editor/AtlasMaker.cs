using System.Collections.Generic;
using System.IO;
using TriInspector;
using UnityEditor;
using UnityEngine;

namespace TapTapDash.Scripts.Editor
{
    [CreateAssetMenu(menuName = "Create AtlasMaker", fileName = "AtlasMaker", order = 0)]
    public class AtlasMaker : ScriptableObject
    {
        [SerializeField] private DefaultAsset folder;
        [ReadOnly][SerializeField] private List<Texture2D> sprites;
        [SerializeField] private Texture2D resultTexture;


        [Button]
        public void AddNewFiles()
        {
            var path = AssetDatabase.GetAssetPath(folder);
            var  files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                if (file.EndsWith(".png"))
                {
                   var asset = AssetDatabase.LoadAssetAtPath<Texture2D>(file);

                   if (!sprites.Contains(asset))
                   {
                       sprites.Add(asset);
                       Debug.Log($"Add new sprite {file}");
                   }
                }
            }
        }

        [Button]
        public void UpdateAtlas()
        {
            FixedGridAtlasCreator.CreateTexture(sprites , AssetDatabase.GetAssetPath(resultTexture));
        }
    }
}