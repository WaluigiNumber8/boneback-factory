using System;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using UnityEngine;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Palette Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderPalette : ModalWindowPropertyBuilderBase
    {
        private readonly PaletteEditorOverseer paletteEditor = PaletteEditorOverseer.Instance;

        public override void OpenForCreate(Action whenConfirm = null) => OpenWindow(new PaletteAsset.Builder().Build(), () => CreateAsset(whenConfirm), "Creating a new Palette", AssetModificationType.Create);

        public override void OpenForUpdate(Action whenConfirm = null) => OpenWindow(new PaletteAsset.Builder().AsCopy(paletteEditor.CurrentAsset).Build(), () => UpdateAsset(whenConfirm), $"Updating {paletteEditor.CurrentAsset.Title}", AssetModificationType.Update);

        public override void OpenForClone(Action whenConfirm = null) => OpenWindow(new PaletteAsset.Builder().AsClone(paletteEditor.CurrentAsset).Build(), () => CloneAsset(whenConfirm), $"Creating a clone of {paletteEditor.CurrentAsset.Title}", AssetModificationType.Clone);
        
        private void OpenWindow(PaletteAsset asset, Action onConfirm, string headerText, AssetModificationType modification)
        {
            asset.UpdateTitle(GetTitleByModificationType(asset, modification));
            OpenForColumns1(headerText, onConfirm, out Transform col1);
            b.BuildInputField("Title", asset.Title, col1, asset.UpdateTitle);
            b.BuildPlainText("Created by", asset.Author, col1);
            b.BuildPlainText("Created on", asset.CreationDate.ToString(), col1);
            
            editedAssetBase = asset;
        }
        
        protected override void CreateAsset(Action whenConfirm)
        {
            editor.CreateNewPalette((PaletteAsset)editedAssetBase);
            whenConfirm?.Invoke();
        }

        protected override void UpdateAsset(Action whenConfirm)
        {
            paletteEditor.UpdateAsset((PaletteAsset)editedAssetBase);
            paletteEditor.CompleteEditing();
            whenConfirm?.Invoke();
        }

        protected override void CloneAsset(Action whenConfirm)
        {
            editor.CreateNewPalette((PaletteAsset) editedAssetBase);
            paletteEditor.AssignAsset((PaletteAsset)editedAssetBase, PackEditorOverseer.Instance.CurrentPack.Palettes.Count - 1);
            paletteEditor.CompleteEditing();
            whenConfirm?.Invoke();
        }
    }
}