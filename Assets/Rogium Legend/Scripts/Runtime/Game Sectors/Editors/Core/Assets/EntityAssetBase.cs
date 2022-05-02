using BoubakProductions.Safety;
using Rogium.Systems.Validation;
using UnityEngine;

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
        protected bool knockbackLockDirectionSelf;
        protected float knockbackForceOther;
        protected float knockbackTimeOther;
        protected bool knockbackLockDirectionOther;

        #region Update Values
        public void UpdateBaseDamage(int newBaseDamage)
        {
            baseDamage = newBaseDamage;
        }

        public void UpdateUseDelay(float newUseDelay)
        {
            newUseDelay = Mathf.Clamp(newUseDelay, 0f, AssetValidation.MaxUseDelay);
            useDelay = newUseDelay;
        }

        public void UpdateKnockbackForceSelf(float newKnockbackSelf) => knockbackForceSelf = newKnockbackSelf;
        public void UpdateKnockbackTimeSelf(float newKnockbackTime)
        {
            newKnockbackTime = Mathf.Clamp(newKnockbackTime, 0f, AssetValidation.MaxKnockbackSelfTime);
            knockbackTimeSelf = newKnockbackTime;
        }
        public void UpdateKnockbackLockDirectionSelf(bool lockDirection) => knockbackLockDirectionSelf = lockDirection;
        public void UpdateKnockbackForceOther(float newKnockbackOther) => knockbackForceOther = newKnockbackOther;
        public void UpdateKnockbackTimeOther(float newKnockbackTime)
        {
            newKnockbackTime = Mathf.Clamp(newKnockbackTime, 0f, AssetValidation.MaxKnockbackOtherTime);
            knockbackTimeOther = newKnockbackTime;
        }
        public void UpdateKnockbackLockDirectionOther(bool lockDirection) => knockbackLockDirectionOther = lockDirection;
        #endregion
        
        public int BaseDamage { get => baseDamage; }
        public float UseDelay { get => useDelay; }
        public float KnockbackForceSelf { get => knockbackForceSelf; }
        public float KnockbackTimeSelf { get => knockbackTimeSelf; }
        public bool KnockbackLockDirectionSelf { get => knockbackLockDirectionSelf; }
        public float KnockbackForceOther { get => knockbackForceOther; }
        public float KnockbackTimeOther { get => knockbackTimeOther; }
        public bool KnockbackLockDirectionOther { get => knockbackLockDirectionOther; }
    }
}