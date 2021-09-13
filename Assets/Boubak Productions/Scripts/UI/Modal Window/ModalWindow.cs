using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using BoubakProductions.Safety;

namespace BoubakProductions.UI
{
    public class ModalWindow : MonoBehaviour
    {
        [Header("Defaults")]
        [SerializeField] private string acceptButtonDefault = "Confirm";
        [SerializeField] private string denyButtonDefault = "Cancel";
        [SerializeField] private string specialButtonDefault = "NONE";

        [Header("UI")]
        [SerializeField] private Transform area;
        [SerializeField] private Image background;
        [SerializeField] private Transform windowBox;
        [SerializeField] private HeaderInfo header;
        [SerializeField] private ContentInfo content;
        [SerializeField] private FooterInfo footer;

        #region Open As Message
        /// <summary>
        /// Opens the Modal Window as a message window.
        /// </summary>
        /// <param name="message">Text in the header.</param>
        /// <param name="acceptButtonText">Text in the Accept Button.</param>
        /// <param name="onAcceptAction">Method, that happens when the Accept Button is clicked.</param>
        /// <param name="denyButtonClosesWindow">If on, the Deny Button just closes the window.</param>
        public void OpenAsMessage(string message, string acceptButtonText, Action onAcceptAction, bool denyButtonClosesWindow = false)
        {
            if (denyButtonClosesWindow)
                OpenAsMessage(message, acceptButtonText, denyButtonDefault, specialButtonDefault, onAcceptAction, Close, null);
            else
                OpenAsMessage(message, acceptButtonText, denyButtonDefault, specialButtonDefault, onAcceptAction, null, null);
        }
        /// <summary>
        /// Opens the Modal Window as a message window.
        /// </summary>
        /// <param name="message">Text in the header.</param>
        /// <param name="acceptButtonText">Text in the Accept Button.</param>
        /// /// <param name="acceptButtonText">Text in the Accept Button.</param>
        /// /// <param name="denyButtonText">Text in the Deny Button.</param>
        /// <param name="onAcceptAction">Method, that happens when the Accept Button is clicked.</param>
        /// <param name="hasCloseButton">If on, the Deny Button just closes the window.</param>
        public void OpenAsMessage(string message, string acceptButtonText, string denyButtonText, Action onAcceptAction, bool denyButtonClosesWindow = false)
        {
            if (denyButtonClosesWindow)
                OpenAsMessage(message, acceptButtonText, denyButtonText, specialButtonDefault, onAcceptAction, Close, null);
            else
                OpenAsMessage(message, acceptButtonText, denyButtonText, specialButtonDefault, onAcceptAction, null, null);
        }
        /// <summary>
        /// Opens the Modal Window as a message window.
        /// </summary>
        /// <param name="message">Text in the header.</param>
        /// <param name="acceptButtonText">Text in the Accept Button.</param>
        /// <param name="denyButtonText">Text in the Deny Button.</param>
        /// <param name="specialButtonText">Text in the Special Button.</param>
        /// <param name="onAcceptAction">Method, that happens when the Accept Button is clicked.</param>
        /// <param name="onDenyAction">Method, that happens when the Dccept Button is clicked.</param>
        /// <param name="onSpecialAction">Method, that happens when the Special Button is clicked.</param>
        public void OpenAsMessage(string message, string acceptButtonText, string denyButtonText, string specialButtonText, Action onAcceptAction, Action onDenyAction = null, Action onSpecialAction = null)
        {
            SafetyNet.EnsureStringNotNullOrEmpty(message, "Modal Window Title");
            
            WindowSetup();

            content.area.gameObject.SetActive(true);
            content.basicLayout.area.gameObject.SetActive(true);
            content.basicLayout.text.gameObject.SetActive(true);

            content.basicLayout.text.text = message;

            footer.area.gameObject.SetActive(true);

            footer.acceptButtonText.text = acceptButtonText;
            footer.OnAcceptButtonClick = onAcceptAction;

            bool usesDeny = (onDenyAction != null);
            footer.denyButton.gameObject.SetActive(usesDeny);
            footer.denyButtonText.text = denyButtonText;
            footer.OnDenyButtonClick = onDenyAction;

            bool usesSpecial = (onSpecialAction != null);
            footer.specialButton.gameObject.SetActive(usesSpecial);
            footer.specialButtonText.text = specialButtonText;
            footer.OnSpecialButtonClick = onSpecialAction;

            windowBox.gameObject.SetActive(true);
        }
        #endregion

        /// <summary>
        /// Prepares the window.
        /// </summary>
        private void WindowSetup()
        {
            header.area.gameObject.SetActive(false);
            content.area.gameObject.SetActive(false);
            footer.area.gameObject.SetActive(false);

            area.gameObject.SetActive(true);
            background.gameObject.SetActive(true);
            windowBox.gameObject.SetActive(false);
        }

        /// <summary>
        /// Stores actions, that will take place once the Accept button is clicked.
        /// </summary>
        public void OnAccept()
        {
            footer.OnAcceptButtonClick?.Invoke();
            Close();
        }

        /// <summary>
        /// Stores actions, that will take place once the Deny button is clicked.
        /// </summary>
        public void OnDeny()
        {
            footer.OnDenyButtonClick?.Invoke();
            Close();
        }

        /// <summary>
        /// Stores actions, that will take place once the Special button is clicked.
        /// </summary>
        public void OnSpecial()
        {
            footer.OnSpecialButtonClick?.Invoke();
            Close();
        }

        /// <summary>
        /// Closes the Window.
        /// </summary>
        public void Close()
        {
            windowBox.gameObject.SetActive(false);
            background.gameObject.SetActive(false);
            area.gameObject.SetActive(false);
        }

        [System.Serializable]
        public struct HeaderInfo
        {
            public Transform area;
            public TextMeshProUGUI text;
        }

        [System.Serializable]
        public struct ContentInfo
        {
            public Transform area;
            public BasicLayoutInfo basicLayout;
        }

        [System.Serializable]
        public struct BasicLayoutInfo
        {
            public Transform area;
            public TextMeshProUGUI text;
        }

        [System.Serializable]
        public struct FooterInfo
        {
            public Transform area;
            public TextMeshProUGUI acceptButtonText;
            public TextMeshProUGUI denyButtonText;
            public TextMeshProUGUI specialButtonText;
            public Button acceptButton;
            public Button denyButton;
            public Button specialButton;

            public Action OnAcceptButtonClick;
            public Action OnDenyButtonClick;
            public Action OnSpecialButtonClick;
        }
    }
}
