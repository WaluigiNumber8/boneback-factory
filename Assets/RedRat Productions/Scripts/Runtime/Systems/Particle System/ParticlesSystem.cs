using System.Collections;
using System.Collections.Generic;
using RedRats.Core;
using RedRats.Core.Helpers;
using UnityEngine;
using UnityEngine.Pool;

namespace RedRats.Systems.Particles
{
    /// <summary>
    /// Manages all <see cref="ParticlesSystem"/>s playing in the scene.
    /// </summary>
    public class ParticlesSystem : MonoSingleton<ParticlesSystem>
    {
        private ObjectDictionaryPool<int, ParticleSystem> effectPool;
        
        protected override void Awake()
        {
            base.Awake();
            effectPool = new ObjectDictionaryPool<int, ParticleSystem>(
                () =>
                {
                    ParticleSystem particle = new GameObject("ParticleEffect").AddComponent<ParticleSystem>();
                    particle.gameObject.AddComponent<HardFollowTarget>();
                    return particle;
                },
                c =>
                {
                    c.Stop(true);
                    c.gameObject.SetActive(true);
                },
                c =>
                {
                    c.Stop(true);
                    c.gameObject.SetActive(false);
                    c.gameObject.GetComponent<HardFollowTarget>().ClearTarget();
                },
                Destroy,
                true, 50);
        }
        
        public ParticleSystem Play(ParticleSystem effectData, Transform target, Vector3 offset, int id = 0)
        {
            ParticleSystem effect = (id == 0) ? effectPool.Get() : effectPool.Get(id);
            
            //Copy all data from the effect to the particle
            effectData.CopyInto(effect);
            
            //Move the particle to the desired position
            effect.GetComponent<HardFollowTarget>().SetTarget(target, offset);
            
            effect.Play();
            StartCoroutine(ReleaseEffectCoroutine());
            return effect;
            
            IEnumerator ReleaseEffectCoroutine()
            {
                yield return new WaitForSeconds(effect.main.duration);
                if (id == 0) TryReleaseEffect(effect);
                else TryReleaseEffect(id);
            }
        }

        public void Stop(int id)
        {
            effectPool.Get(id).Stop();
            TryReleaseEffect(id);
        }
        
        public void Stop(ParticleSystem effect)
        {
            effect.Stop();
            TryReleaseEffect(effect);
        }

        private void TryReleaseEffect(ParticleSystem effect)
        {
            if (effect.isPlaying || !effect.gameObject.activeSelf) return;
            effectPool.Release(effect);
        }

        private void TryReleaseEffect(int id)
        {
            ParticleSystem effect = effectPool.Get(id);
            if (effect.isPlaying || !effect.gameObject.activeSelf) return;
            effectPool.Release(id);
        }
        
        
    }
}