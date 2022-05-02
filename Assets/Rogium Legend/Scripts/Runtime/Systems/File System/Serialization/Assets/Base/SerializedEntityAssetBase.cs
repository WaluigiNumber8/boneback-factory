using Rogium.Editors.Core;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="EntityAssetBase"/>.
    /// </summary>
    [System.Serializable]
    public abstract class SerializedEntityAssetBase<T> : SerializedAnimatedAssetBase<T> where T : EntityAssetBase
    {
        protected int baseDamage;
        protected float useDelay;
        protected float knockbackForceSelf;
        protected float knockbackTimeSelf;
        protected bool knockbackLockDirectionSelf;
        protected float knockbackForceOther;
        protected float knockbackTimeOther;
        protected bool knockbackLockDirectionOther;
        
        protected SerializedEntityAssetBase(T asset) : base(asset)
        {
            baseDamage = asset.BaseDamage;
            useDelay = asset.UseDelay;
            knockbackForceSelf = asset.KnockbackForceSelf;
            knockbackTimeSelf = asset.KnockbackTimeSelf;
            knockbackLockDirectionSelf = asset.KnockbackLockDirectionSelf;
            knockbackForceOther = asset.KnockbackForceOther;
            knockbackTimeOther = asset.KnockbackTimeOther;
            knockbackLockDirectionOther = asset.KnockbackLockDirectionOther;
        }
    }
}