using System;
using Rogium.UserInterface.Interactables;

namespace Rogium.Systems.ActionHistory
{
    public class UpdateInputBindingAction : ActionBase<string>
    {
        private readonly InputBindingReader reader;
        private readonly string value;
        private readonly string lastValue;
        
        public UpdateInputBindingAction(InputBindingReader reader, string value, string lastValue,  Action<string> fallback) : base(fallback)
        {
            this.reader = reader;
            this.value = value;
            this.lastValue = lastValue;
        }
        
        protected override void ExecuteSelf() => reader.Rebind(value);

        protected override void UndoSelf() => reader.Rebind(lastValue);
        
        public override bool NothingChanged() => value == lastValue;
        public override string ToString() => $"{reader.name}: {lastValue} -> {value}";
        public override object AffectedConstruct { get => reader; }
        public override string Value { get => value; }
        public override string LastValue { get => lastValue; }
    }
}