using System;
using System.Collections.Generic;
using System.Linq;
using Rogium.Editors.TileData;

namespace Rogium.Global.UISystem.UI
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
            OpenWindow(tileEditor.CurrentTile, UpdateAsset, $"Updating {tileEditor.CurrentTile.Title}");
        }

        private void OpenWindow(TileAsset currentTileAsset, Action onConfirmAction, string headerText)
        {

            propertyBuilder.BuildInputField("Title", currentTileAsset.Title, window.FirstColumnContent, currentTileAsset.UpdateTitle);
            propertyBuilder.BuildDropdown("Type", Enum.GetNames(typeof(TileType)).ToList(), 0, window.FirstColumnContent, currentTileAsset.UpdateTileType);
            propertyBuilder.BuildPlainText("Created by", currentTileAsset.Author, window.FirstColumnContent);
            propertyBuilder.BuildPlainText("Created on", currentTileAsset.CreationDate.ToString(), window.FirstColumnContent);
            
            editedAssetBase = currentTileAsset;
            window.OpenAsPropertiesColumn1(headerText, "Done", "Cancel", onConfirmAction, true);
        }

        protected override void CreateAsset()
        {
            editor.CreateNewTile((TileAsset)editedAssetBase);
            selectionMenu.ReopenForTiles();
        }

        protected override void UpdateAsset()
        {
            tileEditor.CompleteEditing();
            selectionMenu.ReopenForTiles();
        }
    }
}