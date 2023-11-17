using RedRats.Safety;
using RedRats.Systems.Effectors.Effects;
using UnityEngine;

namespace RedRats.Systems.Effectors.Core
{
    /// <summary>
    /// Detects all effectors on the same <see cref="GameObject"/> and plays them when needed.
    /// </summary>
    public class Effector : MonoBehaviour
    {
        private EffectBase[] effects;
        
        private void Awake() => effects = GetComponents<EffectBase>();

        /// <summary>
        /// Plays all detected effectors on this <see cref="GameObject"/>.
        /// </summary>
        public void Play()
        {
            SafetyNet.EnsureIsNotNull(effects, nameof(effects));
            if (effects.Length <= 0) return;
            foreach (EffectBase effect in effects)
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
            foreach (EffectBase effect in effects)
            {
                effect.Stop();
            }
        }
    }
}