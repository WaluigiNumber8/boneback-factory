using RedRats.Core;
using RedRats.Safety;
using RedRats.UI.ModalWindows;
using UnityEngine;

namespace RedRats.UI.ErrorMessageWindow
{
    /// <summary>
    /// Controls the availability of the Error Message Window.
    /// </summary>
    public class ErrorMessageController : MonoSingleton<ErrorMessageController>
    {
        [SerializeField] private string acceptText;
        [SerializeField] private ModalWindowGenerator windowGenerator;

        private void OnEnable()
        {
            Preconditions.OnFireErrorMessage += Open;
            PreconditionsIO.OnFireErrorMessage += Open;
        }

        private void OnDisable()
        {
            Preconditions.OnFireErrorMessage -= Open;
            PreconditionsIO.OnFireErrorMessage -= Open;
        }

        /// <summary>
        /// Shows the Error Message Window.
        /// </summary>
        /// <param name="errorMessage">The message of the error.</param>
        public void Open(string errorMessage)
        {
            windowGenerator.Open(new ModalWindowData.Builder()
                .WithMessage(errorMessage)
                .WithAcceptButton(acceptText)
                .Build());
        }
    }
}