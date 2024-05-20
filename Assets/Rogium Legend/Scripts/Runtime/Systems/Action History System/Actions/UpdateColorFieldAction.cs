using System;
using Rogium.UserInterface.Interactables;
using UnityEngine;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// An action that updates a color field.
    /// </summary>
    public class UpdateColorFieldAction : ActionBase<Color>
    {
        private readonly ColorField colorField;
        private readonly Color value;
        private readonly Color lastValue;
        
        public UpdateColorFieldAction(ColorField colorField, Color value, Color lastValue, Action<Color> fallback) : base(fallback)
        {
            this.colorField = colorField;
            this.value = value;
            this.lastValue = lastValue;
        }
        
        protected override void ExecuteSelf() => colorField.UpdateValue(value);

        protected override void UndoSelf() => colorField.UpdateValue(lastValue);

        public override bool NothingChanged() => value == lastValue;
        
        public override object AffectedConstruct
        {
            get
            {
                try { return colorField?.gameObject; }
                catch (MissingReferenceException) { return null; }
            }
        }

        public override Color Value { get => value; }
        public override Color LastValue { get => lastValue; }
    }
}