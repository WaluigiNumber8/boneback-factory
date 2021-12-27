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
            OpenWindow(new TileAsset(), CreateAsset, "Creating a new Tile");
        }

        public override void OpenForUpdate()
        {
            OpenWindow(new TileAsset(tileEditor.CurrentAsset), UpdateAsset, $"Updating {tileEditor.CurrentAsset.Title}");
        }

        private void OpenWindow(TileAsset tile, Action onConfirmAction, string headerText)
        {
            propertyBuilder.BuildInputField("Title", tile.Title, window.FirstColumnContent, tile.UpdateTitle);
            propertyBuilder.BuildDropdown("Type", Enum.GetNames(typeof(TileType)).ToList(), 0, window.FirstColumnContent, tile.UpdateTileType);
            propertyBuilder.BuildPlainText("Created by", tile.Author, window.FirstColumnContent);
            propertyBuilder.BuildPlainText("Created on", tile.CreationDate.ToString(), window.FirstColumnContent);
            
            editedAssetBase = tile;
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