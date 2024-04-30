using System;
using System.Collections;
using Rogium.Editors.Weapons;
using Rogium.Gameplay.Entities.Characteristics;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Gameplay.Entities
{
    /// <summary>
    /// The main controller of the weapon entity.
    /// </summary>
    public class WeaponController : EntityController
    {
        public event Action OnUse;
        public event Action OnUseStop;
        
        [Title("Characteristics")]
        [SerializeField] private CharacteristicDamageGiver damageGiver;
        [SerializeField] private CharacteristicProjectileShoot projectileShoot;
        [SerializeField] private CharacteristicVisual visual;
        [SerializeField] private CharacteristicSoundEmitter sound;

        private WeaponAsset weapon;
        
        protected override void Awake()
        {
            base.Awake();
            ChangeActiveState(false);
        }

        private void OnEnable()
        {
            OnUse += sound.PlayUseSound;
        }

        private void OnDisable()
        {
            OnUse -= sound.PlayUseSound;
        }

        
        /// <summary>
        /// Load new weapon data into the entity.
        /// </summary>
        public void Construct(WeaponAsset asset)
        {
            if (weapon != null && weapon.ID == asset.ID) return;
            
            ForcedMoveInfo knockbackSelf = new(asset.KnockbackForceSelf, true, asset.KnockbackLockDirectionSelf);
            ForcedMoveInfo knockbackOther = new(asset.KnockbackForceOther, true, asset.KnockbackLockDirectionOther);
            damageGiver.Construct(new CharDamageGiverInfo(asset.BaseDamage, knockbackSelf, knockbackOther));
            visual.Construct(new CharVisualInfo(asset.Icon, asset.AnimationType, asset.FrameDuration, asset.IconAlt));
            visual.ChangeRenderState(asset.UseType != WeaponUseType.Hidden);
            sound.Construct(new CharSoundInfo(null, null, null, asset.UseSound));
            
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
                OnUse?.Invoke();
                yield return new WaitForSeconds(weapon.UseDuration);
                
                if (showWeapon) ChangeActiveState(false);
                OnUseStop?.Invoke();
            }
        }

        /// <summary>
        /// Changes the whole active state for the weapon.
        /// </summary>
        /// <param name="isEnabled">Changes the state.</param>
        public void ChangeActiveState(bool isEnabled)
        {
            ChangeCollideMode(isEnabled);
            visual.ChangeRenderState(isEnabled);
        }
    }
}