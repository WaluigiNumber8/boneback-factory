using System;
using RedRats.Systems.Effectors.Core;
using UnityEngine;

namespace RedRats.Systems.Effectors.Effects
{
    /// <summary>
    /// An effect that controls other effectors.
    /// </summary>
    public class ControlEffect : EffectBase
    {
        [SerializeField] private Effector effector;
        [SerializeField] private EffectorControlType control;
        
        protected override void PlaySelf()
        {
            switch (control)
            {
                case EffectorControlType.Stop:
                    effector.Stop();
                    break;
                case EffectorControlType.Play:
                    effector.Play();
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