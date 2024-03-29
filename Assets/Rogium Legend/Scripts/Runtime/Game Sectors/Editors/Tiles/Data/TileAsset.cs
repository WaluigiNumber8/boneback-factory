using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using RedRats.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.Validation;

namespace Rogium.Editors.Tiles
{
    /// <summary>
    /// Contains all data needed for a tile in a pack.
    /// </summary>
    public class TileAsset : AssetWithReferencedSpriteBase
    {
        private Tile tile;
        private TileLayerType layerType;
        private TerrainType terrainType;

        #region Constructors
        public TileAsset()
        {
            this.title = EditorConstants.TileTitle;
            this.icon = EditorConstants.TileIcon;
            this.author = EditorConstants.Author;
            this.creationDate = DateTime.Now;
            this.tile = ScriptableObject.CreateInstance<Tile>();
            this.tile.sprite = this.icon;
            this.layerType = EditorConstants.TileLayer;
            this.terrainType = EditorConstants.TileTerrainType;
            
            GenerateID(EditorAssetIDs.TileIdentifier);
        }
        public TileAsset(TileAsset asset)
        {
            AssetValidation.ValidateTitle(asset.title);
            
            this.id = asset.ID;
            this.icon = asset.Icon;
            this.title = asset.Title;
            this.author = asset.Author;
            this.creationDate = asset.CreationDate;

            this.associatedSpriteID = asset.AssociatedSpriteID;

            this.tile = ScriptableObject.CreateInstance<Tile>();
            this.tile.sprite = this.icon;
            this.layerType = asset.LayerType;
            this.terrainType = asset.TerrainType;
        }
        
        public TileAsset(string id, string title, Sprite icon, string author, string associatedSpriteID, TileLayerType layerType,
                         Color tileColor, TerrainType terrainType, DateTime creationDate)
        {
            AssetValidation.ValidateTitle(title);
            
            this.id = id;
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;
            
            this.associatedSpriteID = associatedSpriteID;
            
            this.tile = ScriptableObject.CreateInstance<Tile>();
            this.tile.sprite = icon;
            this.tile.color = tileColor;
            this.layerType = layerType;
            this.terrainType = terrainType;
        }
        #endregion

        #region Update Values
        public override void UpdateIcon(IAsset newSprite)
        {
            base.UpdateIcon(newSprite);
            SafetyNet.EnsureIsNotNull(tile, "TileObject");
            tile.sprite = newSprite.Icon;
        }

        public void UpdateLayerType(int newLayerType) => UpdateLayerType((TileLayerType)newLayerType);
        public void UpdateLayerType(TileLayerType newLayerType) => layerType = newLayerType;
        public void UpdateTerrainType(int newType) => UpdateTerrainType((TerrainType) newType);
        public void UpdateTerrainType(TerrainType newType) => terrainType = newType;

        #endregion

        public override void ClearAssociatedSprite()
        {
            base.ClearAssociatedSprite();
            icon = EditorConstants.TileIcon;
        }

        public Tile Tile { get => tile; }
        public TileLayerType LayerType { get => layerType; }
        public TerrainType TerrainType { get => terrainType; } 
    }
}