using RedRats.Systems.LiteFeel.Effects;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Core
{
    /// <summary>
    /// Detects all effectors on the same <see cref="GameObject"/> and plays them when needed.
    /// </summary>
    [DisallowMultipleComponent]
    public class LFEffector : MonoBehaviour
    {
        [SerializeField] private bool playOnAwake;
        [SerializeField] private bool playOnEnable;
        
        private LFEffectBase[] effects;
        
        private void Awake()
        {
            effects = GetComponents<LFEffectBase>();
            if (!playOnAwake) return;
            if (effects.Length <= 0) return;
            Play();
        }

        private void OnEnable()
        {
            if (!playOnEnable) return;
            if (effects.Length <= 0) return;
            Play();
        }
        
        /// <summary>
        /// Plays all detected effectors on this <see cref="GameObject"/>.
        /// </summary>
        public void Play()
        {
            if (effects.Length <= 0) return;
            foreach (LFEffectBase effect in effects)
            {
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