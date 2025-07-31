using System;
using System.Collections.Generic;
using RedRats.UI.Core;
using Rogium.Systems.ActionHistory;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Overseers everything happening in a dropdown interactable property.
    /// </summary>
    public class IPDropdown : IPWithValueBase<int>
    {
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private UIInfo ui;
        
        private Action<int> whenValueChange;
        private int lastValue;

        private void Awake() => dropdown.onValueChanged.AddListener(WhenValueChanged);

        public override void SetDisabled(bool isDisabled) => dropdown.interactable = !isDisabled;

        /// <summary>
        /// Set the property title and state.
        /// </summary>
        /// <param name="titleText">Property Title.</param>
        /// /// <param name="options">The list of options the dropdown will be filled with.</param>
        /// <param name="startingValue">Index of the dropdownOption.</param>
        /// <param name="whenValueChange">Method that will run when the dropdown value changes.</param>
        public void Construct(string titleText, IEnumerable<string> options, int startingValue, Action<int> whenValueChange)
        {
            FillDropdown(options);
            ConstructTitle(titleText);
            
            lastValue = startingValue;
            dropdown.SetValueWithoutNotify(startingValue);
            dropdown.RefreshShownValue();
            
            this.whenValueChange = whenValueChange;
        }

        /// <summary>
        /// Updates the dropdown value without invoking the value change event. Assigned <see cref="whenValueChange"/> action still runs.
        /// </summary>
        /// <param name="value">The new value for the dropdown.</param>
        public void UpdateValueWithoutNotify(int value)
        {
            lastValue = dropdown.value;
            dropdown.SetValueWithoutNotify(value);
            dropdown.RefreshShownValue();
            whenValueChange?.Invoke(value);
        }
        
        /// <summary>
        /// Updates the dropdown's UI elements.
        /// </summary>
        /// <param name="dropdownSpriteSet">Dropdown graphics on mouse hover.</param>
        /// <param name="itemSpriteSet">Selected dropdown value graphics on mouse hover.</param>
        /// <param name="headerBackground">The "closed" state of the dropdown.</param>
        /// <param name="background">The "open" state of the dropdown.</param>
        /// <param name="dropdownArrow">The arrow sprite.</param>
        /// <param name="checkmark">The selected option checkmark sprite.</param>
        /// <param name="titleFont">The font of the descriptive text.</param>
        /// <param name="labelFont">The font of the value label text.</param>
        public void UpdateTheme(InteractableSpriteInfo dropdownSpriteSet, InteractableSpriteInfo itemSpriteSet, Sprite headerBackground, Sprite background, Sprite dropdownArrow, Sprite checkmark, FontInfo titleFont, FontInfo labelFont)
        {
            UIExtensions.ChangeInteractableSprites(dropdown, ui.headerBackgroundImage, dropdownSpriteSet);
            UIExtensions.ChangeInteractableSprites(ui.itemToggle, ui.toggleBackgroundImage, itemSpriteSet);
            UIExtensions.ChangeFont(title, titleFont);
            UIExtensions.ChangeFont(ui.labelText, labelFont);
            UIExtensions.ChangeFont(ui.toggleLabelText, labelFont);
            ui.headerBackgroundImage.sprite = headerBackground;
            ui.dropdownArrowImage.sprite = dropdownArrow;
            ui.dropdownImage.sprite = background;
            ui.toggleCheckmarkImage.sprite = checkmark;
        }

        private void WhenValueChanged(int value)
        {
            ActionHistorySystem.AddAndExecute(new UpdateDropdownAction(this, value, lastValue, whenValueChange));
            lastValue = value;
        }
        
        /// <summary>
        /// Fills the dropdown with strings.
        /// </summary>
        /// <param name="options">List of strings, that will become values.</param>
        private void FillDropdown(IEnumerable<string> options)
        {
            dropdown.options.Clear();
            foreach (string option in options)
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData {text = option});
            }
        }

        public override int PropertyValue { get => dropdown.value; }

        [Serializable]
        public struct UIInfo
        {
            public Image headerBackgroundImage;
            public TextMeshProUGUI labelText;
            public Image dropdownArrowImage;
            public Image dropdownImage;
            public Toggle itemToggle;
            public TextMeshProUGUI toggleLabelText;
            public Image toggleBackgroundImage;
            public Image toggleCheckmarkImage;
        }
    }
}