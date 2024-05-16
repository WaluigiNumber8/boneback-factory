using Rogium.UserInterface.Interactables.Properties;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// An action that updates a toggle.
    /// </summary>
    public class UpdateToggleAction : IAction
    {
        private readonly InteractablePropertyToggle toggle;
        private readonly bool value;
        
        public UpdateToggleAction(InteractablePropertyToggle toggle, bool value)
        {
            this.toggle = toggle;
            this.value = value;
        }
        
        public void Execute() => toggle.UpdateValueWithoutNotify(value);

        public void Undo() => toggle.UpdateValueWithoutNotify(!value);

        public bool NothingChanged() => false;
        
        public object AffectedConstruct => toggle;
        public object Value { get => value; }
        public object LastValue { get => !value; }

        public override string ToString() => $"{toggle.name}: {!value} -> {value}";
    }
}