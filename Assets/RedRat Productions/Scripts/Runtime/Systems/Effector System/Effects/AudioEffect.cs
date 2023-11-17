using RedRats.Safety;
using RedRats.Systems.Audio;
using Sirenix.OdinInspector;
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
        [SerializeField] private AudioClipSO[] clips;
        
        [Header("Audio Source")] 
        [SerializeField] private int id;
        [SerializeField] private bool playOnlyWhenNotPlaying;
        
        [Header("Override Settings")] 
        [SerializeField] private bool overrideMixerGroup;
        [SerializeField, ShowIf("overrideMixerGroup")] private AudioMixerGroup mixerGroup;
        [SerializeField] private bool overrideVolume;
        [SerializeField, Range(0f, 1f), ShowIf("overrideVolume")] private float volume = 1f;
        [SerializeField] private bool overridePitch;
        [SerializeField, Range(0f, 2f), ShowIf("overridePitch")] private float pitchMin = 0.95f;
        [SerializeField, Range(0f, 2f), ShowIf("overridePitch")] private float pitchMax = 1.05f;
        
        private AudioSystem audioSystem;
        private AudioSource mySource;

        private void Awake() => audioSystem = AudioSystem.GetInstance();

        protected override void PlaySelf()
        {
            SafetyNet.EnsureListIsNotNullOrEmpty(clips, nameof(clips));
            AudioClipSO clip = clips[Random.Range(0, clips.Length)];
            
            SafetyNet.EnsureIsNotNull(clip, nameof(clip));
            AudioMixerGroup mixer = (overrideMixerGroup) ? mixerGroup : clip.MixerGroup;
            float vol = (overrideVolume) ? volume : clip.Volume;
            float pMin = (overridePitch) ? pitchMin : clip.PitchMin;
            float pMax = (overridePitch) ? pitchMax : clip.PitchMax;
            mySource = audioSystem.PlaySound(clip.Clip, mixer, id, playOnlyWhenNotPlaying, vol, pMin, pMax);
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