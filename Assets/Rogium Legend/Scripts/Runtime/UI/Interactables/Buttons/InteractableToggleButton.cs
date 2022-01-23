using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Interactables
{
    /// <summary>
    /// Handles input from toggles, that work like buttons using the GAS System.
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    public class InteractableToggleButton : MonoBehaviour
    {
        [SerializeField] private ButtonType action;
        [SerializeField] private int index = -1;
        
        private Toggle button;

        private void Awake() => button = GetComponent<Toggle>();
        private void OnEnable() => button.onValueChanged.AddListener(OnButtonClicked);
        private void OnDisable() => button.onValueChanged.RemoveListener(OnButtonClicked);

        public void OnButtonClicked(bool value)
        {
            if (!value) return;
            InteractableInput.Handle(action, index);
        }

        public int Index { get => index; set => index = value; }
    }
}