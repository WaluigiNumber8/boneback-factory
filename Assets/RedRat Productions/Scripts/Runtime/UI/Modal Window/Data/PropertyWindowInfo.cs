using System;

namespace RedRats.UI.ModalWindows
{
    public class PropertyWindowInfo : ModalWindowInfoBase
    {
        private readonly string headerText;
        private readonly PropertyLayoutType layout;

        public PropertyWindowInfo(string headerText, PropertyLayoutType layout, ThemeType theme, string acceptButtonText, string denyButtonText, string specialButtonText, Action onAcceptAction, Action onDenyAction = null, Action onSpecialAction = null) : base(theme, acceptButtonText, denyButtonText, specialButtonText, onAcceptAction, onDenyAction, onSpecialAction)
        {
            this.headerText = headerText;
            this.layout = layout;
        }

        public PropertyWindowInfo(string headerText, PropertyLayoutType layout, ThemeType theme, string acceptButtonText, string denyButtonText, Action onAcceptAction, Action onDenyAction = null) : base(theme, acceptButtonText, denyButtonText, onAcceptAction, onDenyAction)
        {
            this.headerText = headerText;
            this.layout = layout;
        }

        public PropertyWindowInfo(string headerText, PropertyLayoutType layout, ThemeType theme, string acceptButtonText, Action onAcceptAction) : base(theme, acceptButtonText, onAcceptAction)
        {
            this.headerText = headerText;
            this.layout = layout;
        }

        public string HeaderText { get => headerText; }
        public PropertyLayoutType Layout { get =>layout; }
    }
}