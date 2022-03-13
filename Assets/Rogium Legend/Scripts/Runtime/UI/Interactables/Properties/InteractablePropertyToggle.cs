using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using BoubakProductions.UI.Core;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Overseers everything happening in a toggle interactable property.
    /// </summary>
    public class InteractablePropertyToggle : MonoBehaviour, IInteractableProperty
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Toggle toggle;
        [SerializeField] private UIInfo ui;

        /// <summary>
        /// Set the property title and state.
        /// </summary>
        /// <param name="titleText">Property Title.</param>
        /// <param name="toggleState">State of the toggle checkbox.</param>
        /// <param name="WhenChangeValue">The method that will run when the toggle changes it's value.</param>
        public void Construct(string titleText, bool toggleState, Action<bool> WhenChangeValue)
        {
            title.text = titleText;
            title.gameObject.SetActive((titleText != ""));
            if (ui.emptySpace != null) ui.emptySpace.SetActive((titleText != ""));
            
            toggle.isOn = toggleState;
            toggle.onValueChanged.AddListener(delegate { WhenChangeValue(toggle.isOn); });
        }

        /// <summary>
        /// Updates UI elements.
        /// </summary>
        /// <param name="toggleSpriteSet">The toggle graphics.</param>
        /// <param name="checkmark">The checkmark for activated toggle.</param>
        public void UpdateTheme(InteractableInfo toggleSpriteSet, Sprite checkmark)
        {
            UIExtensions.ChangeInteractableSprites(toggle, ui.backgroundImage, toggleSpriteSet);
            ui.checkmarkImage.sprite = checkmark;
        }
        
        public string Title { get => title.text; }
        public bool Property { get => toggle.isOn; }

        [Serializable]
        public struct UIInfo
        {
            public Image backgroundImage;
            public Image checkmarkImage;
            public GameObject emptySpace;
        }
    }
}