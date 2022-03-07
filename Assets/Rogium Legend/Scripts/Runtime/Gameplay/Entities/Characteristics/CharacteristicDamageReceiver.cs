using System;
using UnityEngine;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Gives entities the ability to receive damage.
    /// </summary>
    public class CharacteristicDamageReceiver : MonoBehaviour
    {
        public event Action OnDeath;

        [SerializeField] private EntityController entity;
        [SerializeField] private CharDamageReceiverInfo data;
        
        private int health;
        private float invincibilityTimer;

        private void Awake()
        {
            health = data.maxHealth;
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
            data = newInfo;
            health = data.maxHealth;
        }

        /// <summary>
        /// Receive damage.
        /// </summary>
        /// <param name="giver">The damage giver tha initiated the situation.</param>
        private void TakeDamage(CharacteristicDamageGiver giver)
        {
            health -= giver.GetDamageTaken();

            if (health < 0)
            {
                OnDeath?.Invoke();
                return;
            }
            
            giver.ReceiveKnockback(entity);
            invincibilityTimer = Time.time + data.invincibilityTime;
        }
        
    }
}