using System;
using RedRats.Systems.Themes;
using RedRats.UI.Core;
using Rogium.Core;
using Rogium.Editors.Core;
using UnityEngine;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Overseers everything happening in a sprite interactable property.
    /// </summary>
    public class InteractablePropertyAssetField : InteractablePropertyBase<IAsset>
    {
        [SerializeField] private AssetField assetField;

        private Action<IAsset> whenValueChange;

        public override void SetDisabled(bool isDisabled) => assetField.interactable = !isDisabled;

        /// <summary>
        /// Set the property title and state.
        /// </summary>
        /// <param name="titleText">Property Title.</param>
        /// <param name="type">The type of assets to store.</param>
        /// <param name="value">Value of property.</param>
        /// <param name="WhenChangeValue">The method that will run, when the AssetField changes value.</param>
        /// <param name="theme">The theme of the Asset Picker Window.</param>
        public void Construct(string titleText, AssetType type, IAsset value, Action<IAsset> WhenChangeValue, ThemeType theme = ThemeType.Current)
        {
            ConstructTitle(titleText);
            assetField.Construct(type, value);
            
            if (whenValueChange != null) assetField.OnValueChanged -= whenValueChange;
            
            whenValueChange = WhenChangeValue;
            assetField.OnValueChanged += whenValueChange;
        }

        /// <summary>
        /// Updates the UI elements
        /// </summary>
        /// <param name="fieldSet">A Set of Sprites for the button.</param>
        /// <param name="titleFont">The font of the title text.</param>
        /// <param name="valueFont">The font of the asset title.</param>
        public void UpdateTheme(InteractableSpriteInfo fieldSet, FontInfo titleFont, FontInfo valueFont)
        {
            UIExtensions.ChangeInteractableSprites(assetField, fieldSet);
            if (title != null) UIExtensions.ChangeFont(title, titleFont);
            if (assetField.Title != null) UIExtensions.ChangeFont(assetField.Title, valueFont);
        }

        public override IAsset PropertyValue { get => assetField.Value; }
    }
}
