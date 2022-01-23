using Rogium.Editors.Packs;
using Rogium.Editors.Tiles;
using Rogium.Systems.GridSystem;
using System;
using BoubakProductions.Core;
using Rogium.Core;
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
            packEditor = PackEditorOverseer.Instance;
            editor = RoomEditorOverseer.Instance;
            toolbox = new ToolBoxAsset(grid);
        }

        private void OnEnable()
        {
            editor.OnAssignRoom += PrepareEditor;
            grid.OnInteractionClick += UpdateGridCell;
            paletteTile.OnSelect += ChangeCurrentTile;
        }

        private void OnDisable()
        {
            editor.OnAssignRoom -= PrepareEditor;
            grid.OnInteractionClick -= UpdateGridCell;
            paletteTile.OnSelect -= ChangeCurrentTile;
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
        
        public ToolBoxAsset Toolbox { get => toolbox; } 
    }
}