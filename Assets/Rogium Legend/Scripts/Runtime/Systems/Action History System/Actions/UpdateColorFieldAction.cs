using Rogium.UserInterface.Interactables;
using UnityEngine;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// An action that updates a color field.
    /// </summary>
    public class UpdateColorFieldAction : IAction
    {
        private readonly ColorField colorField;
        private readonly Color value;
        private readonly Color lastValue;
        
        public UpdateColorFieldAction(ColorField colorField, Color value, Color lastValue)
        {
            this.colorField = colorField;
            this.value = value;
            this.lastValue = lastValue;
        }
        
        public void Execute() => colorField.UpdateValue(value);

        public void Undo() => colorField.UpdateValue(lastValue);

        public bool NothingChanged() => value == lastValue;

        public object AffectedConstruct { get => colorField; }
    }
}