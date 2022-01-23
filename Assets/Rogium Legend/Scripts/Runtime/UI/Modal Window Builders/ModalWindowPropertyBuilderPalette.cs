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
            propertyBuilder.BuildInputField("Title", palette.Title, window.FirstColumnContent, palette.UpdateTitle);
            propertyBuilder.BuildPlainText("Created by", palette.Author, window.FirstColumnContent);
            propertyBuilder.BuildPlainText("Created on", palette.CreationDate.ToString(), window.FirstColumnContent);
            
            editedAssetBase = palette;
            window.OpenAsPropertiesColumn1(headerText, ThemeType.Blue, "Done", "Cancel", onConfirmAction, true);
        }
        
        protected override void CreateAsset()
        {
            editor.CreateNewPalette((PaletteAsset)editedAssetBase);
            selectionMenu.ReopenForPalettes();
        }

        protected override void UpdateAsset()
        {
            paletteEditor.UpdateAsset((PaletteAsset)editedAssetBase);
            paletteEditor.CompleteEditing();
            selectionMenu.ReopenForPalettes();
        }
    }
}