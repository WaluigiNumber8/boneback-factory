using System;
using BoubakProductions.Core;
using Rogium.Gameplay.Entities.Characteristics;
using Rogium.Gameplay.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Gameplay.HUD
{
    /// <summary>
    /// Updates and controls the in-game HUD.
    /// </summary>
    public class HUDController : MonoSingleton<HUDController>
    {
        [SerializeField] private HealthBarInfo healthBar;
        [SerializeField] private WeaponSlotClustersInfo weaponSlots;

        private void Start() => weaponSlots.SetEmpty();
        
        private void OnEnable()
        {
            SetHealthSliderMaxValue(healthBar.trackedReceiver.MaxHealth);
            SetHealthSliderValue(healthBar.trackedReceiver.MaxHealth);
            healthBar.trackedReceiver.OnDamageReceived += SetHealthSliderValue;
            healthBar.trackedReceiver.OnMaxHealthChange += SetHealthSliderMaxValue;
        }

        private void OnDisable()
        {
            healthBar.trackedReceiver.OnDamageReceived -= SetHealthSliderValue;
            healthBar.trackedReceiver.OnMaxHealthChange -= SetHealthSliderMaxValue;
        }

        /// <summary>
        /// Updates a weapon slot.
        /// </summary>
        /// <param name="index">The index of the weapon slot.</param>
        /// <param name="icon">The new icon to update it with.</param>
        /// <exception cref="InvalidOperationException">Is thrown when the slot is not supported.</exception>
        public void UpdateWeaponSlot(int index, Sprite icon)
        {
            switch (index)
            {
                case 0:
                    weaponSlots.main.SetMain(icon);
                    break;
                case 1:
                    weaponSlots.sub.SetMain(icon);
                    break;
                case 2:
                    weaponSlots.main.SetSub(icon);
                    break;
                case 3:
                    weaponSlots.sub.SetSub(icon);
                    break;
                case 4:
                    weaponSlots.dash.SetMain(icon);
                    break;
                case 5:
                    weaponSlots.dash.SetMain(icon);
                    break;
                default:
                    throw new InvalidOperationException($"Slot number '{index}' is not supported.");
                
            }
        }
        
        private void SetHealthSliderValue(int value) => healthBar.slider.value = value;
        private void SetHealthSliderMaxValue(int value) => healthBar.slider.maxValue = value;

        [System.Serializable]
        public struct HealthBarInfo
        {
            public Slider slider;
            public CharacteristicDamageReceiver trackedReceiver;
        }

        [System.Serializable]
        public struct WeaponSlotClustersInfo
        {
            public WeaponSlotCluster main;
            public WeaponSlotCluster sub;
            public WeaponSlotCluster dash;

            /// <summary>
            /// Empty all slot clusters.
            /// </summary>
            public void SetEmpty()
            {
                main.Empty();
                sub.Empty();
                dash.Empty();
            }
        }
    }
}