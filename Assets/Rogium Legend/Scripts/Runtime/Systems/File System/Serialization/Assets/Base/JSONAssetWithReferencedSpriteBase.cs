using System;
using Rogium.Editors.Core;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="AssetWithReferencedSpriteBase"/>.
    /// </summary>
    [Serializable]
    public abstract class JSONAssetWithReferencedSpriteBase<T> : JSONAssetBase<T> where T : AssetWithReferencedSpriteBase
    {
        public string associatedSpriteID;
        protected JSONAssetWithReferencedSpriteBase(T asset) : base(asset)
        {
            associatedSpriteID = asset.AssociatedSpriteID;
        }
    }
}