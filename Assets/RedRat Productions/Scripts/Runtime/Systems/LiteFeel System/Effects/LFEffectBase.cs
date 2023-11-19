using System;
using System.Collections;
using RedRats.Systems.LiteFeel.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RedRats.Systems.LiteFeel.Effects
{
    [RequireComponent(typeof(LFEffector))]
    public abstract class LFEffectBase : MonoBehaviour
    {
        [SerializeField] private SettingsInfo settings;
        
        [ButtonGroup, Button("Play", ButtonSizes.Medium), GUIColor(0.5f, 0.95f, 0.4f)]
        public void TestPlay() => Play(); 
        [ButtonGroup, Button("Stop", ButtonSizes.Medium)]
        public void TestStop() => Stop();
        
        private void OnEnable()
        {
            if (settings.playOnEnable) Play();
        }

        /// <summary>
        /// Play the effect.
        /// </summary>
        public void Play()
        {
            if (!isActiveAndEnabled) return;
            StartCoroutine(PlayCoroutine());
            IEnumerator PlayCoroutine()
            {
                float delay = (settings.randomizeDelay) ? Random.Range(settings.initialDelayMin, settings.initialDelayMax) : settings.initialDelayMin;
                yield return new WaitForSeconds(delay);
                PlaySelf();
            }
        }
        
        /// <summary>
        /// Stop the effect.
        /// </summary>
        public void Stop()
        {
            StopSelf();
        }
        
        /// <summary>
        /// Activates the effect.
        /// </summary>
        protected abstract void PlaySelf();
        /// <summary>
        /// Disables the effect.
        /// </summary>
        protected abstract void StopSelf();

        [Serializable]
        public struct SettingsInfo
        {
            public bool playOnEnable;
            public bool randomizeDelay;
            [HorizontalGroup, LabelText("Initial Delay")] public float initialDelayMin;
            [HorizontalGroup(MaxWidth = 0.3f), ShowIf("randomizeDelay"), HideLabel] public float initialDelayMax;
        }

    }
}