using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Interactables
{
    /// <summary>
    /// Handles input from the button component via the GAS System.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class InteractableButton : MonoBehaviour
    {
        [SerializeField] private ButtonType action;
        [SerializeField] private int index = -1;
        
        private Button button;

        private void Awake() => button = GetComponent<Button>();
        private void OnEnable() => button.onClick.AddListener(OnButtonClicked);
        private void OnDisable() => button.onClick.RemoveListener(OnButtonClicked);

        public void OnButtonClicked()
        {
            InteractableInput.Handle(action, index);
        }

        public ButtonType Action { get => action; set => action = value; }
        public int Index { get => index; set => index = value; }
    }
}