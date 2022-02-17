using BoubakProductions.Core;
using BoubakProductions.UI;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Tiles;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Editors.PropertyEditor
{
    /// <summary>
    /// Fills the Property Editor with properties, corresponding to each asset type.
    /// </summary>
    public class PropertyEditorBuilder
    {
        private UIPropertyBuilder builder;
        
        private Transform importantContent;
        private Transform propertyContent;

        public PropertyEditorBuilder(Transform importantContent, Transform propertyContent)
        {
            builder = UIPropertyBuilder.GetInstance();
            
            this.importantContent = importantContent;
            this.propertyContent = propertyContent;
        }

        /// <summary>
        /// Prepares the Property Editor for Tile Editing.
        /// </summary>
        /// <param name="asset"></param>
        public void BuildForTiles(TileAsset asset)
        {
            Build();
            ImportantTile(importantContent, asset);
            PropertiesTile(propertyContent, asset);
        }

        private void ImportantTile(Transform content, TileAsset asset)
        {
            builder.BuildInputField("", asset.Title, content, asset.UpdateTitle);
            builder.BuildAssetField("", AssetType.Sprite, asset, content, delegate(AssetBase a) { asset.UpdateIcon(a.Icon);}, ThemeType.Yellow);
        }

        private void PropertiesTile(Transform content, TileAsset asset)
        {
            builder.BuildHeader("General", content);
            builder.BuildDropdown("Type", EnumUtils.ToStringList(typeof(TileType)), (int)asset.Type, content, asset.UpdateTileType);
        }

        private void Build()
        {
            importantContent.gameObject.KillChildren();
            propertyContent.gameObject.KillChildren();
        }
        
        
    }
}