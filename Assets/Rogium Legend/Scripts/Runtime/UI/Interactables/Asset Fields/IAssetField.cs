using System;
using Rogium.Editors.Core;

namespace Rogium.UserInterface.Interactables
{
    /// <summary>
    /// Represents a field that can store assets.
    /// </summary>
    /// <typeparam name="T">Any type</typeparam>
    public interface IAssetField<T>
    {
        public event Action<T> OnValueChanged;
        public event Action OnValueEmptied;
        public T Value { get; }
    }
}