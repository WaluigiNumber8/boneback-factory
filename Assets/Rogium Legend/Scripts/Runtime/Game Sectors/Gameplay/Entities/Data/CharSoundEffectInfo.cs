using System;
using UnityEngine;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Contains data of a single sound from the <see cref="CharacteristicSoundEmitter"/> characteristic.
    /// </summary>
    [Serializable]
    public struct CharSoundEffectInfo
    {
        public AudioClip clip;
        public float volume;
        public float pitch;
        public bool useRandomPitch;

        public CharSoundEffectInfo(AudioClip clip, float volume, float pitch, bool useRandomPitch)
        {
            this.clip = clip;
            this.volume = volume;
            this.pitch = pitch;
            this.useRandomPitch = useRandomPitch;
        }
        
        public bool CanPlay() => clip != null;
    }
}