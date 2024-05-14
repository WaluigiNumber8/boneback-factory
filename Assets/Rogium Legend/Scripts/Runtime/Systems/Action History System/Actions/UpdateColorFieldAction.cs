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
        private readonly Color oldValue;
        
        public UpdateColorFieldAction(ColorField colorField, Color value, Color oldValue)
        {
            this.colorField = colorField;
            this.value = value;
            this.oldValue = oldValue;
        }
        
        public void Execute() => colorField.UpdateValue(value);

        public void Undo() => colorField.UpdateValue(oldValue);

        public bool NothingChanged() => value == oldValue;

        public object AffectedConstruct { get => colorField; }
    }
}