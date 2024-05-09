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
        private readonly string oldValue;

        public UpdateInputFieldAction(InteractablePropertyInputField inputField, string value, string oldValue)
        {
            this.inputField = inputField;
            this.value = value;
            this.oldValue = oldValue;
        }

        public void Execute() => inputField.UpdateValueWithoutNotify(value);

        public void Undo() => inputField.UpdateValueWithoutNotify(oldValue);

        public bool NothingChanged() => value == oldValue;

        public override string ToString() => $"{inputField.name}: {oldValue} -> {value}";
    }
}