using RedRats.UI.Core;
using TMPro;
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
        [SerializeField] private string stringID = "";
        [SerializeField] private UIInfo ui;
        
        private Button button;

        private void Awake() => button = GetComponent<Button>();
        private void OnEnable() => button.onClick.AddListener(OnButtonClicked);
        private void OnDisable() => button.onClick.RemoveListener(OnButtonClicked);

        public void OnButtonClicked()
        {
            InteractableInput.Handle(action, index, stringID);
        }

        /// <summary>
        /// Updates the buttons theme.
        /// </summary>
        /// <param name="buttonSet">The main button.</param>
        /// <param name="buttonFont">The text of the button (if it exists).</param>
        public void UpdateTheme(InteractableInfo buttonSet, FontInfo buttonFont)
        {
            UIExtensions.ChangeInteractableSprites(button, ui.buttonImage, buttonSet);
            if (ui.buttonText == null) return;
            UIExtensions.ChangeFont(ui.buttonText, buttonFont);
        }

        public ButtonType Action { get => action; set => action = value; }
        public int Index { get => index; set => index = value; }
        public string StringID { get => stringID; set => stringID = value; }

        [System.Serializable]
        public struct UIInfo
        {
            public Image buttonImage;
            public TextMeshProUGUI buttonText;
        }
    }
}