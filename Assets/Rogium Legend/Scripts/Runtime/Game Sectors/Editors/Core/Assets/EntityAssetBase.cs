using Rogium.Systems.Validation;
using UnityEngine;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// A base for all entity type assets.
    /// </summary>
    public abstract class EntityAssetBase : AnimatedAssetBase
    {
        protected Color color;
        
        protected int baseDamage;
        protected float useDelay;
        protected float knockbackForceSelf;
        protected bool knockbackLockDirectionSelf;
        protected float knockbackForceOther;
        protected bool knockbackLockDirectionOther;

        #region Update Values
        public void UpdateColor(Color newColor) => color = newColor;
        public void UpdateBaseDamage(int newBaseDamage) => baseDamage = newBaseDamage;
        public void UpdateUseDelay(float newUseDelay)
        {
            newUseDelay = Mathf.Clamp(newUseDelay, 0f, AssetValidation.MaxUseDelay);
            useDelay = newUseDelay;
        }

        public void UpdateKnockbackForceSelf(float newKnockbackSelf) => knockbackForceSelf = newKnockbackSelf;
        public void UpdateKnockbackLockDirectionSelf(bool lockDirection) => knockbackLockDirectionSelf = lockDirection;
        public void UpdateKnockbackForceOther(float newKnockbackOther) => knockbackForceOther = newKnockbackOther;
        public void UpdateKnockbackLockDirectionOther(bool lockDirection) => knockbackLockDirectionOther = lockDirection;
        #endregion
        
        public Color Color { get => color; }
        public int BaseDamage { get => baseDamage; }
        public float UseDelay { get => useDelay; }
        public float KnockbackForceSelf { get => knockbackForceSelf; }
        public bool KnockbackLockDirectionSelf { get => knockbackLockDirectionSelf; }
        public float KnockbackForceOther { get => knockbackForceOther; }
        public bool KnockbackLockDirectionOther { get => knockbackLockDirectionOther; }
        
        public abstract class EntityAssetBuilder<T, TBuilder> : AnimatedAssetBuilder<T, TBuilder> where T : EntityAssetBase where TBuilder : BaseBuilder<T, TBuilder>
        {
            public TBuilder WithColor(Color color)
            {
                Asset.color = color;
                return This;
            }
            
            public TBuilder WithBaseDamage(int baseDamage)
            {
                Asset.baseDamage = baseDamage;
                return This;
            }
            
            public TBuilder WithUseDelay(float useDelay)
            {
                Asset.useDelay = useDelay;
                return This;
            }
            
            public TBuilder WithKnockbackForceSelf(float knockbackForceSelf)
            {
                Asset.knockbackForceSelf = knockbackForceSelf;
                return This;
            }
            
            public TBuilder WithKnockbackLockDirectionSelf(bool knockbackLockDirectionSelf)
            {
                Asset.knockbackLockDirectionSelf = knockbackLockDirectionSelf;
                return This;
            }
            
            public TBuilder WithKnockbackForceOther(float knockbackForceOther)
            {
                Asset.knockbackForceOther = knockbackForceOther;
                return This;
            }
            
            public TBuilder WithKnockbackLockDirectionOther(bool knockbackLockDirectionOther)
            {
                Asset.knockbackLockDirectionOther = knockbackLockDirectionOther;
                return This;
            }
        }
    }
}