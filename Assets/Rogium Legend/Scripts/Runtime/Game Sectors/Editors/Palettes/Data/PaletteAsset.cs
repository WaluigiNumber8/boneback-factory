using System;
using System.Collections.Generic;
using System.Linq;
using RedRats.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Sprites;
using UnityEngine;

namespace Rogium.Editors.Palettes
{
    /// <summary>
    /// Contains all data needed for a palette.
    /// </summary>
    public class PaletteAsset : AssetWithDirectSpriteBase, IAssetForAssociation
    {
        private Color[] colors;
        private ISet<string> associatedAssetsIDs;

        private PaletteAsset() { }
        
        public void AddAssociation(string id) => associatedAssetsIDs.Add(id);
        public void RemoveAssociation(string id) => associatedAssetsIDs.Remove(id);

        public Color[] Colors { get => colors; }
        public ISet<string> AssociatedAssetsIDs { get => associatedAssetsIDs; }
        
        
        public class Builder : BaseBuilder<PaletteAsset, Builder>
        {
            public Builder()
            {
                Asset.title = EditorDefaults.Instance.PaletteTitle;
                Asset.icon = EditorDefaults.Instance.EmptySprite;
                Asset.author = EditorDefaults.Instance.Author;
                Asset.creationDate = DateTime.Now;
                Asset.GenerateID();
                
                Asset.colors = RedRatBuilder.GenerateColorArray(EditorDefaults.Instance.PaletteSize, Color.black);
                Asset.associatedAssetsIDs = new HashSet<string>();
            }
            
            public Builder WithColors(Color[] colors)
            {
                Asset.colors = colors.AsCopy();
                return This;
            }
            
            public Builder WithAssociatedAssetIDs(ISet<string> ids)
            {
                Asset.associatedAssetsIDs = new HashSet<string>(ids);
                return This;
            }
            
            public override Builder AsClone(PaletteAsset asset)
            {
                AsCopy(asset);
                Asset.associatedAssetsIDs.Clear();
                Asset.GenerateID();
                return This;
            }

            public override Builder AsCopy(PaletteAsset asset)
            {
                Asset.id = asset.ID;
                Asset.title = asset.Title;
                Asset.icon = asset.Icon;
                Asset.author = asset.Author;
                Asset.creationDate = asset.CreationDate;
                Asset.colors = asset.Colors.AsCopy();
                Asset.associatedAssetsIDs = new HashSet<string>(asset.AssociatedAssetsIDs);
                return This;
            }

            protected sealed override PaletteAsset Asset { get; } = new();
        }
    }
}
