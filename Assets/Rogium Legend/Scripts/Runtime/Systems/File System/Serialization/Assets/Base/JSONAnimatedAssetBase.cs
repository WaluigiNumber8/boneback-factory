using RedRats.Systems.FileSystem.JSON.Serialization;
using Rogium.Editors.Core;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="AnimatedAssetBase"/>.
    /// </summary>
    [System.Serializable]
    public abstract class JSONAnimatedAssetBase<T> : JSONAssetWithReferencedSpriteBase<T> where T : AnimatedAssetBase
    {
        public int animationType;
        public int frameDuration;
        public JSONSprite iconAlt;
        
        protected JSONAnimatedAssetBase(T asset) : base(asset)
        {
            animationType = (int)asset.AnimationType;
            frameDuration = asset.FrameDuration;
            iconAlt = new JSONSprite(asset.IconAlt);
        }
    }
}