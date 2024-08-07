using System;
using RedRats.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using UnityEngine;

namespace Rogium.Editors.Palettes
{
    /// <summary>
    /// Contains all data needed for a palette.
    /// </summary>
    public class PaletteAsset : AssetWithDirectSpriteBase
    {
        private Color[] colors;

        private PaletteAsset() { }
        
        public Color[] Colors { get => colors; }
        
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
            }
            
            public Builder WithColors(Color[] colors)
            {
                Asset.colors = colors;
                return This;
            }
            
            public override Builder AsClone(PaletteAsset asset)
            {
                AsCopy(asset);
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
                Asset.colors = asset.Colors;
                return This;
            }

            protected sealed override PaletteAsset Asset { get; } = new();
        }
   
    }
}
