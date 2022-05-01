using BoubakProductions.Systems.ClockOfTheGame;
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
        private WeaponAsset currentWeapon;

        private void Awake() => weaponSelectMenu = WeaponSelectMenu.GetInstance();
        private void OnEnable() => InteractObjectWeaponDrop.OnPlayerPickUp += ProcessNewWeapon;
        private void OnDisable() => InteractObjectWeaponDrop.OnPlayerPickUp -= ProcessNewWeapon;

        private void ProcessNewWeapon(WeaponAsset newWeapon)
        {
            currentWeapon = newWeapon;
            GameClock.Instance.Pause();
            
            if (newWeapon.IsEvasive) weaponSelectMenu.OpenForDash(CallForEquip);
            else weaponSelectMenu.OpenForNormal(CallForEquip);
        }

        /// <summary>
        /// Equip the weapon to the weapon holder.
        /// </summary>
        /// <param name="slot">The slot to equip to.</param>
        private void CallForEquip(int slot)
        {
            weaponHolder.Equip(currentWeapon, slot);
            HUDController.GetInstance().UpdateWeaponSlot(slot, currentWeapon.Icon);
            currentWeapon = null;
            GameClock.Instance.Resume();
        }
    }
}