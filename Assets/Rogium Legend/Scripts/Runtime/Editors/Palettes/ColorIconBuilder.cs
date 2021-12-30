using BoubakProductions.Core;
using UnityEngine;

namespace Rogium.Editors.Palettes
{
    /// <summary>
    /// Builds icons based on colors inputted colors.
    /// </summary>
    public class ColorIconBuilder
    {
        /// <summary>
        /// Builds a sprite based on a color array.
        /// </summary>
        /// <param name="colors">The colors to use for the sprite.</param>
        /// <returns>A sprite containing all the colors.</returns>
        public Sprite Build(Color[] colors)
        {
            int texSize = CalculateSize(colors.Length);
            Texture2D tex = BoubakBuilder.GenerateTexture(texSize, texSize);

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
            
            return BoubakBuilder.GenerateSprite(tex, 16);
        }

        private int CalculateSize(int arraySize)
        {
            return Mathf.FloorToInt(Mathf.Sqrt(arraySize));
        }
        
    }
}