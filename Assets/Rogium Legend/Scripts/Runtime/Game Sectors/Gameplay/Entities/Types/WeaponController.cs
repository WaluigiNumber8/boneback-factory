using System;
using System.Collections;
using Rogium.Editors.Weapons;
using Rogium.Gameplay.Entities.Characteristics;
using UnityEngine;

namespace Rogium.Gameplay.Entities
{
    /// <summary>
    /// The main controller of the weapon entity.
    /// </summary>
    public class WeaponController : EntityController
    {
        [SerializeField] private CharacteristicDamageGiver damageGiver;
        [SerializeField] private CharacteristicProjectileShoot projectileShoot;
        [SerializeField] private CharacteristicVisual visual;
        
        private WeaponAsset weapon;
        
        protected override void Awake()
        {
            base.Awake();
            ChangeActiveState(false);
        }

        /// <summary>
        /// Load new weapon data into the entity.
        /// </summary>
        public void LoadUp(WeaponAsset asset)
        {
            if (weapon != null && weapon.ID == asset.ID) return;
            
            ForcedMoveInfo knockbackSelf = new(asset.KnockbackForceSelf, asset.KnockbackTimeSelf, asset.KnockbackLockDirectionSelf);
            ForcedMoveInfo knockbackOther = new(asset.KnockbackForceOther, asset.KnockbackTimeOther, asset.KnockbackLockDirectionOther);
            damageGiver.Construct(new CharDamageGiverInfo(asset.BaseDamage, knockbackSelf, knockbackOther));
            visual.Construct(new CharVisualInfo(asset.Icon, asset.AnimationType, asset.FrameDuration, asset.IconAlt));
            visual.ChangeRenderState(asset.UseType != WeaponUseType.Hidden);
            
            weapon = asset;
            
            ChangeActiveState((weapon.UseType == WeaponUseType.Constant));
        }

        /// <summary>
        /// Activates the effects of the current weapon.
        /// </summary>
        public void Activate()
        {
            ChangeActiveState(weapon.UseType != WeaponUseType.Hidden);
            projectileShoot.Fire(weapon.ProjectileIDs);
            
            switch (weapon.UseType)
            {
                case WeaponUseType.PopUp:
                    StartCoroutine(StaticTypeCoroutine(true));
                    break;
                case WeaponUseType.Hidden:
                    StartCoroutine(StaticTypeCoroutine(false));
                    break;
                case WeaponUseType.Constant:
                    return;
                default:
                    throw new ArgumentOutOfRangeException($"The Use Type '{weapon.UseType}' is not supported or implemented.");
            }
            
            IEnumerator StaticTypeCoroutine(bool showWeapon)
            {
                yield return new WaitForSeconds(weapon.UseDuration);
                if (showWeapon) ChangeActiveState(false);
            }
        }

        /// <summary>
        /// Changes the whole active state for the weapon.
        /// </summary>
        /// <param name="isEnabled">Changes the state.</param>
        private void ChangeActiveState(bool isEnabled)
        {
            ChangeCollideMode(isEnabled);
            visual.ChangeRenderState(isEnabled);
        }
    }
}