using RedRats.Core;
using RedRats.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Represents a property that allows rebinding an single-button input for both keyboard and gamepad.
    /// </summary>
    public class IPInputBinding : IPWithValueBase<InputAction>
    {
        [SerializeField] private InputBindingReader inputReader;
        [SerializeField] private InputBindingReader inputReaderAlt;

        public void Construct(string title, InputAction action, int bindingIndex, int modifier1Index = -1, int modifier2Index = -1, int bindingIndexAlt = -1, int modifier1IndexAlt = -1, int modifier2IndexAlt = -1)
        {
            ConstructTitle(title.WithSpacesBeforeCapitals());
            
            inputReader.Construct(action, bindingIndex, modifier1Index, modifier2Index);
            inputReaderAlt.gameObject.SetActive(bindingIndexAlt != -1);
            if (bindingIndexAlt != -1) inputReaderAlt.Construct(action, bindingIndexAlt, modifier1IndexAlt, modifier2IndexAlt);
        }

        public override void SetDisabled(bool isDisabled)
        {
            inputReader.SetActive(!isDisabled);
            inputReaderAlt.SetActive(!isDisabled);
        }

        public void UpdateTheme(InteractableSpriteInfo inputButtonSet, FontInfo titleFont, FontInfo inputFont)
        {
            UIExtensions.ChangeInteractableSprites(inputReader.GetComponentInChildren<Button>(), inputButtonSet);
            UIExtensions.ChangeInteractableSprites(inputReaderAlt.GetComponentInChildren<Button>(), inputButtonSet);
            UIExtensions.ChangeFont(inputReader.GetComponentInChildren<TextMeshProUGUI>(), inputFont);
            UIExtensions.ChangeFont(inputReaderAlt.GetComponentInChildren<TextMeshProUGUI>(), inputFont);
            UIExtensions.ChangeFont(title, titleFont);
        }
        
        public override InputAction PropertyValue { get => inputReader.Action; }

        public string InputString { get => inputReader.InputString; }
        public string InputStringAlt { get => inputReaderAlt.InputString; }
        public InputBindingReader InputReader { get => inputReader; }
        public InputBindingReader InputReaderAlt { get => inputReaderAlt; }
    }
}