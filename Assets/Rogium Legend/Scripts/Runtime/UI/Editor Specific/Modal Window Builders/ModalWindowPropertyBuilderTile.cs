using System;
using Rogium.Core;
using Rogium.Editors.Tiles;
using UnityEngine;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Room Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderTile : ModalWindowPropertyBuilder
    {
        private readonly TileEditorOverseer tileEditor;

        public ModalWindowPropertyBuilderTile() => tileEditor = TileEditorOverseer.Instance;

        public override void OpenForCreate()
        {
            OpenWindow(new TileAsset(), CreateAsset, "Creating a new Tile");
        }

        public override void OpenForUpdate()
        {
            OpenWindow(new TileAsset(tileEditor.CurrentAsset), UpdateAsset, $"Updating {tileEditor.CurrentAsset.Title}");
        }

        private void OpenWindow(TileAsset tile, Action onConfirm, string headerText)
        {
            OpenForColumns1(headerText, onConfirm, out Transform col1);
            
            b.BuildInputField("Title", tile.Title, col1, tile.UpdateTitle);
            b.BuildDropdown("Type", Enum.GetNames(typeof(TileType)), (int)tile.Type, col1, tile.UpdateTileType);
            b.BuildPlainText("Created by", tile.Author, col1);
            b.BuildPlainText("Created on", tile.CreationDate.ToString(), col1);
            
            editedAssetBase = tile;
        }

        protected override void CreateAsset()
        {
            editor.CreateNewTile((TileAsset)editedAssetBase);
            selectionMenu.Open(AssetType.Tile);
        }

        protected override void UpdateAsset()
        {
            tileEditor.UpdateAsset((TileAsset)editedAssetBase);
            tileEditor.CompleteEditing();
            selectionMenu.Open(AssetType.Tile);
        }
    }
}