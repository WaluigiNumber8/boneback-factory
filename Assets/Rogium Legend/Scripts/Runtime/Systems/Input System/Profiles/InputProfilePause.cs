namespace Rogium.Systems.Input
{
    public class InputProfilePause : InputProfileBase
    {
        private RogiumInputActions.PauseGameActions map;
        
        private readonly InputButton pause;
        
        public InputProfilePause(RogiumInputActions input) : base(input)
        {
            map = input.PauseGame;
            pause = new InputButton(map.Pause);
        }

        protected override void WhenEnabled()
        {
            map.Enable();
            pause.Enable();
        }

        protected override void WhenDisabled()
        {
            pause.Disable();
            map.Disable();
        }
        
        public InputButton Pause { get => pause; }
    }
}