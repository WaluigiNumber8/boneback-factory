namespace Rogium.Systems.Input
{
    /// <summary>
    /// Fires events for the general shortcuts Action Map.
    /// </summary>
    public class InputProfileShortcutsGeneral : InputProfileBase
    {
        private RogiumInputActions.ShortcutsGeneralActions map;

        private readonly InputButton undo;
        private readonly InputButton redo;
        private readonly InputButton save;
        private readonly InputButton cancel;
        
        public InputProfileShortcutsGeneral(RogiumInputActions input) : base(input)
        {
            map = input.ShortcutsGeneral;
            undo = new InputButton(map.Undo);
            redo = new InputButton(map.Redo);
            save = new InputButton(map.Save);
            cancel = new InputButton(map.Cancel);
        }

        protected override void WhenEnabled()
        {
            map.Enable();
            
            undo.Enable();
            redo.Enable();
            save.Enable();
            cancel.Enable();
        }

        protected override void WhenDisabled()
        {
            undo.Disable();
            redo.Disable();
            save.Disable();
            cancel.Disable();
            
            map.Disable();
        }

        public override bool IsMapEnabled { get => map.enabled; }

        public InputButton Undo { get => undo; }
        public InputButton Redo { get => redo; }
        public InputButton Save { get => save; }
        public InputButton Cancel { get => cancel; }
    }
}