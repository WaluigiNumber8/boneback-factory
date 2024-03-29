using System;
using RedRats.Systems.Themes;
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
            BuildColumnImportant(contentMain);
            BuildColumnProperty(contentSecond);
        }
        
        protected override void BuildColumnImportant(Transform content)
        {
            b.BuildAssetField("", AssetType.Sprite, asset, content, delegate(IAsset a) { asset.UpdateIcon(a);}, null, !PackEditorOverseer.Instance.CurrentPack.ContainsAnyTiles, ThemeType.Yellow);
            b.BuildInputField("", asset.Title, content, asset.UpdateTitle);
            b.BuildDropdown("", Enum.GetNames(typeof(TileType)), (int)asset.Type, content, asset.UpdateType);
        }

        protected override void BuildColumnProperty(Transform content)
        {
            b.BuildHeader("General", content);
            b.BuildDropdown("Type", Enum.GetNames(typeof(TileLayerType)), (int)asset.LayerType, content, asset.UpdateLayerType);
            if (asset.LayerType == TileLayerType.Floor) b.BuildDropdown("Terrain", Enum.GetNames(typeof(TerrainType)), (int)asset.TerrainType, content, asset.UpdateTerrainType);
        }
    }
}