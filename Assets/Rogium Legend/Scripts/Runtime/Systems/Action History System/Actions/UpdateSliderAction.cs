using System;
using RedRats.Core;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// An action that updates a slider.
    /// </summary>
    public class UpdateSliderAction : ActionBase<float>
    {
        private readonly IPSlider slider;
        private readonly float value;
        private readonly float lastValue;

        public UpdateSliderAction(IPSlider slider, float value, float lastValue, Action<float> fallback) : base(fallback)
        {
            this.slider = slider;
            this.value = value;
            this.lastValue = lastValue;
        }

        protected override void ExecuteSelf() => slider.UpdateValueWithoutNotify(value);

        protected override void UndoSelf() => slider.UpdateValueWithoutNotify(lastValue);

        public override bool NothingChanged() => value.IsSameAs(lastValue);
        
        public override object AffectedConstruct
        {
            get
            {
                try { return slider?.gameObject; }
                catch (MissingReferenceException) { return null; }
            }
        }

        public override float Value { get => value; }
        public override float LastValue { get => lastValue; }

        public override string ToString() => $"{slider.name}: {lastValue} -> {value}";
    }
}