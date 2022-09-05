using Rogium.Editors.Core;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="EntityAssetBase"/>.
    /// </summary>
    [System.Serializable]
    public abstract class JSONEntityAssetBase<T> : JSONAnimatedAssetBase<T> where T : EntityAssetBase
    {
        public int baseDamage;
        public float useDelay;
        public float knockbackForceSelf;
        public float knockbackTimeSelf;
        public bool knockbackLockDirectionSelf;
        public float knockbackForceOther;
        public float knockbackTimeOther;
        public bool knockbackLockDirectionOther;
        
        protected JSONEntityAssetBase(T asset) : base(asset)
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