using BoubakProductions.Core;
using BoubakProductions.UI;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Tiles;
using UnityEngine;

namespace Rogium.Editors.PropertyEditor.Builders
{
    /// <summary>
    /// Builds the property editor for <see cref="TileAsset"/>.
    /// </summary>
    public class PropertyEditorBuilderTile : PropertyEditorBuilderBase
    {
        private TileAsset asset;
        
        public PropertyEditorBuilderTile(Transform importantContent, Transform propertyContent) : base(importantContent, propertyContent) { }

        public void Build(TileAsset asset)
        {
            this.asset = asset;
            EmptyEditor();
            BuildImportant(importantContent);
            BuildProperty(propertyContent);
        }
        
        protected override void BuildImportant(Transform content)
        {
            builder.BuildInputField("", asset.Title, content, asset.UpdateTitle);
            builder.BuildAssetField("", AssetType.Sprite, asset, content, delegate(AssetBase a) { asset.UpdateIcon(a.Icon);}, ThemeType.Yellow);
        }

        protected override void BuildProperty(Transform content)
        {
            builder.BuildHeader("General", content);
            builder.BuildDropdown("Type", EnumUtils.ToStringList(typeof(TileType)), (int)asset.Type, content, asset.UpdateTileType);
        }
    }
}