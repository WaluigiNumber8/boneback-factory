using System.Collections;
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
        /// <param name="pitchMin">Minimum allowed pitch.</param>
        /// <param name="pitchMax">Maximum allowed pitch.</param>
        public void PlaySound(AudioClip[] clips, AudioMixerGroup mixerGroup, int id, float pitchMin = 1, float pitchMax = 1)
        {
            AudioSource source = (id == 0) ? sourcePool.Get() : sourcePool.Get(id);
            source.clip = clips[Random.Range(0, clips.Length)];
            source.outputAudioMixerGroup = mixerGroup;
            source.pitch = Random.Range(pitchMin, pitchMax);
            source.Play();
            
            StartCoroutine(ReleaseSourceCoroutine());
            IEnumerator ReleaseSourceCoroutine()
            {
                yield return new WaitForSeconds(source.clip.length);
                sourcePool.Release(source);
            }
        }
    }
}