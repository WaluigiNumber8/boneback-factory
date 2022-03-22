using System;
using System.Collections.Generic;
using BoubakProductions.Core;
using BoubakProductions.UI;
using Rogium.Editors.Packs;
using Rogium.Systems.GridSystem;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Objects;
using Rogium.Editors.Rooms.PropertyColumn;
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
        [SerializeField] private RoomPropertyColumn propertyColumn;
        
        [Header("Palettes")]
        [SerializeField] private ItemPaletteAsset paletteTile;
        [SerializeField] private ItemPaletteAsset paletteObject;
        [SerializeField] private ItemPaletteAsset paletteEnemy;
        
        private PackEditorOverseer packEditor;
        private RoomEditorOverseer editor;
        private ToolBox<AssetData, Sprite> toolbox;

        private IList<ObjectAsset> objects;
        
        private GridData<AssetData> currentData;
        private GridData<AssetData> tileData;
        private GridData<AssetData> objectData;
        private GridData<AssetData> enemyData;

        protected override void Awake()
        {
            base.Awake();
            packEditor = PackEditorOverseer.Instance;
            editor = RoomEditorOverseer.Instance;
            toolbox = new ToolBox<AssetData, Sprite>(grid, new AssetData(ParameterDefaults.ParamsEmpty), EditorDefaults.EmptyGridSprite, grid.UpdateCell);
            objects = InternalLibraryOverseer.GetInstance().GetObjectsCopy();

            paletteTile.OnSelect += asset => SelectedValue(asset, AssetType.Tile);
            paletteObject.OnSelect += asset => SelectedValue(asset, AssetType.Object);
            paletteEnemy.OnSelect += asset => SelectedValue(asset, AssetType.Enemy);
        }

        private void OnEnable()
        {
            editor.OnAssignAsset += PrepareEditor;
            grid.OnClick += UpdateGridCell;
            grid.OnClickAlternative += EraseCell;
            
            toolbox.OnChangePaletteValue += PickFrom;
            toolbox.OnSelectValue += SelectedValue;
        }

        private void OnDisable()
        {
            editor.OnAssignAsset -= PrepareEditor;
            grid.OnClick -= UpdateGridCell;
            grid.OnClickAlternative -= EraseCell;
            
            toolbox.OnChangePaletteValue -= PickFrom;
            toolbox.OnSelectValue -= SelectedValue;
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
            if (currentData.BrushValue.ID == EditorDefaults.EmptyAssetID) return;
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
            tileData = new GridData<AssetData>(editor.CurrentAsset.TileGrid, paletteTile, AssetType.Tile, ParameterDefaults.ParamsTile);
            objectData = new GridData<AssetData>(editor.CurrentAsset.ObjectGrid, paletteObject, AssetType.Object, ParameterDefaults.ParamsEmpty);
            enemyData = new GridData<AssetData>(editor.CurrentAsset.EnemyGrid, paletteEnemy, AssetType.Enemy, ParameterDefaults.ParamsEnemy);
            currentData = new GridData<AssetData>(editor.CurrentAsset.EnemyGrid, paletteEnemy, AssetType.None, ParameterDefaults.ParamsEmpty);
            
            paletteTile.Fill(packEditor.CurrentPack.Tiles, AssetType.Tile);
            paletteObject.Fill(objects, AssetType.Object);
            paletteEnemy.Fill(packEditor.CurrentPack.Enemies, AssetType.Enemy);
            
            SwitchLayer(0);
            paletteTile.Select(0);
            
            grid.LoadWithSprites(room.TileGrid, packEditor.CurrentPack.Tiles, 0);
            grid.LoadWithSprites(room.ObjectGrid, objects, 1);
            grid.LoadWithSprites(room.EnemyGrid, packEditor.CurrentPack.Enemies, 2);
            
            propertyColumn.ConstructSettings(editor.CurrentAsset);
        }
        
        /// <summary>
        /// Picks paint from the current palette.
        /// </summary>
        /// <param name="data">The id of the asset to pick.</param>
        private void PickFrom(AssetData data)
        {
            if (data.ID == EditorDefaults.EmptyAssetID)
            {
                toolbox.SwitchTool(ToolType.Eraser);
                return;
            }
            currentData.Palette.Select(data.ID);
            toolbox.SwitchTool(ToolType.Brush);
        }

        private void SelectedValue(IAsset asset, AssetType type)
        {
            AssetData data = type switch
            {
                AssetType.Tile => new AssetData(asset.ID, ParameterDefaults.ParamsTile),
                AssetType.Object => new AssetData(asset.ID, ParameterDefaults.ParamsEmpty),
                AssetType.Enemy => new AssetData(asset.ID, ParameterDefaults.ParamsEnemy),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
            SelectedValue(data, type, asset);
        }
        
        /// <summary>
        /// Select a value from the current grid.
        /// </summary>
        /// <param name="data">The data of the value on the grid.</param>
        private void SelectedValue(AssetData data)
        {
            SelectedValue(data, currentData.Type);
        }
        
        /// <summary>
        /// Select a value from the current grid.
        /// </summary>
        /// <param name="data">The data of the value on the grid.</param>
        /// <param name="type">The type of asset that was selected.</param>
        private void SelectedValue(AssetData data, AssetType type, IAsset asset = null)
        {
            if (data.ID == EditorDefaults.EmptyAssetID)
            {
                propertyColumn.ConstructEmpty();
                return;
            }
            
            switch (type)
            {
                case AssetType.Tile:
                    propertyColumn.ConstructAssetPropertiesTile(data);
                    asset ??= packEditor.CurrentPack.Tiles.FindValueFirst(data.ID);
                    break;
                case AssetType.Object:
                    asset ??= objects.FindValueFirst(data.ID);
                    break;
                case AssetType.Enemy:
                    propertyColumn.ConstructAssetPropertiesEnemies(data);
                    asset ??= packEditor.CurrentPack.Enemies.FindValueFirst(data.ID);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Layer with type '{type}' is not supported.");
            }
            propertyColumn.ConstructAsset(asset, type);
        }
        
        public ToolBox<AssetData, Sprite> Toolbox { get => toolbox; } 
    }
}