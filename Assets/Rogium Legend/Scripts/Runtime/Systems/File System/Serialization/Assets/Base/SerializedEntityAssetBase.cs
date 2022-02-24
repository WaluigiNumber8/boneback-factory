using Rogium.Editors.Core;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="SpriteAsset"/>.
    /// </summary>
    [System.Serializable]
    public abstract class SerializedEntityAssetBase<T> : SerializedAssetBase<T> where T : EntityAssetBase
    {
        protected int baseDamage;
        protected float useDelay;
        protected float knockbackSelf;
        protected float knockbackOther;
        
        protected SerializedEntityAssetBase(T asset) : base(asset)
        {
            baseDamage = asset.BaseDamage;
            useDelay = asset.UseDelay;
            knockbackSelf = asset.KnockbackSelf;
            knockbackOther = asset.KnockbackOther;
        }
    }
}