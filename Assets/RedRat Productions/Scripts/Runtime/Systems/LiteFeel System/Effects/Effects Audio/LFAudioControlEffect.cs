using System;
using RedRats.Systems.Audio;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// Controls a sound playing under a specific ID in the <see cref="AudioSystem"/>.
    /// </summary>
    public class LFAudioControlEffect : LFEffectBase
    {
        [SerializeField] private int id;
        [SerializeField] private AudioControlType mode = AudioControlType.Free;

        private AudioSystem audioSystem;
        
        protected override void Initialize() => audioSystem = AudioSystem.Instance;

        protected override void PlaySelf()
        {
            switch (mode)
            {
                case AudioControlType.Free:
                    audioSystem.StopSound(id);
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

        protected override string FeedbackColor { get => "#FFCD1C"; }
    }
}