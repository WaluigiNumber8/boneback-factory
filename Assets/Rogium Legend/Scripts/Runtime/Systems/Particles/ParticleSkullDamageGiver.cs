using System.Collections.Generic;
using Rogium.Gameplay.Entities.Characteristics;
using UnityEngine;

namespace Rogium.Systems.Particles
{
    /// <summary>
    /// Damages entities when they collide with the particle.
    /// </summary>
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleSkullDamageGiver : MonoBehaviour
    {
        [SerializeField] private int damage;
        
        private ParticleSystem effect;

        private void Awake() => effect = GetComponent<ParticleSystem>();

        private void OnEnable()
        {
            //Find all damage receivers.
            CharacteristicDamageReceiver[] allReceivers = FindObjectsByType<CharacteristicDamageReceiver>(FindObjectsSortMode.None);
            foreach (CharacteristicDamageReceiver receiver in allReceivers)
            {
                effect.trigger.AddCollider(receiver);   
            }
        }

        private void OnDisable()
        {
           //Clear all damage receivers.
           for (int i = effect.trigger.colliderCount - 1; i >= 0; i--)
           {
               effect.trigger.RemoveCollider(i);
           }
        }

        private void OnParticleTrigger()
        {
            List<ParticleSystem.Particle> particles = new();
            int amount = effect.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, particles);
            for (int i = 0; i < amount; i++)
            {
                 CharacteristicDamageReceiver receiver = effect.trigger.GetCollider(i).GetComponent<CharacteristicDamageReceiver>();
                 receiver?.TakeDamage(damage, transform);
            }
        }
    }
}