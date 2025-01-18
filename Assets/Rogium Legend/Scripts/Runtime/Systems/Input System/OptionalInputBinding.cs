using System;
using UnityEngine.InputSystem;

namespace Rogium.Systems.Input
{
    public struct OptionalInputBinding : IEquatable<OptionalInputBinding>
    {
        private InputBinding binding;
        
        public OptionalInputBinding(InputBinding binding)
        {
            this.binding = binding;
        }
        
        public bool HasValue => string.IsNullOrEmpty(binding.effectivePath) == false;

        public void SetID(Guid id)
        {
            InputBinding inputBinding = binding;
            inputBinding.id = id;
            binding = inputBinding;
        }

        public override string ToString() => binding.effectivePath;

        #region Equals
        public bool Equals(OptionalInputBinding other) => binding.Equals(other.binding);
        public override bool Equals(object obj) => obj is OptionalInputBinding other && Equals(other);
        public override int GetHashCode() => binding.GetHashCode();
        public static bool operator ==(OptionalInputBinding left, OptionalInputBinding right) => left.Equals(right);
        public static bool operator !=(OptionalInputBinding left, OptionalInputBinding right) => !left.Equals(right);
        #endregion
        
        public InputBinding Binding { get => binding; }
        public string Path { get => binding.effectivePath; }
        public string DisplayString { get => (string.IsNullOrEmpty(Path)) ? "" : binding.ToDisplayString(); }
        public Guid ID { get => binding.id; }
    }
}