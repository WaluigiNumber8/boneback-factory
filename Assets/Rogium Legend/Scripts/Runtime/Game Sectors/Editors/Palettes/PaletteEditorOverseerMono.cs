using RedRats.Core;
using Rogium.Systems.ItemPalette;
using UnityEngine;
using TMPro;

namespace Rogium.Editors.Palettes
{
    /// <summary>
    /// Overseers the UI portion of the Palette Editor.
    /// </summary>
    public class PaletteEditorOverseerMono : MonoSingleton<PaletteEditorOverseerMono>
    {
        [SerializeField] private ItemPaletteColor palette;
        [SerializeField] private TextMeshProUGUI titleText;
        
        [Header("Color Picker")]
        [SerializeField] private ColorPicker colorPicker;
        
        private PaletteEditorOverseer editor;
        private ColorSlot lastSlot;

        protected override void Awake()
        {
            base.Awake();
            editor = PaletteEditorOverseer.Instance;
        }

        private void OnEnable()
        {
            editor.OnAssignAsset += RefreshEditor;
            editor.OnCompleteEditingBefore += RefreshEditorData;
            palette.OnSelect += RefreshForSlot;
        }

        private void OnDisable()
        {
            editor.OnAssignAsset -= RefreshEditor;
            editor.OnCompleteEditingBefore -= RefreshEditorData;
            palette.OnSelect -= RefreshForSlot;
        }
        
        /// <summary>
        /// Selects a specific slot in the palette.
        /// </summary>
        /// <param name="index">The index of the slot to select.</param>
        public void SelectSlot(int index) => palette.Select(index);
        
        /// <summary>
        /// Updates the color of a specific slot.
        /// </summary>
        /// <param name="color">The new color to set.</param>
        /// <param name="index">The index of the slot to update.</param>
        public void UpdateColorSlotColor(Color color, int index)
        {
            palette.GetSlot(index).UpdateColor(color);
            if (lastSlot != null && lastSlot.Index == index) RefreshForSlot(lastSlot);
        }

        /// <summary>
        /// Refreshes the editor based on a newly assigned color.
        /// </summary>
        /// <param name="asset">The palette to fill the data from.</param>
        private void RefreshEditor(PaletteAsset asset)
        {
            lastSlot = null;
            titleText.text = asset.Title;
            
            Color[] colors = asset.Colors;
            palette.Fill(colors);
            
            palette.Select(0);
        }
        
        /// <summary>
        /// Refreshes the editor for a specific slot.
        /// </summary>
        /// <param name="slot">The color data to read from.</param>
        private void RefreshForSlot(ColorSlot slot)
        {
            RefreshEditorData();
            lastSlot = slot;
            colorPicker.Construct(slot.CurrentColor, slot.ColorImage);
        }
        
        /// <summary>
        /// Updates changes made to the currently edited color.
        /// </summary>
        private void RefreshEditorData()
        {
            if (lastSlot == null) return;
            if (lastSlot.Index == -1) return;
            
            // lastSlot.UpdateColor(colorPicker.CurrentColor);
            editor.UpdateColor(colorPicker.CurrentColor, lastSlot.Index);
        }
    }
}