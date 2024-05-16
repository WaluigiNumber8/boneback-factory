using Rogium.Editors.Core;
using Rogium.UserInterface.Interactables;

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
        
        public object AffectedConstruct => assetField;

        public override string ToString() => $"{assetField.name}: {lastValue} -> {value}";
    }
}