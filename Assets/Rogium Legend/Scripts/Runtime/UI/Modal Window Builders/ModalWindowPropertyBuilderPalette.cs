using System;
using BoubakProductions.UI;
using Rogium.Editors.Palettes;

namespace Rogium.UserInterface.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Palette Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderPalette : ModalWindowPropertyBuilder
    {
        private PaletteEditorOverseer paletteEditor;

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
            builder.BuildInputField("Title", palette.Title, window.FirstColumnContent, palette.UpdateTitle);
            builder.BuildPlainText("Created by", palette.Author, window.FirstColumnContent);
            builder.BuildPlainText("Created on", palette.CreationDate.ToString(), window.FirstColumnContent);
            
            editedAssetBase = palette;
            window.OpenAsPropertiesColumn1(headerText, ThemeType.Purple, "Done", "Cancel", onConfirmAction, true);
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