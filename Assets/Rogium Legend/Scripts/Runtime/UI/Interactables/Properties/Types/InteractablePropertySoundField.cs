using System;
using RedRats.UI.Core;
using Rogium.Editors.Core;
using UnityEngine;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Represents a sound picker property.
    /// </summary>
    public class InteractablePropertySoundField : InteractablePropertyBase<AssetData>
    {
        [SerializeField] private SoundField soundField;

        private Action<AssetData> whenSoundEdited;

        private void Awake() => soundField.OnValueChanged += whenSoundEdited;

        public void Construct(string titleText, AssetData value, Action<AssetData> whenSoundEdited, bool canBeEmpty = false)
        {
            ConstructTitle(titleText);
            soundField.Construct(value, canBeEmpty);
            this.whenSoundEdited = whenSoundEdited;
        }
        
        public override void SetDisabled(bool isDisabled) => soundField.SetActive(!isDisabled);

        public void UpdateTheme(InteractableSpriteInfo openWindowButtonSet, InteractableSpriteInfo buttonSet, 
                                Sprite playButtonIcon, FontInfo titleFont, FontInfo valueFont)
        {
            UIExtensions.ChangeInteractableSprites(soundField.UI.showWindowButton, openWindowButtonSet);
            UIExtensions.ChangeInteractableSprites(soundField.UI.playButton, buttonSet);
            UIExtensions.ChangeFont(title, titleFont);
            UIExtensions.ChangeFont(soundField.UI.soundTitle, valueFont);
            soundField.UI.playSoundButtonIcon.sprite = playButtonIcon;
        }

        public override AssetData PropertyValue { get => soundField.Value; }
    }
}