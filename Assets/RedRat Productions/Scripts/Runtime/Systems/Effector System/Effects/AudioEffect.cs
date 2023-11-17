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
        [Header("Clips")]
        [SerializeField] private AudioClip[] clips;
        
        [Header("Mixing")]
        [SerializeField] private AudioMixerGroup mixerGroup;
        [SerializeField] private int id;
        [SerializeField] private bool playOnlyWhenNotPlaying;
        
        [Header("Settings")]
        [SerializeField, Range(0f, 1f)] private float volume = 1f;
        [SerializeField, Range(0f, 2f)] private float pitchMin = 1f;
        [SerializeField, Range(0f, 2f)] private float pitchMax = 1f;
        private AudioSystem audioSystem;

        private void Awake() => audioSystem = AudioSystem.GetInstance();

        /// <summary>
        /// Plays the audio.
        /// </summary>
        protected override void PlayEffects()
        {
            audioSystem.PlaySound(clips, mixerGroup, id, playOnlyWhenNotPlaying, volume, pitchMin, pitchMax);
        }
    }
}