using RedRats.Core;
using RedRats.Safety;
using RedRats.UI.ModalWindows;
using Rogium.Systems.ThemeSystem;
using UnityEngine;

namespace RedRats.UI.ErrorMessageWindow
{
    /// <summary>
    /// Controls the availability of the Error Message Window.
    /// </summary>
    public class ErrorMessageController : MonoSingleton<ErrorMessageController>
    {
        [SerializeField] private string acceptText;

        private ModalWindowOverseer windowOverseer;

        protected override void Awake()
        {
            base.Awake();
            windowOverseer = ModalWindowOverseer.GetInstance();
        }

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
            windowOverseer.OpenWindow(new MessageWindowInfo(errorMessage, ThemeOverseerMono.GetInstance().CurrentTheme, acceptText));
        }
    }
}