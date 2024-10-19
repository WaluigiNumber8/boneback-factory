using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.GridSystem;
using System;
using System.Collections.Generic;
using RedRats.Safety;
using Rogium.Editors.Palettes;
using Rogium.Systems.IconBuilders;

namespace Rogium.Editors.Sprites
{
    /// <summary>
    /// Contains all data needed for a Sprite.
    /// </summary>
    public class SpriteAsset : AssetWithDirectSpriteBase, IAssetForAssociation, IAssetWithPalette
    {
        private ObjectGrid<int> spriteData;
        private string associatedPaletteID;
        private ISet<string> associatedAssetsIDs;

        private SpriteAsset() { }
        
        #region Update Values
        public void UpdateSpriteData(ObjectGrid<int> newSpriteData) => spriteData = new ObjectGrid<int>(newSpriteData);
        public void UpdateAssociatedPaletteID(string newPaletteID) => associatedPaletteID = newPaletteID;

        #endregion
        
        public void AddAssociation(string id) => associatedAssetsIDs.Add(id);
        
        public void RemoveAssociation(string id) => associatedAssetsIDs.Remove(id);
        public void ClearAssociatedPalette()
        {
            associatedPaletteID = EditorDefaults.EmptyAssetID;
            UpdateIcon(IconBuilder.DrawFromGrid(SpriteData, EditorDefaults.Instance.MissingPalette));
        }

        public void UpdatePalette(IAsset newPalette)
        {
            associatedPaletteID = newPalette.ID;
            if (newPalette is not EmptyAsset)
            {
                SafetyNet.EnsureIsType<PaletteAsset>(newPalette, nameof(newPalette));
                PaletteAsset p = (PaletteAsset) newPalette;
                icon = IconBuilder.DrawFromGrid(SpriteData, p.Colors);
                return;
            }
            icon = IconBuilder.DrawFromGrid(SpriteData, EditorDefaults.Instance.MissingPalette);
        }
        
        public ObjectGrid<int> SpriteData { get => spriteData; }
        public string AssociatedPaletteID { get => associatedPaletteID; }
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
                Asset.associatedPaletteID = string.Empty;
                Asset.associatedAssetsIDs = new HashSet<string>();
            }
            
            public Builder WithSpriteData(ObjectGrid<int> spriteData)
            {
                Asset.spriteData = new ObjectGrid<int>(spriteData);
                return This;
            }
            
            public Builder WithAssociatedPaletteID(string paletteID)
            {
                Asset.associatedPaletteID = paletteID;
                return This;
            }
            
            public Builder WithAssociatedAssetIDs(ISet<string> associatedAssetIDs)
            {
                Asset.associatedAssetsIDs = new HashSet<string>(associatedAssetIDs ?? new HashSet<string>());
                return This;
            }
            
            public override Builder AsClone(SpriteAsset asset)
            {
                AsCopy(asset);
                Asset.GenerateID();
                Asset.associatedAssetsIDs.Clear();
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
                Asset.associatedPaletteID = asset.AssociatedPaletteID;
                Asset.associatedAssetsIDs = new HashSet<string>(asset.AssociatedAssetsIDs);
                return This;
            }

            protected sealed override SpriteAsset Asset { get; } = new();
        }

    }
}