using Rogium.Editors.Enemies;
using Rogium.Gameplay.Entities.Characteristics;
using UnityEngine;

namespace Rogium.Gameplay.Entities.Enemy
{
    /// <summary>
    /// Overseers and controls the enemy object.
    /// </summary>
    public class EnemyController : EntityController
    {
        [SerializeField] private CharacteristicDamageGiver damageGiver;
        [SerializeField] private CharacteristicDamageReceiver damageReceiver;
        
        private void OnEnable()
        {
            damageReceiver.OnDeath += Die;
        }

        private void OnDisable()
        {
            damageReceiver.OnDeath -= Die;
        }

        
        /// <summary>
        /// Constructs the enemy.
        /// </summary>
        /// <param name="asset">The asset to take data from.</param>
        public void Construct(EnemyAsset asset)
        {
            ForcedMoveInfo knockbackSelf = new(asset.KnockbackSelf, asset.KnockbackSelf * 0.01f);
            ForcedMoveInfo knockbackOther = new(asset.KnockbackOther, asset.KnockbackOther * 0.01f);
            CharDamageGiverInfo damageGiver = new(asset.BaseDamage, knockbackSelf, knockbackOther);
            CharDamageReceiverInfo damageReceiver = new(asset.MaxHealth, asset.InvincibilityTime);

            this.damageReceiver.Construct(damageReceiver);
            this.damageGiver.Construct(damageGiver);
        }

        private void Die()
        {
            Destroy(gameObject);
        }
        
    }
}