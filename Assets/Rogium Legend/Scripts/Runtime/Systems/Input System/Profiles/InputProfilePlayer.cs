
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
        
        private readonly InputButton buttonStart;

        public InputProfilePlayer(RogiumInputActions input) : base(input)
        {
            map = input.Player;
            movement = new InputVector2(map.Movement);
            buttonMain = new InputButton(map.UseMain);
            buttonSub = new InputButton(map.UseSub);
            buttonDash = new InputButton(map.UseDash);
            buttonMainAlt = new InputButton(map.AlternativeMain);
            buttonSubAlt = new InputButton(map.AlternativeSub);
            buttonDashAlt = new InputButton(map.AlternativeDash);
            buttonStart = new InputButton(map.Start);
        }
        
        public override void Enable()
        {
            map.Enable();
            
            movement.Enable();
            buttonMain.Enable();
            buttonSub.Enable();
            buttonDash.Enable();
            buttonMainAlt.Enable();
            buttonSubAlt.Enable();
            buttonDashAlt.Enable();
            buttonStart.Enable();
        }

        public override void Disable()
        {
            movement.Disable();
            buttonMain.Disable();
            buttonSub.Disable();
            buttonDash.Disable();
            buttonMainAlt.Disable();
            buttonSubAlt.Disable();
            buttonDashAlt.Disable();
            buttonStart.Disable();
            
            map.Disable();
        }

        public InputVector2 Movement { get => movement; }
        public InputButton ButtonMain { get => buttonMain; }
        public InputButton ButtonSub { get => buttonSub; }
        public InputButton ButtonDash { get => buttonDash; }
        public InputButton ButtonMainAlt { get => buttonMainAlt; }
        public InputButton ButtonSubAlt { get => buttonSubAlt; }
        public InputButton ButtonDashAlt { get => buttonDashAlt; }
        
        public InputButton ButtonStart { get => buttonStart; }
    }
}