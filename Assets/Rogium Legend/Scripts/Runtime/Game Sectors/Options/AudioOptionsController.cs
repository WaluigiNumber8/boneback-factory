using MoreMountains.Tools;
using UnityEngine;

namespace Rogium.Options.OptionControllers
{
    /// <summary>
    /// Controls the audio of the game.
    /// </summary>
    public class AudioOptionsController : MonoBehaviour
    {
        [SerializeField] private MMSoundManager soundManager;
        
        /// <summary>
        /// Changes the volume of the master channel.
        /// </summary>
        /// <param name="newVolume">The new volume to use.</param>
        public void UpdateMasterVolume(float newVolume)
        {
            soundManager.SetVolumeMaster(newVolume);
        }
        
        /// <summary>
        /// Changes the volume of the music channel.
        /// </summary>
        /// <param name="newVolume">The new volume to use.</param>
        public void UpdateMusicVolume(float newVolume)
        {
            soundManager.SetVolumeMusic(newVolume);
        }
        
        /// <summary>
        /// Changes the volume of the sound effects channel.
        /// </summary>
        /// <param name="newVolume">The new volume to use.</param>
        public void UpdateSoundVolume(float newVolume)
        {
            soundManager.SetVolumeSfx(newVolume);
        }
        
        /// <summary>
        /// Changes the volume of the UI channel.
        /// </summary>
        /// <param name="newVolume">The new volume to use.</param>
        public void UpdateUIVolume(float newVolume)
        {
            soundManager.SetVolumeUI(newVolume);
        }

    }
}