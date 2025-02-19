using System.Collections.Generic;
using RedRats.Systems.Audio;
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
        [SerializeField] private AudioClipSO hitWallSound;
        
        private ParticleSystem effect;
        private AudioSystem audioSystem;

        private void Awake()
        {
            effect = GetComponent<ParticleSystem>();
            audioSystem = AudioSystem.Instance;
        }

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
            int amount = effect.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, particles, out ParticleSystem.ColliderData data);
            for (int i = 0; i < amount; i++)
            {
                if (particles[i].velocity.sqrMagnitude < 10f) continue;
                for (int j = 0; j < data.GetColliderCount(i); j++)
                {
                    // Get the GameObject that the particle collided with
                    CharacteristicDamageReceiver receiver = data.GetCollider(i, j).GetComponent<CharacteristicDamageReceiver>();
                    receiver?.TakeDamage(damage, transform);
                }
            }
            particles.Clear();
        }

        private void OnParticleCollision(GameObject other)
        {
            audioSystem.PlaySound(hitWallSound, new AudioSourceSettingsInfo(0, false, false, false));
        }
    }
}