using System;
using Rogium.Editors.Core;
using Rogium.Editors.Sounds;
using Rogium.UserInterface.Interactables;
using UnityEngine;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// An action that updates a sound field.
    /// </summary>
    public class UpdateSoundFieldAction : ActionBase<AssetData>
    {
        private readonly SoundField soundField;
        private readonly AssetData value;
        private readonly AssetData lastValue;
        private readonly SoundAsset soundAsset;
        private readonly SoundAsset lastSoundAsset;
        
        public UpdateSoundFieldAction(SoundField soundField, AssetData value, AssetData lastValue, SoundAsset soundAsset, SoundAsset lastSoundAsset, Action<AssetData> fallback) : base(fallback)
        {
            this.soundField = soundField;
            this.value = value;
            this.lastValue = lastValue;
            this.soundAsset = soundAsset;
            this.lastSoundAsset = lastSoundAsset;
        }
        
        protected override void ExecuteSelf()
        {
            soundField.UpdateValue(value);
            soundField.UpdateSoundAsset(soundAsset);
        }

        protected override void UndoSelf()
        {
            soundField.UpdateValue(lastValue);
            soundField.UpdateSoundAsset(lastSoundAsset);
        }

        public override bool NothingChanged() => value.Equals(lastValue) && soundAsset == lastSoundAsset;

        public override object AffectedConstruct
        {
            get
            {
                try { return soundField?.gameObject; }
                catch (MissingReferenceException) { return null; }
            }
        }

        public override AssetData Value { get => value; }
        public override AssetData LastValue { get => lastValue; }
    }
}