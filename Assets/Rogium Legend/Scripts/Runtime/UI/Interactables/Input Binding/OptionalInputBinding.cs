using System;
using UnityEngine.InputSystem;

namespace Rogium.UserInterface.Interactables
{
    public class OptionalInputBinding : IEquatable<OptionalInputBinding>
    {
        private InputBinding? binding;
        
        public OptionalInputBinding(InputBinding binding)
        {
            this.binding = binding;
        }
        
        public OptionalInputBinding(InputBinding? binding)
        {
            this.binding = binding;
        }
        
        public bool HasValue => binding.HasValue;

        public void SetID(Guid id)
        {
            if (!binding.HasValue) throw new InvalidOperationException("Cannot set ID on a null binding.");
            InputBinding inputBinding = binding.Value;
            inputBinding.id = id;
            binding = inputBinding;
        }

        public override string ToString() => binding?.effectivePath ?? string.Empty;

        #region Equals
        public bool Equals(OptionalInputBinding other) => Nullable.Equals(binding, other.binding);
        public override bool Equals(object obj) => obj is OptionalInputBinding other && Equals(other);
        public override int GetHashCode() => binding.GetHashCode();
        public static bool operator ==(OptionalInputBinding left, OptionalInputBinding right) => left.Equals(right);
        public static bool operator !=(OptionalInputBinding left, OptionalInputBinding right) => !left.Equals(right);
        #endregion
        
        public InputBinding Binding { get => binding.Value; }
        public string Path { get => binding?.effectivePath ?? string.Empty; }
        public string DisplayString { get => binding?.ToDisplayString() ?? string.Empty; }
        public Guid ID { get => binding?.id ?? Guid.Empty; }
    }
}