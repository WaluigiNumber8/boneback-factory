using System;
using BoubakProductions.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Prepares the Slider property for correct use.
    /// </summary>
    public class InteractablePropertySlider : MonoBehaviour, IInteractableProperty
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Slider slider;
        [SerializeField] private UIInfo ui;
        
        /// <summary>
        /// Sets the property title and state.
        /// </summary>
        /// <param name="titleText">The text of the property title.</param>
        /// <param name="minValue">Minimum allowed value on the slider.</param>
        /// <param name="maxValue">Maximum allowed value on the slider.</param>
        /// <param name="startingValue">Starting value of the slider.</param>
        /// <param name="whenValueChange">Method that will run when the slider changes value.</param>
        public void Construct(string titleText, float minValue, float maxValue, float startingValue, Action<float> whenValueChange)
        {
            title.text = titleText;
            title.gameObject.SetActive((titleText != ""));
            
            slider.minValue = minValue;
            slider.maxValue = maxValue;
            slider.value = startingValue;
            slider.onValueChanged.AddListener(delegate { whenValueChange(slider.value); });
        }

        /// <summary>
        /// Updates the slider's UI elements.
        /// </summary>
        /// <param name="sliderSet">The Slider graphics.</param>
        /// <param name="backgroundSprite">Background of the slider.</param>
        /// <param name="handleSprite">Slider's handle.</param>
        /// <param name="titleFont">Font of the property title.</param>
        public void UpdateTheme(InteractableInfo sliderSet, Sprite backgroundSprite, Sprite handleSprite, FontInfo titleFont)
        {
            UIExtensions.ChangeInteractableSprites(slider, ui.fillImage, sliderSet);
            UIExtensions.ChangeFont(title, titleFont);
            ui.backgroundImage.sprite = backgroundSprite;
            ui.handleImage.sprite = handleSprite;
        }
        
        public string Title { get; }

        [Serializable]
        public struct UIInfo
        {
            public Image fillImage;
            public Image backgroundImage;
            public Image handleImage;
        }
    }
}