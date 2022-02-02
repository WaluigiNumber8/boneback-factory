using BoubakProductions.Core;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Palettes;
using Rogium.Systems.GridSystem;
using Rogium.Systems.ItemPalette;
using Rogium.Systems.Toolbox;
using Rogium.UserInterface.Core;
using UnityEngine;

namespace Rogium.Editors.Sprites
{
    /// <summary>
    /// Overseers the UI of the Sprite Editor.
    /// </summary>
    public class SpriteEditorOverseerMono : MonoSingleton<SpriteEditorOverseerMono>
    {
        [SerializeField] private InteractableEditorGrid grid;
        [SerializeField] private ItemPaletteColor palette;
        
        private SpriteEditorOverseer editor;
        private PalettePicker palettePicker;
        private ToolBoxColor toolbox;

        private SpriteAsset currentSprite;
        private ColorSlot currentSlot;
        private AssetBase lastPaletteAsset;

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
            palette.OnSelect += ChangeCurrentColor;
            toolbox.OnChangePaletteValue += SelectFromColors;
        }

        private void OnDisable()
        {
            editor.OnAssignAsset -= PrepareEditor;
            grid.OnInteractionClick -= UpdateGridCell;
            palette.OnSelect -= ChangeCurrentColor;
            toolbox.OnChangePaletteValue -= SelectFromColors;
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
        /// Changes the palette using the Asset Picker Window.
        /// </summary>
        public void SwitchPaletteViaWindow()
        {
            CanvasOverseer.GetInstance().PickerWindow.GrabAsset(AssetType.Palette, asset =>
            {
                lastPaletteAsset = asset;
                PaletteAsset pal = (PaletteAsset) asset;
                SwitchPalette(pal.Colors);
            }, lastPaletteAsset);
        }
        
        /// <summary>
        /// Changes the palette used by the Sprite Editor.
        /// </summary>
        /// <param name="colors"></param>
        public void SwitchPalette(Color[] colors)
        {
            grid.LoadWithColors(colors, currentSprite.SpriteData);
            palette.Fill(colors);
        }
        
        /// <summary>
        /// Prepares the Sprite Editor for operation.
        /// </summary>
        /// <param name="sprite">The sprite to read from.</param>
        private void PrepareEditor(SpriteAsset sprite)
        {
            Color[] colorArray = palettePicker.GrabBasedOn(sprite.PreferredPaletteID);
            currentSprite = sprite;
            
            SwitchPalette(colorArray);
            palette.Select(0);
        }
        
        /// <summary>
        /// Changes the current color used for drawing on the grid.
        /// </summary>
        /// <param name="slot">The new slot holding the color.</param>
        private void ChangeCurrentColor(ColorSlot slot)
        {
            currentSlot = slot;
        }
        
        /// <summary>
        /// Selects a color from the colors palette.
        /// </summary>
        /// <param name="id">The id of the color to select.</param>
        private void SelectFromColors(int id)
        {
            if (id == EditorDefaults.EmptyColorID)
            {
                toolbox.SwitchTool(ToolType.Eraser);
                return;
            }
            palette.Select(id);
            toolbox.SwitchTool(ToolType.Brush);
        }
        
        public ToolBoxColor Toolbox { get => toolbox; } 
    }
}