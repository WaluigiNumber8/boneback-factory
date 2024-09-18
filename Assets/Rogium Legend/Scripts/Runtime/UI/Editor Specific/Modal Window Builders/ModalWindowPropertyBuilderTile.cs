using System;
using Rogium.Core;
using Rogium.Editors.Tiles;
using UnityEngine;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Room Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderTile : ModalWindowPropertyBuilderBase
    {
        private readonly TileEditorOverseer tileEditor;

        public ModalWindowPropertyBuilderTile() => tileEditor = TileEditorOverseer.Instance;

        public override void OpenForCreate(Action whenConfirm = null) => OpenWindow(new TileAsset.Builder().Build(), () => CreateAsset(whenConfirm), "Creating a new Tile");

        public override void OpenForUpdate(Action whenConfirm = null) => OpenWindow(new TileAsset.Builder().AsCopy(tileEditor.CurrentAsset).Build(), () => UpdateAsset(whenConfirm), $"Updating {tileEditor.CurrentAsset.Title}");

        private void OpenWindow(TileAsset tile, Action onConfirm, string headerText)
        {
            OpenForColumns1(headerText, onConfirm, out Transform col1);
            
            b.BuildInputField("Title", tile.Title, col1, tile.UpdateTitle);
            b.BuildDropdown("Type", Enum.GetNames(typeof(TileType)), (int)tile.Type, col1, tile.UpdateType);
            b.BuildDropdown("Layer", Enum.GetNames(typeof(TileLayerType)), (int)tile.LayerType, col1, tile.UpdateLayerType);
            b.BuildPlainText("Created by", tile.Author, col1);
            b.BuildPlainText("Created on", tile.CreationDate.ToString(), col1);
            
            editedAssetBase = tile;
        }

        protected override void CreateAsset(Action whenConfirm)
        {
            editor.CreateNewTile((TileAsset)editedAssetBase);
            whenConfirm?.Invoke();
        }

        protected override void UpdateAsset(Action whenConfirm)
        {
            tileEditor.UpdateAsset((TileAsset)editedAssetBase);
            tileEditor.CompleteEditing();
            whenConfirm?.Invoke();
        }
    }
}