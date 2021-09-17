using UnityEngine;
using TMPro;
using System;

namespace Rogium.Global.UISystem.Interactables.Properties
{
    /// <summary>
    /// Overseers everything happening in a dropdown interactable property.
    /// </summary>
    public class InteractablePropertyDropdown : MonoBehaviour, IInteractableProperty
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TMP_Dropdown dropdown;

        /// <summary>
        /// Set the property title and state.
        /// </summary>
        /// <param name="title">Property Title.</param>
        /// <param name="index">Index of the dropdownOption.</param>
        /// <param name="OnValueChange">Method that will run when the dropdown value changes.</param>
        public void Set(string title, int index, Action<int> OnValueChange)
        {
            this.title.text = title;
            this.dropdown.value = index;
            this.dropdown.onValueChanged.AddListener(delegate { OnValueChange(dropdown.value); });
        }

        public string Title { get => title.text; }
        public int Property { get => dropdown.value; }
    }
}