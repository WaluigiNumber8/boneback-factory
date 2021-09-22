using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

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
        /// /// <param name="options">The list of options the dropdown will be filled with.</param>
        /// <param name="index">Index of the dropdownOption.</param>
        /// <param name="OnValueChange">Method that will run when the dropdown value changes.</param>
        public void Set(string title, IList<string> options, int index, Action<int> OnValueChange)
        {
            FillDropdown(options);
            this.title.text = title;
            this.dropdown.value = index;
            this.dropdown.onValueChanged.AddListener(delegate { OnValueChange(dropdown.value); });
        }

        /// <summary>
        /// Fills the dropdown with strings.
        /// </summary>
        /// <param name="options">List of strings, that will become values.</param>
        private void FillDropdown(IList<string> options)
        {
            dropdown.options.Clear();
            foreach (string option in options)
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData() { text = option });
            }
        }

        public string Title { get => title.text; }
        public int Property { get => dropdown.value; }
    }
}