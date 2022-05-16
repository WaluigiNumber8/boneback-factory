using System;
using RedRats.UI;
using RedRats.UI.Core;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Overseers everything happening in a sprite interactable property.
    /// </summary>
    public class InteractablePropertyAssetField : InteractablePropertyBase
    {
        [SerializeField] private Image icon;
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
        public void Construct(string titleText, AssetType type, IAsset value, Action<IAsset> WhenChangeValue, ThemeType theme = ThemeType.NoTheme)
        {
            asset = value;

            title.text = titleText;
            title.gameObject.SetActive((titleText != ""));
            if (ui.emptySpace != null) ui.emptySpace.SetActive((titleText != ""));
            
            icon.sprite = asset.Icon;
            
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
        public void UpdateTheme(InteractableInfo fieldSpriteSet, FontInfo titleFont)
        {
            UIExtensions.ChangeInteractableSprites(assetField, ui.borderImage, fieldSpriteSet);
            UIExtensions.ChangeFont(title, titleFont);
        }

        public Sprite Icon { get => icon.sprite; }

        [Serializable]
        public struct UIInfo
        {
            public Image borderImage;
            public GameObject emptySpace;
        }
    }
}