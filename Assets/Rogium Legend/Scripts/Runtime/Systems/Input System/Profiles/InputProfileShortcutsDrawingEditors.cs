namespace Rogium.Systems.Input
{
    /// <summary>
    /// Fires events for the drawing editors shortcuts Action Map.
    /// </summary>
    public class InputProfileShortcutsDrawingEditors : InputProfileBase
    {
        private RogiumInputActions.ShortcutsDrawingEditorsActions map;

        private readonly InputButton selectTool;
        private readonly InputButton brushTool;
        private readonly InputButton eraserTool;
        private readonly InputButton fillTool;
        private readonly InputButton pickerTool;
        private readonly InputButton clearCanvas;
        private readonly InputButton toggleGrid;
        
        public InputProfileShortcutsDrawingEditors(RogiumInputActions input) : base(input)
        {
            map = input.ShortcutsDrawingEditors;
            selectTool = new InputButton(map.SelectTool);
            brushTool = new InputButton(map.BrushTool);
            eraserTool = new InputButton(map.EraserTool);
            fillTool = new InputButton(map.FillTool);
            pickerTool = new InputButton(map.PickerTool);
            clearCanvas = new InputButton(map.ClearCanvas);
            toggleGrid = new InputButton(map.ToggleGrid);
        }

        protected override void WhenEnabled()
        {
            map.Enable();
            
            selectTool.Enable();
            brushTool.Enable();
            eraserTool.Enable();
            fillTool.Enable();
            pickerTool.Enable();
            clearCanvas.Enable();
            toggleGrid.Enable();
        }

        protected override void WhenDisabled()
        {
            selectTool.Disable();
            brushTool.Disable();
            eraserTool.Disable();
            fillTool.Disable();
            pickerTool.Disable();
            clearCanvas.Disable();
            toggleGrid.Disable();
            
            map.Disable();
        }

        public override bool IsMapEnabled { get => map.enabled; }
        
        public InputButton SelectTool { get => selectTool; }
        public InputButton BrushTool { get => brushTool; }
        public InputButton EraserTool { get => eraserTool; }
        public InputButton FillTool { get => fillTool; }
        public InputButton PickerTool { get => pickerTool; }
        public InputButton ClearCanvas { get => clearCanvas; }
        public InputButton ToggleGrid { get => toggleGrid; }
    }
}