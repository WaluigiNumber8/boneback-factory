using System;
using RedRats.Safety;
using Rogium.Editors.Sprites;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// A base for all assets, which contain a reference to a <see cref="SpriteAsset"/>.
    /// </summary>
    public abstract class AssetWithReferencedSpriteBase : AssetBase
    {
        public static event Action<string, string> OnRemoveAssociation; 
        
        protected string associatedSpriteID;
        
        public virtual void UpdateIcon(IAsset newSprite)
        {
            SafetyNet.EnsureIsType<SpriteAsset>(newSprite, nameof(newSprite));
            SpriteAsset s = (SpriteAsset) newSprite;

            if (!string.IsNullOrEmpty(associatedSpriteID) && associatedSpriteID != newSprite.ID)
            {
                OnRemoveAssociation?.Invoke(associatedSpriteID, ID);
            }
            associatedSpriteID = newSprite.ID;
            s.TryAddAssociation(this);
            icon = s.Icon;
        }
        
        public string AssociatedSpriteID { get => associatedSpriteID; }
    }
}