using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace TapTapDash.Scripts.Editor
{
    public class FixedGridAtlasCreator : EditorWindow
    {
        private const int CELL_SIZE = 128;
        private const int ATLAS_WIDTH = 1024;
        private const int MAX_ATLAS_HEIGHT = 8192;
        
        public static void CreateTexture(List<Texture2D> sprites , string savePathCustom = "")
        {
            // Рассчитываем необходимую высоту атласа
            int cellsPerRow = ATLAS_WIDTH / CELL_SIZE;
            int rowsNeeded = Mathf.CeilToInt((float)sprites.Count / cellsPerRow);
            int atlasHeight = Mathf.Min(rowsNeeded * CELL_SIZE, MAX_ATLAS_HEIGHT);

            if (rowsNeeded * CELL_SIZE > MAX_ATLAS_HEIGHT)
            {
                Debug.LogError($"Too many sprites! Maximum height is {MAX_ATLAS_HEIGHT}px");
                return;
            }

            // Создаем текстуру атласа
            Texture2D atlasTexture = new Texture2D(ATLAS_WIDTH, atlasHeight, TextureFormat.RGBA32, false);

            // Очищаем атлас (прозрачный фон)
            Color[] clearPixels = new Color[ATLAS_WIDTH * atlasHeight];
            for (int i = 0; i < clearPixels.Length; i++) clearPixels[i] = Color.clear;
            atlasTexture.SetPixels(clearPixels);

            // Располагаем спрайты в сетке
            for (int i = 0; i < sprites.Count; i++)
            {
                int row = i / cellsPerRow;
                int col = i % cellsPerRow;

                // Позиция ячейки (верхний левый угол)
                int x = col * CELL_SIZE;
                int y = atlasHeight - (row + 1) * CELL_SIZE; // Начинаем сверху

                // Копируем спрайт в ячейку (без центрирования)
                Texture2D sprite = sprites[i];
                int copyWidth = Mathf.Min(CELL_SIZE, sprite.width);
                int copyHeight = Mathf.Min(CELL_SIZE, sprite.height);
            
                atlasTexture.SetPixels(x, y, copyWidth, copyHeight, sprite.GetPixels(0, 0, copyWidth, copyHeight));
            }

            atlasTexture.Apply();


            if (!string.IsNullOrEmpty(savePathCustom))
            {
                File.WriteAllBytes(savePathCustom, atlasTexture.EncodeToPNG());
 
                AssetDatabase.Refresh();
                Debug.Log($"Grid atlas saved to: {savePathCustom}");
                //Debug.Log($"Atlas size: {width}x{height}, Cells: {cellsPerRow}x{cellsPerRow}");
            }
            else
            {
                // Сохраняем PNG
                string savePath = EditorUtility.SaveFilePanel("Save Grid Atlas PNG", "Assets", "GridAtlas.png", "png");
                if (!string.IsNullOrEmpty(savePath))
                {
                    File.WriteAllBytes(savePath, atlasTexture.EncodeToPNG());
     
                    AssetDatabase.Refresh();
                    Debug.Log($"Grid atlas saved to: {savePath}");
                   // Debug.Log($"Atlas size: {width}x{height}, Cells: {cellsPerRow}x{cellsPerRow}");
                }
            }

            
        }
        // Создаем читаемую текстуру из спрайта
        private static Texture2D GetReadableTexture(Sprite sprite)
        {
            Texture2D tex = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, TextureFormat.RGBA32, false);
            Color[] pixels = sprite.texture.GetPixels(
                (int)sprite.rect.x, 
                (int)sprite.rect.y, 
                (int)sprite.rect.width, 
                (int)sprite.rect.height);
            tex.SetPixels(pixels);
            tex.Apply();
            return tex;
        }

        // Создаем читаемую текстуру из Texture2D
        private static Texture2D GetReadableTexture(Texture2D source)
        {
            RenderTexture rt = RenderTexture.GetTemporary(
                source.width,
                source.height,
                0,
                RenderTextureFormat.Default,
                RenderTextureReadWrite.Linear);

            Graphics.Blit(source, rt);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = rt;

            Texture2D readableTex = new Texture2D(source.width, source.height);
            readableTex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            readableTex.Apply();

            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(rt);
            return readableTex;
        }
    }
}