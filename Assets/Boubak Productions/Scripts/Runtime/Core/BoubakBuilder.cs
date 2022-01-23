using UnityEngine;

namespace BoubakProductions.Core
{
    /// <summary>
    /// Builds different types of Types.
    /// </summary>
    public static class BoubakBuilder
    {
        #region Collections

        /// <summary>
        /// Generates a color array, with a predefined color in each slot.
        /// </summary>
        /// <param name="size">The size of the array.</param>
        /// <param name="defaultColor">The color for each slot.</param>
        /// <returns>An array filled with a predefined color.</returns>
        public static Color[] GenerateColorArray(int size, Color defaultColor)
        {
            Color[] array = new Color[size];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = defaultColor;
            }
            return array;
        }

        #endregion

        #region Sprites & Textures

        /// <summary>
        /// Generates an empty 2D texture.
        /// </summary>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="filterMode">The filter mode for the texture.</param>
        /// <returns>A new <see cref="Texture2D"/>.</returns>
        public static Texture2D GenerateTexture(int width, int height, FilterMode filterMode = FilterMode.Point)
        {
            Texture2D tex = new Texture2D(width, height);
            tex.filterMode = filterMode;

            return tex;
        }

        /// <summary>
        /// Generates a 2D texture of a single color.
        /// </summary>
        /// <param name="color">The color of the texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="filterMode">The filter mode for the texture.</param>
        /// <returns>A new <see cref="Texture2D"/>Generated texture.</returns>
        public static Texture2D GenerateTexture(Color color, int width, int height, FilterMode filterMode = FilterMode.Point)
        {
            Texture2D tex = new Texture2D(width, height);
            tex.filterMode = filterMode;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    tex.SetPixel(x, y, color);
                }
            }
            tex.Apply();
            return tex;
        }
        
        /// <summary>
        /// Generates a new empty sprite.
        /// </summary>
        /// <param name="width">The width of the sprite.</param>
        /// <param name="height">The height of the sprite.</param>
        /// <param name="pixelsPerUnit">How many pixels will be stored in 1 Unity Unit.</param>
        /// <returns>A newly created sprite.</returns>
        public static Sprite GenerateSprite(int width, int height, int pixelsPerUnit)
        {
            return GenerateSprite(width, height, new Vector2(0.5f, 0.5f), pixelsPerUnit);
        }
        /// <summary>
        /// Generates a new empty sprite.
        /// </summary>
        /// <param name="tex">The texture to use for the sprite.</param>
        /// <param name="pixelsPerUnit">How many pixels will be stored in 1 Unity Unit.</param>
        /// <returns>A newly created sprite.</returns>
        public static Sprite GenerateSprite(Texture2D tex, int pixelsPerUnit)
        {
            return GenerateSprite(tex, new Vector2(0.5f, 0.5f), pixelsPerUnit);
        }

        /// <summary>
        /// Generates a new empty sprite.
        /// </summary>
        /// <param name="color">The color of the sprite.</param>
        /// <param name="width">The width of the sprite.</param>
        /// <param name="height">The height of the sprite.</param>
        /// <param name="pixelsPerUnit">How many pixels will be stored in 1 Unity Unit.</param>
        /// <returns>A newly created sprite.</returns>
        public static Sprite GenerateSprite(Color color, int width, int height, int pixelsPerUnit)
        {
            Texture2D tex = GenerateTexture(color, width, height);
            return GenerateSprite(tex, new Vector2(0.5f, 0.5f), pixelsPerUnit);
        }
        
        /// <summary>
        /// Generates a new empty sprite.
        /// </summary>
        /// <param name="color">The color of the sprite.</param>
        /// <param name="width">The width of the sprite.</param>
        /// <param name="height">The height of the sprite.</param>
        /// <param name="pivot">Sprite's pivot.</param>
        /// <param name="pixelsPerUnit">How many pixels will be stored in 1 Unity Unit.</param>
        /// <returns>A newly created sprite.</returns>
        public static Sprite GenerateSprite(Color color, int width, int height, Vector2 pivot, int pixelsPerUnit)
        {
            Texture2D tex = GenerateTexture(color, width, height);
            return GenerateSprite(tex, pivot, pixelsPerUnit);
        }
        
        /// <summary>
        /// Generates a new empty sprite.
        /// </summary>
        /// <param name="width">The width of the sprite.</param>
        /// <param name="height">The height of the sprite.</param>
        /// <param name="pivot">Sprite's pivot.</param>
        /// <param name="pixelsPerUnit">How many pixels will be stored in 1 Unity Unit.</param>
        /// <returns>A newly created sprite.</returns>
        public static Sprite GenerateSprite(int width, int height, Vector2 pivot, int pixelsPerUnit)
        {
            Texture2D tex = GenerateTexture(width, height);
            return GenerateSprite(tex, pivot, pixelsPerUnit);
        }

        /// <summary>
        /// Generates a new empty sprite.
        /// </summary>
        /// <param name="tex">The texture to use for the sprite.</param>
        /// <param name="pivot">Sprite's pivot.</param>
        /// <param name="pixelsPerUnit">How many pixels will be stored in 1 Unity Unit.</param>
        /// <returns>A newly created sprite.</returns>
        public static Sprite GenerateSprite(Texture2D tex, Vector2 pivot, int pixelsPerUnit)
        {
            tex.Apply();
            return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), pivot, pixelsPerUnit);
        }

        #endregion
        
    }
}