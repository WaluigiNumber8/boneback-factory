using System;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using Rogium.Editors.Tiles;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Editors.PropertyEditor.Builders
{
    /// <summary>
    /// Builds the property editor for <see cref="TileAsset"/>.
    /// </summary>
    public class PropertyEditorBuilderTile : PropertyEditorBuilderBase
    {
        private TileAsset asset;
        private PackAsset currentPack;

        private InteractablePropertyContentBlock terrainTypeBlock;
        public PropertyEditorBuilderTile(Transform contentMain, Transform contentSecond) : base(contentMain, contentSecond) { }

        public void Build(TileAsset asset)
        {
            this.asset = asset;
            currentPack = PackEditorOverseer.Instance.CurrentPack;
            
            Clear();
            BuildColumnImportant(contentMain);
            BuildColumnProperty(contentSecond);
        }
        
        protected override void BuildColumnImportant(Transform content)
        {
            IAsset initialIcon = currentPack.TryGetSprite(asset.AssociatedSpriteID, EditorConstants.TileIcon);
            
            b.BuildAssetField("", AssetType.Sprite, initialIcon, content, a => asset.UpdateIcon(a), null, !currentPack.ContainsAnySprites);
            b.BuildInputField("", asset.Title, content, asset.UpdateTitle);
            b.BuildDropdown("", Enum.GetNames(typeof(TileType)), (int) asset.Type, content, (i) => { asset.UpdateType(i); CheckTerrainEnable(); });
        }

        protected override void BuildColumnProperty(Transform content)
        {
            b.BuildHeader("General", content);
            b.BuildDropdown("Type", Enum.GetNames(typeof(TileLayerType)), (int)asset.LayerType, content, (i) => { asset.UpdateLayerType(i); CheckTerrainEnable(); });
            terrainTypeBlock = b.CreateContentBlockVertical(content, asset.LayerType != TileLayerType.Floor);
            b.BuildDropdown("Terrain", Enum.GetNames(typeof(TerrainType)), (int)asset.TerrainType, terrainTypeBlock.GetTransform, asset.UpdateTerrainType);
        }
        
        private void CheckTerrainEnable()
        {
            bool isFloorTile = asset.Type == TileType.Tile && asset.LayerType == TileLayerType.Floor;
            terrainTypeBlock.SetDisabled(!isFloorTile);
        }
    }
}