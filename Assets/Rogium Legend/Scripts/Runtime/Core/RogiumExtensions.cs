using RedRats.Core;
using Rogium.Editors.Core;
using TMPro;
using UnityEngine.UI;

namespace Rogium.Core
{
    /// <summary>
    /// Extends various classes with functionality.
    /// </summary>
    public static class RogiumExtensions
    {
        /// <summary>
        /// Sets an Asset's icon to an <see cref="Image"/>. If the asset is null, the image will be hidden.
        /// </summary>
        /// <param name="img">The <see cref="Image"/> to set the icon to.</param>
        /// <param name="asset">The asset whose icon will be used as the sprite.</param>
        /// <param name="alpha">The alpha to set.</param>
        public static void SetSpriteFromAsset(this Image img, IAsset asset, float alpha = 1)
        {
            if (img == null) return;
            img.SetSprite(asset?.Icon, alpha);
        }

        /// <summary>
        /// Sets Asset's title as the text of the <see cref="TMP_Text"/>. If the asset is null, it will be set to a default value.
        /// </summary>
        /// <param name="text">The <see cref="TMP_Text"/> to set.</param>
        /// <param name="asset">The asset whose title will be used as text.</param>
        /// <param name="noneValue">Text that is set when <see cref="asset"/> is empty.</param>
        public static void SetTextValueFromAssetTitle(this TMP_Text text, IAsset asset, string noneValue = "None")
        {
            if (text == null) return;
            text.SetTextValue(asset?.Title, noneValue);
        }
    }
}