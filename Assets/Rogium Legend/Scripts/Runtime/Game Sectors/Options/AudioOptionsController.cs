using RedRats.Systems.Audio;
using UnityEngine;
using UnityEngine.Audio;

namespace Rogium.Options.OptionControllers
{
    /// <summary>
    /// Controls the audio of the game.
    /// </summary>
    public class AudioOptionsController : MonoBehaviour
    {
        private AudioMixer mixer;
        private AudioSystem.AudioMixerParamsInfo parameters;
        
        private void Awake()
        {
            mixer = AudioSystem.Instance.AudioMixer;
            parameters = AudioSystem.Instance.MixerParameters;
        }

        /// <summary>
        /// Changes the volume of the master channel.
        /// </summary>
        /// <param name="newVolume">The new volume to use.</param>
        public void UpdateMasterVolume(float newVolume) => UpdateVolume(parameters.masterVolume, newVolume);

        /// <summary>
        /// Changes the volume of the music channel.
        /// </summary>
        /// <param name="newVolume">The new volume to use.</param>
        public void UpdateMusicVolume(float newVolume) => UpdateVolume(parameters.musicVolume, newVolume);

        /// <summary>
        /// Changes the volume of the sound effects channel.
        /// </summary>
        /// <param name="newVolume">The new volume to use.</param>
        public void UpdateSoundVolume(float newVolume) => UpdateVolume(parameters.sfxVolume, newVolume);

        /// <summary>
        /// Changes the volume of the UI channel.
        /// </summary>
        /// <param name="newVolume">The new volume to use.</param>
        public void UpdateUIVolume(float newVolume) => UpdateVolume(parameters.uiVolume, newVolume);

        /// <summary>
        /// Updates the volume on the <see cref="AudioMixer"/>.
        /// </summary>
        /// <param name="parameter">The volume parameter to update.</param>
        /// <param name="newVolume">The new volume to set.</param>
        private void UpdateVolume(string parameter, float newVolume)
        {
            newVolume = Mathf.Max(0.0001f, newVolume);
            mixer.SetFloat(parameter, Mathf.Log10(newVolume) * 20);
        }

    }
}