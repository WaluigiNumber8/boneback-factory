using System.Collections;
using System.Collections.Generic;
using BoubakProductions.Safety;
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
        [SerializeField] private PositionInfo weaponPositions;
        [SerializeField] private CharWeaponHoldInfo defaultData;

        private IList<WeaponAsset> weapons;
        private float useDelayTimer;

        private void Awake() => weapons = new List<WeaponAsset>(new WeaponAsset[startingSlots]);

        private void Update()
        {
            if (entity.FaceDirection == Vector2.up) UpdateWeaponPosRot(weaponPositions.north, 0);
            else if (entity.FaceDirection == Vector2.down) UpdateWeaponPosRot(weaponPositions.south, 180);
            else if (entity.FaceDirection == Vector2.right) UpdateWeaponPosRot(weaponPositions.east, 270);
            else if (entity.FaceDirection == Vector2.left) UpdateWeaponPosRot(weaponPositions.west, 90);
            
        }

        /// <summary>
        /// Constructs the characteristic.
        /// </summary>
        /// <param name="newInfo">The new data to use.</param>
        /// <param name="presetWeapons">A list of starting weapon assets.</param>
        public void Construct(CharWeaponHoldInfo newInfo, IList<WeaponAsset> presetWeapons = null)
        {
            defaultData = newInfo;
            
            if (presetWeapons == null || presetWeapons.Count <= 0) return;
            weapons = new List<WeaponAsset>(presetWeapons);
        }

        /// <summary>
        /// Construct the characteristic.
        /// </summary>
        /// <param name="newInfo">The new data to use.</param>
        /// <param name="slotAmount">The amount of weapon slots to reserve.</param>
        public void Construct(CharWeaponHoldInfo newInfo, int slotAmount = 0)
        {
            defaultData = newInfo;
            weapons = new List<WeaponAsset>(slotAmount);
        }

        /// <summary>
        /// Use a specific weapon.
        /// </summary>
        /// <param name="slot">The index of the weapon to use.</param>
        public void Use(int slot)
        {
            if (entity.ActionsLocked) return;
            if (useDelayTimer > Time.time) return;
            if (weapons == null || weapons.Count <= 0) return;
            if (weapons[slot] == null) return;

            WeaponAsset weapon = weapons[slot];
            if (weapon.FreezeUser) entity.LockMovement(weapon.UseDuration + weapon.UseStartDelay);
            StartCoroutine(ActivateCoroutine());

            IEnumerator ActivateCoroutine()
            {
                yield return new WaitForSeconds(weapon.UseStartDelay);
                
                weaponEntity.LoadUp(weapon);
                weaponEntity.Activate();
                entity.ForceMove(-entity.FaceDirection, weapon.KnockbackForceSelf, weapon.KnockbackTimeSelf, weapon.KnockbackLockDirectionSelf);
                useDelayTimer = Time.time + defaultData.useDelay;
            }
        }

        /// <summary>
        /// Add a new weapon to the list of usable ones.
        /// </summary>
        /// <param name="newWeapon">The new weapon to add.</param>
        public void Add(WeaponAsset newWeapon)
        {
            weapons.Add(newWeapon);
        }

        /// <summary>
        /// Equips a new weapon.
        /// </summary>
        /// <param name="newWeapon">The new weapon to add.</param>
        /// <param name="slot">The slot to equip to.</param>
        public void Equip(WeaponAsset newWeapon, int slot)
        {
            SafetyNet.EnsureIndexWithingCollectionRange(slot, weapons, "List of usable weapons");
            weapons[slot] = newWeapon;
        }

        /// <summary>
        /// Updates the position and rotation of the weapon entity.
        /// </summary>
        private void UpdateWeaponPosRot(Vector3 pos, float rotZ)
        {
            weaponEntity.transform.localPosition = pos;
            weaponEntity.transform.localRotation = Quaternion.Euler(Vector3.forward * rotZ);
        }
        
        public int WeaponCount { get => weapons.Count; }
        
        [System.Serializable]
        private struct PositionInfo
        {
            public Vector3 north;
            public Vector3 east;
            public Vector3 south;
            public Vector3 west;
        }
    }
}