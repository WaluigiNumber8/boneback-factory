using System.Collections;
using RedRats.Core;
using Rogium.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Palettes;
using Rogium.Systems.ActionHistory;
using Rogium.Systems.GridSystem;
using Rogium.Systems.ItemPalette;
using Rogium.Systems.Toolbox;
using Rogium.UserInterface.Cursors;
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
        [SerializeField] private ToolBoxUIManager toolBoxUIManager;
        [SerializeField] private CursorChangerToolbox cursorChanger;
        
        private SpriteEditorOverseer editor;
        private PalettePicker palettePicker;
        private ToolBox<int> toolbox;

        private SpriteAsset currentSpriteAsset;
        private IColorSlot currentSlot;
        private PaletteAsset lastPalette;
        private int spriteSize;
        private bool paletteChanged;

        protected override void Awake()
        {
            base.Awake();
            editor = SpriteEditorOverseer.Instance;
            palettePicker = new PalettePicker();
            toolbox = new ToolBox<int>(grid, grid.UpdateCell, EditorDefaults.EmptyColorID);
            spriteSize = EditorDefaults.Instance.SpriteSize;
        }

        private void OnEnable()
        {
            editor.OnAssignAsset += PrepareEditor;
            grid.OnClick += UpdateGridCell;
            grid.OnClickAlternative += EraseCell;
            palette.OnSelect += UpdateCurrentColor;
            ColorSlot.OnChangeColor += WhenPaletteSlotChange;
            
            toolbox.OnChangePaletteValue += PickFrom;
            toolbox.OnSwitchTool += toolBoxUIManager.SwitchTool;
            toolbox.OnSwitchTool += cursorChanger.UpdateCursor;
        }

        private void OnDisable()
        {
            editor.OnAssignAsset -= PrepareEditor;
            grid.OnClick -= UpdateGridCell;
            grid.OnClickAlternative -= EraseCell;
            palette.OnSelect -= UpdateCurrentColor;
            ColorSlot.OnChangeColor -= WhenPaletteSlotChange;
            
            toolbox.OnChangePaletteValue -= PickFrom;
            toolbox.OnSwitchTool -= toolBoxUIManager.SwitchTool;
            toolbox.OnSwitchTool -= cursorChanger.UpdateCursor;
        }

        /// <summary>
        /// Updates the grid based on inputted position.
        /// </summary>
        /// <param name="position">The grid position to affect.</param>
        public void UpdateGridCell(Vector2Int position)
        {
            if (currentSlot == null) return;
            Sprite brushSprite = RedRatBuilder.GenerateSprite(currentSlot.CurrentColor, spriteSize, spriteSize, spriteSize);
            toolbox.ApplyCurrent(editor.CurrentAsset.SpriteData, position, currentSlot.Index, brushSprite, grid.ActiveLayer);
        }

        /// <summary>
        /// Changes the palette using the Asset Picker Window.
        /// </summary>
        public void SwitchPaletteViaWindow()
        {
            ModalWindowBuilder.GetInstance().OpenAssetPickerWindow(AssetType.Palette, asset =>
            {
                editor.UpdatePalette((PaletteAsset) asset);
                ActionHistorySystem.AddAndExecute(new SwitchSpriteEditorPaletteAction(editor.CurrentPalette, lastPalette, SwitchPalette));
                currentSpriteAsset.UpdateAssociatedPaletteID(editor.CurrentPalette.ID);
                lastPalette = editor.CurrentPalette;
            }, lastPalette);
        }
        
        /// <summary>
        /// Changes the palette used by the Sprite Editor.
        /// </summary>
        /// <param name="asset"></param>
        public void SwitchPalette(PaletteAsset asset)
        {
            currentSpriteAsset.UpdateAssociatedPaletteID(asset.ID);
            editor.UpdatePalette(asset);
            lastPalette = asset;
            grid.LoadWithColors(currentSpriteAsset.SpriteData, asset.Colors);
            palette.Fill(asset.Colors);
            palette.Select(0);
            paletteChanged = false;
        }
        
        /// <summary>
        /// Clears the active grid of all data.
        /// </summary>
        public void ClearActiveGrid()
        {
            ActionHistorySystem.ForceBeginGrouping();
            for (int x = 0; x < grid.Size.x; x++)
            {
                for (int y = 0; y < grid.Size.y; y++)
                {
                    EraseCell(new Vector2Int(x, y));
                }
            }
            ActionHistorySystem.ForceEndGrouping();
        }
        
        /// <summary>
        /// Changes the current color used for drawing on the grid.
        /// </summary>
        /// <param name="slot">The new slot holding the color.</param>
        public void UpdateCurrentColor(IColorSlot slot) => currentSlot = slot;
        
        /// <summary>
        /// Prepares the Sprite Editor for operation.
        /// </summary>
        /// <param name="sprite">The sprite to read from.</param>
        private void PrepareEditor(SpriteAsset sprite)
        {
            lastPalette = palettePicker.GrabBasedOn(sprite.AssociatedPaletteID);
            currentSpriteAsset = sprite;
            paletteChanged = false;
            
            toolbox.Refresh();
            SwitchPalette(lastPalette);
            StartCoroutine(DelayCoroutine());
            IEnumerator DelayCoroutine()
            {
                yield return null;
                palette.Select(0);
            }
        }
        
        /// <summary>
        /// Selects a color from the colors palette.
        /// </summary>
        /// <param name="id">The id of the color to select.</param>
        private void PickFrom(int id)
        {
            palette.Select(id);
            toolbox.SwitchTool((id == EditorDefaults.EmptyColorID) ? ToolType.Eraser : ToolType.Brush);
        }
        
        /// <summary>
        /// Erases a specific cell.
        /// </summary>
        /// <param name="position">The cell to erase.</param>
        private void EraseCell(Vector2Int position)
        {
            Sprite brushSprite = RedRatBuilder.GenerateSprite(EditorDefaults.Instance.EmptyGridColor, spriteSize, spriteSize, spriteSize);
            toolbox.ApplySpecific(ToolType.Eraser, currentSpriteAsset.SpriteData, position, currentSlot.Index, brushSprite, grid.ActiveLayer);
        }

        private void WhenPaletteSlotChange(int slotIndex)
        {
            UpdateCurrentPaletteData(slotIndex);
            RedrawColorOnGrid(slotIndex);
            FlagPaletteChange();
        }
        
        /// <summary>
        /// Redraw the value in the entire grid if it is the same as the one selected.
        /// </summary>
        /// <param name="slotIndex">The index of the slot to change.</param>
        private void RedrawColorOnGrid(int slotIndex)
        {
            ObjectGrid<int> dataGrid = currentSpriteAsset.SpriteData;
            Sprite colorSprite = RedRatBuilder.GenerateSprite(palette.GetSlot(slotIndex).CurrentColor, spriteSize, spriteSize, spriteSize);
            
            for (int x = 0; x < dataGrid.Width; x++)
            {
                for (int y = 0; y < dataGrid.Height; y++)
                {
                    if (dataGrid.GetAt(x, y).CompareTo(slotIndex) != 0) continue;
                    grid.UpdateCell(new Vector2Int(x, y), colorSprite);
                }
            }
            grid.Apply(grid.ActiveLayer);
        }
        
        private void UpdateCurrentPaletteData(int slotIndex) => editor.CurrentPalette.Colors[slotIndex] = palette.GetSlot(slotIndex).CurrentColor;
        
        private void FlagPaletteChange() => paletteChanged = true;

        public PaletteAsset CurrentPaletteAsset { get => editor.CurrentPalette; }
        public Color CurrentBrushColor { get => currentSlot.CurrentColor; }
        public ItemPaletteColor Palette { get => palette; }
        public ToolBox<int> Toolbox { get => toolbox; } 
        public ObjectGrid<int> GetCurrentGridCopy => new(editor.CurrentAsset.SpriteData);
        public Sprite CurrentGridSprite => grid.ActiveLayerSprite;
        public bool PaletteChanged { get => paletteChanged; }
    }
}