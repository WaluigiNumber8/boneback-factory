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
        [SerializeField] private CharacteristicVisual visual;
        
        private void OnEnable()
        {
            if (damageReceiver == null) return;
            damageReceiver.OnDeath += Die;
        }

        private void OnDisable()
        {
            if (damageReceiver == null) return;
            damageReceiver.OnDeath -= Die;
        }

        
        /// <summary>
        /// Constructs the enemy.
        /// </summary>
        /// <param name="asset">The asset to take data from.</param>
        public void Construct(EnemyAsset asset)
        {
            //Damage Giver
            if (damageGiver != null)
            {
                ForcedMoveInfo knockbackSelf = new(asset.KnockbackForceSelf, asset.KnockbackForceSelf * 0.1f);
                ForcedMoveInfo knockbackOther = new(asset.KnockbackForceOther, asset.KnockbackForceOther * 0.1f);
                CharDamageGiverInfo damageGiver = new(asset.BaseDamage, knockbackSelf, knockbackOther);
                this.damageGiver.Construct(damageGiver);
            }

            //Damage Receiver
            if (damageReceiver != null)
            {
                CharDamageReceiverInfo damageReceiver = new(asset.MaxHealth, asset.InvincibilityTime);
                this.damageReceiver.Construct(damageReceiver);
            }

            //Visual
            if (visual != null)
            {
                CharVisualInfo visual = new(asset.Icon);
                this.visual.Construct(visual);
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}