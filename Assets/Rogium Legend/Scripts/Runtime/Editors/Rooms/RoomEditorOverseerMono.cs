using System;
using Rogium.Editors.Packs;
using Rogium.Editors.Tiles;
using Rogium.Global.GridSystem;
using UnityEngine;

namespace Rogium.Editors.Rooms
{
    /// <summary>
    /// Works like a bridge between the UI & <see cref="RoomEditorOverseer"/>.
    /// </summary>
    public class RoomEditorOverseerMono : MonoBehaviour
    {
        private PackEditorOverseer editor;
        private RoomEditorOverseer roomEditor;
        private AssetPickerOverseer assetPicker;
        
        [SerializeField] private EditorGridOverseer editorGrid;

        private void Awake()
        {
            editor = PackEditorOverseer.Instance;
            roomEditor = RoomEditorOverseer.Instance;
            assetPicker ??= new AssetPickerOverseer();
        }

        private void OnEnable()
        {
            roomEditor.OnAssignRoom += PrepareRoomEditor;
            editorGrid.OnInteractionClick += UpdateGridCell;
        }

        private void OnDisable()
        {
            roomEditor.OnAssignRoom -= PrepareRoomEditor;
            editorGrid.OnInteractionClick -= UpdateGridCell;
        }

        /// <summary>
        /// Prepares the room editor, whenever it is opened.
        /// </summary>
        private void PrepareRoomEditor(RoomAsset roomAsset)
        {
            assetPicker.AssignTile(editor.CurrentPack.Tiles[0]);
            editorGrid.LoadGrid(PackEditorOverseer.Instance.CurrentPack.Tiles, roomAsset);
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