using RedRats.Core;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

namespace RedRats.Systems.Audio
{
    /// <summary>
    /// Manages <see cref="AudioSource"/>s and played audio.
    /// </summary>
    public class AudioSystem : PersistentMonoSingleton<AudioSystem>
    {
        private ObjectPool<AudioSource> sourcePool;
        
        //TODO Implement the pool for AudioSources.
        
        
        
        public void PlaySound(AudioClip[] clips, AudioMixerGroup mixerGroup, float pitchMin, float pitchMax)
        {
            //TODO Play a random clip from clips with proper settings and mixer group.
            throw new System.NotImplementedException();
        }
    }
}