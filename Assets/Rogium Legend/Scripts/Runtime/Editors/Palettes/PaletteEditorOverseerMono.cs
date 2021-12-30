using BoubakProductions.Core;
using Rogium.Editors.Core.Defaults;
using TMPro;
using UnityEngine;

namespace Rogium.Editors.Palettes
{
    /// <summary>
    /// Overseers the UI portion of the Palette Editor.
    /// </summary>
    public class PaletteEditorOverseerMono : MonoSingleton<PaletteEditorOverseerMono>
    {
        [SerializeField] private TextMeshProUGUI titleText;
        
        [Header("Slot Row")]
        [SerializeField] private GameObject colorSlotPrefab;
        [SerializeField] private Transform colorSlotContent;

        [Header("Color Picker")]
        [SerializeField] private ColorPickerManager colorPicker;
        
        private PaletteEditorOverseer editor;
        private ColorSlot[] slots;
        private int lastSelectedSlotIndex = -1;

        protected override void Awake()
        {
            base.Awake();
            
            editor = PaletteEditorOverseer.Instance;
            slots = new ColorSlot[EditorDefaults.PaletteSize];
            
            BuildSlotRow(EditorDefaults.PaletteSize);
        }

        private void OnEnable()
        {
            editor.OnAssignAsset += FillSlotRow;
            editor.OnCompleteEditing += UpdateEditedColor;
            ColorSlot.OnSelectedAny += SwitchEditedColor;
        }

        private void OnDisable()
        {
            editor.OnAssignAsset -= FillSlotRow;
            editor.OnCompleteEditing -= UpdateEditedColor;
            ColorSlot.OnSelectedAny -= SwitchEditedColor;
        }

        /// <summary>
        /// Prepares the color slot row of the editor.
        /// </summary>
        /// <param name="slotAmount">The amount of slots to spawn.</param>
        private void BuildSlotRow(int slotAmount)
        {
            for (int i = 0; i < slotAmount; i++)
            {
                slots[i] = Instantiate(colorSlotPrefab, colorSlotContent).GetComponent<ColorSlot>();
                slots[i].Construct(Color.black, i);
            }
        }

        /// <summary>
        /// Fills the slot row with colors in the palette.
        /// </summary>
        /// <param name="palette">The palette to fill the data from.</param>
        private void FillSlotRow(PaletteAsset palette)
        {
            titleText.text = palette.Title;
            
            lastSelectedSlotIndex = -1;
            Color[] colors = palette.Colors;
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].Construct(colors[i], i);
            }
            
            SwitchEditedColor(0);
        }
        
        private void SwitchEditedColor(int newIndex)
        {
            UpdateEditedColor(null, 0);
            colorPicker.Construct(slots[newIndex].CurrentColor, slots[newIndex].colorImage);
            lastSelectedSlotIndex = newIndex;
        }
        
        /// <summary>
        /// Updates changes made to the currently edited color.
        /// </summary>
        private void UpdateEditedColor(PaletteAsset paletteAsset, int i)
        {
            if (lastSelectedSlotIndex != -1)
                editor.UpdateColor(colorPicker.CurrentColor, lastSelectedSlotIndex);
        }
        
    }
}