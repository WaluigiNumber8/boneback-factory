using Rogium.Editors.Core;
using Rogium.Editors.Sounds;
using Rogium.UserInterface.Interactables;
using UnityEngine;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// An action that updates a sound field.
    /// </summary>
    public class UpdateSoundFieldAction : IAction
    {
        private readonly SoundField soundField;
        private readonly AssetData value;
        private readonly AssetData lastValue;
        private readonly SoundAsset soundAsset;
        private readonly SoundAsset lastSoundAsset;
        
        public UpdateSoundFieldAction(SoundField soundField, AssetData value, AssetData lastValue, SoundAsset soundAsset, SoundAsset lastSoundAsset)
        {
            this.soundField = soundField;
            this.value = value;
            this.lastValue = lastValue;
            this.soundAsset = soundAsset;
            this.lastSoundAsset = lastSoundAsset;
        }
        
        public void Execute()
        {
            soundField.UpdateValue(value);
            soundField.UpdateSoundAsset(soundAsset);
        }

        public void Undo()
        {
            soundField.UpdateValue(lastValue);
            soundField.UpdateSoundAsset(lastSoundAsset);
        }

        public bool NothingChanged() => value.Equals(lastValue) && soundAsset == lastSoundAsset;

        public object AffectedConstruct
        {
            get
            {
                try { return soundField?.gameObject; }
                catch (MissingReferenceException) { return null; }
            }
        }

        public object Value { get => value; }
        public object LastValue { get => lastValue; }
    }
}