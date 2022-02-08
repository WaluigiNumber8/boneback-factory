using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace  Rogium.UserInterface.Navigation
{
    /// <summary>
    /// Controls everything happening on the Navigation Bar. Everything from what buttons do what, to what shows up.
    /// </summary>
    public class NavigationBar : MonoBehaviour
    {
        [SerializeField] private NavBarUIInfo ui;

        /// <summary>
        /// Runs when the "Return" Button is clicked.
        /// </summary>
        public void OnReturnPressed() => ui.onReturnClick?.Invoke();

        /// <summary>
        /// Runs when the "Config" Button is clicked.
        /// </summary>
        public void OnConfigPressed() => ui.onConfigClick?.Invoke();
        
        /// <summary>
        /// Shows the Navigation Bar.
        /// </summary>
        /// <param name="onReturnClick">Method that will run, once the "Return" Button is pressed.</param>
        /// <param name="onConfigClick">Method that will run, once the "Config" Button is pressed.</param>
        /// <param name="packTitle">Title of the pack that will show up.</param>
        /// <param name="packIcon">Icon of the pack, that will show up.</param>
        public void Show(Action onReturnClick, Action onConfigClick = null, string packTitle = null, Sprite packIcon = null)
        {
            Hide();
            ui.area.gameObject.SetActive(true);

            DrawReturnButton(onReturnClick);
            DrawPackInfo(packTitle, packIcon);
            DrawConfigButton(onConfigClick);
        }
        
        /// <summary>
        /// Hides the Navigation Bar completely.
        /// </summary>
        public void Hide()
        {
            ui.packInfo.area.gameObject.SetActive(false);
            ui.packInfo.titleText.gameObject.SetActive(false);
            ui.packInfo.icon.gameObject.SetActive(false);
            
            ui.returnButton.gameObject.SetActive(false);
            ui.configButton.gameObject.SetActive(false);
            
            ui.area.gameObject.SetActive(false);
        }

        #region Draw Methods
        /// <summary>
        /// Draws the pack info bar.
        /// </summary>
        /// <param name="packTitle">Title of the pack that will show up.</param>
        /// <param name="packIcon">Icon of the pack, that will show up.</param>
        private void DrawPackInfo(string packTitle, Sprite packIcon)
        {
            if (packTitle == null && packIcon == null) return;
            
            ui.packInfo.area.gameObject.SetActive(true);
            ui.packInfo.titleText.gameObject.SetActive(true);
            ui.packInfo.icon.gameObject.SetActive(true);
            ui.packInfo.titleText.text = packTitle;
            ui.packInfo.icon.sprite = packIcon;
        }

        /// <summary>
        /// Draws the "Return" button.
        /// </summary>
        /// <param name="onReturnClick">Method that will run, once the "Return" Button is pressed.</param>
        private void DrawReturnButton(Action onReturnClick)
        {
            ui.returnButton.gameObject.SetActive(true);
            ui.onReturnClick = onReturnClick;
        }
        
        /// <summary>
        /// Draws the "Config" button.
        /// </summary>
        /// <param name="onConfigClick">Method that will run, once the "Config" Button is pressed.</param>
        private void DrawConfigButton(Action onConfigClick)
        {
            if (onConfigClick == null) return;
            
            ui.configButton.gameObject.SetActive(true);
            ui.onConfigClick = onConfigClick;
        }
        #endregion
        
        [Serializable]
        public struct NavBarUIInfo
        {
            public Transform area;
            public PackInfoUIInfo packInfo;
            public Button returnButton;
            public Button configButton;

            public Action onReturnClick;
            public Action onConfigClick;
        }

        [Serializable]
        public struct PackInfoUIInfo
        {
            public Transform area;
            public TextMeshProUGUI titleText;
            public Image icon;
        }
    }
}