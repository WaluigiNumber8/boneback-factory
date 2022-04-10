using BoubakProductions.Safety;
using Rogium.Systems.ThemeSystem;
using UnityEngine;

namespace BoubakProductions.UI.ErrorMessageWindow
{
    /// <summary>
    /// Controls the availability of the Error Message Window.
    /// </summary>
    public class ErrorMessageController : MonoBehaviour
    {
        [SerializeField] private ModalWindow errorWindow;
        [SerializeField] private string acceptText;
        
        private void OnEnable()
        {
            SafetyNet.OnFireErrorMessage += Open;
            SafetyNetIO.OnFireErrorMessage += Open;
            SafetyNetUnity.OnFireErrorMessage += Open;
        }

        private void OnDisable()
        {
            SafetyNet.OnFireErrorMessage -= Open;
            SafetyNetIO.OnFireErrorMessage -= Open;
            SafetyNetUnity.OnFireErrorMessage -= Open;
        }

        /// <summary>
        /// Shows the Error Message Window.
        /// </summary>
        /// <param name="errorMessage">The message of the error.</param>
        private void Open(string errorMessage)
        {
            if (errorWindow.IsOpened) return;
            errorWindow.OpenAsMessage(errorMessage, ThemeOverseerMono.GetInstance().CurrentTheme, acceptText, errorWindow.Close);
        }
        
    }
}