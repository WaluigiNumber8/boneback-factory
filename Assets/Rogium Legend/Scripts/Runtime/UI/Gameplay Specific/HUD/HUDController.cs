using System;
using Rogium.Gameplay.Entities.Characteristics;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Gameplay.HUD
{
    /// <summary>
    /// Updates and controls the in-game HUD.
    /// </summary>
    public class HUDController : MonoBehaviour
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