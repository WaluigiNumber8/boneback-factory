using RedRats.Systems.FileSystem.Serialization;
using Rogium.Editors.Core;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="AnimatedAssetBase"/>.
    /// </summary>
    [System.Serializable]
    public abstract class SerializedAnimatedAssetBase<T> : SerializedAssetBase<T> where T : AnimatedAssetBase
    {
        protected int animationType;
        protected int frameDuration;
        protected SerializedSprite iconAlt;
        
        protected SerializedAnimatedAssetBase(T asset) : base(asset)
        {
            animationType = (int)asset.AnimationType;
            frameDuration = asset.FrameDuration;
            iconAlt = new SerializedSprite(asset.IconAlt);
        }
    }
}