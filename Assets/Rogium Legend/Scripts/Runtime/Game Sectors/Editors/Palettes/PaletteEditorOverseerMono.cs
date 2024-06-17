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
            editor.OnCompleteEditingBefore += UpdateEditedColor;
            palette.OnSelect += SwitchEditedColor;
        }

        private void OnDisable()
        {
            editor.OnAssignAsset -= RefreshEditor;
            editor.OnCompleteEditingBefore -= UpdateEditedColor;
            palette.OnSelect -= SwitchEditedColor;
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
        /// Changes the currently edited color to a new one.
        /// </summary>
        /// <param name="slot">The color data to read from.</param>
        private void SwitchEditedColor(ColorSlot slot)
        {
            UpdateEditedColor();
            colorPicker.Construct(slot.CurrentColor, slot.ColorImage);
            lastSlot = slot;
        }
        
        /// <summary>
        /// Updates changes made to the currently edited color.
        /// </summary>
        private void UpdateEditedColor()
        {
            if (lastSlot == null) return;
            if (lastSlot.Index == -1) return;
            
            lastSlot.UpdateColor(colorPicker.CurrentColor);
            editor.UpdateColor(colorPicker.CurrentColor, lastSlot.Index);
        }
        
    }
}