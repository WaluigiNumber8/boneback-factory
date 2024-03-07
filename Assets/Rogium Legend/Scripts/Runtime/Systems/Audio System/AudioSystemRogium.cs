using System.Collections.Generic;
using System.Linq;
using RedRats.Core;
using RedRats.Systems.Audio;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Sounds;
using UnityEngine;
using UnityEngine.Audio;

namespace Rogium.Systems.Audio
{
    /// <summary>
    /// Extends the <see cref="AudioSystem"/> with Rogium-specific features.
    /// </summary>
    public class AudioSystemRogium : MonoSingleton<AudioSystemRogium>
    {
        private AudioSystem audioSystem;

        private IDictionary<string, SoundAsset> allSounds;
        
        protected override void Awake()
        {
            base.Awake();
            audioSystem = AudioSystem.GetInstance();
            allSounds = InternalLibraryOverseer.GetInstance().GetSoundsCopy().ToDictionary(x => x.ID);
        }

        /// <summary>
        /// Plays a sound from the sound library based on the given <paramref name="soundData"/>.
        /// </summary>
        /// <param name="soundData">Data of used sound, volume, pitch and others.</param>
        /// <param name="mixerGroup">The mixer group under which to play the sound.</param>
        /// <param name="settings">Settings for the <see cref="AudioSource"/>.</param>
        public void PlaySound(AssetData soundData, AudioMixerGroup mixerGroup, AudioSourceSettingsInfo settings)
        {
            if (soundData.ID == EditorConstants.EmptyAssetID) return;
            
            AudioClip clip = allSounds[soundData.ID].Data.Clip;
            float volume = soundData.Parameters.floatValue1;
            float pitch = soundData.Parameters.floatValue2;
            float pitchMin = (soundData.Parameters.boolValue1) ? pitch - EditorConstants.SoundPitchOffset : pitch;
            float pitchMax = (soundData.Parameters.boolValue1) ? pitch + EditorConstants.SoundPitchOffset : pitch;
            
            audioSystem.PlaySound(clip, mixerGroup, settings, volume, pitchMin, pitchMax);
        }
    }
}