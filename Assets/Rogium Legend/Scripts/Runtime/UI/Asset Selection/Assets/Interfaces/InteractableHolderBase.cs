using System;
using Rogium.UserInterface.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.AssetSelection
{
    /// <summary>
    /// A base for all classes that hold information and send it out via button presses.
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    public abstract class InteractableHolderBase : MonoBehaviour, IToggleable
    {
        public static event Action<int> OnSelectedAny;
        
        protected int index = -1;
        private Toggle toggle;
        
        private void Awake()
        {
            toggle = GetComponent<Toggle>();
            toggle.group = GetComponentInParent<ToggleGroup>();
        }

        private void OnEnable() => toggle.onValueChanged.AddListener(WhenSelected);
        private void OnDisable() => toggle.onValueChanged.RemoveListener(WhenSelected);

        public void SetToggle(bool value) => toggle.isOn = value;

        /// <summary>
        /// Fires the select event when the toggle was clicked.
        /// </summary>
        private void WhenSelected(bool value)
        {
            if (!value) return;
            OnSelectedAny?.Invoke(index);
        }
        
        public int Index { get => index; }
    }
}