using Rogium.Editors.Core;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="EntityAssetBase"/>.
    /// </summary>
    [System.Serializable]
    public abstract class SerializedEntityAssetBase<T> : SerializedAssetBase<T> where T : EntityAssetBase
    {
        protected int baseDamage;
        protected float useDelay;
        protected float knockbackForceSelf;
        protected float knockbackTimeSelf;
        protected float knockbackForceOther;
        protected float knockbackTimeOther;
        
        protected SerializedEntityAssetBase(T asset) : base(asset)
        {
            baseDamage = asset.BaseDamage;
            useDelay = asset.UseDelay;
            knockbackForceSelf = asset.KnockbackForceSelf;
            knockbackTimeSelf = asset.KnockbackTimeSelf;
            knockbackForceOther = asset.KnockbackForceOther;
            knockbackTimeOther = asset.KnockbackTimeOther;
        }
    }
}