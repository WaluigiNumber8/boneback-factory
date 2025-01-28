using System;
using System.Collections.Generic;
using RedRats.Core;
using RedRats.Systems.Themes;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Systems.Input;
using Rogium.Systems.ThemeSystem;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

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
        [SerializeField] private InteractablePropertyDropdown dropdownProperty;
        [SerializeField] private InteractablePropertyToggle toggleProperty;
        [SerializeField] private InteractablePropertySlider sliderProperty;
        [SerializeField] private InteractablePropertyAssetField assetFieldProperty;
        [SerializeField] private InteractablePropertySoundField soundFieldProperty;
        [SerializeField] private InteractablePropertyColorField colorFieldProperty;
        [SerializeField] private InteractablePropertyAssetEmblemList assetEmblemListProperty;
        [SerializeField] private InteractablePropertyInputBinding inputBindingProperty;
        
        [Title("Other properties")]
        [SerializeField] private ContentBlockInfo contentBlocks;
        [SerializeField] private VerticalVariantsInfo verticalVariants;
        
        [Title("Other")] [SerializeField]
        private Transform poolParent;

        private UIPropertyPool<InteractablePropertyHeader> headerPool;
        private UIPropertyPool<InteractablePropertyPlainText> plainTextPool, plainTextVerticalPool;
        private UIPropertyPool<InteractablePropertyInputField> inputFieldPool, inputFieldAreaPool, inputFieldVerticalPool;
        private UIPropertyPool<InteractablePropertyDropdown> dropdownPool, dropdownVerticalPool;
        private UIPropertyPool<InteractablePropertyToggle> togglePool;
        private UIPropertyPool<InteractablePropertySlider> sliderPool;
        private UIPropertyPool<InteractablePropertyAssetField> assetFieldPool;
        private UIPropertyPool<InteractablePropertySoundField> soundFieldPool;
        private UIPropertyPool<InteractablePropertyColorField> colorFieldPool;
        private UIPropertyPool<InteractablePropertyAssetEmblemList> assetEmblemListPool;
        private UIPropertyPool<InteractablePropertyInputBinding> inputBindingPool;
        
        private UIPropertyPool<InteractablePropertyContentBlock> contentBlockHorizontalPool, contentBlockVerticalPool, contentBlockColumn2Pool;

        protected override void Awake()
        {
            base.Awake();
            headerPool = new UIPropertyPool<InteractablePropertyHeader>(headerProperty, poolParent, 25, 100);
            plainTextPool = new UIPropertyPool<InteractablePropertyPlainText>(plainTextProperty, poolParent, 50, 150);
            plainTextVerticalPool = new UIPropertyPool<InteractablePropertyPlainText>(verticalVariants.plainTextProperty, poolParent, 50, 150);
            inputFieldPool = new UIPropertyPool<InteractablePropertyInputField>(inputFieldProperty, poolParent, 50, 150);
            inputFieldAreaPool = new UIPropertyPool<InteractablePropertyInputField>(inputFieldAreaProperty, poolParent, 50, 150);
            inputFieldVerticalPool = new UIPropertyPool<InteractablePropertyInputField>(verticalVariants.inputFieldProperty, poolParent, 50, 150);
            dropdownPool = new UIPropertyPool<InteractablePropertyDropdown>(dropdownProperty, poolParent, 50, 150);
            dropdownVerticalPool = new UIPropertyPool<InteractablePropertyDropdown>(verticalVariants.dropdownProperty, poolParent, 50, 150);
            togglePool = new UIPropertyPool<InteractablePropertyToggle>(toggleProperty, poolParent, 50, 150);
            sliderPool = new UIPropertyPool<InteractablePropertySlider>(sliderProperty, poolParent, 50, 150);
            assetFieldPool = new UIPropertyPool<InteractablePropertyAssetField>(assetFieldProperty, poolParent, 50, 150);
            soundFieldPool = new UIPropertyPool<InteractablePropertySoundField>(soundFieldProperty, poolParent, 50, 150);
            colorFieldPool = new UIPropertyPool<InteractablePropertyColorField>(colorFieldProperty, poolParent, 50, 150);
            assetEmblemListPool = new UIPropertyPool<InteractablePropertyAssetEmblemList>(assetEmblemListProperty, poolParent, 50, 150);
            inputBindingPool = new UIPropertyPool<InteractablePropertyInputBinding>(inputBindingProperty, poolParent, 50, 150);
            
            contentBlockHorizontalPool = new UIPropertyPool<InteractablePropertyContentBlock>(contentBlocks.horizontal, poolParent, 10, 40);
            contentBlockVerticalPool = new UIPropertyPool<InteractablePropertyContentBlock>(contentBlocks.vertical, poolParent, 10, 40);
            contentBlockColumn2Pool = new UIPropertyPool<InteractablePropertyContentBlock>(contentBlocks.column2, poolParent, 10, 40);
        }

        #region Properties

        /// <summary>
        /// Builds the Header Property.
        /// </summary>
        /// <param name="headerText">The text of the header.</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <returns>The property itself.</returns>
        public void BuildHeader(string headerText, Transform parent)
        {
            InteractablePropertyHeader header = headerPool.Get(parent);
            header.name = $"{headerText} Header";
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
            InteractablePropertyInputField inputField = (isVertical) ? inputFieldVerticalPool.Get(parent) : inputFieldPool.Get(parent);
            inputField.name = $"{title} InputField";
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
            InteractablePropertyInputField inputField = inputFieldAreaPool.Get(parent);
            inputField.name = $"{title} InputField";
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
            InteractablePropertyToggle toggle = togglePool.Get(parent);
            toggle.name = $"{title} Toggle";
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
            InteractablePropertyDropdown dropdown = (isVertical) ? dropdownVerticalPool.Get(parent) : dropdownPool.Get(parent);
            dropdown.name = $"{title} Dropdown";
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
            InteractablePropertyPlainText plainText = (isVertical) ? plainTextVerticalPool.Get(parent) : plainTextPool.Get(parent);
            plainText.name = $"{title} PlainText";
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
        /// <param name="whenSelectEmpty">Method that runs when "empty" is selected. If this is not null, adds the option into the Picker Window.</param>
        /// <param name="theme">The theme for the Asset Picker Window.</param>
        /// <returns>The property itself.</returns>
        public void BuildAssetField(string title, AssetType type, IAsset value, Transform parent, Action<IAsset> whenValueChange, Action whenSelectEmpty = null, bool isDisabled = false, ThemeType theme = ThemeType.Current)
        {
            InteractablePropertyAssetField assetField = assetFieldPool.Get(parent);
            assetField.name = $"{title} AssetField";
            assetField.Construct(title, type, value, whenValueChange, whenSelectEmpty, theme);
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
            InteractablePropertySlider slider = sliderPool.Get(parent);
            slider.name = $"{title} Slider";
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
            InteractablePropertySlider slider = sliderPool.Get(parent);
            slider.name = $"{title} Slider";
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
        /// <param name="canBeEmpty">If TRUE, the the field can contain no value.</param>
        /// <param name="isDisabled">Initialize the property as a non-interactable</param>
        public void BuildSoundField(string title, AssetData value, Transform parent, Action<AssetData> whenValueChange, bool canBeEmpty = false, bool isDisabled = false)
        {
            InteractablePropertySoundField soundField = soundFieldPool.Get(parent);
            soundField.name = $"{title} SoundField";
            soundField.Construct(title, value, whenValueChange, canBeEmpty);
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
            InteractablePropertyColorField colorField = colorFieldPool.Get(parent);
            colorField.name = $"{title} ColorField";
            colorField.Construct(title, value, whenValueChange);
            colorField.SetDisabled(isDisabled);
            ThemeUpdaterRogium.UpdateColorField(colorField);
        }

        /// <summary>
        /// Builds the Asset Emblem List property.
        /// </summary>
        /// <param name="title">The text of the property title.</param>
        /// <param name="value">The icon to use for the first emblem.</param>
        /// <param name="parent">The parent under which to instantiate the property.</param>
        public void BuildAssetEmblemList(string title, Sprite value, Transform parent) => BuildAssetEmblemList(title, new List<Sprite>{value}, parent);
        /// <summary>
        /// Builds the Asset Emblem List property.
        /// </summary>
        /// <param name="title">The text of the property title.</param>
        /// <param name="values">The list of icons to use for each emblem.</param>
        /// <param name="parent">The parent under which to instantiate the property.</param>
        public void BuildAssetEmblemList(string title, IList<Sprite> values, Transform parent)
        {
            InteractablePropertyAssetEmblemList assetEmblemList = assetEmblemListPool.Get(parent);
            assetEmblemList.name = $"{title} Emblem List";
            assetEmblemList.Construct(title, values);
            ThemeUpdaterRogium.UpdateAssetEmblemList(assetEmblemList);
        }

        /// <summary>
        /// Builds the Input Binding property.
        /// </summary>
        /// <param name="action">The input action the binding is assigned to.</param>
        /// <param name="device">To which device is the property limited to.</param>
        /// <param name="parent">The parent under which to instantiate the property.</param>
        /// <param name="useAlt">The action can have an alternative input.</param>
        /// <param name="isDisabled">Initialize the property as a non-interactable</param>
        public void BuildInputBinding(InputAction action, InputDeviceType device, Transform parent, bool useAlt = true, bool isDisabled = false)
        {
            int bindingIndex = InputSystemUtils.GetBindingIndexByDevice(action, device);
            int bindingIndexAlt = useAlt ? InputSystemUtils.GetBindingIndexByDevice(action, device, true) : -1;
            
            //If action is composite, spawn for each binding
            if (action.bindings[bindingIndex].isPartOfComposite)
            {
                //If is a modifier composite, spawn only one
                if (action.bindings[bindingIndex - 1].IsTwoOptionalModifiersComposite())
                {
                    ConstructInputBinding(action.name, true);
                    return;
                }
                //Any other type spawn for each binding
                while (bindingIndex < action.bindings.Count && action.bindings[bindingIndex].isPartOfComposite)
                {
                    string title = $"{action.name}{action.bindings[bindingIndex].name.Capitalize()}";
                    ConstructInputBinding(title);
                    bindingIndex++;
                    bindingIndexAlt++;
                }
                return;
            }
            ConstructInputBinding(action.name);
            
            void ConstructInputBinding(string title, bool useModifiers = false)
            {
                InteractablePropertyInputBinding inputBinding = inputBindingPool.Get(parent);
                inputBinding.name = $"{title} InputBinding";
                inputBinding.Construct(title, action, 
                                       (useModifiers) ? bindingIndex + 2 : bindingIndex,
                                       (useModifiers) ? bindingIndex : -1,
                                       (useModifiers) ? bindingIndex + 1 : -1,
                                       (useModifiers) ? bindingIndexAlt + 2 : bindingIndexAlt, 
                                       (useModifiers) ? bindingIndexAlt : -1, 
                                       (useModifiers) ? bindingIndexAlt + 1 : -1);
                inputBinding.SetDisabled(isDisabled);
                ThemeUpdaterRogium.UpdateInputBinding(inputBinding);
            }
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
            InteractablePropertyContentBlock contentBlock = contentBlockHorizontalPool.Get(parent);
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
            InteractablePropertyContentBlock contentBlock = contentBlockVerticalPool.Get(parent);
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
            InteractablePropertyContentBlock contentBlock = contentBlockColumn2Pool.Get(parent);
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