using RedRats.Safety;
using RedRats.Systems.Audio;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// Plays audio.
    /// </summary>
    public class LFAudioEffect : LFEffectBase
    {
        [SerializeField] private AudioClipSO[] clips;
        [SerializeField] private AudioSourceSettingsInfo sourceSettings;
        [SerializeField] private bool ignoreEffectStop = true;
        
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

        protected override void Initialize() => audioSystem = AudioSystem.GetInstance();

        protected override void PlaySelf()
        {
            if (mySource != null && mySource.isPlaying && mySource.loop) return;
            SafetyNet.EnsureListIsNotNullOrEmpty(clips, nameof(clips));
            
            AudioClipSO clip = clips[Random.Range(0, clips.Length)];
            
            SafetyNet.EnsureIsNotNull(clip, nameof(clip));
            AudioMixerGroup mixer = (overrideMixerGroup) ? mixerGroup : clip.MixerGroup;
            float vol = (overrideVolume) ? volume : clip.Volume;
            float pMin = (overridePitch) ? pitchMin : clip.PitchMin;
            float pMax = (overridePitch) ? pitchMax : clip.PitchMax;
            mySource = audioSystem.PlaySound(clip.Clip, mixer, sourceSettings, vol, pMin, pMax);
        }

        protected override void StopSelf()
        {
            if (ignoreEffectStop) return;
            if (sourceSettings.id != 0)
            {
                audioSystem.StopSound(sourceSettings.id);
                return;
            }
            if (mySource != null) audioSystem.StopSound(mySource);
        }
    }
}