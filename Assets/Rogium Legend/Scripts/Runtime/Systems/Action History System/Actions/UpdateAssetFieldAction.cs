using System;
using Rogium.Editors.Core;
using Rogium.UserInterface.Interactables;
using UnityEngine;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// An action that updates an asset field.
    /// </summary>
    public class UpdateAssetFieldAction : ActionBase<IAsset>
    {
        private readonly AssetField assetField;
        private readonly IAsset value;
        private readonly IAsset lastValue;
        
        public UpdateAssetFieldAction(AssetField assetField, IAsset value, IAsset lastValue, Action<IAsset> fallback) : base(fallback)
        {
            this.assetField = assetField;
            this.value = value;
            this.lastValue = lastValue;
        }
        
        protected override void ExecuteSelf() => assetField.UpdateValue(value);

        protected override void UndoSelf() => assetField.UpdateValue(lastValue);

        public override bool NothingChanged() => value.Equals(lastValue);
        
        public override object AffectedConstruct
        {
            get
            {
                try { return assetField?.gameObject; }
                catch (MissingReferenceException) { return null; }
            }
        }

        public override IAsset Value { get => value; }
        public override IAsset LastValue { get => lastValue; }

        public override string ToString() => $"{assetField.name}: {lastValue} -> {value}";
    }
}