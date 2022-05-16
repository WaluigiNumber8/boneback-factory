using RedRats.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Systems.IconBuilders
{
    /// <summary>
    /// Builds icons based on colors inputted colors.
    /// </summary>
    public class IconBuilder
    {
        /// <summary>
        /// Builds a sprite based on a color array.
        /// </summary>
        /// <param name="colors">The colors to use for the sprite.</param>
        /// <returns>A sprite containing all the colors.</returns>
        public Sprite BuildFromArray(Color[] colors)
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
        public Sprite BuildFromGrid(ObjectGrid<int> grid, Color[] colors)
        {
            Texture2D tex = RedRatBuilder.GenerateTexture(grid.Width, grid.Height);
            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    int posValue = grid.GetValue(x, y);

                    //When color ID is bigger than the color array.
                    if (posValue >= colors.Length - 1)
                    {
                        tex.SetPixel(x, y, EditorDefaults.MissingColor);
                        continue;
                    }
                    //if color ID is empty = transparent.
                    if (posValue < 0)
                    {
                        tex.SetPixel(x, y, EditorDefaults.NoColor);
                        continue;
                    }
                    tex.SetPixel(x, y, colors[posValue]);
                }
            }
            return RedRatBuilder.GenerateSprite(tex, EditorDefaults.PixelsPerUnit);
        }

        private int CalculateSize(int arraySize)
        {
            return Mathf.FloorToInt(Mathf.Sqrt(arraySize));
        }
        
    }
}