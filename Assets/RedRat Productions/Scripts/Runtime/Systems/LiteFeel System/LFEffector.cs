using System.Collections;
using RedRats.Core;
using RedRats.Systems.LiteFeel.Effects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Core
{
    /// <summary>
    /// Detects all effectors on the same <see cref="GameObject"/> and plays them when needed.
    /// </summary>
    [DisallowMultipleComponent]
    public class LFEffector : MonoBehaviour
    {
        [SerializeField] private AutoplayType autoplay;
        [SerializeField, EnumToggleButtons] private TimescaleType timescale = TimescaleType.Normal;
        
        
        [ButtonGroup, Button("Play", ButtonSizes.Medium), GUIColor(0.5f, 0.95f, 0.4f), DisableInEditorMode]
        public void TestPlay()
        {
            effects = GetComponents<LFEffectBase>();
            Play();
        }

        [ButtonGroup, Button("Stop", ButtonSizes.Medium), DisableInEditorMode]
        public void TestStop() => Stop();
        
        private LFEffectBase[] effects;
        
        private void Awake()
        {
            effects = GetComponents<LFEffectBase>();
            foreach (LFEffectBase effect in effects)
            {
                if (autoplay == AutoplayType.None) effect.MarkAsNotPlayImmediately();
            }
        }

        private void Start()
        {
            if (autoplay != AutoplayType.OnStart) return;
            if (effects.Length <= 0) return;
            StartCoroutine(DelayCoroutine());
            IEnumerator DelayCoroutine()
            {
                yield return new WaitForUpdate();
                Play();
            }
        }

        private void OnEnable()
        {
            if (autoplay != AutoplayType.OnEnable) return;
            if (effects.Length <= 0) return;
            StartCoroutine(DelayCoroutine());
            IEnumerator DelayCoroutine()
            {
                yield return new WaitForUpdate();
                Play();
            }

        }
        
        /// <summary>
        /// Plays all detected effectors on this <see cref="GameObject"/>.
        /// </summary>
        public void Play()
        {
            if (effects.Length <= 0) return;
            foreach (LFEffectBase effect in effects)
            {
                if (timescale != TimescaleType.PerEffect) effect.UpdateTimescale(timescale == TimescaleType.Unscaled);
                effect.Play();
            }
        }

        /// <summary>
        /// Stops all detected effectors on this <see cref="GameObject"/>.
        /// </summary>
        public void Stop()
        {
            if (effects.Length <= 0) return;
            foreach (LFEffectBase effect in effects)
            {
                effect.Stop();
            }
        }
    }
}