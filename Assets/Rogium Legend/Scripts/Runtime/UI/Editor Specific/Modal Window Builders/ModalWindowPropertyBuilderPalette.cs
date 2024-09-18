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
        private readonly PaletteEditorOverseer paletteEditor = PaletteEditorOverseer.Instance;

        public override void OpenForCreate(Action whenConfirm = null) => OpenWindow(new PaletteAsset.Builder().Build(), () => CreateAsset(whenConfirm), "Creating a new Palette");

        public override void OpenForUpdate(Action whenConfirm = null) => OpenWindow(new PaletteAsset.Builder().AsCopy(paletteEditor.CurrentAsset).Build(), () => UpdateAsset(whenConfirm), $"Updating {paletteEditor.CurrentAsset.Title}");

        private void OpenWindow(PaletteAsset palette, Action onConfirm, string headerText)
        {
            OpenForColumns1(headerText, onConfirm, out Transform col1);
            b.BuildInputField("Title", palette.Title, col1, palette.UpdateTitle);
            b.BuildPlainText("Created by", palette.Author, col1);
            b.BuildPlainText("Created on", palette.CreationDate.ToString(), col1);
            
            editedAssetBase = palette;
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
    }
}