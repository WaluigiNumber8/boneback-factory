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
        public bool knockbackLockDirectionSelf;
        public float knockbackForceOther;
        public bool knockbackLockDirectionOther;
        
        protected JSONEntityAssetBase(T asset) : base(asset)
        {
            baseDamage = asset.BaseDamage;
            useDelay = asset.UseDelay;
            knockbackForceSelf = asset.KnockbackForceSelf;
            knockbackLockDirectionSelf = asset.KnockbackLockDirectionSelf;
            knockbackForceOther = asset.KnockbackForceOther;
            knockbackLockDirectionOther = asset.KnockbackLockDirectionOther;
        }
    }
}