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
        private readonly Tile tile;
        private TileType type;
        private TileLayerType layerType;
        private TerrainType terrainType;

        #region Constructors
        public TileAsset()
        {
            InitBase(EditorDefaults.Instance.TileTitle, EditorDefaults.Instance.TileIcon, EditorDefaults.Instance.Author, DateTime.Now);
            GenerateID();
            
            this.tile = ScriptableObject.CreateInstance<Tile>();
            this.tile.sprite = this.icon;
            this.type = EditorDefaults.Instance.TileType;
            this.layerType = EditorDefaults.Instance.TileLayer;
            this.terrainType = EditorDefaults.Instance.TileTerrainType;
            
        }

        public TileAsset(TileAsset asset)
        {
            AssetValidation.ValidateTitle(asset.title);
            
            this.id = asset.ID;
            InitBase(asset.Title, asset.Icon, asset.Author, asset.CreationDate);

            this.associatedSpriteID = asset.AssociatedSpriteID;

            this.tile = ScriptableObject.CreateInstance<Tile>();
            this.tile.sprite = this.icon;
            this.type = asset.Type;
            this.layerType = asset.LayerType;
            this.terrainType = asset.TerrainType;
        }
        
        public TileAsset(string id, string title, Sprite icon, string author, string associatedSpriteID, Color tileColor,
                         TileType type, TileLayerType layerType, TerrainType terrainType, DateTime creationDate)
        {
            AssetValidation.ValidateTitle(title);
            
            this.id = id;
            InitBase(title, icon, author, creationDate);
            
            this.associatedSpriteID = associatedSpriteID;
            
            this.tile = ScriptableObject.CreateInstance<Tile>();
            this.tile.sprite = icon;
            this.tile.color = tileColor;
            this.type = type;
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
    }
}