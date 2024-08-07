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
        private ISet<string> associatedAssetsIDs;

        private SpriteAsset() { }
        
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
        
        public class Builder : BaseBuilder<SpriteAsset, Builder>
        {
            public Builder()
            {
                Asset.title = EditorDefaults.Instance.SpriteTitle;
                Asset.icon = EditorDefaults.Instance.EmptySprite;
                Asset.author = EditorDefaults.Instance.Author;
                Asset.creationDate = DateTime.Now;
                Asset.GenerateID();
                
                Asset.spriteData = new ObjectGrid<int>(EditorDefaults.Instance.SpriteSize, EditorDefaults.Instance.SpriteSize, () => -1);
                Asset.preferredPaletteID = string.Empty;
                Asset.associatedAssetsIDs = new HashSet<string>();
            }
            
            public Builder WithSpriteData(ObjectGrid<int> spriteData)
            {
                Asset.spriteData = new ObjectGrid<int>(spriteData);
                return This;
            }
            
            public Builder WithPreferredPaletteID(string paletteID)
            {
                Asset.preferredPaletteID = paletteID;
                return This;
            }
            
            public Builder WithAssociatedAssetIDs(ISet<string> associatedAssetIDs)
            {
                Asset.associatedAssetsIDs = new HashSet<string>(associatedAssetIDs);
                return This;
            }
            
            public override Builder AsClone(SpriteAsset asset)
            {
                AsCopy(asset);
                Asset.GenerateID();
                return This;
            }

            public override Builder AsCopy(SpriteAsset asset)
            {
                Asset.id = asset.ID;
                Asset.title = asset.Title;
                Asset.icon = asset.Icon;
                Asset.author = asset.Author;
                Asset.creationDate = asset.CreationDate;
                Asset.spriteData = new ObjectGrid<int>(asset.SpriteData);
                Asset.preferredPaletteID = asset.PreferredPaletteID;
                Asset.associatedAssetsIDs = new HashSet<string>(asset.AssociatedAssetsIDs);
                return This;
            }

            protected sealed override SpriteAsset Asset { get; } = new();
        }
    }
}