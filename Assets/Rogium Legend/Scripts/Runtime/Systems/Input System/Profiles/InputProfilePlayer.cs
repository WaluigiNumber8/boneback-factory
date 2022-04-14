
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
        private readonly InputButton buttonMainAlternative;
        private readonly InputButton buttonSubAlternative;
        private readonly InputButton buttonDashAlternative;
        
        private readonly InputButton buttonStart;

        public InputProfilePlayer()
        {
            map = input.Player;
            movement = new InputVector2(map.Movement);
            buttonMain = new InputButton(map.UseMain);
            buttonSub = new InputButton(map.UseSub);
            buttonDash = new InputButton(map.UseDash);
            buttonMainAlternative = new InputButton(map.AlternativeMain);
            buttonSubAlternative = new InputButton(map.AlternativeSub);
            buttonDashAlternative = new InputButton(map.AlternativeDash);
            buttonStart = new InputButton(map.Start);
        }
        
        public override void Enable()
        {
            map.Enable();
            
            movement.Enable();
            buttonMain.Enable();
            buttonSub.Enable();
            buttonDash.Enable();
            buttonMainAlternative.Enable();
            buttonSubAlternative.Enable();
            buttonDashAlternative.Enable();
            buttonStart.Enable();
        }

        public override void Disable()
        {
            movement.Disable();
            buttonMain.Disable();
            buttonSub.Disable();
            buttonDash.Disable();
            buttonMainAlternative.Disable();
            buttonSubAlternative.Disable();
            buttonDashAlternative.Disable();
            buttonStart.Disable();
            
            map.Disable();
        }

        public InputVector2 Movement { get => movement; }
        public InputButton ButtonMain { get => buttonMain; }
        public InputButton ButtonSub { get => buttonSub; }
        public InputButton ButtonDash { get => buttonDash; }
        public InputButton ButtonMainAlternative { get => buttonMainAlternative; }
        public InputButton ButtonSubAlternative { get => buttonSubAlternative; }
        public InputButton ButtonDashAlternative { get => buttonDashAlternative; }
        
        public InputButton ButtonStart { get => buttonStart; }
    }
}