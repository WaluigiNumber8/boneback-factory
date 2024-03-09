using System;
using RedRats.Systems.Themes;

namespace RedRats.UI.ModalWindows
{
    /// <summary>
    /// Contains data needed for opening a properties modal window.
    /// </summary>
    public class PropertyWindowInfo : ModalWindowInfoBase
    {
        private readonly string headerText;
        private readonly PropertyLayoutType layout;

        /// <summary>
        ///Stores data for a properties type modal window.
        /// </summary>
        /// <param name="headerText">Text in the header.</param>
        /// <param name="layout">The type of layout the window uses.</param>
        /// <param name="acceptButtonText">Text in the Accept Button.</param>
        /// <param name="denyButtonText">Text in the Deny Button.</param>
        /// <param name="specialButtonText">Text in the Special Button.</param>
        /// <param name="onAcceptAction">Method, that happens when the Accept Button is clicked. If is NULL, the window will only close itself.</param>
        /// <param name="onDenyAction">Method, that happens when the Deny Button is clicked. If is NULL, the window will only close itself.</param>
        /// <param name="theme">The graphic theme of the window.</param>
        /// <param name="onSpecialAction">Method, that happens when the Special Button is clicked. If is NULL, the window will only close itself.</param>
        public PropertyWindowInfo(string headerText, PropertyLayoutType layout, string acceptButtonText, string denyButtonText, string specialButtonText, Action onAcceptAction = null, Action onDenyAction = null, Action onSpecialAction = null, ThemeType theme = ThemeType.Current) : base(acceptButtonText, denyButtonText, specialButtonText, onAcceptAction, onDenyAction, onSpecialAction, theme)
        {
            this.headerText = headerText;
            this.layout = layout;
        }

        /// <summary>
        ///Stores data for a properties type modal window.
        /// </summary>
        /// <param name="headerText">Text in the header.</param>
        /// <param name="layout">The type of layout the window uses.</param>
        /// <param name="acceptButtonText">Text in the Accept Button.</param>
        /// <param name="denyButtonText">Text in the Deny Button.</param>
        /// <param name="onAcceptAction">Method, that happens when the Accept Button is clicked.  If is NULL, the window will only close itself.</param>
        /// <param name="onDenyAction">Method, that happens when the Deny Button is clicked.  If is NULL, the window will only close itself.</param>
        /// <param name="theme">The graphic theme of the window.</param>
        public PropertyWindowInfo(string headerText, PropertyLayoutType layout, string acceptButtonText, string denyButtonText, Action onAcceptAction = null, Action onDenyAction = null, ThemeType theme = ThemeType.Current) : base(acceptButtonText, denyButtonText, onAcceptAction, onDenyAction, theme)
        {
            this.headerText = headerText;
            this.layout = layout;
        }

        /// <summary>
        ///Stores data for a properties type modal window.
        /// </summary>
        /// <param name="headerText">Text in the header.</param>
        /// <param name="layout">The type of layout the window uses.</param>
        /// <param name="acceptButtonText">Text in the Accept Button.</param>
        /// <param name="onAcceptAction">Method, that happens when the Accept Button is clicked.  If is NULL, the window will only close itself.</param>
        /// <param name="theme">The graphic theme of the window.</param>
        public PropertyWindowInfo(string headerText, PropertyLayoutType layout, string acceptButtonText, Action onAcceptAction = null, ThemeType theme = ThemeType.Current) : base(acceptButtonText, onAcceptAction, theme)
        {
            this.headerText = headerText;
            this.layout = layout;
        }
        
        /// <summary>
        ///Stores data for a properties type modal window.
        /// </summary>
        /// <param name="headerText">Text in the header.</param>
        /// <param name="layout">The type of layout the window uses.</param>
        /// <param name="acceptButtonText">Text in the Accept Button.</param>
        /// <param name="onAcceptAction">Method, that happens when the Accept Button is clicked.  If is NULL, the window will only close itself.</param>
        public PropertyWindowInfo(string headerText, PropertyLayoutType layout, string acceptButtonText, Action onAcceptAction = null) : base(acceptButtonText, onAcceptAction)
        {
            this.headerText = headerText;
            this.layout = layout;
        }

        public string HeaderText { get => headerText; }
        public PropertyLayoutType Layout { get =>layout; }
    }
}