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

        private SpriteAsset currentSprite;
        private IColorSlot currentSlot;
        private PaletteAsset currentPalette;
        private PaletteAsset lastPalette;

        protected override void Awake()
        {
            base.Awake();
            editor = SpriteEditorOverseer.Instance;
            palettePicker = new PalettePicker();
            toolbox = new ToolBox<int>(grid, grid.UpdateCell, EditorDefaults.EmptyColorID);
        }

        private void OnEnable()
        {
            editor.OnAssignAsset += PrepareEditor;
            grid.OnClick += UpdateGridCell;
            grid.OnClickAlternative += EraseCell;
            palette.OnSelect += UpdateCurrentColor;
            
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
            Sprite brushSprite = RedRatBuilder.GenerateSprite(currentSlot.CurrentColor, EditorDefaults.Instance.SpriteSize, EditorDefaults.Instance.SpriteSize, EditorDefaults.Instance.SpriteSize);
            toolbox.ApplyCurrent(editor.CurrentAsset.SpriteData, position, currentSlot.Index, brushSprite, grid.ActiveLayer);
        }

        /// <summary>
        /// Changes the palette using the Asset Picker Window.
        /// </summary>
        public void SwitchPaletteViaWindow()
        {
            ModalWindowBuilder.GetInstance().OpenAssetPickerWindow(AssetType.Palette, asset =>
            {
                currentPalette = (PaletteAsset) asset;
                ActionHistorySystem.AddAndExecute(new SwitchSpriteEditorPaletteAction(currentPalette, lastPalette, SwitchPalette));
                currentSprite.UpdatePreferredPaletteID(currentPalette.ID);
                lastPalette = currentPalette;
            }, lastPalette);
        }
        
        /// <summary>
        /// Changes the palette used by the Sprite Editor.
        /// </summary>
        /// <param name="asset"></param>
        public void SwitchPalette(PaletteAsset asset)
        {
            currentPalette = asset;
            lastPalette = asset;
            grid.LoadWithColors(currentSprite.SpriteData, asset.Colors);
            palette.Fill(asset.Colors);
            palette.Select(0);
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
            lastPalette = palettePicker.GrabBasedOn(sprite.PreferredPaletteID);
            currentSprite = sprite;
            
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
            Sprite brushSprite = RedRatBuilder.GenerateSprite(EditorDefaults.Instance.EmptyGridColor, EditorDefaults.Instance.SpriteSize, EditorDefaults.Instance.SpriteSize, EditorDefaults.Instance.SpriteSize);
            toolbox.ApplySpecific(ToolType.Eraser, editor.CurrentAsset.SpriteData, position, currentSlot.Index, brushSprite, grid.ActiveLayer);
        }
        
        public PaletteAsset CurrentPalette { get => currentPalette; }
        public ToolBox<int> Toolbox { get => toolbox; } 
        public ObjectGrid<int> GetCurrentGridCopy => new(editor.CurrentAsset.SpriteData);
        public Sprite CurrentGridSprite => grid.ActiveLayerSprite;
    }
}