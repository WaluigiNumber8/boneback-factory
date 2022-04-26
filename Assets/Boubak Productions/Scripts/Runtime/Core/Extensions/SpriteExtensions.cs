using UnityEngine;

namespace BoubakProductions.Core
{
    /// <summary>
    /// Extension methods for the <see cref="Sprite"/>.
    /// </summary>
    public static class SpriteExtensions
    {
        /// <summary>
        /// Checks if a sprite is null and if it is, will return a clear sprite.
        /// </summary>
        /// <param name="sprite">The sprite to check.</param>
        /// <returns>Returns a sprite.</returns>
        public static Sprite ClearIfNull(this Sprite sprite)
        {
            if (sprite != null) return sprite;
            return BoubakBuilder.GenerateSprite(new Color(0, 0, 0, 1), 1, 1, 16);
        } 
    }
}