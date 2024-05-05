using System;
using System.Collections.Generic;
using Rogium.Editors.Weapons;
using Rogium.Gameplay.Entities.Characteristics;
using Rogium.Gameplay.InteractableObjects;
using Rogium.UserInterface.Gameplay.HUD;
using UnityEngine;

namespace Rogium.Gameplay.Inventory
{
    /// <summary>
    /// Overseers the player's inventory.
    /// </summary>
    public class InventoryOverseerMono : MonoBehaviour
    {
        [SerializeField] private CharacteristicWeaponHold weaponHolder;
        
        private WeaponSelectMenu weaponSelectMenu;
        
        private IList<WeaponAsset> currentWeapons;
        private WeaponAsset processedWeapon;
        private HUDController hudController;

        private void Awake() => hudController = HUDController.GetInstance();

        private void Start()
        {
            currentWeapons = new List<WeaponAsset>(new WeaponAsset[weaponHolder.WeaponCount]);
            weaponSelectMenu = WeaponSelectMenu.GetInstance();
        }

        private void OnEnable() => InteractObjectWeaponDrop.OnPlayerPickUp += ProcessNewWeapon;
        private void OnDisable() => InteractObjectWeaponDrop.OnPlayerPickUp -= ProcessNewWeapon;

        private void ProcessNewWeapon(WeaponAsset newWeapon)
        {
            processedWeapon = newWeapon;

            weaponSelectMenu.RefreshSlotIcons(currentWeapons[0]?.Icon, currentWeapons[1]?.Icon, currentWeapons[2]?.Icon, 
                                              currentWeapons[3]?.Icon, currentWeapons[4]?.Icon, currentWeapons[5]?.Icon);
            if (newWeapon.IsEvasive) weaponSelectMenu.OpenForDash(CallForEquip);
            else weaponSelectMenu.OpenForNormal(CallForEquip);
        }

        /// <summary>
        /// Equip the weapon to the weapon holder.
        /// </summary>
        /// <param name="slot">The slot to equip to.</param>
        private void CallForEquip(int slot)
        {
            weaponHolder.Equip(processedWeapon, slot);
            hudController.UpdateWeaponSlot(slot, processedWeapon.Icon);
            currentWeapons[slot] = processedWeapon;
            processedWeapon = null;
        }
    }
}