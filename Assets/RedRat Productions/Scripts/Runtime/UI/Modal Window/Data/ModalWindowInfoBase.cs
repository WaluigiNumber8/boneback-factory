using System;
using Rogium.Systems.ThemeSystem;

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

        /// <summary>
        /// Stores data for a message type modal window.
        /// </summary>
        /// <param name="theme">The graphic theme of the window.</param>
        /// <param name="acceptButtonText">Text in the Accept Button.</param>
        /// <param name="denyButtonText">Text in the Deny Button.</param>
        /// <param name="specialButtonText">Text in the Special Button.</param>
        /// <param name="onAcceptAction">Method, that happens when the Accept Button is clicked. If is NULL, the window will only close itself.</param>
        /// <param name="onDenyAction">Method, that happens when the Deny Button is clicked. If is NULL, the window will only close itself.</param>
        /// <param name="onSpecialAction">Method, that happens when the Special Button is clicked. If is NULL, the window will only close itself.</param>
        public ModalWindowInfoBase(ThemeType theme, string acceptButtonText, string denyButtonText, string specialButtonText, Action onAcceptAction = null, Action onDenyAction = null, Action onSpecialAction = null)
        {
            this.theme = theme;
            this.acceptButtonText = acceptButtonText;
            this.denyButtonText = denyButtonText;
            this.specialButtonText = specialButtonText;
            this.onAcceptAction = onAcceptAction;
            this.onDenyAction = onDenyAction;
            this.onSpecialAction = onSpecialAction;
        }
        
        /// <summary>
        /// Stores data for a message type modal window.
        /// </summary>
        /// <param name="theme">The graphic theme of the window.</param>
        /// <param name="acceptButtonText">Text in the Accept Button.</param>
        /// <param name="denyButtonText">Text in the Deny Button.</param>
        /// <param name="onAcceptAction">Method, that happens when the Accept Button is clicked. If is NULL, the window will only close itself.</param>
        /// <param name="onDenyAction">Method, that happens when the Deny Button is clicked. If is NULL, the window will only close itself.</param>
        public ModalWindowInfoBase(ThemeType theme, string acceptButtonText, string denyButtonText, Action onAcceptAction = null, Action onDenyAction = null)
        {
            this.theme = theme;
            this.acceptButtonText = acceptButtonText;
            this.denyButtonText = denyButtonText;
            this.specialButtonText = "";
            this.onAcceptAction = onAcceptAction;
            this.onDenyAction = onDenyAction;
            this.onSpecialAction = null;
        }

        /// <summary>
        /// Stores data for a message type modal window.
        /// </summary>
        /// <param name="theme">The graphic theme of the window.</param>
        /// <param name="acceptButtonText">Text in the Accept Button.</param>
        /// <param name="onAcceptAction">Method, that happens when the Accept Button is clicked. If is NULL, the window will only close itself.</param>
        public ModalWindowInfoBase(ThemeType theme, string acceptButtonText, Action onAcceptAction = null)
        {
            this.theme = theme;
            this.acceptButtonText = acceptButtonText;
            this.denyButtonText = "";
            this.specialButtonText = "";
            this.onAcceptAction = onAcceptAction;
            this.onDenyAction = null;
            this.onSpecialAction = null;
        }
        
        /// <summary>
        /// Stores data for a message type modal window.
        /// </summary>
        /// <param name="acceptButtonText">Text in the Accept Button.</param>
        /// <param name="onAcceptAction">Method, that happens when the Accept Button is clicked. If is NULL, the window will only close itself.</param>
        public ModalWindowInfoBase(string acceptButtonText, Action onAcceptAction = null)
        {
            this.theme = ThemeOverseerMono.GetInstance().CurrentTheme;
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