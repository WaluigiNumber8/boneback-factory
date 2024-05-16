using Rogium.UserInterface.Interactables.Properties;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// An action that updates an input field.
    /// </summary>
    public class UpdateInputFieldAction : IAction
    {
        private readonly InteractablePropertyInputField inputField;
        private readonly string value;
        private readonly string lastValue;

        public UpdateInputFieldAction(InteractablePropertyInputField inputField, string value, string lastValue)
        {
            this.inputField = inputField;
            this.value = value;
            this.lastValue = lastValue;
        }

        public void Execute() => inputField.UpdateValueWithoutNotify(value);

        public void Undo() => inputField.UpdateValueWithoutNotify(lastValue);

        public bool NothingChanged() => value == lastValue;
        
        public object AffectedConstruct => inputField;
        public object Value { get => value; }
        public object LastValue { get => lastValue; }

        public override string ToString() => $"{inputField.name}: {lastValue} -> {value}";
    }
}