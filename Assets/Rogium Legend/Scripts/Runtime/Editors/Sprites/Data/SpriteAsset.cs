using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using System;
using BoubakProductions.Core;
using UnityEngine;

namespace Rogium.Editors.Sprites
{
    /// <summary>
    /// Contains all data needed for a Sprite
    /// </summary>
    public class SpriteAsset : AssetBase
    {
        private Sprite sprite;

        #region Constructors
        public SpriteAsset()
        {
            this.title = EditorDefaults.SpriteName;
            this.icon = EditorDefaults.SpriteIcon;
            this.author = EditorDefaults.Author;
            this.creationDate = DateTime.Now;
            GenerateID(EditorAssetIDs.SpriteIdentifier);

            this.sprite = BoubakBuilder.GenerateSprite(16, 16, 16);
        }

        public SpriteAsset(SpriteAsset asset)
        {
            this.id = asset.ID;
            this.title = asset.Title;
            this.icon = asset.Icon;
            this.author = asset.Author;
            this.creationDate = asset.CreationDate;

            this.sprite = asset.Sprite;
        }
        
        public SpriteAsset(string id, string title, Sprite icon, string author, Sprite sprite, DateTime creationDate)
        {
            this.id = id;
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;

            this.sprite = sprite;
        }

        #endregion

        #region Update Values
        public void UpdateSprite(Sprite newSprite)
        {
            this.sprite = newSprite;
        }
        #endregion
        
        public Sprite Sprite {get => sprite;}
    }
}