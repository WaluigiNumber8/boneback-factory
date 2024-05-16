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
        private readonly int lastValue;

        public UpdateDropdownAction(InteractablePropertyDropdown dropdown, int value, int lastValue)
        {
            this.dropdown = dropdown;
            this.value = value;
            this.lastValue = lastValue;
            
        }

        public void Execute() => dropdown.UpdateValueWithoutNotify(value);

        public void Undo() => dropdown.UpdateValueWithoutNotify(lastValue);

        public bool NothingChanged() => value == lastValue;
        
        public object AffectedConstruct => dropdown;
        public object Value { get => value; }
        public object LastValue { get => lastValue; }

        public override string ToString() => $"{dropdown.name}: {lastValue} -> {value}";
    }
}