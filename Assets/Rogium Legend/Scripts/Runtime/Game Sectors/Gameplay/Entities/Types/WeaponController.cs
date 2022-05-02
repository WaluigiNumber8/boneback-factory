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
        [SerializeField] private CharacteristicVisual visual;
        
        private WeaponAsset weapon;
        private string lastWeaponID;
        
        private Vector2 startPos;

        protected override void Awake()
        {
            base.Awake();
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Load new weapon data into the entity.
        /// </summary>
        public void LoadUp(WeaponAsset asset)
        {
            if (lastWeaponID == asset.ID) return;
            
            ForcedMoveInfo knockbackSelf = new(asset.KnockbackForceSelf, asset.KnockbackTimeSelf, asset.KnockbackLockDirectionSelf);
            ForcedMoveInfo knockbackOther = new(asset.KnockbackForceOther, asset.KnockbackTimeOther, asset.KnockbackLockDirectionOther);
            damageGiver.Construct(new CharDamageGiverInfo(asset.BaseDamage, knockbackSelf, knockbackOther));
            visual.Construct(new CharVisualInfo(asset.Icon, asset.AnimationType, asset.FrameDuration, asset.IconAlt));
            visual.SwitchRenderState(asset.UseType != WeaponUseType.Hidden);
            
            weapon = asset;
            lastWeaponID = weapon.ID;
            
            gameObject.SetActive((weapon.UseType == WeaponUseType.Constant));
        }

        /// <summary>
        /// Activates the effects of the current weapon.
        /// </summary>
        public void Activate()
        {
            gameObject.SetActive(true);
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
            
        }

        private IEnumerator StaticTypeCoroutine(bool showWeapon)
        {
            yield return new WaitForSeconds(weapon.UseDuration);
            if (showWeapon) gameObject.SetActive(false);
        }
        
    }
}