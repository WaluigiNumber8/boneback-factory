using RedRats.Safety;
using UnityEngine.UI;
using TMPro;

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
        public static void ChangeInteractableSprites(Selectable interactable, Image interactableImage, InteractableSpriteInfo spriteData)
        {
            SafetyNet.EnsureIsNotNull(interactable, "Button to update.");
            SafetyNet.EnsureIsNotNull(interactableImage, "Button image");
            
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
    }
}