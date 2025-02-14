using RedRats.Safety;
using Rogium.Editors.Sprites;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// A base for all assets, which contain a reference to a <see cref="SpriteAsset"/>.
    /// </summary>
    public abstract class AssetWithReferencedSpriteBase : AssetBase, IAssetWithIcon
    {
        protected string associatedSpriteID;
        
        public virtual void UpdateIcon(IAsset newSprite)
        {
            associatedSpriteID = newSprite.ID;
            if (newSprite is not EmptyAsset)
            {
                Preconditions.IsType<SpriteAsset>(newSprite, nameof(newSprite));
                SpriteAsset s = (SpriteAsset) newSprite;
                icon = s.Icon;
                return;
            }
            icon = newSprite.Icon;
        }

        /// <summary>
        /// Clears the associated sprite reference and replaces assets icon with it's default.
        /// </summary>
        public virtual void ClearAssociatedSprite() => associatedSpriteID = string.Empty;

        public string AssociatedSpriteID { get => associatedSpriteID; }

        
        public abstract class AssetWithReferencedSpriteBuilder<T, TBuilder> : BaseBuilder<T, TBuilder> where T : AssetWithReferencedSpriteBase where TBuilder : BaseBuilder<T, TBuilder>
        {
            public TBuilder WithIcon(IAsset newSprite)
            {
                Asset.UpdateIcon(newSprite);
                return This;
            }
            
            public TBuilder WithAssociatedSpriteID(string associatedSpriteID)
            {
                Asset.associatedSpriteID = associatedSpriteID;
                return This;
            }
        }
    }
}