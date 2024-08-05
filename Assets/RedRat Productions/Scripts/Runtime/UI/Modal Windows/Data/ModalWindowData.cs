using System;
using RedRats.Systems.Themes;

namespace RedRats.UI.ModalWindows
{
    /// <summary>
    /// Contains data needed for opening a modal window.
    /// </summary>
    public class ModalWindowData
    {
        private ThemeType theme;
        private string acceptButtonText;
        private string denyButtonText;
        private string specialButtonText;

        private Action onAcceptAction;
        private Action onDenyAction;
        private Action onSpecialAction;
        
        private string message;
        private string headerText;
        private ModalWindowLayoutType layout;

        private ModalWindowData() { }
        
        public void UpdateLayout(ModalWindowLayoutType newLayout) => layout = newLayout;

        public ThemeType Theme { get => theme; }
        public string AcceptButtonText { get => acceptButtonText; }
        public string DenyButtonText { get => denyButtonText; }
        public string SpecialButtonText { get => specialButtonText; }
        public Action OnAcceptAction { get => onAcceptAction; }
        public Action OnDenyAction { get => onDenyAction; }
        public Action OnSpecialAction { get => onSpecialAction; }
        public string Message { get => message; }
        public string HeaderText { get => headerText; }
        public ModalWindowLayoutType Layout { get =>layout; }

        public class Builder
        {
            private ModalWindowLayoutType layout = ModalWindowLayoutType.Message;
            private ThemeType theme = ThemeType.Current;
            private string headerText = "";
            private string message = "";
            private string acceptButtonText = "";
            private string denyButtonText = "";
            private string specialButtonText = "";

            private Action onAcceptAction;
            private Action onDenyAction;
            private Action onSpecialAction;
            
            public Builder WithTheme(ThemeType theme)
            {
                this.theme = theme;
                return this;
            }
            
            public Builder WithAcceptButton(string acceptButtonText, Action onAcceptAction = null)
            {
                this.acceptButtonText = acceptButtonText;
                this.onAcceptAction = onAcceptAction;
                return this;
            }
            
            public Builder WithDenyButton(string denyButtonText, Action onDenyAction = null)
            {
                this.denyButtonText = denyButtonText;
                this.onDenyAction = onDenyAction;
                return this;
            }
            
            public Builder WithSpecialButton(string specialButtonText, Action onSpecialAction = null)
            {
                this.specialButtonText = specialButtonText;
                this.onSpecialAction = onSpecialAction;
                return this;
            }

            public Builder WithMessage(string message)
            {
                this.message = message;
                return this;
            }
            
            public Builder WithHeaderText(string headerText)
            {
                this.headerText = headerText;
                return this;
            }
            
            public Builder WithLayout(ModalWindowLayoutType layout)
            {
                this.layout = layout;
                return this;
            }
            
            public ModalWindowData Build()
            {
                ModalWindowData modalWindow = new()
                {
                    theme = theme,
                    acceptButtonText = acceptButtonText,
                    denyButtonText = denyButtonText,
                    specialButtonText = specialButtonText,
                    onAcceptAction = onAcceptAction,
                    onDenyAction = onDenyAction,
                    onSpecialAction = onSpecialAction,
                    message = message,
                    headerText = headerText,
                    layout = layout
                };
                return modalWindow;
            }
        }
    }
}