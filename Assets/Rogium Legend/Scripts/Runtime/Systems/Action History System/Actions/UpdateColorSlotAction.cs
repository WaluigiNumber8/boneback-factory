using Rogium.Editors.Palettes;
using UnityEngine;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// An action that updates a <see cref="ColorSlot"/>.
    /// </summary>
    public class UpdateColorSlotAction : IAction
    {
        private readonly ColorSlot slot;
        private readonly Color value;
        private readonly Color lastValue;

        public UpdateColorSlotAction(ColorSlot slot, Color value, Color lastValue)
        {
            this.slot = slot;
            this.value = value;
            this.lastValue = lastValue;
        }

        public void Execute() => slot.UpdateColor(value);

        public void Undo() => slot.UpdateColor(lastValue);

        public bool NothingChanged() => value == lastValue;

        public object AffectedConstruct { get => slot; }
        public object Value { get => value; }
        public object LastValue { get => lastValue; }
    }
}