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
        [SerializeField] private int maxHealth;
        [SerializeField, Range(0.05f, 0.5f)] private float invincibilityTime;
        
        private int health;
        private float invincibilityTimer;

        private void Awake()
        {
            health = maxHealth;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (invincibilityTimer > Time.time) return;
            if (!other.TryGetComponent(out CharacteristicDamageGiver giver)) return;
            
            TakeDamage(giver);
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
            invincibilityTimer = Time.time + invincibilityTime;
        }
        
    }
}