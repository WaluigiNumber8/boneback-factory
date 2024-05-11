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
        private readonly float oldValue;

        public UpdateSliderAction(InteractablePropertySlider slider, float value, float oldValue)
        {
            this.slider = slider;
            this.value = value;
            this.oldValue = oldValue;
        }

        public void Execute() => slider.UpdateValueWithoutNotify(value);

        public void Undo() => slider.UpdateValueWithoutNotify(oldValue);

        public bool NothingChanged() => value.IsSameAs(oldValue);

        public override string ToString() => $"{slider.name}: {oldValue} -> {value}";
    }
}