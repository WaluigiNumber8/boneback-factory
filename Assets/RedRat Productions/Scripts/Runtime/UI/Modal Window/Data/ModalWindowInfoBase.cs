using System;

namespace RedRats.UI.ModalWindows
{
    /// <summary>
    /// Contains data needed for opening a generic modal window.
    /// </summary>
    public abstract class ModalWindowInfoBase
    {
        private readonly ThemeType theme;
        private readonly string acceptButtonText;
        private readonly string denyButtonText;
        private readonly string specialButtonText;

        private readonly Action onAcceptAction;
        private readonly Action onDenyAction;
        private readonly Action onSpecialAction;

        public ModalWindowInfoBase(ThemeType theme, string acceptButtonText, string denyButtonText, string specialButtonText, Action onAcceptAction, Action onDenyAction = null, Action onSpecialAction = null)
        {
            this.theme = theme;
            this.acceptButtonText = acceptButtonText;
            this.denyButtonText = denyButtonText;
            this.specialButtonText = specialButtonText;
            this.onAcceptAction = onAcceptAction;
            this.onDenyAction = onDenyAction;
            this.onSpecialAction = onSpecialAction;
        }
        
        public ModalWindowInfoBase(ThemeType theme, string acceptButtonText, string denyButtonText, Action onAcceptAction, Action onDenyAction = null)
        {
            this.theme = theme;
            this.acceptButtonText = acceptButtonText;
            this.denyButtonText = denyButtonText;
            this.specialButtonText = "";
            this.onAcceptAction = onAcceptAction;
            this.onDenyAction = onDenyAction;
            this.onSpecialAction = null;
        }
        
        public ModalWindowInfoBase(ThemeType theme, string acceptButtonText, Action onAcceptAction)
        {
            this.theme = theme;
            this.acceptButtonText = acceptButtonText;
            this.denyButtonText = "";
            this.specialButtonText = "";
            this.onAcceptAction = onAcceptAction;
            this.onDenyAction = null;
            this.onSpecialAction = null;
        }

        public ThemeType Theme { get => theme; }
        public string AcceptButtonText { get => acceptButtonText; }
        public string DenyButtonText { get => denyButtonText; }
        public string SpecialButtonText { get => specialButtonText; }
        public Action OnAcceptAction { get => onAcceptAction; }
        public Action OnDenyAction { get => onDenyAction; }
        public Action OnSpecialAction { get => onSpecialAction; }
    }
}