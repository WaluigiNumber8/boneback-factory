using System;
using Rogium.Core;
using Rogium.Editors.Palettes;
using UnityEngine;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Palette Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderPalette : ModalWindowPropertyBuilderBase
    {
        private readonly PaletteEditorOverseer paletteEditor;

        public ModalWindowPropertyBuilderPalette()
        {
            paletteEditor = PaletteEditorOverseer.Instance;;
        }
        
        public override void OpenForCreate() => OpenWindow(new PaletteAsset.Builder().Build(), CreateAsset, "Creating a new Palette");

        public override void OpenForUpdate() => OpenWindow(new PaletteAsset.Builder().AsCopy(paletteEditor.CurrentAsset).Build(), UpdateAsset, $"Updating {paletteEditor.CurrentAsset.Title}");

        private void OpenWindow(PaletteAsset palette, Action onConfirm, string headerText)
        {
            OpenForColumns1(headerText, onConfirm, out Transform col1);
            b.BuildInputField("Title", palette.Title, col1, palette.UpdateTitle);
            b.BuildPlainText("Created by", palette.Author, col1);
            b.BuildPlainText("Created on", palette.CreationDate.ToString(), col1);
            
            editedAssetBase = palette;
        }
        
        protected override void CreateAsset()
        {
            editor.CreateNewPalette((PaletteAsset)editedAssetBase);
            selectionMenu.Open(AssetType.Palette);
        }

        protected override void UpdateAsset()
        {
            paletteEditor.UpdateAsset((PaletteAsset)editedAssetBase);
            paletteEditor.CompleteEditing();
            selectionMenu.Open(AssetType.Palette);
        }
    }
}