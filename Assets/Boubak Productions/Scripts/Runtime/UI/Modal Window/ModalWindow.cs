using System;
using BoubakProductions.Core;
using BoubakProductions.Safety;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoubakProductions.UI
{
    /// <summary>
    /// Draws a modal window on the screen.
    /// </summary>
    public class ModalWindow : MonoBehaviour
    {
        [Header("Defaults")]
        [SerializeField] private string acceptButtonDefault = "Confirm";
        [SerializeField] private string denyButtonDefault = "Cancel";
        [SerializeField] private string specialButtonDefault = "NONE";

        [SerializeField] private UIInfo ui;
        [SerializeField] private ThemeStyleInfo[] themes;

        private ThemeType currentTheme;
        
        #region Open As Message

        /// <summary>
        /// Opens the Modal Window as a message window.
        /// </summary>
        /// <param name="message">Text in the header.</param>
        /// <param name="theme">The graphic theme of the window.</param>
        /// <param name="acceptButtonText">Text in the Accept Button.</param>
        /// <param name="onAcceptAction">Method, that happens when the Accept Button is clicked.</param>
        /// <param name="denyButtonClosesWindow">If on, the Deny Button just closes the window.</param>
        public void OpenAsMessage(string message, ThemeType theme, string acceptButtonText, Action onAcceptAction, bool denyButtonClosesWindow = false)
        {
            if (denyButtonClosesWindow)
                OpenAsMessage(message, theme, acceptButtonText, denyButtonDefault, specialButtonDefault, onAcceptAction, Close);
            else
                OpenAsMessage(message, theme, acceptButtonText, denyButtonDefault, specialButtonDefault, onAcceptAction);
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
        public void OpenAsMessage(string message, ThemeType theme, string acceptButtonText, string denyButtonText, Action onAcceptAction, bool denyButtonClosesWindow = false)
        {
            if (denyButtonClosesWindow)
                OpenAsMessage(message, theme, acceptButtonText, denyButtonText, specialButtonDefault, onAcceptAction, Close);
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
        public void OpenAsMessage(string message, ThemeType theme, string acceptButtonText, string denyButtonText, string specialButtonText, Action onAcceptAction, Action onDenyAction = null, Action onSpecialAction = null)
        {
            SafetyNet.EnsureStringNotNullOrEmpty(message, "Modal Window Title");
            
            WindowSetup(theme);

            ui.layout.area.gameObject.SetActive(true);
            ui.layout.message.area.gameObject.SetActive(true);
            ui.layout.message.text.gameObject.SetActive(true);

            ui.layout.message.text.text = message;

            DrawFooter(acceptButtonText, denyButtonText, specialButtonText, onAcceptAction, onDenyAction, onSpecialAction);
            ui.area.GetComponent<RectTransform>().ForceUpdateRectTransforms();
            ui.windowBox.gameObject.SetActive(true);
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
        public void OpenAsPropertiesColumn2(string headerText, ThemeType theme, string acceptButtonText, string denyButtonText, Action onAcceptAction, bool denyActionClosesWindow = false)
        {
            if (denyActionClosesWindow)
                OpenAsPropertiesColumn2(headerText, theme, acceptButtonText, denyButtonText, specialButtonDefault, onAcceptAction, Close, null);
            else
                OpenAsPropertiesColumn2(headerText, theme, acceptButtonText, denyButtonText, specialButtonDefault, onAcceptAction, null, null);
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
        public void OpenAsPropertiesColumn2(string headerText, ThemeType theme, string acceptButtonText, string denyButtonText, Action onAcceptAction, Action onDenyAction)
        {
            OpenAsPropertiesColumn2(headerText, theme, acceptButtonText, denyButtonText, specialButtonDefault, onAcceptAction, onDenyAction, null);
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
        public void OpenAsPropertiesColumn2(string headerText, ThemeType theme, string acceptButtonText, string denyButtonText, string specialButtonText, Action onAcceptAction, Action onDenyAction, Action onSpecialAction)
        {
            WindowSetup(theme);
            DrawHeader(headerText);

            ui.layout.properties.area.gameObject.SetActive(true);
            ui.layout.properties.firstColumn.gameObject.SetActive(true);
            ui.layout.properties.secondColumn.gameObject.SetActive(true);
            ui.layout.properties.firstColumnContent.gameObject.SetActive(true);
            ui.layout.properties.secondColumnContent.gameObject.SetActive(true);

            ui.layout.area.gameObject.SetActive(true);

            DrawFooter(acceptButtonText, denyButtonText, specialButtonText, onAcceptAction, onDenyAction, onSpecialAction);
            ui.area.GetComponent<RectTransform>().ForceUpdateRectTransforms();
            ui.windowBox.gameObject.SetActive(true);
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
        public void OpenAsPropertiesColumn1(string headerText, ThemeType theme, string acceptButtonText, string denyButtonText, Action onAcceptAction, bool denyActionClosesWindow = false)
        {
            if (denyActionClosesWindow)
                OpenAsPropertiesColumn1(headerText, theme, acceptButtonText, denyButtonText, specialButtonDefault, onAcceptAction, Close, null);
            else
                OpenAsPropertiesColumn1(headerText, theme, acceptButtonText, denyButtonText, specialButtonDefault, onAcceptAction, null, null);
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
        public void OpenAsPropertiesColumn1(string headerText, ThemeType theme, string acceptButtonText, string denyButtonText, Action onAcceptAction, Action onDenyAction)
        {
            OpenAsPropertiesColumn1(headerText, theme, acceptButtonText, denyButtonText, specialButtonDefault, onAcceptAction, onDenyAction, null);
        }
        public void OpenAsPropertiesColumn1(string headerText, ThemeType theme, string acceptButtonText, string denyButtonText, string specialButtonText, Action onAcceptAction, Action onDenyAction, Action onSpecialAction)
        {
            WindowSetup(theme);
            DrawHeader(headerText);

            ui.layout.properties.area.gameObject.SetActive(true);
            ui.layout.properties.firstColumn.gameObject.SetActive(true);
            ui.layout.properties.secondColumn.gameObject.SetActive(false);
            ui.layout.properties.firstColumnContent.gameObject.SetActive(true);
            ui.layout.properties.secondColumnContent.gameObject.SetActive(false);

            ui.layout.area.gameObject.SetActive(true);

            DrawFooter(acceptButtonText, denyButtonText, specialButtonText, onAcceptAction, onDenyAction, onSpecialAction);
            ui.area.GetComponent<RectTransform>().ForceUpdateRectTransforms();
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
        private void DrawFooter(string acceptButtonText, string denyButtonText, string specialButtonText, Action onAcceptAction, Action onDenyAction, Action onSpecialAction)
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
            ui.footer.area.gameObject.SetActive(false);

            ui.area.gameObject.SetActive(true);
            ui.background.gameObject.SetActive(true);
            ui.windowBox.gameObject.SetActive(false);
            FillWithTheme(theme);
        }

        #region Themes

        /// <summary>
        /// Fills the Modal Window with a proper theme.
        /// </summary>
        /// <param name="themeType">The theme to load.</param>
        private void FillWithTheme(ThemeType themeType)
        {
            if (themeType == currentTheme && themeType != ThemeType.NoTheme) return;

            ThemeStyleInfo theme = themes[(int)themeType];

            ui.windowBoxImage.sprite = theme.backgroundImage;
            ui.header.headerImage.sprite = theme.headerImage;
            FillWithThemeButton(theme, ui.footer.acceptButtonImage, ui.footer.acceptButton);
            FillWithThemeButton(theme, ui.footer.denyButtonImage, ui.footer.denyButton);
            FillWithThemeButton(theme, ui.footer.specialButtonImage, ui.footer.specialButton);
            
            currentTheme = themeType;
        }

        /// <summary>
        /// Applies a theme to a button.
        /// </summary>
        /// <param name="theme">The theme to apply.</param>
        /// <param name="buttonImage">Button's Image Component</param>
        /// <param name="button">The Button itself.</param>
        private void FillWithThemeButton(ThemeStyleInfo theme, Image buttonImage, Button button)
        {
            buttonImage.sprite = theme.buttonNormal;
            SpriteState ss = new SpriteState();
            ss.highlightedSprite = theme.buttonHighlight;
            ss.pressedSprite = theme.buttonPress;
            ss.selectedSprite = theme.buttonSelected;
            ss.disabledSprite = theme.buttonDisabled;
            button.spriteState = ss;
        }

        #endregion
        
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
        }

        public Transform FirstColumnContent => ui.layout.properties.firstColumnContent;
        public Transform SecondColumnContent => ui.layout.properties.secondColumnContent;

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
            public BasicLayoutInfo message;
            public PropertiesLayoutInfo properties;
        }

        [Serializable]
        public struct BasicLayoutInfo
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
        
        
        [Serializable]
        public struct ThemeStyleInfo
        {
            public Sprite backgroundImage;
            public Sprite headerImage;
            public Sprite buttonNormal;
            public Sprite buttonHighlight;
            public Sprite buttonPress;
            public Sprite buttonSelected;
            public Sprite buttonDisabled;
        }
        
    }
}
