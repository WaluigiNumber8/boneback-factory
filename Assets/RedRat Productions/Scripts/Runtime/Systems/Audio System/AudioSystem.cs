using System.Collections;
using System.Linq;
using RedRats.Core;
using UnityEngine;
using UnityEngine.Audio;

namespace RedRats.Systems.Audio
{
    /// <summary>
    /// Manages <see cref="AudioSource"/>s and played audio.
    /// </summary>
    public class AudioSystem : PersistentMonoSingleton<AudioSystem>
    {
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
        /// Play a single sound.
        /// </summary>
        /// <param name="clips">The <see cref="AudioClip"/> to play. If multiple are provided, plays a random one.</param>
        /// <param name="mixerGroup">Which group the sound played</param>
        /// <param name="id">The id of the source to use. If 0 is used, a random <see cref="AudioSource"/> will be picked instead.</param>
        /// <param name="playOnlyWhenNotPlaying">Only play the given clip if it's not being played already.</param>
        /// <param name="volume">How loud the sound is.</param>
        /// <param name="pitchMin">Minimum allowed pitch.</param>
        /// <param name="pitchMax">Maximum allowed pitch.</param>
        public void PlaySound(AudioClip[] clips, AudioMixerGroup mixerGroup, int id, bool playOnlyWhenNotPlaying = false,
            float volume = 1, float pitchMin = 1, float pitchMax = 1)
        {
            AudioClip clip = clips[Random.Range(0, clips.Length)];

            // If the clip is already playing, don't play it again.
            if (playOnlyWhenNotPlaying && sourcePool.GetActive().Any(s => s.isPlaying && s.clip == clip)) return;

            AudioSource source = (id == 0) ? sourcePool.Get() : sourcePool.Get(id);
            source.clip = clip;
            source.outputAudioMixerGroup = mixerGroup;
            source.volume = volume;
            source.pitch = Random.Range(pitchMin, pitchMax);
            source.Play();

            StartCoroutine(ReleaseSourceCoroutine());
            IEnumerator ReleaseSourceCoroutine()
            {
                yield return new WaitForSeconds(source.clip.length);
                if (id == 0) sourcePool.Release(source);
                else sourcePool.Release(id);
            }
        }
    }
}