using System;
using System.Collections.Generic;
using Rogium.Editors.Packs;
using Rogium.Systems.GridSystem;
using BoubakProductions.Core;
using BoubakProductions.UI;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Objects;
using Rogium.Systems.ItemPalette;
using Rogium.Systems.Toolbox;
using UnityEngine;

namespace Rogium.Editors.Rooms
{
    /// <summary>
    /// Works like a bridge between the UI & <see cref="RoomEditorOverseer"/>.
    /// </summary>
    public class RoomEditorOverseerMono : MonoSingleton<RoomEditorOverseerMono>
    {
        [SerializeField] private InteractableEditorGridV2 grid;
        [SerializeField] private TabGroup partsDrawer;
        [SerializeField] private ItemPaletteAsset paletteTile;
        [SerializeField] private ItemPaletteAsset paletteObject;
        [SerializeField] private ItemPaletteAsset paletteEnemy;
        
        private PackEditorOverseer packEditor;
        private RoomEditorOverseer editor;
        private ToolBox<string, Sprite> toolbox;

        private IList<ObjectAsset> objects;
        private GridData currentData;
        private GridData tileData;
        private GridData objectData;
        private GridData enemyData;
        
        protected override void Awake()
        {
            base.Awake();
            packEditor = PackEditorOverseer.Instance;
            editor = RoomEditorOverseer.Instance;
            toolbox = new ToolBox<string, Sprite>(grid, EditorDefaults.EmptyAssetID, EditorDefaults.EmptyGridSprite, grid.UpdateCell);
            objects = InternalLibraryOverseer.GetInstance().GetObjectsCopy();
        }

        private void OnEnable()
        {
            editor.OnAssignAsset += PrepareEditor;
            grid.OnClick += UpdateGridCell;
            grid.OnClickAlternative += EraseCell;
            toolbox.OnChangePaletteValue += SelectFrom;
        }

        private void OnDisable()
        {
            editor.OnAssignAsset -= PrepareEditor;
            grid.OnClick -= UpdateGridCell;
            grid.OnClickAlternative -= EraseCell;
            toolbox.OnChangePaletteValue -= SelectFrom;
        }

        /// <summary>
        /// Switches the currently used layer.
        /// </summary>
        /// <param name="index">The index of the layer.</param>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when no layer is stored under the index.</exception>
        public void SwitchLayer(int index)
        {
            currentData = index switch
            {
                0 => tileData,
                1 => objectData,
                2 => enemyData,
                _ => throw new ArgumentOutOfRangeException($"Layer under the index '{index}' is not supported.")
            };
            grid.SwitchActiveLayer(index);
            partsDrawer.Switch(index);
            currentData.Palette.SelectLast();
        }
        
        /// <summary>
        /// Updates the grid based on inputted position.
        /// </summary>
        /// <param name="position">The grid position to affect.</param>
        public void UpdateGridCell(Vector2Int position)
        {
            if (currentData.BrushValue == EditorDefaults.EmptyAssetID) return;
            toolbox.ApplyCurrent(currentData.Grid, position, currentData.BrushValue, currentData.BrushSprite);
        }

        /// <summary>
        /// Erases a specific cell.
        /// </summary>
        /// <param name="position">The cell to erase.</param>
        private void EraseCell(Vector2Int position)
        {
            toolbox.ApplySpecific(ToolType.Eraser, currentData.Grid, position, currentData.BrushValue);
        }
        
        /// <summary>
        /// Prepares the room editor, whenever it is opened.
        /// </summary>
        private void PrepareEditor(RoomAsset room)
        {
            tileData = new GridData(editor.CurrentAsset.TileGrid, paletteTile);
            objectData = new GridData(editor.CurrentAsset.ObjectGrid, paletteObject);
            enemyData = new GridData(editor.CurrentAsset.EnemyGrid, paletteEnemy);
            
            paletteTile.Fill(packEditor.CurrentPack.Tiles, AssetType.Tile);
            paletteObject.Fill(objects, AssetType.Object);
            paletteEnemy.Fill(packEditor.CurrentPack.Enemies, AssetType.Enemy);
            
            SwitchLayer(0);
            paletteTile.Select(0);
            
            grid.LoadWithSprites(room.TileGrid, packEditor.CurrentPack.Tiles, 0);
            grid.LoadWithSprites(room.ObjectGrid, objects, 1);
            grid.LoadWithSprites(room.EnemyGrid, packEditor.CurrentPack.Enemies, 2);
            
        }
        
        /// <summary>
        /// Selects paint from the current palette.
        /// </summary>
        /// <param name="id">The id of the asset to select.</param>
        private void SelectFrom(string id)
        {
            if (id == EditorDefaults.EmptyAssetID)
            {
                toolbox.SwitchTool(ToolType.Eraser);
                return;
            }
            currentData.Palette.Select(id);
            toolbox.SwitchTool(ToolType.Brush);
        }
        
        public ToolBox<string, Sprite> Toolbox { get => toolbox; } 
    }
}