using System;
using System.Linq;
using RedRats.Core;
using RedRats.UI;
using RedRats.UI.ModalWindows;
using Rogium.Editors.Tiles;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Room Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderTile : ModalWindowPropertyBuilder
    {
        private readonly TileEditorOverseer tileEditor;

        public ModalWindowPropertyBuilderTile() => tileEditor = TileEditorOverseer.Instance;

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
            b.BuildInputField("Title", tile.Title, windowColumn1, tile.UpdateTitle);
            b.BuildDropdown("Type", Enum.GetNames(typeof(TileType)), (int)tile.Type, windowColumn1, tile.UpdateTileType);
            b.BuildPlainText("Created by", tile.Author, windowColumn1);
            b.BuildPlainText("Created on", tile.CreationDate.ToString(), windowColumn1);
            
            editedAssetBase = tile;
            Open(new PropertyWindowInfo(headerText, PropertyLayoutType.Column1, ThemeType.Yellow, "Done", "Cancel", onConfirmAction));
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