using System;
using RedRats.Core;
using UnityEngine;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Gives entities the ability to receive damage.
    /// </summary>
    public class CharacteristicDamageReceiver : CharacteristicBase
    {
        public event Action<int> OnMaxHealthChange; 
        public event Action<int> OnDamageReceived;
        public event Action<int, Vector3> OnHit; 
        public event Action OnDeath;

        [SerializeField] private LayerMask ignoredMask;
        [SerializeField] private CharDamageReceiverInfo defaultData;
        
        private int health;
        private float invincibilityTimer;

        private void Awake() => health = defaultData.maxHealth;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.TryGetComponent(out CharacteristicDamageGiver giver)) return;
            
            TakeDamage(giver.GetDamageTaken(), giver.transform, giver);
        }

        /// <summary>
        /// Constructs the characteristic.
        /// </summary>
        /// <param name="newInfo">New data to use.</param>
        public void Construct(CharDamageReceiverInfo newInfo)
        {
            defaultData = newInfo;
            health = defaultData.maxHealth;
        }

        /// <summary>
        /// Increases/Decreases the Max Health Property.
        /// </summary>
        /// <param name="amount">The amount to change the health by.</param>
        public void IncreaseMaxHealth(int amount)
        {
            defaultData.maxHealth += amount;
            OnMaxHealthChange?.Invoke(defaultData.maxHealth);
        }

        /// <summary>
        /// Become invincible for a period of time. (uses default invincibility time)
        /// </summary>
        public void BecomeInvincible() => BecomeInvincible(defaultData.invincibilityTime);
        /// <summary>
        /// Become invincible for a period of time.
        /// </summary>
        /// <param name="time">How long to stay invincible.</param>
        public void BecomeInvincible(float time) => invincibilityTimer = Time.time + time;

        /// <summary>
        /// Receive damage.
        /// </summary>
        /// <param name="amount">The amount of damage to take.</param>
        /// <param name="damagerTransform">The transform of the object that initiated the hit.</param>
        /// <param name="damager">The damage giver that initiated the situation.</param>
        public void TakeDamage(int amount, Transform damagerTransform, CharacteristicDamageGiver damager = null)
        {
            if (invincibilityTimer > Time.time) return;
            if (GameObjectUtils.IsInLayerMask(damagerTransform.gameObject, ignoredMask)) return;

            health -= amount;
            OnDamageReceived?.Invoke(health);

            //Death
            if (health <= 0)
            {
                OnDeath?.Invoke();
                invincibilityTimer = Time.time + defaultData.invincibilityTime * 10;
                return;
            }
            
            //Hit
            Vector3 hitDirection = (damagerTransform.transform.position - entity.TTransform.position).normalized;
            OnHit?.Invoke(amount, hitDirection);
            invincibilityTimer = Time.time + defaultData.invincibilityTime;
            damager?.ReceiveKnockback(entity);
        }

        public int CurrentHealth { get => health; }
        public int MaxHealth { get => defaultData.maxHealth; }
    }
}