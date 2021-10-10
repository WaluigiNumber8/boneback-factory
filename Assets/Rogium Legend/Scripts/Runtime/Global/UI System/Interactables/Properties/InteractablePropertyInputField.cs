using UnityEngine;
using TMPro;
using System;

namespace Rogium.Global.UISystem.Interactables.Properties
{
    /// <summary>
    /// Overseers everything happening in an input field interactable property.
    /// </summary>
    public class InteractablePropertyInputField : MonoBehaviour, IInteractableProperty
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TMP_InputField inputField;

        /// <summary>
        /// Set the property title and state.
        /// </summary>
        /// <param name="title">Property Title.</param>
        /// <param name="inputtedText">Text in the input field.</param>
        public void Set(string title, string inputtedText, Action<string> onChangeValue)
        {
            this.title.text = title;
            inputField.text = inputtedText;
            inputField.onValueChanged.AddListener(delegate { onChangeValue(inputField.text); });
        }

        public string Title { get => title.text; }
        public string Property { get => inputField.text; }
    }
}