using RedRats.Core;
using Rogium.UserInterface.Interactables.Properties;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// An action that updates a slider.
    /// </summary>
    public class UpdateSliderAction : IAction
    {
        private readonly InteractablePropertySlider slider;
        private readonly float value;
        private readonly float lastValue;

        public UpdateSliderAction(InteractablePropertySlider slider, float value, float lastValue)
        {
            this.slider = slider;
            this.value = value;
            this.lastValue = lastValue;
        }

        public void Execute() => slider.UpdateValueWithoutNotify(value);

        public void Undo() => slider.UpdateValueWithoutNotify(lastValue);

        public bool NothingChanged() => value.IsSameAs(lastValue);
        
        public object AffectedConstruct => slider;
        public object Value { get => value; }
        public object LastValue { get => lastValue; }

        public override string ToString() => $"{slider.name}: {lastValue} -> {value}";
    }
}