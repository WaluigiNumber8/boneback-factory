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
        private readonly TileEditorOverseer tileEditor = TileEditorOverseer.Instance;

        public override void OpenForCreate(Action whenConfirm = null) => OpenWindow(new TileAsset.Builder().Build(), () => CreateAsset(whenConfirm), "Creating a new Tile", AssetModificationType.Create);

        public override void OpenForUpdate(Action whenConfirm = null) => OpenWindow(new TileAsset.Builder().AsCopy(tileEditor.CurrentAsset).Build(), () => UpdateAsset(whenConfirm), $"Updating {tileEditor.CurrentAsset.Title}", AssetModificationType.Update);

        public override void OpenForClone(Action whenConfirm = null) => OpenWindow(new TileAsset.Builder().AsClone(tileEditor.CurrentAsset).Build(), () => CloneAsset(whenConfirm), $"Creating a clone of {tileEditor.CurrentAsset.Title}", AssetModificationType.Clone);
        
        private void OpenWindow(TileAsset asset, Action onConfirm, string headerText, AssetModificationType modification)
        {
            asset.UpdateTitle(GetTitleByModificationType(asset, modification));
            OpenForColumns1(headerText, onConfirm, out Transform col1);
            
            b.BuildInputField("Title", asset.Title, col1, asset.UpdateTitle);
            b.BuildDropdown("Type", Enum.GetNames(typeof(TileType)), (int)asset.Type, col1, asset.UpdateType);
            b.BuildDropdown("Layer", Enum.GetNames(typeof(TileLayerType)), (int)asset.LayerType, col1, asset.UpdateLayerType);
            b.BuildPlainText("Created by", asset.Author, col1);
            b.BuildPlainText("Created on", asset.CreationDate.ToString(), col1);
            
            editedAssetBase = asset;
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

        protected override void CloneAsset(Action whenConfirm)
        {
            editor.CreateNewTile((TileAsset) editedAssetBase);
            tileEditor.UpdateAsset((TileAsset)editedAssetBase);
            tileEditor.CompleteEditing();
            whenConfirm?.Invoke();
        }
    }
}