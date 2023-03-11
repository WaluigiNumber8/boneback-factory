using System;

namespace RedRats.UI.ModalWindows
{
    /// <summary>
    /// Contains data needed for opening a message modal window.
    /// </summary>
    public class MessageWindowInfo : ModalWindowInfoBase
    {
        private readonly string message;

        public MessageWindowInfo(string message, ThemeType theme, string acceptButtonText, string denyButtonText, string specialButtonText, Action onAcceptAction, Action onDenyAction = null, Action onSpecialAction = null) : base(theme, acceptButtonText, denyButtonText, specialButtonText, onAcceptAction, onDenyAction, onSpecialAction)
        {
            this.message = message;
        }

        public MessageWindowInfo(string message, ThemeType theme, string acceptButtonText, string denyButtonText, Action onAcceptAction, Action onDenyAction = null) : base(theme, acceptButtonText, denyButtonText, onAcceptAction, onDenyAction)
        {
            this.message = message;
        }

        public MessageWindowInfo(string message, ThemeType theme, string acceptButtonText, Action onAcceptAction) : base(theme, acceptButtonText, onAcceptAction)
        {
            this.message = message;
        }

        public string Message { get => message; }
    }
}