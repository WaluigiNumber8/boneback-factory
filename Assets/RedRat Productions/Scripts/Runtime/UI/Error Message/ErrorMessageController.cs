using RedRats.Core;
using RedRats.Safety;
using Rogium.Systems.ThemeSystem;
using UnityEngine;

namespace RedRats.UI.ErrorMessageWindow
{
    /// <summary>
    /// Controls the availability of the Error Message Window.
    /// </summary>
    public class ErrorMessageController : MonoSingleton<ErrorMessageController>
    {
        [SerializeField] private ModalWindow errorWindow;
        [SerializeField] private int maxCharacters = 700;
        [SerializeField] private string acceptText;
        
        private void OnEnable()
        {
            SafetyNet.OnFireErrorMessage += Open;
            SafetyNetIO.OnFireErrorMessage += Open;
        }

        private void OnDisable()
        {
            SafetyNet.OnFireErrorMessage -= Open;
            SafetyNetIO.OnFireErrorMessage -= Open;
        }

        /// <summary>
        /// Shows the Error Message Window.
        /// </summary>
        /// <param name="errorMessage">The message of the error.</param>
        public void Open(string errorMessage)
        {
            string text = errorMessage;
            if (errorWindow.IsOpened)
                text = (errorWindow.GetMessageText.Length + errorMessage.Length > maxCharacters) ? errorMessage : $"{errorWindow.GetMessageText} \n\n {errorMessage}";
            errorWindow.OpenAsMessage(text, ThemeOverseerMono.GetInstance().CurrentTheme, acceptText, errorWindow.Close);
        }
        
    }
}