using System;
using RedRats.Systems.Audio;
using RedRats.UI.Core;
using RedRats.UI.ModalWindows;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Sounds;
using Rogium.Systems.Audio;
using Rogium.Systems.ThemeSystem;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Rogium.UserInterface.ModalWindows
{
    /// <summary>
    /// Represents a modal window that allows the user to pick a sound from the sound library.
    /// </summary>
    public class SoundPickerWindow : ModalWindowBase
    {
        [SerializeField] private UIInfo ui;
        
        [Header("Interactables")]
        [SerializeField] private InteractablePropertyAssetField soundField;
        [SerializeField] private InteractablePropertySlider volumeSlider;
        [SerializeField] private InteractablePropertySlider pitchSlider;
        [SerializeField] private InteractablePropertySlider chanceToPlaySlider;
        [SerializeField] private InteractablePropertyToggle randomPitchToggle;
        [Header("Audio")]
        [SerializeField] private Button playSoundButton;
        [SerializeField] private AudioMixerGroup mixerGroup;
        
        private InternalLibraryOverseer lib;
        
        private SoundAsset currentSoundAsset;
        private AssetData currentData;
        private Action<AssetData> whenAnyValueChanged;
        private Action<SoundAsset> whenSoundChanged;

        protected override void Awake()
        {
            base.Awake();
            lib = InternalLibraryOverseer.GetInstance();
            playSoundButton.onClick.AddListener(() => AudioSystemRogium.GetInstance().PlaySound(currentData, mixerGroup, new AudioSourceSettingsInfo(0, false, false, false)));
        }

        /// <summary>
        /// Prepares the window for editing an asset.
        /// </summary>
        /// <param name="whenSoundChanged">Method that runs when the currently edited sound is changed.</param>
        /// <param name="whenAnyValueChanged">Method that runs when any property in the window is updated.</param>
        /// <param name="value">Which data to load up into the window.</param>
        public void Construct(Action<SoundAsset> whenSoundChanged, Action<AssetData> whenAnyValueChanged, AssetData value)
        {
            currentData = value;
            currentSoundAsset = (!currentData.IsEmpty()) ? lib.GetSoundByID(currentData.ID) : null;
            
            soundField.Construct("", AssetType.Sound, currentSoundAsset, WhenSoundFieldUpdated, WhenSoundFieldEmptied);
            
            this.whenAnyValueChanged = whenAnyValueChanged;
            this.whenSoundChanged = whenSoundChanged;
            UpdateProperties(currentData);
        }

        public void UpdateTheme(Sprite windowBackgroundSprite, Sprite propertiesBackgroundSprite, InteractableSpriteInfo soundFieldSet, 
                                InteractableSpriteInfo buttonSet, Sprite playButtonSprite)
        {   
            ThemeUpdaterRogium.UpdateAssetFieldText(soundField);
            ThemeUpdaterRogium.UpdateSlider(volumeSlider);
            ThemeUpdaterRogium.UpdateSlider(pitchSlider);
            ThemeUpdaterRogium.UpdateSlider(chanceToPlaySlider);
            ThemeUpdaterRogium.UpdateToggle(randomPitchToggle);
            UIExtensions.ChangeInteractableSprites(playSoundButton, playSoundButton.image, buttonSet);
            UIExtensions.ChangeInteractableSprites(CloseButton, CloseButton.image, soundFieldSet);
            generalUI.windowArea.sprite = windowBackgroundSprite;
            ui.propertiesBackground.sprite = propertiesBackgroundSprite;
            ui.playSoundButtonIcon.sprite = playButtonSprite;
        }

        protected override void UpdateTheme() => ThemeUpdaterRogium.UpdateSoundPickerWindow(this);

        /// <summary>
        /// Update settings on all interactable properties on this asset.
        /// </summary>
        /// <param name="data">The data to update with.</param>
        private void UpdateProperties(IParameterAsset data)
        {
            volumeSlider.Construct("Volume", 0.01f, 1f, data.Parameters.floatValue1, WhenVolumeChanged);
            pitchSlider.Construct("Pitch", 0.01f, 2f, data.Parameters.floatValue2, WhenPitchChanged);
            randomPitchToggle.Construct("Randomize Pitch", data.Parameters.boolValue1, WhenRandomPitchChanged);
            chanceToPlaySlider.Construct("Play Chance", 0.01f, 1f, data.Parameters.floatValue3, WhenChanceToPlayChanged);
        }

        private void WhenSoundFieldUpdated(IAsset asset)
        {
            currentData = AssetDataBuilder.ForSound(asset);
            currentSoundAsset = asset as SoundAsset;
            WhenSoundFieldChanged();
        }
        
        private void WhenSoundFieldEmptied()
        {
            currentData = new AssetData();
            currentSoundAsset = null;
            WhenSoundFieldChanged();
        }

        private void WhenSoundFieldChanged()
        {
            //Set old values to new sound
            currentData.UpdateFloatValue1(volumeSlider.PropertyValue);
            currentData.UpdateFloatValue2(pitchSlider.PropertyValue);
            currentData.UpdateBoolValue1(randomPitchToggle.PropertyValue);
            currentData.UpdateFloatValue3(chanceToPlaySlider.PropertyValue);
            
            UpdateProperties(currentData);
            UpdateOriginalValue();
            
            whenSoundChanged?.Invoke(currentSoundAsset);
        }
        
        private void WhenVolumeChanged(float newValue)
        {
            currentData.UpdateFloatValue1(newValue);
            UpdateOriginalValue();
        }
        
        private void WhenPitchChanged(float newValue)
        {
            currentData.UpdateFloatValue2(newValue);
            UpdateOriginalValue();
        }
        
        private void WhenRandomPitchChanged(bool newValue)
        {
            currentData.UpdateBoolValue1(newValue);
            UpdateOriginalValue();
        }

        private void WhenChanceToPlayChanged(float newValue)
        {
            currentData.UpdateFloatValue3(newValue);
            UpdateOriginalValue();
        }
        
        /// <summary>
        /// Call to update the original value.
        /// </summary>
        private void UpdateOriginalValue() => whenAnyValueChanged?.Invoke(currentData);

        [Serializable]
        public struct UIInfo
        {
            public Image propertiesBackground;
            public Image playSoundButtonIcon;
        }
    }
}