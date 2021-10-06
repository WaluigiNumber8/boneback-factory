using System;
using Rogium.Editors.Core;
using Rogium.Editors.TileData;
using Rogium.Global.GridSystem;
using UnityEngine;

namespace Rogium.Editors.RoomData
{
    /// <summary>
    /// Works like a bridge between the UI & <see cref="RoomEditorOverseer"/>.
    /// </summary>
    public class RoomEditorUIOverseer : MonoBehaviour
    {
        private RoomEditorOverseer roomEditor;
        private AssetPickerOverseer assetPicker;
        
        [SerializeField] private EditorGridOverseer editorGrid;

        private void Start()
        {
            roomEditor = RoomEditorOverseer.Instance;
            assetPicker = new AssetPickerOverseer();

            roomEditor.OnAssignRoom += PrepareRoomEditor;
            editorGrid.OnInteractionClick += UpdateGridCell;
        }

        /// <summary>
        /// Prepares the room editor, whenever it is opened.
        /// </summary>
        private void PrepareRoomEditor(RoomAsset roomAsset)
        {
            editorGrid.LoadGrid(EditorOverseer.Instance.CurrentPack.Tiles, roomAsset);
        }
        
        /// <summary>
        /// Updates current rooms grid based properties.
        /// </summary>
        /// <param name="gridPosition"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void UpdateGridCell(Vector2Int gridPosition)
        {
            PlaceableAsset currentAsset = assetPicker.CurrentAsset;
            switch (currentAsset.Type)
            {
                case PlaceableAssetType.Tile:
                    roomEditor.UpdateTile(gridPosition, (TileAsset)currentAsset.Asset);
                    break;
                case PlaceableAssetType.Object:
                    break;
                case PlaceableAssetType.Enemy:
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"'{currentAsset.Type}' is not a supported Placeable Asset Type.");
            }
            
            editorGrid.UpdateCellSprite(gridPosition, currentAsset.Asset.Icon);
        }
    }
}