using System;
using Rogium.Editors.Palettes;
using UnityEngine;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Palette Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderPalette : ModalWindowPropertyBuilder
    {
        private readonly PaletteEditorOverseer paletteEditor;

        public ModalWindowPropertyBuilderPalette()
        {
            paletteEditor = PaletteEditorOverseer.Instance;;
        }
        
        public override void OpenForCreate()
        {
            OpenWindow(new PaletteAsset(), CreateAsset, "Creating a new Palette");
        }

        public override void OpenForUpdate()
        {
            OpenWindow(new PaletteAsset(paletteEditor.CurrentAsset), UpdateAsset, $"Updating {paletteEditor.CurrentAsset.Title}");
        }

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
            selectionMenu.OpenForPalettes();
        }

        protected override void UpdateAsset()
        {
            paletteEditor.UpdateAsset((PaletteAsset)editedAssetBase);
            paletteEditor.CompleteEditing();
            selectionMenu.OpenForPalettes();
        }
    }
}