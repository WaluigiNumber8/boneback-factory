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
        
        private InputAction action;
        
        public void Construct(string title, InputAction action, int bindingIndex)
        {
            title = Regex.Replace(title, "([A-Z])", " $1").Trim();
            ConstructTitle(title);
            inputReader.Construct(action, bindingIndex);
            this.action = action;
        }
        
        public override void SetDisabled(bool isDisabled)
        {
            inputReader.SetActive(!isDisabled);
        }

        public void UpdateTheme(InteractableSpriteInfo inputButtonSet, FontInfo titleFont, FontInfo inputFont)
        {
            UIExtensions.ChangeInteractableSprites(inputReader.GetComponentInChildren<Button>(), inputButtonSet);
            UIExtensions.ChangeFont(inputReader.GetComponentInChildren<TextMeshProUGUI>(), inputFont);
            UIExtensions.ChangeFont(title, titleFont);
        }
        
        public override InputAction PropertyValue { get => inputReader.Action; }

        public string KeyboardInputString { get => inputReader.InputString; }
    }
}