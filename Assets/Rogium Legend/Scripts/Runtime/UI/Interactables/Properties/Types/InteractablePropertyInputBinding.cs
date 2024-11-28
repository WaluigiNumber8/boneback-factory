using System.Text.RegularExpressions;
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
    public class InteractablePropertyInputBinding : InteractablePropertyBase<InputAction>
    {
        [SerializeField] private InputBindingReader inputReader;
        [SerializeField] private InputBindingReader inputReaderAlt;

        public void Construct(string title, InputAction action, int bindingIndex, int bindingIndexAlt = -1)
        {
            title = Regex.Replace(title, "([A-Z])", " $1").Trim();
            ConstructTitle(title);
            
            inputReader.Construct(action, bindingIndex);
            inputReaderAlt.gameObject.SetActive(bindingIndexAlt != -1);
            if (bindingIndexAlt != -1) inputReaderAlt.Construct(action, bindingIndexAlt);
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
    }
}