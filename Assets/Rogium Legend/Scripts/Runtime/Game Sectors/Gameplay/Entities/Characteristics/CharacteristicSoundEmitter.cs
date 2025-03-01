using RedRats.Systems.Audio;
using RedRats.Systems.Clocks;
using Rogium.Core;
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
        
        private AudioSystemRogium audioSystem;
        
        private CharSoundInfo soundData;
        private CountdownTimer idleTimer;

        private void Awake()
        {
            audioSystem = AudioSystemRogium.Instance;
            idleTimer = new CountdownTimer(() =>
            {
                PlaySound(soundData.idleSound);
                WindIdleTimer();
            });
        }

        public void Construct(CharSoundInfo soundInfo)
        {
            this.soundData = soundInfo;
            WindIdleTimer();
        }

        private void Update() => idleTimer?.Tick();

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
            audioSystem.PlaySound(soundData, mixerGroup, new AudioSourceSettingsInfo(0, false, false, false));
        }
        
        private void WindIdleTimer()
        {
            if (soundData.idleSound.IsEmpty()) return;
            
            float length = audioSystem.GetSound(soundData.idleSound).Data.Clip.length;
            float pitch = soundData.idleSound.Parameters.floatValue2;
            float finalLength = length / Mathf.Abs(pitch) + Random.Range(0f, 0.05f);
            idleTimer.Start(finalLength);
        }
    }
}