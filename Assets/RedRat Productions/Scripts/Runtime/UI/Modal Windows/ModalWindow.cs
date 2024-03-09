using System;
using RedRats.Core;
using RedRats.Safety;
using RedRats.Systems.Themes;
using RedRats.UI.Core;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RedRats.UI.ModalWindows
{
    /// <summary>
    /// Draws a modal window on the screen.
    /// </summary>
    public class ModalWindow : ModalWindowBase
    {
        [SerializeField] private UIInfo ui;
        private ThemeType lastTheme = ThemeType.Current;

        /// <summary>
        /// Opens the Modal Window as a message window.
        /// </summary>
        /// <param name="data">Data used to create the window.</param>
        public void OpenAsMessage(MessageWindowInfo data)
        {
            SafetyNet.EnsureStringNotNullOrEmpty(data.Message, "Modal Window Title");

            WindowSetup(data.Theme);

            ui.layout.area.gameObject.SetActive(true);
            ui.layout.message.area.gameObject.SetActive(true);
            ui.layout.message.text.gameObject.SetActive(true);

            ui.layout.message.text.text = data.Message;

            DrawFooter(data.AcceptButtonText, data.DenyButtonText, data.SpecialButtonText, data.OnAcceptAction, 
                       data.OnDenyAction, data.OnSpecialAction);
            UpdateRect();
            Open();
        }

        /// <summary>
        /// Draws the window as a properties window with 2 columns.
        /// </summary>
        /// <param name="data">Data used to create the window.</param>
        public void OpenAsPropertiesColumn2(PropertyWindowInfo data)
        {
            WindowSetup(data.Theme);
            DrawHeader(data.HeaderText);

            ui.layout.properties.area.gameObject.SetActive(true);
            ui.layout.properties.firstColumn.gameObject.SetActive(true);
            ui.layout.properties.secondColumn.gameObject.SetActive(true);
            ui.layout.properties.firstColumnContent.gameObject.SetActive(true);
            ui.layout.properties.secondColumnContent.gameObject.SetActive(true);

            ui.layout.area.gameObject.SetActive(true);

            DrawFooter(data.AcceptButtonText, data.DenyButtonText, data.SpecialButtonText, data.OnAcceptAction, 
                       data.OnDenyAction, data.OnSpecialAction);
            UpdateRect();
            Open();
        }

        /// <summary>
        /// Draws the window as a properties window with 1 column.
        /// </summary>
        /// <param name="data">Data used to create the window.</param>
        public void OpenAsPropertiesColumn1(PropertyWindowInfo data)
        {
            WindowSetup(data.Theme);
            DrawHeader(data.HeaderText);

            ui.layout.properties.area.gameObject.SetActive(true);
            ui.layout.properties.firstColumn.gameObject.SetActive(true);
            ui.layout.properties.secondColumn.gameObject.SetActive(false);
            ui.layout.properties.firstColumnContent.gameObject.SetActive(true);
            ui.layout.properties.secondColumnContent.gameObject.SetActive(false);

            ui.layout.area.gameObject.SetActive(true);

            DrawFooter(data.AcceptButtonText, data.DenyButtonText, data.SpecialButtonText, data.OnAcceptAction, 
                       data.OnDenyAction, data.OnSpecialAction);
            UpdateRect();
            Open();
        }

        /// <summary>
        /// Update the message of the window.
        /// </summary>
        /// <param name="newMessage">The new text to use.</param>
        public void UpdateMessageText(string newMessage)
        {
            ui.layout.message.text.text = newMessage;
        }

        /// <summary>
        /// Fills the Modal Window with a proper theme.
        /// </summary>
        /// <param name="backgroundSprite">The background of the window.</param>
        /// <param name="headerSprite">The bar around the header.</param>
        /// <param name="buttonSet">Buttons of teh window.</param>
        /// <param name="headerFont">Font of the header text.</param>
        /// <param name="textFont">Font of text in the content section.</param>
        public void UpdateTheme(Sprite backgroundSprite, Sprite headerSprite, InteractableSpriteInfo buttonSet, FontInfo headerFont, FontInfo textFont)
        {
            UIExtensions.ChangeInteractableSprites(ui.footer.acceptButton, ui.footer.acceptButtonImage, buttonSet);
            UIExtensions.ChangeInteractableSprites(ui.footer.denyButton, ui.footer.denyButtonImage, buttonSet);
            UIExtensions.ChangeInteractableSprites(ui.footer.specialButton, ui.footer.specialButtonImage, buttonSet);
            UIExtensions.ChangeFont(ui.header.text, headerFont);
            UIExtensions.ChangeFont(ui.layout.message.text, textFont);
            UIExtensions.ChangeFont(ui.footer.acceptButtonText, headerFont);
            UIExtensions.ChangeFont(ui.footer.denyButtonText, headerFont);
            UIExtensions.ChangeFont(ui.footer.specialButtonText, headerFont);
            generalUI.windowArea.sprite = backgroundSprite;
            ui.header.headerImage.sprite = headerSprite;
        }

        /// <summary>
        /// Stores actions, that will take place once the Accept button is clicked.
        /// </summary>
        public void OnAccept()
        {
            ui.footer.OnAcceptButtonClick?.Invoke();
            CloseWindow();
        }

        /// <summary>
        /// Stores actions, that will take place once the Deny button is clicked.
        /// </summary>
        public void OnDeny()
        {
            ui.footer.OnDenyButtonClick?.Invoke();
            CloseWindow();
        }

        /// <summary>
        /// Stores actions, that will take place once the Special button is clicked.
        /// </summary>
        public void OnSpecial()
        {
            ui.footer.OnSpecialButtonClick?.Invoke();
            CloseWindow();
        }

        /// <summary>
        /// Closes the Window.
        /// </summary>
        private void CloseWindow()
        {
            Close();
            ui.layout.message.area.gameObject.SetActive(false);
            ui.layout.properties.area.gameObject.SetActive(false);
            ui.layout.properties.firstColumnContent.gameObject.KillChildren();
            ui.layout.properties.secondColumnContent.gameObject.KillChildren();
        }

        protected override void UpdateTheme() => ThemeUpdater.UpdateModalWindow(this, lastTheme);

        #region Window Part Drawing

        /// <summary>
        /// Draws the Header part of the Window.
        /// </summary>
        /// <param name="headerText"></param>
        private void DrawHeader(string headerText)
        {
            ui.header.text.text = headerText;
            ui.header.text.gameObject.SetActive(true);
            ui.header.area.gameObject.SetActive(true);
        }

        /// <summary>
        /// Draws the Footer of the Window.
        /// </summary>
        /// <param name="acceptButtonText">Text in the Accept Button.</param>
        /// <param name="denyButtonText">Text in the Deny Button.</param>
        /// <param name="specialButtonText">Text in the Special Button.</param>
        /// <param name="onAcceptAction">Method, that happens when the Accept Button is clicked.</param>
        /// <param name="onDenyAction">Method, that happens when the Dccept Button is clicked.</param>
        /// <param name="onSpecialAction">Method, that happens when the Special Button is clicked.</param>
        private void DrawFooter(string acceptButtonText, string denyButtonText, string specialButtonText,
                                Action onAcceptAction, Action onDenyAction, Action onSpecialAction)
        {
            ui.footer.acceptButtonText.text = acceptButtonText;
            ui.footer.OnAcceptButtonClick = onAcceptAction;

            bool usesDeny = !string.IsNullOrEmpty(denyButtonText);
            ui.footer.denyButton.gameObject.SetActive(usesDeny);
            ui.footer.denyButtonText.text = denyButtonText;
            ui.footer.OnDenyButtonClick = onDenyAction;

            bool usesSpecial = !string.IsNullOrEmpty(specialButtonText);
            ui.footer.specialButton.gameObject.SetActive(usesSpecial);
            ui.footer.specialButtonText.text = specialButtonText;
            ui.footer.OnSpecialButtonClick = onSpecialAction;
            
            ui.footer.area.gameObject.SetActive(true);
        }

        #endregion

        /// <summary>
        /// Prepares the window.
        /// </summary>
        private void WindowSetup(ThemeType theme)
        {
            lastTheme = theme;
            ui.header.area.gameObject.SetActive(false);
            ui.layout.area.gameObject.SetActive(false);
            ui.layout.message.area.gameObject.SetActive(false);
            ui.layout.properties.area.gameObject.SetActive(false);
            ui.footer.area.gameObject.SetActive(false);
        }
        
        private void UpdateRect()
        {
            generalUI.windowArea.GetComponent<RectTransform>().ForceUpdateRectTransforms();
            Canvas.ForceUpdateCanvases();
        }

        /// <summary>
        /// Get the message text.
        /// </summary>
        public string GetMessageText => ui.layout.message.text.text;
        public Transform FirstColumnContent { get =>ui.layout.properties.firstColumnContent; }
        public Transform SecondColumnContent {get => ui.layout.properties.secondColumnContent; }

        [Serializable]
        public struct UIInfo
        {
            public HeaderInfo header;
            public LayoutInfo layout;
            public FooterInfo footer;
        }
        
        [Serializable]
        public struct HeaderInfo
        {
            public Transform area;
            public Image headerImage;
            public TextMeshProUGUI text;
        }

        [Serializable]
        public struct LayoutInfo
        {
            public Transform area;
            public MessageLayoutInfo message;
            public PropertiesLayoutInfo properties;
        }

        [Serializable]
        public struct MessageLayoutInfo
        {
            public Transform area;
            public TextMeshProUGUI text;
        }

        [Serializable]
        public struct PropertiesLayoutInfo
        {
            public Transform area;
            public Transform firstColumn;
            public Transform secondColumn;
            public Transform firstColumnContent;
            public Transform secondColumnContent;
        }

        [Serializable]
        public struct FooterInfo
        {
            public Transform area;
            public TextMeshProUGUI acceptButtonText;
            public TextMeshProUGUI denyButtonText;
            public TextMeshProUGUI specialButtonText;
            public Image acceptButtonImage;
            public Image denyButtonImage;
            public Image specialButtonImage;
            public Button acceptButton;
            public Button denyButton;
            public Button specialButton;

            public Action OnAcceptButtonClick;
            public Action OnDenyButtonClick;
            public Action OnSpecialButtonClick;
        }
    }
}
