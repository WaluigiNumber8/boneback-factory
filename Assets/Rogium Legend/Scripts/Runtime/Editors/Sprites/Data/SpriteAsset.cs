using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.GridSystem;
using System;
using UnityEngine;

namespace Rogium.Editors.Sprites
{
    /// <summary>
    /// Contains all data needed for a Sprite
    /// </summary>
    public class SpriteAsset : AssetBase
    {
        private ObjectGrid<int> spriteData;
        private string preferredPaletteID;
        
        #region Constructors
        public SpriteAsset()
        {
            this.title = EditorDefaults.SpriteName;
            this.icon = EditorDefaults.SpriteIcon;
            this.author = EditorDefaults.Author;
            this.creationDate = DateTime.Now;
            GenerateID(EditorAssetIDs.SpriteIdentifier);

            this.spriteData = new ObjectGrid<int>(EditorDefaults.SpriteSize, EditorDefaults.SpriteSize, () => -1);
        }

        public SpriteAsset(SpriteAsset asset)
        {
            this.id = asset.ID;
            this.title = asset.Title;
            this.icon = asset.Icon;
            this.author = asset.Author;
            this.creationDate = asset.CreationDate;

            this.spriteData = asset.SpriteData;
            this.preferredPaletteID = asset.PreferredPaletteID;
        }
        
        public SpriteAsset(string id, string title, Sprite icon, string author, ObjectGrid<int> spriteData, string preferredPaletteID, DateTime creationDate)
        {
            this.id = id;
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;

            this.spriteData = spriteData;
            this.preferredPaletteID = preferredPaletteID;
        }

        #endregion

        #region Update Values
        public void UpdateSpriteData(ObjectGrid<int> newSpriteData)
        {
            this.spriteData = newSpriteData;
        }

        public void UpdatePreferredPaletteID(string newPaletteID)
        {
            this.preferredPaletteID = newPaletteID;
        }
        #endregion
        
        public ObjectGrid<int> SpriteData { get => spriteData; }
        public string PreferredPaletteID { get => preferredPaletteID; }
    }
}