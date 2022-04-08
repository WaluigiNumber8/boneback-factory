using BoubakProductions.Safety;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// A base for all entity type assets.
    /// </summary>
    public abstract class EntityAssetBase : AnimatedAssetBase
    {
        protected int baseDamage;
        protected float useDelay;
        protected float knockbackForceSelf;
        protected float knockbackTimeSelf;
        protected float knockbackForceOther;
        protected float knockbackTimeOther;

        #region Update Values
        public void UpdateBaseDamage(int newBaseDamage) => baseDamage = newBaseDamage;
        public void UpdateUseDelay(float newUseDelay)
        {
            SafetyNet.EnsureFloatIsBiggerOrEqualTo(newUseDelay, 0, "New UseDelay");
            useDelay = newUseDelay;
        }

        public void UpdateKnockbackForceSelf(float newKnockbackSelf) => knockbackForceSelf = newKnockbackSelf;
        public void UpdateKnockbackTimeSelf(float newKnockbackTime)
        {
            SafetyNet.EnsureFloatIsBiggerOrEqualTo(newKnockbackTime, 0, "New Knockback time self");
            knockbackTimeSelf = newKnockbackTime;
        }
        public void UpdateKnockbackForceOther(float newKnockbackOther) => knockbackForceOther = newKnockbackOther;
        public void UpdateKnockbackTimeOther(float newKnockbackTime)
        {
            SafetyNet.EnsureFloatIsBiggerOrEqualTo(newKnockbackTime, 0, "New Knockback time other");
            knockbackTimeOther = newKnockbackTime;
        }
        #endregion
        
        public int BaseDamage { get => baseDamage; }
        public float UseDelay { get => useDelay; }
        public float KnockbackForceSelf { get => knockbackForceSelf; }
        public float KnockbackTimeSelf { get => knockbackTimeSelf; }
        public float KnockbackForceOther { get => knockbackForceOther; }
        public float KnockbackTimeOther { get => knockbackTimeOther; }
    }
}