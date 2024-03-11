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
        [SerializeField] private AudioSpatialSettingsInfo spatialSettings;
        
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
        private float clipDuration;

        protected override void Initialize() => audioSystem = AudioSystem.GetInstance();

        protected override void PlaySelf()
        {
            if (mySource != null && mySource.isPlaying && mySource.loop) return;
            SafetyNet.EnsureListIsNotNullOrEmpty(clips, nameof(clips));
            
            AudioClipSO c = clips[Random.Range(0, clips.Length)];
            
            SafetyNet.EnsureIsNotNull(c, nameof(c));
            clipDuration = c.Clip.length;
            AudioMixerGroup mixer = (overrideMixerGroup) ? mixerGroup : c.MixerGroup;
            float vol = (overrideVolume) ? volume : c.Volume;
            float pMin = (overridePitch) ? pitchMin : c.PitchMin;
            float pMax = (overridePitch) ? pitchMax : c.PitchMax;
            mySource = audioSystem.PlaySound(c.Clip, mixer, sourceSettings, spatialSettings, vol, pMin, pMax);
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

        protected override void ResetState()
        {
            // Nothing to do here.
        }

        #region Update values

        public void ChangeClip(AudioClipSO newClip) => clips = new[] {newClip};
        public void ChangeClips(AudioClipSO[] newClips) => clips = newClips;
        public void ChangePitch(float newPitch) => ChangePitch(newPitch, newPitch);
        public void ChangePitch(float newPitchMin, float newPitchMax)
        {
            overridePitch = true;
            pitchMin = newPitchMin;
            pitchMax = newPitchMax;
        }
        public void ChangeSoundTarget(Transform newTarget) => spatialSettings.soundTarget = newTarget;

        #endregion
        
        protected override float TotalDuration { get => clipDuration * ((sourceSettings.loop) ? int.MaxValue : 1); }
        protected override string FeedbackColor { get => "#FFCD1C"; }
    }
}