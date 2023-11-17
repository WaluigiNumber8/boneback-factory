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
        [SerializeField, Range(0f, 2f)] private float pitchMin = 0.95f;
        [SerializeField, Range(0f, 2f)] private float pitchMax = 1.05f;
        
        private AudioSystem audioSystem;
        private AudioSource mySource;

        private void Awake() => audioSystem = AudioSystem.GetInstance();

        protected override void PlaySelf()
        {
            mySource = audioSystem.PlaySound(clips, mixerGroup, id, playOnlyWhenNotPlaying, volume, pitchMin, pitchMax);
        }

        protected override void StopSelf()
        {
            if (id != 0)
            {
                audioSystem.StopSound(id);
                return;
            }
            if (mySource != null) audioSystem.StopSound(mySource);
        }
    }
}