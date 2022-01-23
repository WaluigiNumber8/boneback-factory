using BoubakProductions.Core;
using Rogium.Editors.Palettes;
using Rogium.Systems.GridSystem;
using Rogium.Systems.ItemPalette;
using Rogium.Systems.Toolbox;
using UnityEngine;

namespace Rogium.Editors.Sprites
{
    /// <summary>
    /// Overseers the UI of the Sprite Editor.
    /// </summary>
    public class SpriteEditorOverseerMono : MonoSingleton<SpriteEditorOverseerMono>
    {
        [SerializeField] private InteractableEditorGrid grid;
        [SerializeField] private ItemPaletteColor paletteColor;
        
        private SpriteEditorOverseer editor;
        private PalettePicker palettePicker;
        private ToolBoxColor toolbox;

        private ColorSlot currentSlot;

        protected override void Awake()
        {
            base.Awake();
            editor = SpriteEditorOverseer.Instance;
            palettePicker = new PalettePicker();
            toolbox = new ToolBoxColor(grid);
        }

        private void OnEnable()
        {
            editor.OnAssignAsset += PrepareEditor;
            grid.OnInteractionClick += UpdateGridCell;
            paletteColor.OnSelect += ChangeCurrentColor;
        }

        private void OnDisable()
        {
            editor.OnAssignAsset -= PrepareEditor;
            grid.OnInteractionClick -= UpdateGridCell;
            paletteColor.OnSelect -= ChangeCurrentColor;
        }

        /// <summary>
        /// Prepares the Sprite Editor for operation.
        /// </summary>
        /// <param name="sprite">The sprite to read from.</param>
        private void PrepareEditor(SpriteAsset sprite)
        {
            Color[] colorArray = palettePicker.GrabBasedOn(sprite.PreferredPaletteID);
            grid.LoadWithColors(colorArray, sprite.SpriteData);
            
            paletteColor.Fill(colorArray);
            paletteColor.Select(0);
        }
        
        /// <summary>
        /// Updates the grid based on inputted position.
        /// </summary>
        /// <param name="position">The grid position to affect.</param>
        public void UpdateGridCell(Vector2Int position)
        {
            if (currentSlot == null) return;
            toolbox.ApplyCurrent(editor.CurrentAsset.SpriteData, position, currentSlot);
        }
        
        /// <summary>
        /// Changes the current color used for drawing on the grid.
        /// </summary>
        /// <param name="slot">The new slot holding the color.</param>
        private void ChangeCurrentColor(ColorSlot slot)
        {
            currentSlot = slot;
        }
        
        public ToolBoxColor Toolbox { get => toolbox; } 
    }
}