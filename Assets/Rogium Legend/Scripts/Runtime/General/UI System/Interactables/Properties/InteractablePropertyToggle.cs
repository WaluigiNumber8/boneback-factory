using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Rogium.Global.UISystem.Interactables.Properties
{
    /// <summary>
    /// Overseers everything happening in a toggle interactable property.
    /// </summary>
    public class InteractablePropertyToggle : MonoBehaviour, IInteractableProperty
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Toggle toggle;

        /// <summary>
        /// Set the property title and state.
        /// </summary>
        /// <param name="title">Property Title.</param>
        /// <param name="toggleState">State of the toggle checkbox.</param>
        /// <param name="OnChangeValue">The method that will run when the toggle changes it's value.</param>
        public void Set(string title, bool toggleState, Action<bool> OnChangeValue)
        {
            this.title.text = title;
            this.toggle.isOn = toggleState;
            this.toggle.onValueChanged.AddListener(delegate { OnChangeValue(toggle.isOn); });
        }

        public string Title { get => title.text; }
        public bool Property { get => toggle.isOn; }
    }
}