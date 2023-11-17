using System;
using UnityEngine;

namespace RedRats.Systems.Effectors.Effects
{
    /// <summary>
    /// An effect that controls other effects.
    /// </summary>
    public class ControlEffect : EffectBase
    {
        [SerializeField] private EffectBase effect;
        [SerializeField] private EffectControlType control;
        
        protected override void PlaySelf()
        {
            switch (control)
            {
                case EffectControlType.Stop:
                    effect.Stop();
                    break;
                case EffectControlType.Play:
                    effect.Play();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void StopSelf()
        {
            // Nothing to do here.
        }
    }
}