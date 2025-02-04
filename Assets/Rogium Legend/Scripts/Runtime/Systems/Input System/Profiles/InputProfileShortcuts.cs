namespace Rogium.Systems.Input
{
    /// <summary>
    /// Fires events for the general shortcuts Action Map.
    /// </summary>
    public class InputProfileShortcuts : InputProfileBase
    {
        private RogiumInputActions.ShortcutsActions map;

        private readonly InputButton undo, redo, save;
        private readonly InputButton newAsset, edit, editProperties, delete;
        
        private readonly InputButton selectionTool, brushTool, eraserTool, fillTool, pickerTool;
        private readonly InputButton clearCanvas, toggleGrid;
        private readonly InputButton tilesLayer,  decorLayer, objectsLayer, enemiesLayer;
        private readonly InputButton changePalette;
        
        private readonly InputButton switchLeft, switchRight;
        private readonly InputButton refreshCurrent, refreshAll;
        private readonly InputButton play;
        
        private readonly InputButton showPalettes, showSprites, showWeapons, showProjectiles, showEnemies, showRooms, showTiles;
        
        private readonly InputButton selectAll, deselectAll, selectRandom;
        
        public InputProfileShortcuts(RogiumInputActions input) : base(input)
        {
            map = input.Shortcuts;
            
            undo = new InputButton(map.Undo);
            redo = new InputButton(map.Redo);
            save = new InputButton(map.Save);
            newAsset = new InputButton(map.New);
            edit = new InputButton(map.Edit);
            editProperties = new InputButton(map.EditProperties);
            delete = new InputButton(map.Delete);
            
            selectionTool = new InputButton(map.SelectTool);
            brushTool = new InputButton(map.BrushTool);
            eraserTool = new InputButton(map.EraserTool);
            fillTool = new InputButton(map.FillTool);
            pickerTool = new InputButton(map.PickerTool);
            clearCanvas = new InputButton(map.ClearCanvas);
            toggleGrid = new InputButton(map.ToggleGrid);
            
            tilesLayer = new InputButton(map.TileLayer);
            decorLayer = new InputButton(map.DecorLayer);
            objectsLayer = new InputButton(map.ObjectLayer);
            enemiesLayer = new InputButton(map.EnemyLayer);
            
            changePalette = new InputButton(map.ChangePalette);
            
            switchLeft = new InputButton(map.SwitchLeft);
            switchRight = new InputButton(map.SwitchRight);
            refreshCurrent = new InputButton(map.Refresh);
            refreshAll = new InputButton(map.RefreshAll);
            play = new InputButton(map.Play);
            
            showPalettes = new InputButton(map.ShowPalettes);
            showSprites = new InputButton(map.ShowSprites);
            showWeapons = new InputButton(map.ShowWeapons);
            showProjectiles = new InputButton(map.ShowProjectiles);
            showEnemies = new InputButton(map.ShowEnemies);
            showRooms = new InputButton(map.ShowRooms);
            showTiles = new InputButton(map.ShowTiles);
            
            selectAll = new InputButton(map.SelectAll);
            deselectAll = new InputButton(map.DeselectAll);
            selectRandom = new InputButton(map.SelectRandom);
        }

        protected override void WhenEnabled()
        {
            map.Enable();
            
            undo.Enable();
            redo.Enable();
            save.Enable();
            newAsset.Enable();
            edit.Enable();
            editProperties.Enable();    
            delete.Enable();
            
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
            
            changePalette.Enable();
            
            switchLeft.Enable();
            switchRight.Enable();
            refreshCurrent.Enable();
            refreshAll.Enable();
            play.Enable();
            
            showPalettes.Enable();
            showSprites.Enable();
            showWeapons.Enable();
            showProjectiles.Enable();
            showEnemies.Enable();
            showRooms.Enable();
            showTiles.Enable();
            
            selectAll.Enable();
            deselectAll.Enable();
            selectRandom.Enable();
        }

        protected override void WhenDisabled()
        {
            undo.Disable();
            redo.Disable();
            save.Disable();
            newAsset.Disable();
            edit.Disable();
            editProperties.Disable();
            delete.Disable();
            
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
            
            changePalette.Disable();
            
            switchLeft.Disable();
            switchRight.Disable();
            refreshCurrent.Disable();
            refreshAll.Disable();
            play.Disable();
            
            showPalettes.Disable();
            showSprites.Disable();
            showWeapons.Disable();
            showProjectiles.Disable();
            showEnemies.Disable();
            showRooms.Disable();
            showTiles.Disable();
            
            selectAll.Disable();
            deselectAll.Disable();
            selectRandom.Disable();
            
            map.Disable();
        }

        public override bool IsMapEnabled { get => map.enabled; }

        public InputButton Undo { get => undo; }
        public InputButton Redo { get => redo; }
        public InputButton Save { get => save; }
        public InputButton NewAsset { get => newAsset; }
        public InputButton Edit { get => edit; }
        public InputButton EditProperties { get => editProperties; }
        public InputButton Delete { get => delete; }
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
        public InputButton ChangePalette { get => changePalette; }
        public InputButton SwitchLeft { get => switchLeft; }
        public InputButton SwitchRight { get => switchRight; }
        public InputButton RefreshCurrent { get => refreshCurrent; }
        public InputButton RefreshAll { get => refreshAll; }
        public InputButton Play { get => play; }
        public InputButton ShowPalettes { get => showPalettes; }
        public InputButton ShowSprites { get => showSprites; }
        public InputButton ShowWeapons { get => showWeapons; }
        public InputButton ShowProjectiles { get => showProjectiles; }
        public InputButton ShowEnemies { get => showEnemies; }
        public InputButton ShowRooms { get => showRooms; }
        public InputButton ShowTiles { get => showTiles; }
        public InputButton SelectAll { get => selectAll; }
        public InputButton DeselectAll { get => deselectAll; }
        public InputButton SelectRandom { get => selectRandom; }
    }
}