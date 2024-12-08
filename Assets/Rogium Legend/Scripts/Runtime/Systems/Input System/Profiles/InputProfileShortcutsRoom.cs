namespace Rogium.Systems.Input
{
    /// <summary>
    /// Fires events for the room editor shortcuts Action Map.
    /// </summary>
    public class InputProfileShortcutsRoom : InputProfileBase
    {
        private RogiumInputActions.ShortcutsRoomActions map;
        
        private readonly InputButton showTiles;
        private readonly InputButton showDecors;
        private readonly InputButton showObjects;
        private readonly InputButton showEnemies;
        
        public InputProfileShortcutsRoom(RogiumInputActions input) : base(input)
        {
            map = input.ShortcutsRoom;
            showTiles = new InputButton(map.ShowTiles);
            showDecors = new InputButton(map.ShowDecors);
            showObjects = new InputButton(map.ShowObjects);
            showEnemies = new InputButton(map.ShowEnemies);
        }

        protected override void WhenEnabled()
        {
            map.Enable();
            
            showTiles.Enable();
            showDecors.Enable();
            showObjects.Enable();
            showEnemies.Enable();
        }

        protected override void WhenDisabled()
        {
            showTiles.Disable();
            showDecors.Disable();
            showObjects.Disable();
            showEnemies.Disable();
            
            map.Disable();
        }

        public override bool IsMapEnabled { get => map.enabled; }
        
        public InputButton ShowTiles { get => showTiles; }
        public InputButton ShowDecors { get => showDecors; }
        public InputButton ShowObjects { get => showObjects; }
        public InputButton ShowEnemies { get => showEnemies; }
    }
}