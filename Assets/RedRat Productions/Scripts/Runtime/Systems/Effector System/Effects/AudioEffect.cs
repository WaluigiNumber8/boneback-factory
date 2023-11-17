using RedRats.Systems.Audio;
using UnityEngine;
using UnityEngine.Audio;

namespace RedRats.Systems.Effectors.Effects
{
    /// <summary>
    /// Plays audio.
    /// </summary>
    public class AudioEffect : EffectBase
    {
        [SerializeField] private AudioClip[] clips;
        [SerializeField] private AudioMixerGroup mixerGroup;
        [SerializeField] private int id;
        [SerializeField] private bool playOnlyWhenNotPlaying;
        
        [SerializeField, Range(0f, 2f)] private float pitchMin = 1;
        [SerializeField, Range(0f, 2f)] private float pitchMax = 1;


        /// <summary>
        /// Plays the audio.
        /// </summary>
        protected override void PlayEffects()
        {
            AudioSystem.GetInstance().PlaySound(clips, mixerGroup, id, playOnlyWhenNotPlaying, pitchMin, pitchMax);
        }
    }
}