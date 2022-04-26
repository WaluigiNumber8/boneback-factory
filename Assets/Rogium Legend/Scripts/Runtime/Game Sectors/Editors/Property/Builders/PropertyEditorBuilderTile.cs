using System;
using BoubakProductions.Core;
using BoubakProductions.UI;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
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
        
        public PropertyEditorBuilderTile(Transform contentMain, Transform contentSecond) : base(contentMain, contentSecond) { }

        public void Build(TileAsset asset)
        {
            this.asset = asset;
            Clear();
            BuildImportant(contentMain);
            BuildProperty(contentSecond);
        }
        
        protected override void BuildImportant(Transform content)
        {
            b.BuildInputField("", asset.Title, content, asset.UpdateTitle);
            b.BuildAssetField("", AssetType.Sprite, asset, content, delegate(IAsset a) { asset.UpdateIcon(a.Icon);}, !PackEditorOverseer.Instance.CurrentPack.ContainsAnyTiles, ThemeType.Yellow);
        }

        protected override void BuildProperty(Transform content)
        {
            b.BuildHeader("General", content);
            b.BuildDropdown("Type", Enum.GetNames(typeof(TileType)), (int)asset.Type, content, asset.UpdateTileType);
        }
    }
}