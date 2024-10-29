using RedRats.Safety;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Editors.AssetSelection
{
    /// <summary>
    /// A base for all classes working with an internal toggle.
    /// </summary>
    public abstract class ToggleableBase : MonoBehaviour
    {
        [SerializeField] protected Toggle toggle;
        
        protected virtual void Awake() => toggle.group = GetComponentInParent<ToggleGroup>();

        /// <summary>
        /// Sets the value of the Internal Toggle.
        /// </summary>
        /// <param name="value">The new value to set.</param>
        public void SetToggle(bool value) => toggle.isOn = value;
        
        /// <summary>
        /// Toggles the value.
        /// </summary>
        public void Toggle() => toggle.isOn = !toggle.isOn;

        /// <summary>
        /// Assign a toggle group to the toggle.
        /// </summary>
        /// <param name="toggleGroup">The new <see cref="ToggleGroup"/> to assign.</param>
        public void RegisterToggleGroup(ToggleGroup toggleGroup)
        {
            SafetyNet.EnsureIsNotNull(toggleGroup, "New Toggle Group");
            toggle.group = toggleGroup;
        }

        /// <summary>
        /// Returns the value of the toggle.
        /// </summary>
        public bool IsOn => toggle.isOn;
    }
}