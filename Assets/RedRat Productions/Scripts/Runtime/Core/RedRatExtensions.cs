using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RedRats.Core
{
    /// <summary>
    /// Extend various classes with functionality.
    /// </summary>
    public static class RedRatExtensions
    {
        /// <summary>
        /// Sets a sprite to an <see cref="Image"/>. If the sprite is null, the image will be hidden.
        /// </summary>
        /// <param name="img">The <see cref="Image"/> to set the sprite to.</param>
        /// <param name="sprite">The sprite to set.</param>
        /// <param name="alpha">The alpha to set.</param>
        public static void SetSprite(this Image img, Sprite sprite, float alpha = 1)
        {
            if (img == null) return;
            if (sprite == null)
            {
                img.sprite = null;
                img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
                return;
            }
            img.sprite = sprite;
            img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
        }

        /// <summary>
        /// Sets a text to a <see cref="TMP_Text"/>. If the text is null or empty, it will be set to a default value.
        /// </summary>
        /// <param name="text">The <see cref="TMP_Text"/> to set.</param>
        /// <param name="value">The value of the text.</param>
        /// <param name="noneValue">Text that is set when <see cref="value"/> is empty.</param>
        public static void SetTextValue(this TMP_Text text, string value, string noneValue = "None")
        {
            if (text == null) return;
            if (string.IsNullOrEmpty(value))
            {
                text.text = noneValue;
                return;
            }
            text.text = value;
        }
    }
}