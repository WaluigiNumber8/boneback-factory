using System;
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
        public event Action OnDeath;

        [SerializeField] private CharDamageReceiverInfo defaultData;
        
        private int health;
        private float invincibilityTimer;

        private void Awake()
        {
            health = defaultData.maxHealth;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (invincibilityTimer > Time.time) return;
            if (!other.TryGetComponent(out CharacteristicDamageGiver giver)) return;
            
            TakeDamage(giver);
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
        /// Receive damage.
        /// </summary>
        /// <param name="giver">The damage giver tha initiated the situation.</param>
        private void TakeDamage(CharacteristicDamageGiver giver)
        {
            health -= giver.GetDamageTaken();
            OnDamageReceived?.Invoke(health);
            
            if (health <= 0)
            {
                OnDeath?.Invoke();
                return;
            }
            
            giver.ReceiveKnockback(entity);
            invincibilityTimer = Time.time + defaultData.invincibilityTime;
        }
        
        public int CurrentHealth { get => health; }
        public int MaxHealth { get => defaultData.maxHealth; }
    }
}