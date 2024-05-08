using TMPro;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// An action that updates an input field.
    /// </summary>
    public class UpdateInputFieldAction : IAction
    {
        private readonly TMP_InputField inputField;
        private readonly string value;
        private readonly string oldValue;

        public UpdateInputFieldAction(TMP_InputField inputField, string value, string oldValue)
        {
            this.inputField = inputField;
            this.value = value;
            this.oldValue = oldValue;
        }

        public void Execute()
        {
            inputField.text = value;
        }

        public void Undo()
        {
            inputField.text = oldValue;
        }
        
        public bool NothingChanged() => value == oldValue;

        public override string ToString() => $"UpdateInputFieldAction {oldValue} -> {value}";
    }
}