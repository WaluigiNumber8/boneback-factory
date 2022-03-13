namespace Rogium.Systems.Input
{
    public class InputProfileUI : InputProfileBase
    {
        private RogiumInputActions.UIActions map;

        private readonly InputVector2 navigate;
        private readonly InputVector2 pointerPosition;

        private readonly InputButton click;
        private readonly InputButton clickAlternative;

        public InputProfileUI()
        {
            map = input.UI;

            navigate = new InputVector2(map.Navigate);
            pointerPosition = new InputVector2(map.Point);
            click = new InputButton(map.Click);
            clickAlternative = new InputButton(map.RightClick);
        }
        
        public override void Enable()
        {
            map.Enable();
            
            navigate.Enable();
            pointerPosition.Enable();
            click.Enable();
            clickAlternative.Enable();
        }

        public override void Disable()
        {
            map.Disable();
            
            navigate.Disable();
            pointerPosition.Disable();
            click.Disable();
            clickAlternative.Disable();
        }
        
        public InputVector2 Navigate { get => navigate; }
        public InputVector2 PointerPosition { get => pointerPosition; }
        public InputButton Click { get => click; }
        public InputButton ClickAlternative { get => clickAlternative; }
    }
}