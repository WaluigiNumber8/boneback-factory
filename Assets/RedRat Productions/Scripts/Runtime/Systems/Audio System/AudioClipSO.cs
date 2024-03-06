using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

namespace RedRats.Systems.Audio
{
    /// <summary>
    /// Represents a playable audio clip with proper settings.
    /// </summary>
    [CreateAssetMenu(fileName = "asset_SFX_NewSoundClip", menuName = "RedRat Productions/Sound Data", order = 0)]
    public class AudioClipSO : ScriptableObject
    {
        [Header("Clips")] 
        [SerializeField] private AudioClip[] clips;
        [SerializeField] private AudioMixerGroup mixerGroup;

        [Header("Settings")] 
        [SerializeField, Range(0f, 1f)] private float volume = 1f;
        [SerializeField, Range(0f, 2f)] private float pitchMin = 0.95f;
        [SerializeField, Range(0f, 2f)] private float pitchMax = 1.05f;

        public AudioClip Clip { get => GetClip(); }
        public AudioMixerGroup MixerGroup { get => mixerGroup; }
        public float Volume { get => volume; }
        public float PitchMin { get => pitchMin; }
        public float PitchMax { get => pitchMax; }

        private void OnValidate()
        {
            if (pitchMin > pitchMax) pitchMax = pitchMin;
        }

        private AudioClip GetClip() => clips[Random.Range(0, clips.Length)];

        #region Editor Preview Code

#if UNITY_EDITOR
        private AudioSource previewer;

        private void OnEnable() => previewer = EditorUtility.CreateGameObjectWithHideFlags("AudioPreview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();

        private void OnDisable() => DestroyImmediate(previewer.gameObject);
        
        [ButtonGroup("previewControls")]
        [GUIColor(.3f, 0.9f, .3f)]
        [Button(ButtonSizes.Large)]
        private void PlayPreview() => Play(previewer);

        [ButtonGroup("previewControls")]
        [GUIColor(0.9f, .3f, .3f)]
        [Button(ButtonSizes.Large)]
        [EnableIf("@previewer.isPlaying")]
        private void StopPreview() => previewer.Stop();

        private void Play(AudioSource source)
        {
            if (clips == null || clips.Length == 0 || clips[0] == null) return;

            // set source config
            source.clip = GetClip();
            source.volume = Volume;
            source.pitch = Random.Range(PitchMin, PitchMax);

            source.Play();
        }
#endif

        #endregion
    }
}