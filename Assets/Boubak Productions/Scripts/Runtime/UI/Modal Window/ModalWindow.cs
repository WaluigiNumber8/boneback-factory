using System;
using System.ServiceModel.Configuration;
using BoubakProductions.Core;
using BoubakProductions.Safety;
using BoubakProductions.UI.Core;
using Rogium.Systems.ThemeSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BoubakProductions.UI
{
    /// <summary>
    /// Draws a modal window on the screen.
    /// </summary>
    public class ModalWindow : MonoBehaviour
    {
        [SerializeField] private string denyButtonDefault = "Cancel";
        [SerializeField] private string specialButtonDefault = "NONE";
        [SerializeField] private UIInfo ui;

        private bool isOpened;

        #region Open As Message

        /// <summary>
        /// Opens the Modal Window as a message window.
        /// </summary>
        /// <param name="message">Text in the header.</param>
        /// <param name="theme">The graphic theme of the window.</param>
        /// <param name="acceptButtonText">Text in the Accept Button.</param>
        /// <param name="onAcceptAction">Method, that happens when the Accept Button is clicked.</param>
        /// <param name="denyButtonClosesWindow">If on, the Deny Button just closes the window.</param>
        public void OpenAsMessage(string message, ThemeType theme, string acceptButtonText, Action onAcceptAction,
            bool denyButtonClosesWindow = false)
        {
            if (denyButtonClosesWindow)
                OpenAsMessage(message, theme, acceptButtonText, denyButtonDefault, specialButtonDefault, onAcceptAction,
                    Close);
            else
                OpenAsMessage(message, theme, acceptButtonText, denyButtonDefault, specialButtonDefault,
                    onAcceptAction);
        }

        /// <summary>
        /// Opens the Modal Window as a message window.
        /// </summary>
        /// <param name="message">Text in the header.</param>
        /// <param name="theme">The graphic theme of the window.</param>
        /// <param name="acceptButtonText">Text in the Accept Button.</param>
        /// /// <param name="denyButtonText">Text in the Deny Button.</param>
        /// <param name="onAcceptAction">Method, that happens when the Accept Button is clicked.</param>
        /// <param name="denyButtonClosesWindow">The method that runs when the modal window is closed.</param>
        public void OpenAsMessage(string message, ThemeType theme, string acceptButtonText, string denyButtonText,
            Action onAcceptAction, bool denyButtonClosesWindow = false)
        {
            if (denyButtonClosesWindow)
                OpenAsMessage(message, theme, acceptButtonText, denyButtonText, specialButtonDefault, onAcceptAction,
                    Close);
            else
                OpenAsMessage(message, theme, acceptButtonText, denyButtonText, specialButtonDefault, onAcceptAction);
        }

        /// <summary>
        /// Opens the Modal Window as a message window.
        /// </summary>
        /// <param name="message">Text in the header.</param>
        /// <param name="theme">The graphic theme of the window.</param>
        /// <param name="acceptButtonText">Text in the Accept Button.</param>
        /// <param name="denyButtonText">Text in the Deny Button.</param>
        /// <param name="specialButtonText">Text in the Special Button.</param>
        /// <param name="onAcceptAction">Method, that happens when the Accept Button is clicked.</param>
        /// <param name="onDenyAction">Method, that happens when the Dccept Button is clicked.</param>
        /// <param name="onSpecialAction">Method, that happens when the Special Button is clicked.</param>
        public void OpenAsMessage(string message, ThemeType theme, string acceptButtonText, string denyButtonText,
            string specialButtonText, Action onAcceptAction, Action onDenyAction = null, Action onSpecialAction = null)
        {
            SafetyNet.EnsureStringNotNullOrEmpty(message, "Modal Window Title");

            WindowSetup(theme);

            ui.layout.area.gameObject.SetActive(true);
            ui.layout.message.area.gameObject.SetActive(true);
            ui.layout.message.text.gameObject.SetActive(true);

            ui.layout.message.text.text = message;

            DrawFooter(acceptButtonText, denyButtonText, specialButtonText, onAcceptAction, onDenyAction,
                onSpecialAction);
            ui.area.GetComponent<RectTransform>().ForceUpdateRectTransforms();
            Canvas.ForceUpdateCanvases();
            ui.windowBox.gameObject.SetActive(true);
            isOpened = true;
        }

        #endregion

        #region Open As Properties

        /// <summary>
        /// Draws the window as a properties window.
        /// </summary>
        /// <param name="headerText">Text in the header.</param>
        /// <param name="theme">The graphic theme of the window.</param>
        /// <param name="acceptButtonText">Text in the Accept Button.</param>
        /// <param name="denyButtonText">Text in the Deny Button.</param>
        /// <param name="onAcceptAction">Method, that happens when the Accept Button is clicked.</param>
        /// <param name="denyActionClosesWindow">If true, clicking the deny button will just close the window. Otherwise is null.</param>
        public void OpenAsPropertiesColumn2(string headerText, ThemeType theme, string acceptButtonText,
            string denyButtonText, Action onAcceptAction, bool denyActionClosesWindow = false)
        {
            if (denyActionClosesWindow)
                OpenAsPropertiesColumn2(headerText, theme, acceptButtonText, denyButtonText, specialButtonDefault,
                    onAcceptAction, Close, null);
            else
                OpenAsPropertiesColumn2(headerText, theme, acceptButtonText, denyButtonText, specialButtonDefault,
                    onAcceptAction, null, null);
        }

        /// <summary>
        /// Draws the window as a properties window.
        /// </summary>
        /// <param name="headerText">Text in the header.</param>
        /// <param name="theme">The graphic theme of the window.</param>
        /// <param name="acceptButtonText">Text in the Accept Button.</param>
        /// <param name="denyButtonText">Text in the Deny Button.</param>
        /// <param name="onAcceptAction">Method, that happens when the Accept Button is clicked.</param>
        /// <param name="onDenyAction">Method, that happens when the Deny Button is clicked.</param>
        public void OpenAsPropertiesColumn2(string headerText, ThemeType theme, string acceptButtonText,
            string denyButtonText, Action onAcceptAction, Action onDenyAction)
        {
            OpenAsPropertiesColumn2(headerText, theme, acceptButtonText, denyButtonText, specialButtonDefault,
                onAcceptAction, onDenyAction, null);
        }

        /// <summary>
        /// Draws the window as a properties window.
        /// </summary>
        /// <param name="headerText">Text in the header.</param>
        /// <param name="theme">The graphic theme of the window.</param>
        /// <param name="acceptButtonText">Text in the Accept Button.</param>
        /// <param name="denyButtonText">Text in the Deny Button.</param>
        /// <param name="specialButtonText">Text in the Special Button.</param>
        /// <param name="onAcceptAction">Method, that happens when the Accept Button is clicked.</param>
        /// <param name="onDenyAction">Method, that happens when the Deny Button is clicked.</param>
        /// <param name="onSpecialAction">Method, that happens when the Special Button is clicked.</param>
        public void OpenAsPropertiesColumn2(string headerText, ThemeType theme, string acceptButtonText,
            string denyButtonText, string specialButtonText, Action onAcceptAction, Action onDenyAction,
            Action onSpecialAction)
        {
            WindowSetup(theme);
            DrawHeader(headerText);

            ui.layout.properties.area.gameObject.SetActive(true);
            ui.layout.properties.firstColumn.gameObject.SetActive(true);
            ui.layout.properties.secondColumn.gameObject.SetActive(true);
            ui.layout.properties.firstColumnContent.gameObject.SetActive(true);
            ui.layout.properties.secondColumnContent.gameObject.SetActive(true);

            ui.layout.area.gameObject.SetActive(true);

            DrawFooter(acceptButtonText, denyButtonText, specialButtonText, onAcceptAction, onDenyAction,
                onSpecialAction);
            ui.area.GetComponent<RectTransform>().ForceUpdateRectTransforms();
            Canvas.ForceUpdateCanvases();
            ui.windowBox.gameObject.SetActive(true);
            isOpened = true;
        }

        /// <summary>
        /// Draws the window as a properties window.
        /// </summary>
        /// <param name="headerText">Text in the header.</param>
        /// <param name="theme">The graphic theme of the window.</param>
        /// <param name="acceptButtonText">Text in the Accept Button.</param>
        /// <param name="denyButtonText">Text in the Deny Button.</param>
        /// <param name="onAcceptAction">Method, that happens when the Accept Button is clicked.</param>
        /// <param name="denyActionClosesWindow">If true, clicking the deny button will just close the window. Otherwise is null.</param>
        public void OpenAsPropertiesColumn1(string headerText, ThemeType theme, string acceptButtonText,
            string denyButtonText, Action onAcceptAction, bool denyActionClosesWindow = false)
        {
            if (denyActionClosesWindow)
                OpenAsPropertiesColumn1(headerText, theme, acceptButtonText, denyButtonText, specialButtonDefault,
                    onAcceptAction, Close, null);
            else
                OpenAsPropertiesColumn1(headerText, theme, acceptButtonText, denyButtonText, specialButtonDefault,
                    onAcceptAction, null, null);
        }

        /// <summary>
        /// Draws the window as a properties window.
        /// </summary>
        /// <param name="headerText">Text in the header.</param>
        /// <param name="theme">The graphic theme of the window.</param>
        /// <param name="acceptButtonText">Text in the Accept Button.</param>
        /// <param name="denyButtonText">Text in the Deny Button.</param>
        /// <param name="onAcceptAction">Method, that happens when the Accept Button is clicked.</param>
        /// <param name="onDenyAction">Method, that happens when the Deny Button is clicked.</param>
        public void OpenAsPropertiesColumn1(string headerText, ThemeType theme, string acceptButtonText,
            string denyButtonText, Action onAcceptAction, Action onDenyAction)
        {
            OpenAsPropertiesColumn1(headerText, theme, acceptButtonText, denyButtonText, specialButtonDefault,
                onAcceptAction, onDenyAction, null);
        }

        public void OpenAsPropertiesColumn1(string headerText, ThemeType theme, string acceptButtonText,
            string denyButtonText, string specialButtonText, Action onAcceptAction, Action onDenyAction,
            Action onSpecialAction)
        {
            WindowSetup(theme);
            DrawHeader(headerText);

            ui.layout.properties.area.gameObject.SetActive(true);
            ui.layout.properties.firstColumn.gameObject.SetActive(true);
            ui.layout.properties.secondColumn.gameObject.SetActive(false);
            ui.layout.properties.firstColumnContent.gameObject.SetActive(true);
            ui.layout.properties.secondColumnContent.gameObject.SetActive(false);

            ui.layout.area.gameObject.SetActive(true);

            DrawFooter(acceptButtonText, denyButtonText, specialButtonText, onAcceptAction, onDenyAction,
                onSpecialAction);
            ui.area.GetComponent<RectTransform>().ForceUpdateRectTransforms();
            Canvas.ForceUpdateCanvases();
            ui.windowBox.gameObject.SetActive(true);
        }

        #endregion

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
            ui.footer.area.gameObject.SetActive(true);

            ui.footer.acceptButtonText.text = acceptButtonText;
            ui.footer.OnAcceptButtonClick = onAcceptAction;

            bool usesDeny = (onDenyAction != null);
            ui.footer.denyButton.gameObject.SetActive(usesDeny);
            ui.footer.denyButtonText.text = denyButtonText;
            ui.footer.OnDenyButtonClick = onDenyAction;

            bool usesSpecial = (onSpecialAction != null);
            ui.footer.specialButton.gameObject.SetActive(usesSpecial);
            ui.footer.specialButtonText.text = specialButtonText;
            ui.footer.OnSpecialButtonClick = onSpecialAction;
        }

        #endregion

        /// <summary>
        /// Prepares the window.
        /// </summary>
        private void WindowSetup(ThemeType theme)
        {
            ui.header.area.gameObject.SetActive(false);
            ui.layout.area.gameObject.SetActive(false);
            ui.layout.message.area.gameObject.SetActive(false);
            ui.layout.properties.area.gameObject.SetActive(false);
            ui.footer.area.gameObject.SetActive(false);

            ui.area.gameObject.SetActive(true);
            ui.background.gameObject.SetActive(true);
            ui.windowBox.gameObject.SetActive(false);
            ThemeUpdater.UpdateModalWindow(this, theme);
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
        public void UpdateTheme(Sprite backgroundSprite, Sprite headerSprite, InteractableInfo buttonSet,
            FontInfo headerFont, FontInfo textFont)
        {
            UIExtensions.ChangeInteractableSprites(ui.footer.acceptButton, ui.footer.acceptButtonImage, buttonSet);
            UIExtensions.ChangeInteractableSprites(ui.footer.denyButton, ui.footer.denyButtonImage, buttonSet);
            UIExtensions.ChangeInteractableSprites(ui.footer.specialButton, ui.footer.specialButtonImage, buttonSet);
            UIExtensions.ChangeFont(ui.header.text, headerFont);
            UIExtensions.ChangeFont(ui.layout.message.text, textFont);
            ui.windowBoxImage.sprite = backgroundSprite;
            ui.header.headerImage.sprite = headerSprite;
        }

        /// <summary>
        /// Stores actions, that will take place once the Accept button is clicked.
        /// </summary>
        public void OnAccept()
        {
            ui.footer.OnAcceptButtonClick?.Invoke();
            Close();
        }

        /// <summary>
        /// Stores actions, that will take place once the Deny button is clicked.
        /// </summary>
        public void OnDeny()
        {
            ui.footer.OnDenyButtonClick?.Invoke();
            Close();
        }

        /// <summary>
        /// Stores actions, that will take place once the Special button is clicked.
        /// </summary>
        public void OnSpecial()
        {
            ui.footer.OnSpecialButtonClick?.Invoke();
            Close();
        }

        /// <summary>
        /// Closes the Window.
        /// </summary>
        public void Close()
        {
            ui.windowBox.gameObject.SetActive(false);

            ui.layout.message.area.gameObject.SetActive(false);

            ui.layout.properties.area.gameObject.SetActive(false);
            ui.layout.properties.firstColumnContent.gameObject.KillChildren();
            ui.layout.properties.secondColumnContent.gameObject.KillChildren();

            ui.background.gameObject.SetActive(false);
            ui.area.gameObject.SetActive(false);
            isOpened = false;
        }

        /// <summary>
        /// Get the text from
        /// </summary>
        public string GetMessageText => ui.layout.message.text.text;


        public Transform FirstColumnContent { get =>ui.layout.properties.firstColumnContent; }
        public Transform SecondColumnContent {get => ui.layout.properties.secondColumnContent; }
        public bool IsOpened { get => isOpened; }

        [Serializable]
        public struct UIInfo
        {
            public Transform area;
            public Image background;
            public Transform windowBox;
            public Image windowBoxImage;
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
