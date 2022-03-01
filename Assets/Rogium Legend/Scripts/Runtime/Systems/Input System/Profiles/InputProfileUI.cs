namespace Rogium.Systems.Input
{
    public class InputProfileUI : InputProfileBase
    {
        private RogiumInputActions.UIActions map;

        private readonly InputVector2 navigate;
        private readonly InputVector2 pointerPosition;

        private readonly InputButton click;

        public InputProfileUI()
        {
            map = input.UI;

            navigate = new InputVector2(map.Navigate);
            pointerPosition = new InputVector2(map.Point);
            click = new InputButton(map.Click);
        }
        
        public override void Enable()
        {
            map.Enable();
            
            navigate.Enable();
            pointerPosition.Enable();
            click.Enable();
        }

        public override void Disable()
        {
            map.Disable();
            
            navigate.Disable();
            pointerPosition.Disable();
            click.Disable();
        }
        
        public InputVector2 Navigate { get => navigate; }
        public InputVector2 PointerPosition { get => pointerPosition; }
        public InputButton Click { get => click; }
    }
}