using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Editors.PropertyModalWindows
{
    /// <summary>
    /// A base for all modal windows.
    /// </summary>
    public abstract class ModalWindowBase : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] protected GeneralUIInfo generalUI;

        private bool isOpen;
        
        protected virtual void Awake()
        {
            generalUI.backgroundArea.onClick.AddListener(Close);
            generalUI.closeButton.onClick.AddListener(Close);
        }

        /// <summary>
        /// Opens the window.
        /// </summary>
        public void Open()
        {
            UpdateTheme();
            generalUI.entireArea.SetActive(true);
            isOpen = true;
        }

        /// <summary>
        /// Close the window.
        /// </summary>
        public void Close()
        {
            generalUI.entireArea.SetActive(false);
            isOpen = false;
        }

        protected abstract void UpdateTheme();
        
        public Button CloseButton { get => generalUI.closeButton; }
        public bool IsOpen { get => isOpen; }
        
        [Serializable]
        public struct GeneralUIInfo
        {
            public GameObject entireArea;
            public Image windowArea;
            public Button backgroundArea;
            public Button closeButton;
        }
    }
}