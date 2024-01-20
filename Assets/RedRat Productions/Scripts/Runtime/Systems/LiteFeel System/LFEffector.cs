using RedRats.Safety;
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
        private LFEffectBase[] effects;
        
        private void Awake() => effects = GetComponents<LFEffectBase>();

        /// <summary>
        /// Plays all detected effectors on this <see cref="GameObject"/>.
        /// </summary>
        public void Play()
        {
            SafetyNet.EnsureIsNotNull(effects, nameof(effects));
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
            SafetyNet.EnsureIsNotNull(effects, nameof(effects));
            if (effects.Length <= 0) return;
            foreach (LFEffectBase effect in effects)
            {
                effect.Stop();
            }
        }
    }
}