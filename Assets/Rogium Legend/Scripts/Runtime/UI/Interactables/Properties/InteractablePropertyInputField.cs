using System;
using BoubakProductions.UI.Core;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Overseers everything happening in an input field interactable property.
    /// </summary>
    public class InteractablePropertyInputField : MonoBehaviour, IInteractableProperty
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private UIInfo ui;

        /// <summary>
        /// Set the property title and state.
        /// </summary>
        /// <param name="titleText">Property Title.</param>
        /// <param name="inputtedText">Text in the input field.</param>
        /// <param name="whenValueChange">Method that runs when value in the field changes.</param>
        /// <param name="characterValidation">The validation to use for inputted symbols.</param>
        public void Construct(string titleText, string inputtedText, Action<string> whenValueChange, TMP_InputField.CharacterValidation characterValidation)
        {
            title.text = titleText;
            title.gameObject.SetActive((titleText != ""));
            if (ui.emptySpace != null) ui.emptySpace.SetActive((titleText != ""));
            
            inputField.text = inputtedText;
            inputField.characterValidation = characterValidation;
            inputField.onValueChanged.AddListener(delegate { whenValueChange(inputField.text); });
        }

        /// <summary>
        /// Updates the elements sprites to match the editor theme.
        /// </summary>
        /// <param name="inputFieldSpriteSet">Sprites to use for the input field.</param>
        /// <param name="titleFont">Font for property title.</param>
        /// <param name="inputFont">Font for input text.</param>
        public void UpdateTheme(InteractableInfo inputFieldSpriteSet, FontInfo titleFont, FontInfo inputFont)
        {
            UIExtensions.ChangeInteractableSprites(inputField, ui.inputFieldImage, inputFieldSpriteSet);
            UIExtensions.ChangeFont(title, titleFont);
            UIExtensions.ChangeFont(ui.inputtedText, inputFont);
        }
        
        public string Title { get => title.text; }
        public string Property { get => inputField.text; }

        [Serializable]
        public struct UIInfo
        {
            public Image inputFieldImage;
            public TextMeshProUGUI inputtedText;
            public GameObject emptySpace;
        }
    }
}