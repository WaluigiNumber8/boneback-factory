using System;
using RedRats.Systems.Audio;
using RedRats.UI.Core;
using Rogium.Editors.Core;
using Rogium.Systems.Audio;
using Rogium.UserInterface.ModalWindows;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Represents a sound picker property.
    /// </summary>
    public class InteractablePropertySoundField : InteractablePropertyBase<AssetData>
    {
        [SerializeField] private Button showWindowButton;
        [SerializeField] private Button playButton;
        [SerializeField] private AudioMixerGroup mixerGroup;
        [SerializeField] private UIInfo ui;
        
        private Action<AssetData> onChangeValue;
        private AssetData currentData;

        private void Awake()
        {
            showWindowButton.onClick.AddListener(() => ModalWindowBuilder.GetInstance().OpenSoundPickerWindow(RefreshOnChange, onChangeValue, currentData));
            playButton.onClick.AddListener(() => AudioSystemRogium.GetInstance().PlaySound(currentData, mixerGroup, new AudioSourceSettingsInfo(0, false, false, false)));
        }

        public void Construct(string titleText, AssetData value, Action<AssetData> whenSoundEdited)
        {
            currentData = value;
            ConstructTitle(titleText);
            onChangeValue = whenSoundEdited;
            RefreshOnChange(InternalLibraryOverseer.GetInstance().GetSoundByID(value.ID));
        }
        
        public override void SetDisabled(bool isDisabled)
        {
            showWindowButton.interactable = !isDisabled;
            playButton.interactable = !isDisabled;
        }

        public override AssetData PropertyValue { get => currentData; }

        public void UpdateTheme(InteractableSpriteInfo openWindowButtonSet, InteractableSpriteInfo buttonSet, 
                                Sprite playButtonIcon, FontInfo titleFont, FontInfo valueFont)
        {
            UIExtensions.ChangeInteractableSprites(showWindowButton, openWindowButtonSet);
            UIExtensions.ChangeInteractableSprites(playButton, buttonSet);
            UIExtensions.ChangeFont(title, titleFont);
            UIExtensions.ChangeFont(ui.soundTitle, valueFont);
            ui.playSoundButtonIcon.sprite = playButtonIcon;
        }
        
        private void RefreshOnChange(IAsset newAsset)
        {
            ui.soundTitle.text = newAsset.Title;
            ui.soundIcon.sprite = newAsset.Icon;
        }

        [Serializable]
        public struct UIInfo
        {
            public TextMeshProUGUI soundTitle;
            public Image soundIcon;
            public Image playSoundButtonIcon;
        }
    }
}