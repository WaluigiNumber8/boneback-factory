using System;
using System.Collections;
using System.Collections.Generic;
using RedRats.Core;
using RedRats.UI.Tabs;
using Rogium.Core;
using Rogium.Editors.Packs;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Objects;
using Rogium.Editors.Rooms.PropertyColumn;
using Rogium.Editors.Tiles;
using Rogium.Systems.ActionHistory;
using Rogium.Systems.GridSystem;
using Rogium.Systems.ItemPalette;
using Rogium.Systems.Toolbox;
using Rogium.UserInterface.Cursors;
using UnityEngine;

namespace Rogium.Editors.Rooms
{
    /// <summary>
    /// Works like a bridge between the UI & <see cref="RoomEditorOverseer"/>.
    /// </summary>
    public class RoomEditorOverseerMono : MonoSingleton<RoomEditorOverseerMono>
    {
        [SerializeField] private InteractableEditorGrid grid;
        [SerializeField] private TabGroup partsDrawer;
        [SerializeField] private RoomPropertyColumn propertyColumn;
        [SerializeField] private ToolBoxUIManager toolBoxUIManager;
        [SerializeField] private CursorChangerLayersInfo cursorChangers;
        
        [Header("Palettes")]
        [SerializeField] private ItemPaletteAsset paletteTile;
        [SerializeField] private ItemPaletteAsset paletteDecor;
        [SerializeField] private ItemPaletteAsset paletteObject;
        [SerializeField] private ItemPaletteAsset paletteEnemy;
        
        private PackEditorOverseer packEditor;
        private RoomEditorOverseer editor;
        private ToolBox<AssetData> toolbox;

        private IList<ObjectAsset> objects;
        
        private GridData<AssetData> currentData;
        private GridData<AssetData> tileData;
        private GridData<AssetData> decorData;
        private GridData<AssetData> objectData;
        private GridData<AssetData> enemyData;

        protected override void Awake()
        {
            base.Awake();
            packEditor = PackEditorOverseer.Instance;
            editor = RoomEditorOverseer.Instance;
            toolbox = new ToolBox<AssetData>(grid, grid.UpdateCell, new AssetData());
            objects = InternalLibraryOverseer.GetInstance().GetObjectsCopy();

            paletteTile.OnSelect += asset => SelectedValue(asset, AssetType.Tile);
            paletteDecor.OnSelect += asset => SelectedValue(asset, AssetType.Tile);
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
            toolbox.OnSwitchTool += toolBoxUIManager.SwitchTool;
            cursorChangers.SubscribeTo(toolbox);
        }

        private void OnDisable()
        {
            editor.OnAssignAsset -= PrepareEditor;
            grid.OnClick -= UpdateGridCell;
            grid.OnClickAlternative -= EraseCell;
            
            toolbox.OnChangePaletteValue -= PickFrom;
            toolbox.OnSelectValue -= SelectedValue;
            toolbox.OnSwitchTool -= toolBoxUIManager.SwitchTool;
            cursorChangers.UnsubscribeFrom(toolbox);
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
                1 => decorData,
                2 => objectData,
                3 => enemyData,
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
            if (currentData.BrushValue.ID == EditorConstants.EmptyAssetID) return;
            toolbox.ApplyCurrent(currentData.Grid, position, new AssetData(currentData.BrushValue), currentData.BrushSprite, grid.ActiveLayer);
        }

        /// <summary>
        /// Clears the active layer of all data.
        /// </summary>
        public void ClearActiveLayer()
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
        /// Erases a specific cell.
        /// </summary>
        /// <param name="position">The cell to erase.</param>
        private void EraseCell(Vector2Int position)
        {
            toolbox.ApplySpecific(ToolType.Eraser, currentData.Grid, position, currentData.BrushValue, EditorConstants.EmptyGridSprite, grid.ActiveLayer);
        }
        
        /// <summary>
        /// Prepares the room editor, whenever it is opened.
        /// </summary>
        private void PrepareEditor(RoomAsset room)
        {
            tileData = new GridData<AssetData>(editor.CurrentAsset.TileGrid, paletteTile, AssetType.Tile, AssetDataBuilder.ForTile);
            decorData = new GridData<AssetData>(editor.CurrentAsset.DecorGrid, paletteDecor, AssetType.Tile, AssetDataBuilder.ForTile);
            objectData = new GridData<AssetData>(editor.CurrentAsset.ObjectGrid, paletteObject, AssetType.Object, AssetDataBuilder.ForObject);
            enemyData = new GridData<AssetData>(editor.CurrentAsset.EnemyGrid, paletteEnemy, AssetType.Enemy, AssetDataBuilder.ForEnemy);
            currentData = new GridData<AssetData>(editor.CurrentAsset.EnemyGrid, paletteEnemy, AssetType.None);

            IList<TileAsset> tiles = new List<TileAsset>();
            IList<TileAsset> decor = new List<TileAsset>();
            foreach (TileAsset tile in packEditor.CurrentPack.Tiles)
            {
                if (tile.Type == TileType.Tile)
                    tiles.Add(tile);
                else decor.Add(tile);
            }
            
            paletteTile.Fill(tiles, AssetType.Tile);
            paletteDecor.Fill(decor, AssetType.Tile);
            paletteObject.Fill(objects, AssetType.Object);
            paletteEnemy.Fill(packEditor.CurrentPack.Enemies, AssetType.Enemy);

            StartCoroutine(SwitchLayer0Delay(0.1f));
            
            grid.LoadWithAssets(room.TileGrid, tiles, 0);
            grid.LoadWithAssets(room.DecorGrid, decor, 1);
            grid.LoadWithAssets(room.ObjectGrid, objects, 2);
            grid.LoadWithAssets(room.EnemyGrid, packEditor.CurrentPack.Enemies, 3);
            
            toolbox.Refresh();
            propertyColumn.ConstructSettings(editor.CurrentAsset);
        }
        
        /// <summary>
        /// Picks paint from the current palette.
        /// </summary>
        /// <param name="data">The id of the asset to pick.</param>
        private void PickFrom(AssetData data)
        {
            currentData.Palette.Select(data.ID);
            toolbox.SwitchTool((data.IsEmpty()) ? ToolType.Eraser : ToolType.Brush);
        }

        /// <summary>
        /// Select a value from the current grid.
        /// </summary>
        /// <param name="asset">The asset to use.</param>
        /// <param name="type">The type of asset that was selected.</param>
        private void SelectedValue(IAsset asset, AssetType type)
        {
            AssetData data = type switch
            {
                AssetType.Tile => AssetDataBuilder.ForTile(packEditor.CurrentPack.Tiles.FindValueFirst(asset.ID)),
                AssetType.Object => AssetDataBuilder.ForObject(asset),
                AssetType.Enemy => AssetDataBuilder.ForEnemy(packEditor.CurrentPack.Enemies.FindValueFirst(asset.ID)),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
            SelectedValue(data, type, asset);
        }
        
        /// <summary>
        /// Select a value from the current grid.
        /// </summary>
        /// <param name="data">The data of the value on the grid.</param>
        private void SelectedValue(AssetData data) => SelectedValue(data, currentData.Type);

        /// <summary>
        /// Select a value from the current grid.
        /// </summary>
        /// <param name="data">The data of the value on the grid.</param>
        /// <param name="type">The type of asset that was selected.</param>
        /// <param name="asset">The asset to use.</param>
        private void SelectedValue(AssetData data, AssetType type, IAsset asset = null)
        {
            if (data.ID == EditorConstants.EmptyAssetID)
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
                    propertyColumn.ConstructAssetPropertiesObject(data);
                    asset ??= objects.FindValueFirst(data.ID);
                    break;
                case AssetType.Enemy:
                    propertyColumn.ConstructAssetPropertiesEnemies(data);
                    asset ??= packEditor.CurrentPack.Enemies.FindValueFirst(data.ID);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Layer with type '{type}' is not supported.");
            }
            currentData.UpdateUsedPaint(data);
            propertyColumn.ConstructAsset(asset, type);
        }

        private IEnumerator SwitchLayer0Delay(float delay)
        {
            yield return new WaitForSeconds(delay);
            SwitchLayer(0);
            paletteTile.Select(0);
        }

        public ToolBox<AssetData> Toolbox { get => toolbox; }

        [Serializable]
        public struct CursorChangerLayersInfo
        {
            public CursorChangerToolbox cursorChangerTile;
            public CursorChangerToolbox cursorChangerDecor;
            public CursorChangerToolbox cursorChangerObject;
            public CursorChangerToolbox cursorChangerEnemy;
            
            public void SubscribeTo(ToolBox<AssetData> toolBox)
            {
                toolBox.OnSwitchTool += cursorChangerTile.UpdateCursor;
                toolBox.OnSwitchTool += cursorChangerDecor.UpdateCursor;
                toolBox.OnSwitchTool += cursorChangerObject.UpdateCursor;
                toolBox.OnSwitchTool += cursorChangerEnemy.UpdateCursor;
            }
            
            public void UnsubscribeFrom(ToolBox<AssetData> toolBox)
            {
                toolBox.OnSwitchTool -= cursorChangerTile.UpdateCursor;
                toolBox.OnSwitchTool -= cursorChangerDecor.UpdateCursor;
                toolBox.OnSwitchTool -= cursorChangerObject.UpdateCursor;
                toolBox.OnSwitchTool -= cursorChangerEnemy.UpdateCursor;
            }
        }
    }
}