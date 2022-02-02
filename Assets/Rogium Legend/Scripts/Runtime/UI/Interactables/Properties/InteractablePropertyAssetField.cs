using System;
using BoubakProductions.UI.Core;
using Rogium.Core;
using Rogium.Editors.Core;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Overseers everything happening in a sprite interactable property.
    /// </summary>
    public class InteractablePropertyAssetField : MonoBehaviour, IInteractableProperty
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Image icon;
        [SerializeField] private AssetField assetField;
        [SerializeField] private UIInfo ui;

        private AssetBase asset;
        private Action<AssetBase> lastMethod;

        /// <summary>
        /// Set the property title and state.
        /// </summary>
        /// <param name="title">Property Title.</param>
        /// <param name="value">Value of property.</param>
        /// <param name="WhenChangeValue">The method that will run, when the AssetField changes value.</param>
        public void Construct(string title, AssetType type, AssetBase value, Action<AssetBase> WhenChangeValue)
        {
            this.asset = value;
            
            this.title.text = title;
            this.icon.sprite = asset.Icon;
            
            this.assetField.SetType(type);
            if (lastMethod != null) 
                this.assetField.OnValueChanged -= lastMethod;
            
            this.lastMethod = WhenChangeValue;
            this.assetField.OnValueChanged += lastMethod;
        }

        /// <summary>
        /// Updates the UI elements
        /// </summary>
        /// <param name="fieldSpriteSet">A Set of Sprites for the button.</param>
        public void UpdateTheme(InteractableInfo fieldSpriteSet)
        {
            UIExtensions.ChangeInteractableSprites(assetField, ui.borderImage, fieldSpriteSet);
        }

        public string Title { get => title.text; }
        public Sprite Icon { get => icon.sprite; }

        [Serializable]
        public struct UIInfo
        {
            public Image borderImage;
        }
    }
}