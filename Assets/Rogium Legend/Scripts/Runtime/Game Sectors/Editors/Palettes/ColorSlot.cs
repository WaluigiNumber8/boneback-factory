using System;
using Rogium.UserInterface.Editors.AssetSelection;
using Rogium.UserInterface.ModalWindows;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rogium.Editors.Palettes
{
    /// <summary>
    /// Holds information about a given color slot from a palette.
    /// </summary>
    public class ColorSlot : ToggleableIndexBase, IColorSlot, IPointerClickHandler
    {
        public static event Action<int> OnSelectedAny;
        
        [SerializeField] private UIInfo ui;
        
        private Color currentColor;

        private void OnEnable() => toggle.onValueChanged.AddListener(WhenSelected);
        private void OnDisable() => toggle.onValueChanged.RemoveListener(WhenSelected);
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Right) return;
            ModalWindowBuilder.GetInstance().OpenColorPickerWindow((_) => { }, currentColor); //TODO Make the window affect things.
        }
        
        /// <summary>
        /// Constructs a color slot, without giving it a new index.
        /// </summary>
        /// <param name="color">The new color it's going to carry.</param>
        public void Construct(Color color) => Construct(color, index);

        /// <summary>
        /// Constructs the color slot.
        /// </summary>
        /// <param name="color">The new color it's going to carry.</param>
        /// <param name="index">The index of the color.</param>
        public void Construct(Color color, int index)
        {
            this.currentColor = color;
            this.index = index;
            RefreshUI();
        }

        /// <summary>
        /// Refreshes all the slots UI elements.
        /// </summary>
        private void RefreshUI()
        {
            ui.colorImg.color = currentColor;
        }
        
        /// <summary>
        /// Fires the select event when the toggle was clicked.
        /// </summary>
        private void WhenSelected(bool value)
        {
            if (!value) return;
            OnSelectedAny?.Invoke(index);
        }

        public Color CurrentColor { get => currentColor; }
        public Image ColorImage { get => ui.colorImg; }

        [Serializable]
        public struct UIInfo
        {
            public Image colorImg;
        }
    }
}