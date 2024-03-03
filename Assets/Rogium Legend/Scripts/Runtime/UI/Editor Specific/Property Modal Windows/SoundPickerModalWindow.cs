using System;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Sounds;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.UserInterface.Editors.PropertyModalWindows
{
    /// <summary>
    /// Represents a modal window that allows the user to pick a sound from the sound library.
    /// </summary>
    public class SoundPickerModalWindow : PropertyModalWindowBase
    {
        public event Action<IAsset> OnSoundSelected;
        
        [SerializeField] private InteractablePropertyAssetField soundField;
        [SerializeField] private InteractablePropertySlider volumeSlider;
        [SerializeField] private InteractablePropertySlider pitchSlider;
        [SerializeField] private InteractablePropertyToggle randomPitchToggle;
        
        private InternalLibraryOverseer lib;
        private AssetData currentAssetData;
        private Action<AssetData> onChangeValue;

        protected override void Awake()
        {
            base.Awake();
            lib = InternalLibraryOverseer.GetInstance();
        }

        /// <summary>
        /// Prepares the window for editing an asset.
        /// </summary>
        /// <param name="onChangeValue">Method that runs when anything in the window is updated.</param>
        /// <param name="value">Which value to load up into the window.</param>
        public void Construct(Action<AssetData> onChangeValue, AssetData value)
        {
            currentAssetData = value;
            SoundAsset asset = lib.GetSoundByID(value.ID);
            
            soundField.Construct("", AssetType.Sound, asset, WhenSoundFieldUpdated);
            UpdateProperties(currentAssetData);
            OnSoundSelected?.Invoke(asset);
            
            this.onChangeValue = onChangeValue; //Assign after everything is set up.
        }

        /// <summary>
        /// Update settings on all interactable properties on this asset.
        /// </summary>
        /// <param name="data">The data to update with.</param>
        private void UpdateProperties(IParameterAsset data)
        {
            volumeSlider.Construct("Volume", 0f, 1f, data.Parameters.floatValue1, WhenVolumeChanged);
            pitchSlider.Construct("Pitch", 0f, 2f, data.Parameters.floatValue2, WhenPitchChanged);
            randomPitchToggle.Construct("Randomize Pitch", data.Parameters.boolValue1, WhenRandomPitchChanged);
        }

        private void WhenSoundFieldUpdated(IAsset asset)
        {
            currentAssetData = AssetDataBuilder.ForSound(asset);
            UpdateProperties(currentAssetData);
            UpdateOriginalValue();
            OnSoundSelected?.Invoke(asset);
        }
        
        private void WhenVolumeChanged(float newValue)
        {
            currentAssetData.UpdateFloatValue1(newValue);
            UpdateOriginalValue();
        }
        
        private void WhenPitchChanged(float newValue)
        {
            currentAssetData.UpdateFloatValue2(newValue);
            UpdateOriginalValue();
        }
        
        private void WhenRandomPitchChanged(bool newValue)
        {
            currentAssetData.UpdateBoolValue1(newValue);
            UpdateOriginalValue();
        }

        /// <summary>
        /// Call to update the original value.
        /// </summary>
        private void UpdateOriginalValue() => onChangeValue?.Invoke(currentAssetData);
    }
}