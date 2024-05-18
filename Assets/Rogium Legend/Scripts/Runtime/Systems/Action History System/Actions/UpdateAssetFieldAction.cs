using System;
using Rogium.Editors.Core;
using Rogium.UserInterface.Interactables;
using UnityEngine;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// An action that updates an asset field.
    /// </summary>
    public class UpdateAssetFieldAction : IAction
    {
        private readonly AssetField assetField;
        private readonly IAsset value;
        private readonly IAsset lastValue;
        
        public UpdateAssetFieldAction(AssetField assetField, IAsset value, IAsset lastValue)
        {
            this.assetField = assetField;
            this.value = value;
            this.lastValue = lastValue;
        }
        
        public void Execute() => assetField.UpdateValue(value);

        public void Undo() => assetField.UpdateValue(lastValue);

        public bool NothingChanged() => value.Equals(lastValue);
        
        public object AffectedConstruct
        {
            get
            {
                try { return assetField?.gameObject; }
                catch (MissingReferenceException) { return null; }
            }
        }

        public object Value { get => value; }
        public object LastValue { get => lastValue; }

        public override string ToString() => $"{assetField.name}: {lastValue} -> {value}";
    }
}