namespace Rogium.Systems.Input
{
    /// <summary>
    /// Fires events for the general shortcuts Action Map.
    /// </summary>
    public class InputProfileShortcuts : InputProfileBase
    {
        private RogiumInputActions.ShortcutsActions map;

        private readonly InputButton undo;
        private readonly InputButton redo;
        private readonly InputButton save;
        private readonly InputButton cancel;
        
        private readonly InputButton selectionTool;
        private readonly InputButton brushTool;
        private readonly InputButton eraserTool;
        private readonly InputButton fillTool;
        private readonly InputButton pickerTool;
        private readonly InputButton clearCanvas;
        private readonly InputButton toggleGrid;

        private readonly InputButton tilesLayer;
        private readonly InputButton decorLayer;
        private readonly InputButton objectsLayer;
        private readonly InputButton enemiesLayer;
        
        
        public InputProfileShortcuts(RogiumInputActions input) : base(input)
        {
            map = input.Shortcuts;
            undo = new InputButton(map.Undo);
            redo = new InputButton(map.Redo);
            save = new InputButton(map.Save);
            cancel = new InputButton(map.Cancel);
            selectionTool = new InputButton(map.SelectionTool);
            brushTool = new InputButton(map.BrushTool);
            eraserTool = new InputButton(map.EraserTool);
            fillTool = new InputButton(map.FillTool);
            pickerTool = new InputButton(map.PickerTool);
            clearCanvas = new InputButton(map.ClearCanvas);
            toggleGrid = new InputButton(map.ToggleGrid);
            tilesLayer = new InputButton(map.TilesLayer);
            decorLayer = new InputButton(map.DecorLayer);
            objectsLayer = new InputButton(map.ObjectsLayer);
            enemiesLayer = new InputButton(map.EnemiesLayer);
        }

        protected override void WhenEnabled()
        {
            map.Enable();
            
            undo.Enable();
            redo.Enable();
            save.Enable();
            cancel.Enable();
            selectionTool.Enable();
            brushTool.Enable();
            eraserTool.Enable();
            fillTool.Enable();
            pickerTool.Enable();
            clearCanvas.Enable();
            toggleGrid.Enable();
            tilesLayer.Enable();
            decorLayer.Enable();
            objectsLayer.Enable();
            enemiesLayer.Enable();
        }

        protected override void WhenDisabled()
        {
            map.Disable();
            
            undo.Disable();
            redo.Disable();
            save.Disable();
            cancel.Disable();
            selectionTool.Disable();
            brushTool.Disable();
            eraserTool.Disable();
            fillTool.Disable();
            pickerTool.Disable();
            clearCanvas.Disable();
            toggleGrid.Disable();
            tilesLayer.Disable();
            decorLayer.Disable();
            objectsLayer.Disable();
            enemiesLayer.Disable();
        }

        public override bool IsMapEnabled { get => map.enabled; }

        public InputButton Undo { get => undo; }
        public InputButton Redo { get => redo; }
        public InputButton Save { get => save; }
        public InputButton Cancel { get => cancel; }
        public InputButton SelectionTool { get => selectionTool; }
        public InputButton BrushTool { get => brushTool; }
        public InputButton EraserTool { get => eraserTool; }
        public InputButton FillTool { get => fillTool; }
        public InputButton PickerTool { get => pickerTool; }
        public InputButton ClearCanvas { get => clearCanvas; }
        public InputButton ToggleGrid { get => toggleGrid; }
        public InputButton TilesLayer { get => tilesLayer; }
        public InputButton DecorLayer { get => decorLayer; }
        public InputButton ObjectsLayer { get => objectsLayer; }
        public InputButton EnemiesLayer { get => enemiesLayer; }
    }
}