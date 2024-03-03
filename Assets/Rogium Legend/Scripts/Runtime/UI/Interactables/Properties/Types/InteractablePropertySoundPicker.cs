using System;
using Rogium.Editors.Core;
using Rogium.UserInterface.Core;
using Rogium.UserInterface.Editors.PropertyModalWindows;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Represents a sound picker property.
    /// </summary>
    public class InteractablePropertySoundPicker : InteractablePropertyBase
    {
        [SerializeField] private Button showWindowButton;
        [SerializeField] private Button playButton;
        
        private SoundPickerModalWindow soundPickerWindow;

        private void Awake()
        {
            soundPickerWindow = CanvasOverseer.GetInstance().SoundPickerWindow;
            showWindowButton.onClick.AddListener(soundPickerWindow.Open);
        }

        public void Construct(string titleText, AssetData value, Action<AssetData> whenSoundEdited)
        {
            ConstructTitle(titleText);
            soundPickerWindow.Construct(whenSoundEdited, value);
        }
        
        public override void SetDisabled(bool isDisabled)
        {
            showWindowButton.interactable = !isDisabled;
            playButton.interactable = !isDisabled;
        }
    }
}