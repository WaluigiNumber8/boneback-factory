using System;
using Rogium.Systems.Input;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Editors.PropertyModalWindows
{
    /// <summary>
    /// A base for all property modal windows.
    /// </summary>
    public abstract class PropertyModalWindowBase : MonoBehaviour
    {
        [SerializeField] private UIInfo ui;
        
        private InputSystem inputSystem;

        protected virtual void Awake()
        {
            ui.backgroundArea.onClick.AddListener(Close);
            ui.closeButton.onClick.AddListener(Close);
            inputSystem = InputSystem.GetInstance();
        }

        /// <summary>
        /// Opens the window.
        /// </summary>
        public void Open() => ui.windowArea.SetActive(true);

        /// <summary>
        /// Close the window.
        /// </summary>
        public void Close() => ui.windowArea.SetActive(false);

        [Serializable]
        public struct UIInfo
        {
            public GameObject windowArea;
            public Button backgroundArea;
            public Button closeButton;
        }
    }
}