using System;
using System.Linq;
using BoubakProductions.Core;
using BoubakProductions.UI;
using Rogium.Editors.Tiles;

namespace Rogium.UserInterface.ModalWindowBuilding
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
            builder.BuildInputField("Title", tile.Title, window.FirstColumnContent, tile.UpdateTitle);
            builder.BuildDropdown("Type", EnumUtils.ToStringList(typeof(TileType)), (int)tile.Type, window.FirstColumnContent, tile.UpdateTileType);
            builder.BuildPlainText("Created by", tile.Author, window.FirstColumnContent);
            builder.BuildPlainText("Created on", tile.CreationDate.ToString(), window.FirstColumnContent);
            
            editedAssetBase = tile;
            window.OpenAsPropertiesColumn1(headerText, ThemeType.Yellow, "Done", "Cancel", onConfirmAction, true);
        }

        protected override void CreateAsset()
        {
            editor.CreateNewTile((TileAsset)editedAssetBase);
            selectionMenu.OpenForTiles();
        }

        protected override void UpdateAsset()
        {
            tileEditor.UpdateAsset((TileAsset)editedAssetBase);
            tileEditor.CompleteEditing();
            selectionMenu.OpenForTiles();
        }
    }
}