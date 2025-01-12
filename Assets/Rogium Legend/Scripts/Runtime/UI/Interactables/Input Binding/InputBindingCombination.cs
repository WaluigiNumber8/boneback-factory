using System;
using UnityEngine.InputSystem;

namespace Rogium.UserInterface.Interactables
{
    /// <summary>
    /// Contains a binding for an action made up of a button and 2 optional modifiers.
    /// </summary>
    public class InputBindingCombination : IEquatable<InputBindingCombination>
    {
        private readonly OptionalInputBinding modifier1;
        private readonly OptionalInputBinding modifier2;
        private InputBinding button;

        public InputBindingCombination()
        {
            modifier1 = new OptionalInputBinding(new InputBinding(""));
            modifier2 = new OptionalInputBinding(new InputBinding(""));
            button = new InputBinding("");
        }
        
        public InputBindingCombination(InputBinding? modifier1, InputBinding? modifier2, InputBinding button)
        {
            this.modifier1 = new OptionalInputBinding(modifier1 ?? new InputBinding(""));
            this.modifier2 = new OptionalInputBinding(modifier2 ?? new InputBinding(""));
            this.button = button;
        }

        #region Equals
        public override bool Equals(object obj) => obj is InputBindingCombination container && modifier1 == container.modifier1 && modifier2 == container.modifier2 && button == container.button;
        public bool Equals(InputBindingCombination other) => Equals((object)other);
        public override int GetHashCode() => HashCode.Combine(modifier1, modifier2, button);
        public static bool operator ==(InputBindingCombination left, InputBindingCombination right) => left is null && right is null || left is not null && right is not null && left.Equals(right);

        public static bool operator !=(InputBindingCombination left, InputBindingCombination right) => !(left == right);
        #endregion
        
        public void SetButtonID(Guid id) => button.id = id;

        public override string ToString() => DisplayString;

        public OptionalInputBinding Modifier1 { get => modifier1; }
        public OptionalInputBinding Modifier2 { get => modifier2; }
        public InputBinding Button { get => button; }
        public string DisplayString
        {
            get
            {
                string plus1 = (modifier1.HasValue && !string.IsNullOrEmpty(modifier1.Path)) ? "+" : "";
                string plus2 = (modifier2.HasValue && !string.IsNullOrEmpty(modifier2.Path)) ? "+" : "";
                return $"{modifier1.DisplayString}{plus1}{modifier2.DisplayString}{plus2}{button.ToDisplayString()}";
            }
        }

    }
}