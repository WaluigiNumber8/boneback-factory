using RedRats.Systems.Audio;
using Rogium.Editors.Core;
using Rogium.Systems.Audio;
using UnityEngine;
using UnityEngine.Audio;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Allows entities to emit sounds.
    /// </summary>
    public class CharacteristicSoundEmitter : CharacteristicBase
    {
        [SerializeField] private AudioMixerGroup mixerGroup;

        private CharSoundInfo soundData;

        public void Construct(CharSoundInfo soundInfo) => this.soundData = soundInfo;

        /// <summary>
        /// Plays the hurt sound.
        /// </summary>
        public void PlayHurtSound(int damage = 0) => PlaySound(soundData.hurtSound);
        
        /// <summary>
        /// Play the death sound.
        /// </summary>
        public void PlayDeathSound() => PlaySound(soundData.deathSound);
        
        /// <summary>
        /// Play the use sound.
        /// </summary>
        public void PlayUseSound() => PlaySound(soundData.useSound);

        /// <summary>
        /// Plays a sound.
        /// </summary>
        /// <param name="soundData">The sound data to use.</param>
        private void PlaySound(AssetData soundData)
        {
            AudioSystemRogium.GetInstance().PlaySound(soundData, mixerGroup, new AudioSourceSettingsInfo(0, false, false, false));
        }
    }
}