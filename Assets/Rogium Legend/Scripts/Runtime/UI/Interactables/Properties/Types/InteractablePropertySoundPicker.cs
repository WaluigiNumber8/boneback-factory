using System;
using RedRats.UI.Core;
using Rogium.Editors.Core;
using Rogium.Systems.ThemeSystem;
using Rogium.UserInterface.Core;
using Rogium.UserInterface.Editors.PropertyModalWindows;
using TMPro;
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
        [SerializeField] private UIInfo ui;
        
        private SoundPickerModalWindow soundPickerWindow;
        private Action<AssetData> onChangeValue;

        private void Awake()
        {
            soundPickerWindow = CanvasOverseer.GetInstance().SoundPickerWindow;
            showWindowButton.onClick.AddListener(soundPickerWindow.Open);
        }

        private void OnEnable() => soundPickerWindow.OnSoundSelected += RefreshOnChange;
        private void OnDisable() => soundPickerWindow.OnSoundSelected -= RefreshOnChange;


        public void Construct(string titleText, AssetData value, Action<AssetData> whenSoundEdited)
        {
            ConstructTitle(titleText);
            onChangeValue = whenSoundEdited;
            soundPickerWindow.Construct(onChangeValue, value);
        }
        
        public override void SetDisabled(bool isDisabled)
        {
            showWindowButton.interactable = !isDisabled;
            playButton.interactable = !isDisabled;
        }

        public void UpdateTheme(InteractableSpriteInfo openWindowButtonSet, InteractableSpriteInfo buttonSet, FontInfo titleFont)
        {
            UIExtensions.ChangeInteractableSprites(showWindowButton, showWindowButton.image, openWindowButtonSet);
            UIExtensions.ChangeInteractableSprites(playButton, playButton.image, buttonSet);
            UIExtensions.ChangeFont(title, titleFont);
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
        }
    }
}