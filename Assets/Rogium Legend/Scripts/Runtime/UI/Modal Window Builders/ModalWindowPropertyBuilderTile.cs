using System;
using System.Linq;
using BoubakProductions.UI;
using Rogium.Editors.TileData;

namespace Rogium.UserInterface.UI
{
    /// <summary>
    /// Constructor for the Room Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderTile : ModalWindowPropertyBuilder
    {
        private TileEditorOverseer tileEditor;

        public ModalWindowPropertyBuilderTile()
        {
            tileEditor = TileEditorOverseer.Instance;
        }
        
        public override void OpenForCreate()
        {
            OpenWindow(new TileAsset(), CreateAsset, "Creating new tile");
        }

        public override void OpenForUpdate()
        {
            OpenWindow(new TileAsset(tileEditor.CurrentTile), UpdateAsset, $"Updating {tileEditor.CurrentTile.Title}");
        }

        private void OpenWindow(TileAsset currentTileAsset, Action onConfirmAction, string headerText)
        {
            propertyBuilder.BuildInputField("Title", currentTileAsset.Title, window.FirstColumnContent, currentTileAsset.UpdateTitle);
            propertyBuilder.BuildDropdown("Type", Enum.GetNames(typeof(TileType)).ToList(), 0, window.FirstColumnContent, currentTileAsset.UpdateTileType);
            propertyBuilder.BuildPlainText("Created by", currentTileAsset.Author, window.FirstColumnContent);
            propertyBuilder.BuildPlainText("Created on", currentTileAsset.CreationDate.ToString(), window.FirstColumnContent);
            
            editedAssetBase = currentTileAsset;
            window.OpenAsPropertiesColumn1(headerText, ThemeType.Blue, "Done", "Cancel", onConfirmAction, true);
        }

        protected override void CreateAsset()
        {
            editor.CreateNewTile((TileAsset)editedAssetBase);
            selectionMenu.ReopenForTiles();
        }

        protected override void UpdateAsset()
        {
            tileEditor.UpdateAsset((TileAsset)editedAssetBase);
            tileEditor.CompleteEditing();
            selectionMenu.ReopenForTiles();
        }
    }
}