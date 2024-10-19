using System.Collections.Generic;
using RedRats.Core;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using Rogium.Editors.Rooms;
using Rogium.Editors.Tiles;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Systems.IconBuilders
{
    /// <summary>
    /// Builds icons based on inputted colors.
    /// </summary>
    public static class IconBuilder
    {
        /// <summary>
        /// Builds a sprite based on a color array.
        /// </summary>
        /// <param name="colors">The colors to use for the sprite.</param>
        /// <returns>A sprite containing all the colors.</returns>
        public static Sprite DrawFromArray(Color[] colors)
        {
            int texSize = CalculateSize(colors.Length);
            Texture2D tex = RedRatBuilder.GenerateTexture(texSize, texSize);

            int counter = 0;
            for (int y = texSize-1; y >= 0; y--)
            {
                for (int x = 0; x < texSize; x++)
                {
                    if (counter >= colors.Length)
                    {
                        tex.SetPixel(x, y, Color.black);
                        continue;
                    }

                    tex.SetPixel(x, y, colors[counter]);
                    counter++;
                }
            }
            tex.Apply();
            
            return RedRatBuilder.GenerateSprite(tex, 16);
        }

        /// <summary>
        /// Turns a grid of color data into a sprite.
        /// </summary>
        /// <param name="grid">The data to build from.</param>
        /// <param name="colors">The colors to use.</param>
        /// <returns>A Sprite.</returns>
        public static Sprite DrawFromGrid(ObjectGrid<int> grid, Color[] colors)
        {
            Texture2D tex = RedRatBuilder.GenerateTexture(grid.Width, grid.Height);
            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    int posValue = grid.GetAt(x, y);

                    //When color ID is bigger than the color array.
                    if (posValue > colors.Length - 1)
                    {
                        tex.SetPixel(x, y, EditorDefaults.Instance.MissingColor);
                        continue;
                    }
                    //if color ID is empty = transparent.
                    if (posValue < 0)
                    {
                        tex.SetPixel(x, y, EditorDefaults.Instance.NoColor);
                        continue;
                    }
                    tex.SetPixel(x, y, colors[posValue]);
                }
            }
            return RedRatBuilder.GenerateSprite(tex, EditorDefaults.Instance.PixelsPerUnit);
        }

        public static Sprite DrawLowResIconFrom<T>(ObjectGrid<AssetData> dataGrid, IDictionary<string, T> assets, Sprite backgroundSprite = null) where T : IAsset
        {
            Color previousColor = Color.clear;
            string previousID = string.Empty;
            Texture2D tex = (backgroundSprite != null) ? backgroundSprite.texture : RedRatBuilder.GenerateTexture(dataGrid.Width, dataGrid.Height);
            for (int x = 0; x < tex.width; x++)
            {
                for (int y = 0; y < tex.height; y++)
                {
                    AssetData value = dataGrid.GetAt(x, y);
                    if (value.IsEmpty()) continue;
                    if (value.ID == previousID)
                    {
                        tex.SetPixel(x, y, previousColor);
                        continue;
                    }
                    Color color = assets[value.ID].Icon.texture.GetPixel(8, 8);
                    tex.SetPixel(x, y, color);
                    previousColor = color;
                    previousID = value.ID;
                }
            }
            tex.Apply();
            return RedRatBuilder.GenerateSprite(tex, EditorDefaults.Instance.SpriteSize);
        }
        
        private static int CalculateSize(int arraySize)
        {
            return Mathf.FloorToInt(Mathf.Sqrt(arraySize));
        }
        
    }
}