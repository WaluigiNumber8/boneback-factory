using System;
using RedRats.UI;
using RedRats.UI.Core;
using Rogium.Core;
using Rogium.Editors.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Overseers everything happening in a sprite interactable property.
    /// </summary>
    public class InteractablePropertyAssetField : InteractablePropertyBase
    {
        [SerializeField] private AssetField assetField;
        [SerializeField] private UIInfo ui;

        private IAsset asset;
        private Action<IAsset> lastMethod;

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
            asset = value;

            ConstructTitle(titleText);
            ui.icon.sprite = asset.Icon;
            if (ui.title != null) ui.title.text = asset.Title;
            
            assetField.SetType(type);
            assetField.SetTheme(theme);
            
            if (lastMethod != null) assetField.OnValueChanged -= lastMethod;
            
            lastMethod = WhenChangeValue;
            assetField.OnValueChanged += lastMethod;
        }

        /// <summary>
        /// Updates the UI elements
        /// </summary>
        /// <param name="fieldSpriteSet">A Set of Sprites for the button.</param>
        public void UpdateTheme(InteractableSpriteInfo fieldSpriteSet, FontInfo titleFont)
        {
            UIExtensions.ChangeInteractableSprites(assetField, ui.borderImage, fieldSpriteSet);
            if (title != null) UIExtensions.ChangeFont(title, titleFont);
        }

        public Sprite Icon { get => ui.icon.sprite; }

        [Serializable]
        public struct UIInfo
        {
            public Image icon;
            public Image borderImage;
            public TextMeshProUGUI title;
        }
    }
}
