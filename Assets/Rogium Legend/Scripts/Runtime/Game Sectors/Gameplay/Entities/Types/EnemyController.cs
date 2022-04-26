using System.Collections.Generic;
using Rogium.Editors.Enemies;
using Rogium.Editors.Weapons;
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
        [SerializeField] private CharacteristicWeaponHold weaponHold;
        [SerializeField] private CharacteristicVisual visual;

        private float weaponUseTime;
        private float weaponUseTimer;
        private float attackProbability;
        
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

        private void Update()
        {
            UseWeapon();
        }

        /// <summary>
        /// Constructs the enemy.
        /// </summary>
        /// <param name="asset">The asset to take data from.</param>
        public void Construct(EnemyAsset asset, IList<WeaponAsset> weapons = null)
        {
            faceDirection = Vector2.down;
            
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
                CharVisualInfo visual = new(asset.Icon, asset.AnimationType, asset.FrameDuration, asset.IconAlt);
                this.visual.Construct(visual);
            }

            //Weapon Hold
            if (weaponHold != null)
            {
                CharWeaponHoldInfo weaponHold = new(asset.UseDelay);
                this.weaponHold.Construct(weaponHold, weapons);
                
                weaponUseTime = asset.UseDelay;
                attackProbability = asset.AttackProbability;
            }
        }

        /// <summary>
        /// Uses a random weapon.
        /// </summary>
        private void UseWeapon()
        {
            if (weaponUseTimer > Time.time) return;
            if (weaponHold == null) return;
            if (weaponHold.WeaponCount <= 0) return;

            weaponUseTimer = Time.time + weaponUseTime + Random.Range(0f, 0.05f);

            if (Random.Range(0f, 1f) > attackProbability) return;
            
            int slot = (weaponHold.WeaponCount > 1) ? Random.Range(0, weaponHold.WeaponCount) : 0;
            weaponHold.Use(slot);
        }
        
        private void Die()
        {
            Destroy(gameObject);
        }
    }
}