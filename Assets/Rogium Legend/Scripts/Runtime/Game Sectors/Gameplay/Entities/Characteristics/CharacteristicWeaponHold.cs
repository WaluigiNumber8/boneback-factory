using System.Collections;
using System.Collections.Generic;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Editors.Weapons;
using UnityEngine;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Allows entities to carry and use multiple weapons.
    /// </summary>
    public class CharacteristicWeaponHold : CharacteristicBase
    {
        [SerializeField] private WeaponController weaponEntity;
        [SerializeField] private int startingSlots = 0;
        [SerializeField] private CharWeaponHoldInfo defaultData;

        private IList<WeaponAsset> currentWeapons;
        private float useDelayTimer;

        private void Awake() => currentWeapons = new List<WeaponAsset>(new WeaponAsset[startingSlots]);

        private void Update() => UpdateWeaponPosRot();

        /// <summary>
        /// Constructs the characteristic.
        /// </summary>
        /// <param name="newInfo">The new data to use.</param>
        /// <param name="presetWeapons">A list of starting weapon assets.</param>
        public void Construct(CharWeaponHoldInfo newInfo, IList<WeaponAsset> presetWeapons = null)
        {
            defaultData = newInfo;
            
            if (presetWeapons == null || presetWeapons.Count <= 0) return;
            currentWeapons = new List<WeaponAsset>(presetWeapons);
        }

        /// <summary>
        /// Construct the characteristic.
        /// </summary>
        /// <param name="newInfo">The new data to use.</param>
        /// <param name="slotAmount">The amount of weapon slots to reserve.</param>
        public void Construct(CharWeaponHoldInfo newInfo, int slotAmount = 0)
        {
            defaultData = newInfo;
            currentWeapons = new List<WeaponAsset>(slotAmount);
        }

        /// <summary>
        /// Use a specific weapon.
        /// </summary>
        /// <param name="slot">The index of the weapon to use.</param>
        public void Use(int slot)
        {
            if (entity.ActionsLocked) return;
            if (useDelayTimer > Time.time) return;
            if (currentWeapons == null || currentWeapons.Count <= 0) return;
            if (currentWeapons[slot] == null) return;

            WeaponAsset weapon = currentWeapons[slot];
            if (weapon.FreezeUser)
            {
                entity.LockMovement(weapon.UseDuration + weapon.UseStartDelay);
                entity.StopMoving();
            }
            useDelayTimer = Time.time + weapon.UseDuration + weapon.UseStartDelay;
            
            StartCoroutine(ActivateCoroutine());
            IEnumerator ActivateCoroutine()
            {
                yield return new WaitForSeconds(weapon.UseStartDelay);
                weaponEntity.LoadUp(weapon);
                weaponEntity.Activate();
                entity.ForceMove(-entity.FaceDirection, weapon.KnockbackForceSelf, weapon.FreezeUser, weapon.KnockbackLockDirectionSelf);
            }
        }

        /// <summary>
        /// Add a new weapon to the list of usable ones.
        /// </summary>
        /// <param name="newWeapon">The new weapon to add.</param>
        public void Add(WeaponAsset newWeapon)
        {
            currentWeapons.Add(newWeapon);
        }

        /// <summary>
        /// Equips a new weapon.
        /// </summary>
        /// <param name="newWeapon">The new weapon to add.</param>
        /// <param name="slot">The slot to equip to.</param>
        public void Equip(WeaponAsset newWeapon, int slot)
        {
            SafetyNet.EnsureIndexWithingCollectionRange(slot, currentWeapons, "List of usable weapons");
            currentWeapons[slot] = newWeapon;
        }

        /// <summary>
        /// Wipes inventory and stops using the current weapon.
        /// </summary>
        public void WipeInventory()
        {
            weaponEntity.ChangeActiveState(false);
            currentWeapons.Clear();
        }

        /// <summary>
        /// Updates the position and rotation of the weapon entity.
        /// </summary>
        private void UpdateWeaponPosRot()
        {
            weaponEntity.Transform.localPosition = entity.FaceDirection.normalized;
            TransformUtils.SetRotation2D(weaponEntity.Transform, entity.FaceDirection);
        }
        
        public int WeaponCount { get => currentWeapons.Count; }
    }
}