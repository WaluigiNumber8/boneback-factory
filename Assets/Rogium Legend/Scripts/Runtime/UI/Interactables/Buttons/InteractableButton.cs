using System.Collections;
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
        [SerializeField, Min(0f)] private float delay = 0;
        [SerializeField] private int index = -1;
        [SerializeField] private UIInfo ui;
        
        private Button button;

        private void Awake() => button = GetComponent<Button>();
        private void OnEnable() => button.onClick.AddListener(WhenButtonClicked);
        private void OnDisable() => button.onClick.RemoveListener(WhenButtonClicked);

        public void WhenButtonClicked()
        {
            StartCoroutine(DelayCoroutine());
            IEnumerator DelayCoroutine()
            {
                yield return new WaitForSeconds(delay);
                InteractableInput.Handle(action, index);
            }
        }

        /// <summary>
        /// Updates the buttons theme.
        /// </summary>
        /// <param name="buttonSet">The main button.</param>
        /// <param name="buttonFont">The text of the button (if it exists).</param>
        public void UpdateTheme(InteractableSpriteInfo buttonSet, FontInfo buttonFont)
        {
            UIExtensions.ChangeInteractableSprites(button, ui.buttonImage, buttonSet);
            if (ui.buttonText == null) return;
            UIExtensions.ChangeFont(ui.buttonText, buttonFont);
        }

        public ButtonType Action { get => action; set => action = value; }
        public int Index { get => index; set => index = value; }

        [System.Serializable]
        public struct UIInfo
        {
            public Image buttonImage;
            public TextMeshProUGUI buttonText;
        }
    }
}