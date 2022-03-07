using BoubakProductions.Safety;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// A base for all entity type assets.
    /// </summary>
    public abstract class EntityAssetBase : AssetBase
    {
        protected int baseDamage;
        protected float useDelay;
        protected float knockbackForceSelf;
        protected float knockbackForceOther;

        #region Update Values
        public void UpdateBaseDamage(int newBaseDamage) => baseDamage = newBaseDamage;
        public void UpdateUseDelay(float newUseDelay)
        {
            SafetyNet.EnsureFloatIsBiggerOrEqualTo(newUseDelay, 0, "New UseDelay");
            useDelay = newUseDelay;
        }

        public void UpdateKnockbackSelf(float newKnockbackSelf) => knockbackForceSelf = newKnockbackSelf;
        public void UpdateKnockbackOther(float newKnockbackOther) => knockbackForceOther = newKnockbackOther;
        #endregion
        
        public int BaseDamage { get => baseDamage; }
        public float UseDelay { get => useDelay; }
        public float KnockbackSelf { get => knockbackForceSelf; }
        public float KnockbackOther { get => knockbackForceOther; }
    }
}