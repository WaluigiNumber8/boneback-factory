using RedRats.Systems.Audio;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
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
        
        private InternalLibraryOverseer iLib;
        private CharSoundEffectInfo hurtSoundData;

        private void Awake() => iLib = InternalLibraryOverseer.GetInstance();

        public void Construct(CharSoundInfo soundInfo)
        {
            hurtSoundData = BuildCharSoundEffectInfoFrom(soundInfo.hurtSound);
        }

        /// <summary>
        /// Plays the hurt sound.
        /// </summary>
        public void PlayHurtSound(int damage = 0) => PlaySound(hurtSoundData);

        /// <summary>
        /// Plays a sound.
        /// </summary>
        /// <param name="soundData">The sound data to use.</param>
        private void PlaySound(CharSoundEffectInfo soundData)
        {
            if (!soundData.CanPlay()) return;
            float pitch = (soundData.useRandomPitch) ? Random.Range(soundData.pitch - EditorConstants.SoundPitchOffset, soundData.pitch + EditorConstants.SoundPitchOffset) : soundData.pitch;
            AudioSystem.GetInstance().PlaySound(soundData.clip, mixerGroup, new AudioSourceSettingsInfo(0, false, false, false), soundData.volume, pitch);
        }
        
        private CharSoundEffectInfo BuildCharSoundEffectInfoFrom(IParameterAsset soundAssetData)
        {
            if (soundAssetData.ID == EditorConstants.EmptyAssetID) return new CharSoundEffectInfo(null, 0, 0, false);
            return new CharSoundEffectInfo(iLib.GetSoundByID(soundAssetData.ID).Data.Clip, 
                soundAssetData.Parameters.floatValue1, 
                soundAssetData.Parameters.floatValue2, 
                soundAssetData.Parameters.boolValue1);
        }
        
    }
}