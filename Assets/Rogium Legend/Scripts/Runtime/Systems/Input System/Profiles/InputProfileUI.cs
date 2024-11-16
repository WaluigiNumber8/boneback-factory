namespace Rogium.Systems.Input
{
    public class InputProfileUI : InputProfileBase
    {
        private RogiumInputActions.UIActions map;

        private readonly InputVector2 navigate;
        private readonly InputVector2 pointerPosition;

        private readonly InputButton click;
        private readonly InputButton clickAlternative;
        private readonly InputButton menu;

        public InputProfileUI(RogiumInputActions input) : base(input)
        {
            map = input.UI;

            navigate = new InputVector2(map.Navigate);
            pointerPosition = new InputVector2(map.Point);
            click = new InputButton(map.Select);
            clickAlternative = new InputButton(map.ContextSelect);
            menu = new InputButton(map.Menu);
        }
        
        protected override void WhenEnabled()
        {
            map.Enable();
            
            navigate.Enable();
            pointerPosition.Enable();
            click.Enable();
            clickAlternative.Enable();
            menu.Enable();
        }

        protected override void WhenDisabled()
        {
            map.Disable();
            
            navigate.Disable();
            pointerPosition.Disable();
            click.Disable();
            clickAlternative.Disable();
            menu.Disable();
        }
        
        public InputVector2 Navigate { get => navigate; }
        public InputVector2 PointerPosition { get => pointerPosition; }
        public InputButton Click { get => click; }
        public InputButton ClickAlternative { get => clickAlternative; }
        public InputButton Menu { get => menu; }
    }
}