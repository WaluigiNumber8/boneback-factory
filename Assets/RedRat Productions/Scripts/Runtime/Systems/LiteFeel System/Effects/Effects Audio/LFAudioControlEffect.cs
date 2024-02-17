using System;
using RedRats.Systems.Audio;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public class LFAudioControlEffect : LFEffectBase
    {
        [SerializeField] private int id;
        [SerializeField] private AudioControlType mode = AudioControlType.Free;

        private AudioSystem audioSystem;
        
        protected override void Initialize()
        {
            audioSystem = AudioSystem.GetInstance();
        }

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
    }
}