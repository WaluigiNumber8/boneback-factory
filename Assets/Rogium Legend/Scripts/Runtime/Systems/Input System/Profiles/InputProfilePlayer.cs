
namespace Rogium.Systems.Input
{
    /// <summary>
    /// Fires events based on input from the Player Action Map.
    /// </summary>
    public class InputProfilePlayer : InputProfileBase
    {
        private RogiumInputActions.PlayerActions map;

        private readonly InputVector2 movement;
        
        private readonly InputButton buttonMain;
        private readonly InputButton buttonSub;
        private readonly InputButton buttonDash;
        private readonly InputButton buttonMainAlt;
        private readonly InputButton buttonSubAlt;
        private readonly InputButton buttonDashAlt;

        public InputProfilePlayer(RogiumInputActions input) : base(input)
        {
            map = input.Player;
            movement = new InputVector2(map.Move);
            buttonMain = new InputButton(map.Main);
            buttonSub = new InputButton(map.Sub);
            buttonDash = new InputButton(map.Dash);
            buttonMainAlt = new InputButton(map.MainAlt);
            buttonSubAlt = new InputButton(map.SubAlt);
            buttonDashAlt = new InputButton(map.DashAlt);
        }
        
        protected override void WhenEnabled()
        {
            map.Enable();
            
            movement.Enable();
            buttonMain.Enable();
            buttonSub.Enable();
            buttonDash.Enable();
            buttonMainAlt.Enable();
            buttonSubAlt.Enable();
            buttonDashAlt.Enable();
        }

        protected override void WhenDisabled()
        {
            movement.Disable();
            buttonMain.Disable();
            buttonSub.Disable();
            buttonDash.Disable();
            buttonMainAlt.Disable();
            buttonSubAlt.Disable();
            buttonDashAlt.Disable();
            
            map.Disable();
        }

        public InputVector2 Movement { get => movement; }
        public InputButton ButtonMain { get => buttonMain; }
        public InputButton ButtonSub { get => buttonSub; }
        public InputButton ButtonDash { get => buttonDash; }
        public InputButton ButtonMainAlt { get => buttonMainAlt; }
        public InputButton ButtonSubAlt { get => buttonSubAlt; }
        public InputButton ButtonDashAlt { get => buttonDashAlt; }
        
    }
}