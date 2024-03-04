using System;
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

        protected virtual void Awake()
        {
            ui.backgroundArea.onClick.AddListener(Close);
            ui.closeButton.onClick.AddListener(Close);
        }

        /// <summary>
        /// Opens the window.
        /// </summary>
        public void Open() => ui.area.SetActive(true);

        /// <summary>
        /// Close the window.
        /// </summary>
        public void Close() => ui.area.SetActive(false);

        [Serializable]
        public struct UIInfo
        {
            public GameObject area;
            public Button backgroundArea;
            public Button closeButton;
        }
    }
}