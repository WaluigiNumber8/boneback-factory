namespace Rogium.Systems.Input
{
    public class InputProfileUI : InputProfileBase
    {
        private RogiumInputActions.UIActions map;

        private readonly InputVector2 navigate;
        private readonly InputVector2 pointerPosition;

        private readonly InputButton select;
        private readonly InputButton cancel;
        private readonly InputButton contextSelect;
        private readonly InputButton showTooltip;

        public InputProfileUI(RogiumInputActions input) : base(input)
        {
            map = input.UI;

            navigate = new InputVector2(map.Navigate);
            pointerPosition = new InputVector2(map.Point);
            select = new InputButton(map.Select);
            cancel = new InputButton(map.Cancel);
            contextSelect = new InputButton(map.ContextSelect);
            showTooltip = new InputButton(map.ShowTooltip);
        }
        
        protected override void WhenEnabled()
        {
            map.Enable();
            
            navigate.Enable();
            pointerPosition.Enable();
            select.Enable();
            cancel.Enable();
            contextSelect.Enable();
            showTooltip.Enable();
        }

        protected override void WhenDisabled()
        {
            map.Disable();
            
            navigate.Disable();
            pointerPosition.Disable();
            select.Disable();
            cancel.Disable();
            contextSelect.Disable();
            showTooltip.Disable();
        }
        
        public InputVector2 Navigate { get => navigate; }
        public InputVector2 PointerPosition { get => pointerPosition; }
        public InputButton Select { get => select; }
        public InputButton Cancel { get => cancel; }
        public InputButton ContextSelect { get => contextSelect; }
        public InputButton ShowTooltip { get => showTooltip; }
    }
}