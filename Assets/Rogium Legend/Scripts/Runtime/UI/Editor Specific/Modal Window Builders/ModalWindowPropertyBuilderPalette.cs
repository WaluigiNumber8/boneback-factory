using System;
using RedRats.UI;
using RedRats.UI.ModalWindows;
using Rogium.Editors.Core;
using Rogium.Editors.Palettes;

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

        private void OpenWindow(PaletteAsset palette, Action onConfirmAction, string headerText)
        {
            b.BuildInputField("Title", palette.Title, windowColumn1, palette.UpdateTitle);
            b.BuildPlainText("Created by", palette.Author, windowColumn1);
            b.BuildPlainText("Created on", palette.CreationDate.ToString(), windowColumn1);
            
            editedAssetBase = palette;
            Open(new PropertyWindowInfo(headerText, PropertyLayoutType.Column1, ThemeType.Purple, "Done", "Cancel", onConfirmAction));
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