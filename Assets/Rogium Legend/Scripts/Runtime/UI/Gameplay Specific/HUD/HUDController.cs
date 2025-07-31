using System;
using RedRats.Core;
using Rogium.Gameplay.Entities.Characteristics;
using Sirenix.OdinInspector;
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

        private void Start()
        {
            weaponSlots.SetEmpty();
            healthBar.UpdateState();
        }

        private void OnEnable()
        {
            SetHealthBarMaxValue(healthBar.trackedReceiver.MaxHealth);
            SetHealthBarValue(healthBar.trackedReceiver.MaxHealth);
            healthBar.trackedReceiver.OnDamageReceived += SetHealthBarValue;
            healthBar.trackedReceiver.OnMaxHealthChange += SetHealthBarMaxValue;
        }

        private void OnDisable()
        {
            healthBar.trackedReceiver.OnDamageReceived -= SetHealthBarValue;
            healthBar.trackedReceiver.OnMaxHealthChange -= SetHealthBarMaxValue;
        }

        private void Update() => healthBar.Animate();

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
                    weaponSlots.main.SetSub(icon);
                    break;
                case 2:
                    weaponSlots.sub.SetMain(icon);
                    break;
                case 3:
                    weaponSlots.sub.SetSub(icon);
                    break;
                case 4:
                    weaponSlots.dash.SetMain(icon);
                    break;
                case 5:
                    weaponSlots.dash.SetSub(icon);
                    break;
                default:
                    throw new InvalidOperationException($"Slot number '{index}' is not supported.");
            }
        }

        /// <summary>
        /// Set the value of the health slider.
        /// </summary>
        /// <param name="value">The new value of the slider.</param>
        public void SetHealthBarValue(int value)
        {
            healthBar.targetValue = value;
            healthBar.UpdateState();
        }

        /// <summary>
        /// Set the max value of the health slider.
        /// </summary>
        /// <param name="value">The new max value of the slider.</param>
        public void SetHealthBarMaxValue(int value)
        {
            healthBar.slider.maxValue = value;
            if (healthBar.slider.value > healthBar.slider.maxValue) SetHealthBarValue(value);
        }


        [Serializable]
        public struct HealthBarInfo
        {
            public Slider slider;
            public Image barOutline;
            public CharacteristicDamageReceiver trackedReceiver;
            public float lerpSpeed;
            [Range(0f, 1f)] public float damagedThreshold;
            [PreviewField(60)] public Sprite normalState;
            [PreviewField(60)] public Sprite damagedState;
            [PreviewField(60)] public Sprite brokenState;

            [HideInInspector] public float targetValue;

            /// <summary>
            /// Animate the health bar.
            /// </summary>
            public void Animate()
            {
                if (Math.Abs(slider.value - targetValue) < 0.001f) return;
                slider.value = Mathf.Lerp(slider.value, targetValue, lerpSpeed * Time.deltaTime);
            }

            public void UpdateState()
            {
                barOutline.sprite = (targetValue <= slider.minValue) ? brokenState : (targetValue <= slider.maxValue * damagedThreshold) ? damagedState : normalState;
            }
        }

        [Serializable]
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