using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// Controls a particle system.
    /// </summary>
    public class LFParticleControlEffect : LFEffectBase
    {
        [SerializeField, Required] private ParticleSystem effect;
        [SerializeField] private ParticleControlType mode = ParticleControlType.Play;

        protected override void Initialize()
        {
            // Nothing to do here.
        }

        protected override void PlaySelf()
        {
            switch (mode)
            {
                case ParticleControlType.Play:
                    effect.Play();
                    break;
                case ParticleControlType.Stop:
                    effect.Stop();
                    break;
                case ParticleControlType.StopAndClear:
                    effect.Clear(true);
                    effect.Stop();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode));
            }
            
        }

        protected override void StopSelf()
        {
            // Nothing to do here.
        }

        protected override void ResetState()
        {
            // Nothing to do here.
        }

        protected override float TotalDuration { get => 0.001f; }
        protected override string FeedbackColor { get => "#FF8CCD"; }
    }
}