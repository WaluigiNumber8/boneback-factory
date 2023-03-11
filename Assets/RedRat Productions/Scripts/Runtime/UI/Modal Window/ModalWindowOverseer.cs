using System.Collections.Generic;
using RedRats.Core;
using UnityEngine;
using UnityEngine.Pool;

namespace RedRats.UI.ModalWindows
{
    /// <summary>
    /// Overseers the Modal Window system.
    /// </summary>
    public class ModalWindowOverseer : MonoSingleton<ModalWindowOverseer>
    {
        [SerializeField] private ModalWindow windowPrefab;

        private IDictionary<int, ModalWindow> windows;

        protected override void Awake()
        {
            base.Awake();
            windows = new Dictionary<int, ModalWindow>();
        }

        public void Open(int id, ModalWindowInfoBase windowData)
        {
            EnsureWindowExists(id);
            ModalWindow window = windows[id];

            if (windowData is MessageWindowInfo mData)
            {
                // window.OpenAsMessage();
                return;
            }

            if (windowData is PropertyWindowInfo pData)
            {
                // if (pData.Layout == PropertyLayoutType.Column1) window.OpenAsPropertiesColumn1();
                // else if (pData.Layout == PropertyLayoutType.Columns2) window.OpenAsPropertiesColumn2();
                return;
            }

            //Create new window under id if it does not exist.
            //If it exists, override the window.
        }

        /// <summary>
        /// Returns a modal window's first column <see cref="Transform"/>.
        /// </summary>
        /// <param name="id">The ID of the window.</param>
        /// <returns><see cref="Transform"/> of the left-most column.</returns>
        public Transform GetColumn1(int id)
        {
            EnsureWindowExists(id);
            return windows[id].FirstColumnContent;
        }
        
        /// <summary>
        /// Returns a modal window's second column <see cref="Transform"/>.
        /// </summary>
        /// <param name="id">The ID of the window.</param>
        /// <returns><see cref="Transform"/> of the right-most column.</returns>
        public Transform GetColumn2(int id)
        {
            EnsureWindowExists(id);
            return windows[id].SecondColumnContent;
        }

        /// <summary>
        /// Makes sure that a window under a specific ID is created,
        /// </summary>
        /// <param name="id">The ID of the window to check.</param>
        private void EnsureWindowExists(int id)
        {
            if (!windows.ContainsKey(id)) windows.Add(id, Instantiate(windowPrefab, transform));
        }
    }
}