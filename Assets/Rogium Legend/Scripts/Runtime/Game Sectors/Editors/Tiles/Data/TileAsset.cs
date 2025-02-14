using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using RedRats.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;

namespace Rogium.Editors.Tiles
{
    /// <summary>
    /// Contains all data needed for a tile in a pack.
    /// </summary>
    public class TileAsset : AssetWithReferencedSpriteBase
    {
        private Tile tile;
        private TileType type;
        private TileLayerType layerType;
        private TerrainType terrainType;

        private TileAsset() { }

        #region Update Values
        public override void UpdateIcon(IAsset newSprite)
        {
            base.UpdateIcon(newSprite);
            Preconditions.IsNotNull(tile, "TileObject");
            tile.sprite = newSprite.Icon;
        }

        public void UpdateType(int newType) => UpdateType((TileType)newType);
        public void UpdateType(TileType newType) => type = newType;
        public void UpdateLayerType(int newLayerType) => UpdateLayerType((TileLayerType)newLayerType);
        public void UpdateLayerType(TileLayerType newLayerType) => layerType = newLayerType;
        public void UpdateTerrainType(int newType) => UpdateTerrainType((TerrainType) newType);
        public void UpdateTerrainType(TerrainType newType) => terrainType = newType;

        #endregion

        public override void ClearAssociatedSprite()
        {
            base.ClearAssociatedSprite();
            icon = EditorDefaults.Instance.TileIcon;
        }

        public Tile Tile { get => tile; }
        public TileType Type { get => type; }
        public TileLayerType LayerType { get => layerType; }
        public TerrainType TerrainType { get => terrainType; } 
        
        public class Builder : AssetWithReferencedSpriteBuilder<TileAsset, Builder>
        {
            public Builder()
            {
                Asset.title = EditorDefaults.Instance.TileTitle;
                Asset.icon = EditorDefaults.Instance.TileIcon;
                Asset.author = EditorDefaults.Instance.Author;
                Asset.creationDate = DateTime.Now;
                Asset.GenerateID();
                     
                Asset.tile = ScriptableObject.CreateInstance<Tile>();
                Asset.tile.sprite = Asset.icon;
                Asset.type = EditorDefaults.Instance.TileType;
                Asset.layerType = EditorDefaults.Instance.TileLayer;
                Asset.terrainType = EditorDefaults.Instance.TileTerrainType;
            }
            
            public Builder WithTile(Sprite sprite, Color color)
            {
                Asset.tile = ScriptableObject.CreateInstance<Tile>();
                Asset.tile.sprite = sprite;
                Asset.tile.color = color;
                return This;
            }

            public Builder WithType(TileType type)
            {
                Asset.type = type;
                return This;
            }

            public Builder WithLayerType(TileLayerType layerType)
            {
                Asset.layerType = layerType;
                return This;
            }

            public Builder WithTerrainType(TerrainType terrainType)
            {
                Asset.terrainType = terrainType;
                return This;
            }

            public override Builder AsClone(TileAsset asset)
            {
                AsCopy(asset);
                Asset.GenerateID();
                return This;
            }

            public override Builder AsCopy(TileAsset asset)
            {
                Asset.id = asset.ID;
                Asset.title = asset.Title;
                Asset.icon = asset.Icon;
                Asset.author = asset.Author;
                Asset.creationDate = asset.CreationDate;
                Asset.associatedSpriteID = asset.AssociatedSpriteID;
                Asset.tile = asset.Tile;
                Asset.type = asset.Type;
                Asset.layerType = asset.LayerType;
                Asset.terrainType = asset.TerrainType;
                return This;
            }

            protected sealed override TileAsset Asset { get; } = new();
        }
    }
}