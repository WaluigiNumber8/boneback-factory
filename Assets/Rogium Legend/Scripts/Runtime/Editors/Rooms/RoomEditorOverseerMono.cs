using Rogium.Editors.Packs;
using Rogium.Editors.Tiles;
using Rogium.Systems.GridSystem;
using System;
using BoubakProductions.Core;
using Rogium.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.ItemPalette;
using Rogium.Systems.Toolbox;
using Rogium.UserInterface.AssetSelection;
using UnityEngine;

namespace Rogium.Editors.Rooms
{
    /// <summary>
    /// Works like a bridge between the UI & <see cref="RoomEditorOverseer"/>.
    /// </summary>
    public class RoomEditorOverseerMono : MonoSingleton<RoomEditorOverseerMono>
    {
        [SerializeField] private InteractableEditorGrid grid;
        [SerializeField] private ItemPaletteAsset paletteTile;
        
        private PackEditorOverseer packEditor;
        private RoomEditorOverseer editor;
        private ToolBoxAsset toolbox;

        private AssetSlot currentTile;
        
        protected override void Awake()
        {
            base.Awake();
            packEditor = PackEditorOverseer.Instance;
            editor = RoomEditorOverseer.Instance;
            toolbox = new ToolBoxAsset(grid);
        }

        private void OnEnable()
        {
            editor.OnAssignAsset += PrepareEditor;
            grid.OnInteractionClick += UpdateGridCell;
            paletteTile.OnSelect += ChangeCurrentTile;
            toolbox.OnChangePaletteValue += SelectFromTiles;
        }

        private void OnDisable()
        {
            editor.OnAssignAsset -= PrepareEditor;
            grid.OnInteractionClick -= UpdateGridCell;
            paletteTile.OnSelect -= ChangeCurrentTile;
            toolbox.OnChangePaletteValue -= SelectFromTiles;
        }

        /// <summary>
        /// Prepares the room editor, whenever it is opened.
        /// </summary>
        private void PrepareEditor(RoomAsset room)
        {
            paletteTile.Fill(packEditor.CurrentPack.Tiles, AssetType.Tile);
            paletteTile.Select(0);
            
            grid.LoadWithSprites(packEditor.CurrentPack.Tiles, room.TileGrid);
        }
        
        /// <summary>
        /// Updates the grid based on inputted position.
        /// </summary>
        /// <param name="position">The grid position to affect.</param>
        public void UpdateGridCell(Vector2Int position)
        {
            if (currentTile == null) return;
            toolbox.ApplyCurrent(editor.CurrentAsset.TileGrid, position, currentTile);
        }

        /// <summary>
        /// Changes the current tile used for drawing on the grid.
        /// </summary>
        /// <param name="slot">The new slot holding the tile.</param>
        private void ChangeCurrentTile(AssetSlot slot)
        {
            currentTile = slot;
        }

        /// <summary>
        /// Selects a tile from the tiles palette.
        /// </summary>
        /// <param name="id">The id of the asset to select.</param>
        private void SelectFromTiles(string id)
        {
            if (id == EditorDefaults.EmptyAssetID)
            {
                toolbox.SwitchTool(ToolType.Eraser);
                return;
            }
            paletteTile.Select(id);
            toolbox.SwitchTool(ToolType.Brush);
        }
        
        public ToolBoxAsset Toolbox { get => toolbox; } 
    }
}