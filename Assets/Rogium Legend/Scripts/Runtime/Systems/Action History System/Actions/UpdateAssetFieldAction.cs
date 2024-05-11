using Rogium.Editors.Core;
using Rogium.UserInterface.Interactables;
using Rogium.UserInterface.Interactables.Properties;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// An action that updates an asset field.
    /// </summary>
    public class UpdateAssetFieldAction : IAction
    {
        private readonly AssetField assetField;
        private readonly IAsset value;
        private readonly IAsset oldValue;
        
        public UpdateAssetFieldAction(AssetField assetField, IAsset value, IAsset oldValue)
        {
            this.assetField = assetField;
            this.value = value;
            this.oldValue = oldValue;
        }
        
        public void Execute() => assetField.UpdateValue(value);

        public void Undo() => assetField.UpdateValue(oldValue);

        public bool NothingChanged() => value.Equals(oldValue);
        
        public override string ToString() => $"{assetField.name}: {oldValue} -> {value}";
    }
}