using System;
using System.Collections.Generic;
using RedRats.Core;
using RedRats.Systems.Themes;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.ThemeSystem;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Builds Properties.
    /// </summary>
    public class UIPropertyBuilder : MonoSingleton<UIPropertyBuilder>
    {
        [Title("Property prefabs")] 
        [SerializeField] private InteractablePropertyHeader headerProperty;
        [SerializeField] private InteractablePropertyPlainText plainTextProperty;
        [SerializeField] private InteractablePropertyInputField inputFieldProperty;
        [SerializeField] private InteractablePropertyInputField inputFieldAreaProperty;
        [SerializeField] private InteractablePropertyAssetField assetFieldProperty;
        [SerializeField] private InteractablePropertyToggle toggleProperty;
        [SerializeField] private InteractablePropertyDropdown dropdownProperty;
        [SerializeField] private InteractablePropertySlider sliderProperty;
        [SerializeField] private InteractablePropertySoundField soundFieldProperty;
        [SerializeField] private InteractablePropertyColorField colorFieldProperty;
        
        [Title("Other properties")]
        [SerializeField] private ContentBlockInfo contentBlocks;
        [SerializeField] private VerticalVariantsInfo verticalVariants;

        #region Properties

        /// <summary>
        /// Builds the Header Property.
        /// </summary>
        /// <param name="headerText">The text of the header.</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <returns>The property itself.</returns>
        public void BuildHeader(string headerText, Transform parent)
        {
            InteractablePropertyHeader header = Instantiate(headerProperty, parent);
            header.Construct(headerText);
            ThemeUpdaterRogium.UpdateHeader(header);
        }

        /// <summary>
        /// Builds the Input Field Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <param name="whenFinishEditing">Is called when the user finishes editing the InputField.</param>
        /// <param name="isDisabled">Initialize the property as a non-interactable.</param>
        /// <param name="isVertical">Use the vertical variant (title text is above field)?</param>
        /// <param name="characterValidation">The validation to use for inputted symbols.</param>
        /// <param name="minLimit">The minimum allowed value (when InputField deals with numbers).</param>
        /// <param name="maxLimit">The maximum allowed value (when InputField deals with numbers).</param>
        /// <returns>The property itself.</returns>
        public void BuildInputField(string title, string value, Transform parent, Action<string> whenFinishEditing, bool isDisabled = false, bool isVertical = false, TMP_InputField.CharacterValidation characterValidation = TMP_InputField.CharacterValidation.Regex, float minLimit = float.MinValue, float maxLimit = float.MaxValue)
        {
            InteractablePropertyInputField inputField = Instantiate((isVertical) ? verticalVariants.inputFieldProperty : inputFieldProperty, parent);
            inputField.Construct(title, value, whenFinishEditing, characterValidation, minLimit, maxLimit);
            inputField.SetDisabled(isDisabled);
            ThemeUpdaterRogium.UpdateInputField(inputField);
        }

        /// <summary>
        /// Builds the Input Field Area Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <param name="whenFinishEditing">Is called when the user finishes editing the InputField.</param>
        /// <param name="isDisabled">Initialize the property as a non-interactable.</param>
        /// <param name="characterValidation">The validation to use for inputted symbols.</param>
        /// <param name="minLimit">The minimum allowed value (when InputField deals with numbers).</param>
        /// <param name="maxLimit">The maximum allowed value (when InputField deals with numbers).</param>
        /// <returns>The property itself.</returns>
        public void BuildInputFieldArea(string title, string value, Transform parent, Action<string> whenFinishEditing, bool isDisabled = false, TMP_InputField.CharacterValidation characterValidation = TMP_InputField.CharacterValidation.Regex, float minLimit = float.MinValue, float maxLimit = float.MaxValue)
        {
            InteractablePropertyInputField inputField = Instantiate(inputFieldAreaProperty, parent);
            inputField.Construct(title, value, whenFinishEditing, characterValidation, minLimit, maxLimit);
            inputField.SetDisabled(isDisabled);
            ThemeUpdaterRogium.UpdateInputField(inputField);
        }

        /// <summary>
        /// Builds the Toggle Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <param name="whenValueChange">What happens when the property changes value.</param>
        /// <param name="isDisabled">Initialize the property as a non-interactable.</param>
        /// <returns>The property itself.</returns>
        public void BuildToggle(string title, bool value, Transform parent, Action<bool> whenValueChange, bool isDisabled = false)
        {
            InteractablePropertyToggle toggle = Instantiate(toggleProperty, parent).GetComponent<InteractablePropertyToggle>();
            toggle.Construct(title, value, whenValueChange);
            toggle.SetDisabled(isDisabled);
            ThemeUpdaterRogium.UpdateToggle(toggle);
        }

        /// <summary>
        /// Builds the Dropdown Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="options">The list of options the dropdown will be filled with.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <param name="whenValueChange">What happens when the property changes value.</param>
        /// <param name="isDisabled">Initialize the property as a non-interactable.</param>
        /// <param name="isVertical">Use the vertical variant (title text is above field)?</param>
        /// <returns>The property itself.</returns>
        public void BuildDropdown(string title, IEnumerable<string> options, int value, Transform parent, Action<int> whenValueChange, bool isDisabled = false, bool isVertical = false)
        {
            InteractablePropertyDropdown dropdown = Instantiate((isVertical) ? verticalVariants.dropdownProperty : dropdownProperty, parent);
            dropdown.Construct(title, options, value, whenValueChange);
            dropdown.SetDisabled(isDisabled);
            ThemeUpdaterRogium.UpdateDropdown(dropdown);
        }

        /// <summary>
        /// Builds the Plain Text Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <param name="isVertical">Use the vertical variant (title text is above field)?</param>
        /// <returns>The property itself.</returns>
        public void BuildPlainText(string title, string value, Transform parent, bool isVertical = false)
        {
            InteractablePropertyPlainText plainText = Instantiate((isVertical) ? verticalVariants.plainTextProperty : plainTextProperty, parent);
            plainText.Construct(title, value);
            ThemeUpdaterRogium.UpdatePlainText(plainText);
        }

        /// <summary>
        /// Builds the Asset Field Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="type">The type of asset to work with.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <param name="whenValueChange">The method that runs when the asset is changed.</param>
        /// <param name="isDisabled">Initialize the property as a non-interactable.</param>
        /// <param name="theme">The theme for the Asset Picker Window.</param>
        /// <returns>The property itself.</returns>
        public void BuildAssetField(string title, AssetType type, IAsset value, Transform parent, Action<IAsset> whenValueChange, bool isDisabled = false, ThemeType theme = ThemeType.Current)
        {
            InteractablePropertyAssetField assetField = Instantiate(assetFieldProperty, parent);
            assetField.Construct(title, type, value, whenValueChange, theme);
            assetField.SetDisabled(isDisabled);
            ThemeUpdaterRogium.UpdateAssetField(assetField);
        }

        /// <summary>
        /// Builds the Slider Property.
        /// </summary>
        /// <param name="title">The text of the property title.</param>
        /// <param name="minValue">Minimum allowed value on the slider.</param>
        /// <param name="maxValue">Maximum allowed value on the slider.</param>
        /// <param name="startingValue">Starting value of the slider.</param>
        /// <param name="parent">The parent under which to instantiate the property.</param>
        /// <param name="whenValueChange">Method that will run when the slider changes value.</param>
        /// <param name="isDisabled">Initialize the property as a non-interactable.</param>
        /// <returns>The property itself.</returns>
        public void BuildSlider(string title, float minValue, float maxValue, float startingValue, Transform parent, Action<float> whenValueChange, bool isDisabled = false)
        {
            InteractablePropertySlider slider = Instantiate(sliderProperty, parent);
            slider.Construct(title, minValue, maxValue, startingValue, whenValueChange);
            slider.SetDisabled(isDisabled);
            ThemeUpdaterRogium.UpdateSlider(slider);
            if (slider.InputField != null) ThemeUpdaterRogium.UpdateInputField(slider.InputField);
        }

        /// <summary>
        /// Builds the Slider Property.
        /// </summary>
        /// <param name="title">The text of the property title.</param>
        /// <param name="minValue">Minimum allowed value on the slider.</param>
        /// <param name="maxValue">Maximum allowed value on the slider.</param>
        /// <param name="startingValue">Starting value of the slider.</param>
        /// <param name="parent">The parent under which to instantiate the property.</param>
        /// <param name="whenValueChange">Method that will run when the slider changes value.</param>
        /// <param name="isDisabled">Initialize the property as a non-interactable.</param>
        /// <returns>The property itself.</returns>
        public void BuildSlider(string title, int minValue, int maxValue, int startingValue, Transform parent, Action<float> whenValueChange, bool isDisabled = false)
        {
            InteractablePropertySlider slider = Instantiate(sliderProperty, parent);
            slider.Construct(title, minValue, maxValue, startingValue, whenValueChange);
            slider.SetDisabled(isDisabled);
            ThemeUpdaterRogium.UpdateSlider(slider);
        }

        /// <summary>
        /// Builds the Sound Picker property.
        /// </summary>
        /// <param name="title">The text of the property title.</param>
        /// <param name="value">Starting value of the sound picker.</param>
        /// <param name="parent">The parent under which to instantiate the property.</param>
        /// <param name="whenValueChange">Method that runs when anything is updated by the property.</param>
        /// <param name="isDisabled">Initialize the property as a non-interactable</param>
        public void BuildSoundField(string title, AssetData value, Transform parent, Action<AssetData> whenValueChange, bool isDisabled = false)
        {
            InteractablePropertySoundField soundField = Instantiate(soundFieldProperty, parent);
            //TODO Add support for null values to Asset Picker window.
            value = (value.ID == EditorConstants.EmptyAssetID) ? AssetDataBuilder.ForSound(InternalLibraryOverseer.GetInstance().GetSoundByID("001")) : value;
            soundField.Construct(title, value, whenValueChange);
            soundField.SetDisabled(isDisabled);
            ThemeUpdaterRogium.UpdateSoundField(soundField);
        }
        
        /// <summary>
        /// Builds the Color Field property.
        /// </summary>
        /// <param name="title">The text of the property title.</param>
        /// <param name="value">Starting value of the color picker.</param>
        /// <param name="parent">The parent under which to instantiate the property.</param>
        /// <param name="whenValueChange">Method that runs when anything is updated by the property.</param>
        /// <param name="isDisabled">Initialize the property as a non-interactable</param>
        public void BuildColorField(string title, Color value, Transform parent, Action<Color> whenValueChange, bool isDisabled = false)
        {
            InteractablePropertyColorField colorField = Instantiate(colorFieldProperty, parent);
            colorField.Construct(title, value, whenValueChange);
            colorField.SetDisabled(isDisabled);
            ThemeUpdaterRogium.UpdateColorField(colorField);
        }
        #endregion

        #region Content Blocks

        /// <summary>
        /// Build the Horizontal Content Block.
        /// </summary>
        /// <param name="parent">The parent under which to instantiate the property.</param>
        /// <param name="isDisabled">Show/Hide the content block at init..</param>
        /// <returns></returns>
        public InteractablePropertyContentBlock CreateContentBlockHorizontal(Transform parent, bool isDisabled = false)
        {
            InteractablePropertyContentBlock contentBlock = Instantiate(contentBlocks.horizontal, parent);
            contentBlock.SetDisabled(isDisabled);
            return contentBlock;
        }
        
        /// <summary>
        /// Build the Vertical Content Block.
        /// </summary>
        /// <param name="parent">The parent under which to instantiate the property.</param>
        /// <param name="isDisabled">Show/Hide the content block at init..</param>
        /// <returns></returns>
        public InteractablePropertyContentBlock CreateContentBlockVertical(Transform parent, bool isDisabled = false)
        {
            InteractablePropertyContentBlock contentBlock = Instantiate(contentBlocks.vertical, parent);
            contentBlock.SetDisabled(isDisabled);
            return contentBlock;
        }
        
        /// <summary>
        /// Build the 2 Column Content Block.
        /// </summary>
        /// <param name="parent">The parent under which to instantiate the property.</param>
        /// <param name="isDisabled">Show/Hide the content block at init..</param>
        /// <returns></returns>
        public InteractablePropertyContentBlock CreateContentBlockColumn2(Transform parent, bool isDisabled = false)
        {
            InteractablePropertyContentBlock contentBlock = Instantiate(contentBlocks.column2, parent);
            contentBlock.SetDisabled(isDisabled);
            return contentBlock;
        }

        #endregion
        
        [Serializable]
        public struct ContentBlockInfo
        {
            public InteractablePropertyContentBlock vertical;
            public InteractablePropertyContentBlock horizontal;
            public InteractablePropertyContentBlock column2;
        }

        [Serializable]
        public struct VerticalVariantsInfo
        {
            public InteractablePropertyInputField inputFieldProperty;
            public InteractablePropertyDropdown dropdownProperty;
            public InteractablePropertyPlainText plainTextProperty;
        }
    }
}