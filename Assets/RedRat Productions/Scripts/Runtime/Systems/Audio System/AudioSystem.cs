using System;
using System.Collections;
using System.Linq;
using RedRats.Core;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace RedRats.Systems.Audio
{
    /// <summary>
    /// Manages <see cref="AudioSource"/>s and played audio.
    /// </summary>
    public class AudioSystem : MonoSingleton<AudioSystem>
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private AudioMixerParamsInfo mixerParameters;

        private ObjectDictionaryPool<int, AudioSource> sourcePool;

        protected override void Awake()
        {
            base.Awake();
            sourcePool = new ObjectDictionaryPool<int, AudioSource>(
                () =>
                {
                    AudioSource source = new GameObject($"AudioSource").AddComponent<AudioSource>();
                    source.playOnAwake = false;
                    return source;
                },
                c => c.gameObject.SetActive(true),
                c => c.gameObject.SetActive(false),
                Destroy,
                true, 15);
        }

        /// <summary>
        /// Plays a single sound.
        /// </summary>
        /// <param name="clipsSO">The clip along with it's settings. If multiple are provided a random one is chosen.</param>
        /// <param name="sourceSettings">The settings used for the next playback.</param>
        /// <returns>The <see cref="AudioSource"/> that plays the clip.</returns>
        public AudioSource PlaySound(AudioClipSO[] clipsSO, AudioSourceSettingsInfo sourceSettings)
        {
            AudioClipSO clipSO = clipsSO[Random.Range(0, clipsSO.Length)];
            return PlaySound(clipSO.Clip, clipSO.MixerGroup, sourceSettings, volume: clipSO.Volume, pitchMin: clipSO.PitchMin, pitchMax: clipSO.PitchMax);
        }

        /// <summary>
        /// Plays a single sound.
        /// </summary>
        /// <param name="clipSO">The clip along with it's settings.</param>
        /// <param name="sourceSettings">The settings used for the next playback.</param>
        /// <returns>The <see cref="AudioSource"/> that plays the clip.</returns>
        public AudioSource PlaySound(AudioClipSO clipSO, AudioSourceSettingsInfo sourceSettings)
        {
            return PlaySound(clipSO.Clip, clipSO.MixerGroup, sourceSettings, clipSO.Volume, clipSO.PitchMin, clipSO.PitchMax);
        }

        /// <summary>
        /// Play a single sound.
        /// </summary>
        /// <param name="clips">The <see cref="AudioClip"/> to play. If multiple are provided, plays a random one.</param>
        /// <param name="mixerGroup">Which group the sound played</param>
        /// <param name="sourceSettings">The settings used for the next playback.</param>
        /// <param name="volume">How loud the sound is.</param>
        /// <param name="pitchMin">Minimum allowed pitch.</param>
        /// <param name="pitchMax">Maximum allowed pitch.</param>
        /// <returns>The <see cref="AudioSource"/> that plays the clip.</returns>
        public AudioSource PlaySound(AudioClip[] clips, AudioMixerGroup mixerGroup, AudioSourceSettingsInfo sourceSettings, float volume = 1, float pitchMin = 1, float pitchMax = 1)
        {
            AudioClip clip = clips[Random.Range(0, clips.Length)];
            return PlaySound(clip, mixerGroup, sourceSettings, volume, pitchMin, pitchMax);
        }

        /// <summary>
        /// Play a single sound.
        /// </summary>
        /// <param name="clip">The <see cref="AudioClip"/> to play.</param>
        /// <param name="mixerGroup">Which group the sound played</param>
        /// <param name="sourceSettings">The settings used for the next playback.</param>
        /// <param name="volume">How loud the sound is.</param>
        /// <param name="pitchMin">Minimum allowed pitch.</param>
        /// <param name="pitchMax">Maximum allowed pitch.</param>
        /// <returns>The <see cref="AudioSource"/> that plays the clip.</returns>
        public AudioSource PlaySound(AudioClip clip, AudioMixerGroup mixerGroup, AudioSourceSettingsInfo sourceSettings, float volume = 1, float pitchMin = 1, float pitchMax = 1)
        {
            if (clip == null) return null;

            // If playOnlyWhenNotPlaying and the clip is already playing, don't play it again.
            if (sourceSettings.playOnlyWhenNotPlaying && sourcePool.GetActive().Any(s => s.isPlaying && s.clip == clip)) return null;

            // If muteSameSound and the clip is already playing, stop it.
            if (sourceSettings.muteSameSound)
            {
                foreach (AudioSource s in sourcePool.GetActive())
                {
                    if (!s.isPlaying || s.clip != clip) continue;
                    s.Stop();
                    TryReleaseSource(s);
                }
            }

            AudioSource source = (sourceSettings.id == 0) ? sourcePool.Get() : sourcePool.Get(sourceSettings.id);
            source.clip = clip;
            source.outputAudioMixerGroup = mixerGroup;
            source.loop = sourceSettings.loop;
            source.volume = volume;
            source.pitch = Random.Range(pitchMin, pitchMax);
            source.Play();

            if (source.loop) return source;

            StartCoroutine(ReleaseSourceCoroutine());
            IEnumerator ReleaseSourceCoroutine()
            {
                yield return new WaitForSeconds(source.clip.length * 4);
                if (sourceSettings.id == 0) TryReleaseSource(source);
                else TryReleaseSource(sourceSettings.id);
            }

            return source;
        }

        /// <summary>
        /// Stops a sound with a specific id from playing.
        /// </summary>
        /// <param name="id">The id of the <see cref="AudioSource"/> to stop.</param>
        public void StopSound(int id)
        {
            sourcePool.Get(id).Stop();
            TryReleaseSource(id);
        }

        /// <summary>
        /// Stops a sound from playing.
        /// </summary>
        /// <param name="source">The <see cref="AudioSource"/> to stop.</param>
        public void StopSound(AudioSource source)
        {
            source.Stop();
            TryReleaseSource(source);
        }

        private void TryReleaseSource(AudioSource source)
        {
            if (source.isPlaying || !source.gameObject.activeSelf) return;
            sourcePool.Release(source);
        }

        private void TryReleaseSource(int id)
        {
            AudioSource source = sourcePool.Get(id);
            if (source.isPlaying || !source.gameObject.activeSelf) return;
            sourcePool.Release(id);
        }

        public AudioMixer AudioMixer { get => audioMixer; }
        public AudioMixerParamsInfo MixerParameters { get => mixerParameters; }

        [Serializable]
        public struct AudioMixerParamsInfo
        {
            public string masterVolume;
            public string musicVolume;
            public string sfxVolume;
            public string uiVolume;
        }
    }
}