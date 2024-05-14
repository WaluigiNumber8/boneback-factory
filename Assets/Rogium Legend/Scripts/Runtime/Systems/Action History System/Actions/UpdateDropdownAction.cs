using Rogium.UserInterface.Interactables.Properties;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// An action that updates a dropdown.
    /// </summary>
    public class UpdateDropdownAction : IAction
    {
        private readonly InteractablePropertyDropdown dropdown;
        private readonly int value;
        private readonly int oldValue;

        public UpdateDropdownAction(InteractablePropertyDropdown dropdown, int value, int oldValue)
        {
            this.dropdown = dropdown;
            this.value = value;
            this.oldValue = oldValue;
            
        }

        public void Execute() => dropdown.UpdateValueWithoutNotify(value);

        public void Undo() => dropdown.UpdateValueWithoutNotify(oldValue);

        public bool NothingChanged() => value == oldValue;
        
        public object AffectedConstruct => dropdown;

        public override string ToString() => $"{dropdown.name}: {oldValue} -> {value}";
    }
}