using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.GridSystem;
using System;
using System.Collections.Generic;
using Rogium.Systems.Validation;
using UnityEngine;

namespace Rogium.Editors.Sprites
{
    /// <summary>
    /// Contains all data needed for a Sprite.
    /// </summary>
    public class SpriteAsset : AssetWithDirectSpriteBase
    {
        private ObjectGrid<int> spriteData;
        private string preferredPaletteID;
        private readonly ISet<string> associatedAssetsIDs;

        #region Constructors
        public SpriteAsset()
        {
            InitBase(EditorDefaults.Instance.SpriteTitle, EditorDefaults.Instance.EmptySprite, EditorDefaults.Instance.Author, DateTime.Now);
            GenerateID(EditorAssetIDs.SpriteIdentifier);

            this.spriteData = new ObjectGrid<int>(EditorDefaults.Instance.SpriteSize, EditorDefaults.Instance.SpriteSize, () => -1);
            this.associatedAssetsIDs = new HashSet<string>();
        }

        public SpriteAsset(SpriteAsset asset)
        {
            AssetValidation.ValidateTitle(asset.title);
            
            this.id = asset.ID;
            InitBase(asset.Title, asset.Icon, asset.Author, asset.CreationDate);

            this.spriteData = new ObjectGrid<int>(asset.SpriteData);
            this.preferredPaletteID = asset.PreferredPaletteID;
            this.associatedAssetsIDs = new HashSet<string>(asset.AssociatedAssetsIDs);
        }
        
        public SpriteAsset(string id, string title, Sprite icon, string author, ObjectGrid<int> spriteData, 
                           string preferredPaletteID, ISet<string> associatedAssets, DateTime creationDate)
        {
            AssetValidation.ValidateTitle(title);
            
            this.id = id;
            InitBase(title, icon, author, creationDate);

            this.spriteData = new ObjectGrid<int>(spriteData);
            this.preferredPaletteID = preferredPaletteID;
            this.associatedAssetsIDs = new HashSet<string>(associatedAssets);
        }

        #endregion

        #region Update Values
        public void UpdateSpriteData(ObjectGrid<int> newSpriteData) => spriteData = new ObjectGrid<int>(newSpriteData);
        public void UpdatePreferredPaletteID(string newPaletteID) => preferredPaletteID = newPaletteID;

        #endregion
        
        public void TryAddAssociation(IIDHolder newReferencedAsset) => TryAddAssociation(newReferencedAsset.ID);
        public void TryAddAssociation(string id) => associatedAssetsIDs.Add(id);
        
        public void TryRemoveAssociation(IIDHolder referencedAsset) => TryRemoveAssociation(referencedAsset.ID);
        public void TryRemoveAssociation(string id) => associatedAssetsIDs.Remove(id);

        public ObjectGrid<int> SpriteData { get => spriteData; }
        public string PreferredPaletteID { get => preferredPaletteID; }
        public ISet<string> AssociatedAssetsIDs { get => associatedAssetsIDs; }
    }
}