using System;
using RedRats.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Interactables
{
    /// <summary>
    /// Allows updating the theme of scrollbars.
    /// </summary>
    public class InteractableScrollbar : MonoBehaviour
    {
        [SerializeField] private Scrollbar scrollbar;
        [SerializeField] private UIInfo ui;

        private void Awake() => scrollbar = GetComponent<Scrollbar>();

        /// <summary>
        /// Sets a value to the scrollbar.
        /// </summary>
        /// <param name="value">The new value to set.</param>
        public void SetValue(float value) => scrollbar.value = value;

        /// <summary>
        /// Updates the scrollbar theme.
        /// </summary>
        /// <param name="handleSet">Sprites used for the scrollbar's handle.</param>
        public void UpdateTheme(InteractableSpriteInfo handleSet)
        {
            UIExtensions.ChangeInteractableSprites(scrollbar, ui.handle, handleSet);
        }

        [Serializable]
        public struct UIInfo
        {
            public Image handle;
        }
    }
}