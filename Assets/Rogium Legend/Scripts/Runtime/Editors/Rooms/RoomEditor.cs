using System;
using Rogium.Editors.TileData;
using Rogium.Global.GridSystem;
using UnityEngine;

namespace Rogium.Editors.RoomData
{
    /// <summary>
    /// Works like a bridge between the UI & <see cref="RoomEditorOverseer"/>.
    /// </summary>
    public class RoomEditor : MonoBehaviour
    {
        private RoomEditorOverseer roomEditor;
        private AssetPickerOverseer assetPicker;
        
        [SerializeField] private EditorGridController editorGrid;

        private void Start()
        {
            roomEditor = RoomEditorOverseer.Instance;
            assetPicker = new AssetPickerOverseer();

            editorGrid.OnInteractionClick += UpdateRoomGrid;
        }

        /// <summary>
        /// Updates current rooms grid based properties.
        /// </summary>
        /// <param name="gridPosition"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void UpdateRoomGrid(Vector2Int gridPosition)
        {
            PlaceableAsset currentAsset = assetPicker.CurrentAsset;
            switch (currentAsset.Type)
            {
                //TODO Update Grid Visualizers with correct data.
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