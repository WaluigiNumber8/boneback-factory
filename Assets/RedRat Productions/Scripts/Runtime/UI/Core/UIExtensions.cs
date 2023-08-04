using RedRats.Safety;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

namespace RedRats.UI.Core
{
    /// <summary>
    /// Contains helpful methods for dealing with UI.
    /// </summary>
    public static class UIExtensions
    {
        /// <summary>
        /// Updates sprites to an interactable. (Button, Dropdown, Input Field, etc.)
        /// </summary>
        /// <param name="interactable">The interactable itself.</param>
        /// <param name="interactableImage">Image of the interactable.</param>
        /// <param name="spriteData">The set of sprites to update with.</param>
        public static void ChangeInteractableSprites(Selectable interactable, Image interactableImage,
            InteractableSpriteInfo spriteData)
        {
            SafetyNet.EnsureIsNotNull(interactable, "Selectable to update.");
            SafetyNet.EnsureIsNotNull(interactableImage, "Selectable image");

            interactableImage.sprite = spriteData.normal;

            SpriteState ss = interactable.spriteState;
            ss.highlightedSprite = spriteData.highlighted;
            ss.pressedSprite = spriteData.pressed;
            ss.selectedSprite = spriteData.selected;
            ss.disabledSprite = spriteData.disabled;

            interactable.spriteState = ss;
        }

        /// <summary>
        /// Updates a font on a text.
        /// </summary>
        /// <param name="textAsset">The asset to update.</param>
        /// <param name="fontData">The new data to update with.</param>
        public static void ChangeFont(TextMeshProUGUI textAsset, FontInfo fontData)
        {
            SafetyNet.EnsureIsNotNull(textAsset, "Text whose font will be updated");

            textAsset.font = fontData.font;
            textAsset.color = fontData.color;
            textAsset.fontSize = fontData.size;
        }

        /// <summary>
        /// Returns a ratio modifer based on the current settings in the <see cref="Canvas"/>'s <see cref="CanvasScaler"/>.
        /// </summary>
        /// <param name="component">Component on the canvas we want the ratio for.</param>
        /// <returns>The ratio for both width & height.</returns>
        public static Vector2 GetScalerRatioFromParent(Component component)
        {
            CanvasScaler scaler = component.GetComponentInParent<CanvasScaler>();
            if (scaler == null) scaler = Object.FindObjectOfType<CanvasScaler>();
            return (scaler != null) ? new Vector2(Screen.width / scaler.referenceResolution.x, Screen.height / scaler.referenceResolution.y) : Vector2.one;
        }
    }
}