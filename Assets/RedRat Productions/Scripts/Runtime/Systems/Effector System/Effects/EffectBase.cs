using System;
using System.Collections;
using RedRats.Systems.Effectors.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RedRats.Systems.Effectors.Effects
{
    [RequireComponent(typeof(Effector))]
    public abstract class EffectBase : MonoBehaviour
    {
        [SerializeField] private SettingsInfo settings;
        
        private void OnEnable()
        {
            if (settings.playOnEnable) Play();
        }

        /// <summary>
        /// Play the effect.
        /// </summary>
        public void Play()
        {
            StartCoroutine(PlayCoroutine());
            IEnumerator PlayCoroutine()
            {
                yield return new WaitForSeconds(Random.Range(settings.initialDelayMin, settings.initialDelayMax));
                PlayEffects();
            }
        }
        
        /// <summary>
        /// Activates the effect.
        /// </summary>
        protected abstract void PlayEffects();

        [Serializable]
        public struct SettingsInfo
        {
            public bool playOnEnable;
            public float initialDelayMin;
            public float initialDelayMax;
        }
    }
}