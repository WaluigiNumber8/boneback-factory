using System.Collections;
using RedRats.Core;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Palettes;
using Rogium.Systems.GridSystem;
using Rogium.Systems.ItemPalette;
using Rogium.Systems.Toolbox;
using Rogium.UserInterface.ModalWindows;
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
        private ToolBox<int> toolbox;

        private SpriteAsset currentSprite;
        private ColorSlot currentSlot;
        private IAsset lastPaletteAsset;

        protected override void Awake()
        {
            base.Awake();
            editor = SpriteEditorOverseer.Instance;
            palettePicker = new PalettePicker();
            toolbox = new ToolBox<int>(grid, grid.UpdateCell, EditorConstants.EmptyColorID);
        }

        private void OnEnable()
        {
            editor.OnAssignAsset += PrepareEditor;
            grid.OnClick += UpdateGridCell;
            grid.OnClickAlternative += EraseCell;
            palette.OnSelect += ChangeCurrentColor;
            toolbox.OnChangePaletteValue += SelectFrom;
        }

        private void OnDisable()
        {
            editor.OnAssignAsset -= PrepareEditor;
            grid.OnClick -= UpdateGridCell;
            grid.OnClickAlternative -= EraseCell;
            palette.OnSelect -= ChangeCurrentColor;
            toolbox.OnChangePaletteValue -= SelectFrom;
        }

        /// <summary>
        /// Updates the grid based on inputted position.
        /// </summary>
        /// <param name="position">The grid position to affect.</param>
        public void UpdateGridCell(Vector2Int position)
        {
            if (currentSlot == null) return;
            Sprite brushSprite = RedRatBuilder.GenerateSprite(currentSlot.CurrentColor, (int)grid.CellSize.x, (int)grid.CellSize.y, (int)grid.CellSize.x);
            toolbox.ApplyCurrent(editor.CurrentAsset.SpriteData, position, currentSlot.Index, brushSprite, grid.ActiveLayer);
        }

        /// <summary>
        /// Changes the palette using the Asset Picker Window.
        /// </summary>
        public void SwitchPaletteViaWindow()
        {
            ModalWindowBuilder.GetInstance().OpenAssetPickerWindow(AssetType.Palette, asset =>
            {
                lastPaletteAsset = asset;
                PaletteAsset pal = (PaletteAsset) asset;
                SwitchPalette(pal.Colors);
                currentSprite.UpdatePreferredPaletteID(pal.ID);
            }, lastPaletteAsset);
        }
        
        /// <summary>
        /// Changes the palette used by the Sprite Editor.
        /// </summary>
        /// <param name="colors"></param>
        public void SwitchPalette(Color[] colors)
        {
            grid.LoadWithColors(currentSprite.SpriteData, colors);
            palette.Fill(colors);
            palette.Select(0);
        }
        
        /// <summary>
        /// Clears the active grid of all data.
        /// </summary>
        public void ClearActiveGrid()
        {
            grid.ClearAllCells();
            editor.CurrentAsset.SpriteData.ClearAllCells();
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
            StartCoroutine(SwitchLayerDelay(0.1f));
        }
        
        /// <summary>
        /// Changes the current color used for drawing on the grid.
        /// </summary>
        /// <param name="slot">The new slot holding the color.</param>
        private void ChangeCurrentColor(ColorSlot slot) => currentSlot = slot;

        /// <summary>
        /// Selects a color from the colors palette.
        /// </summary>
        /// <param name="id">The id of the color to select.</param>
        private void SelectFrom(int id)
        {
            palette.Select(id);
            toolbox.SwitchTool(ToolType.Brush);
        }
        
        /// <summary>
        /// Erases a specific cell.
        /// </summary>
        /// <param name="position">The cell to erase.</param>
        private void EraseCell(Vector2Int position)
        {
            Sprite brushSprite = RedRatBuilder.GenerateSprite(EditorConstants.EmptyGridColor, (int)grid.CellSize.x, (int)grid.CellSize.y, (int)grid.CellSize.x);
            toolbox.ApplySpecific(ToolType.Eraser, editor.CurrentAsset.SpriteData, position, currentSlot.Index, brushSprite, grid.ActiveLayer);
        }
        
        private IEnumerator SwitchLayerDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            palette.Select(0);
        }
        
        public ToolBox<int> Toolbox { get => toolbox; } 
    }
}