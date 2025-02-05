namespace Rogium.Systems.Input
{
    /// <summary>
    /// Fires events for the general shortcuts Action Map.
    /// </summary>
    public class InputProfileShortcuts : InputProfileBase
    {
        private RogiumInputActions.ShortcutsGeneralActions generalMap;
        private RogiumInputActions.ShortcutsSelectionMenuActions selectionMenuMap;
        private RogiumInputActions.ShortcutsCampaignSelectionActions campaignSelectionMap;
        private RogiumInputActions.ShortcutsDrawingEditorsActions drawingEditorsMap;
        private RogiumInputActions.ShortcutsSpriteEditorActions spriteMap;
        private RogiumInputActions.ShortcutsRoomEditorActions roomMap;
        private RogiumInputActions.ShortcutsCampaignEditorActions campaignEditorMap;

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
            generalMap = input.ShortcutsGeneral;
            selectionMenuMap = input.ShortcutsSelectionMenu;
            campaignSelectionMap = input.ShortcutsCampaignSelection;
            drawingEditorsMap = input.ShortcutsDrawingEditors;
            spriteMap = input.ShortcutsSpriteEditor;
            roomMap = input.ShortcutsRoomEditor;
            campaignEditorMap = input.ShortcutsCampaignEditor;
            
            undo = new InputButton(generalMap.Undo);
            redo = new InputButton(generalMap.Redo);
            save = new InputButton(generalMap.Save);
            newAsset = new InputButton(generalMap.New);
            edit = new InputButton(generalMap.Edit);
            editProperties = new InputButton(generalMap.EditProperties);
            delete = new InputButton(generalMap.Delete);
            
            selectionTool = new InputButton(drawingEditorsMap.SelectionTool);
            brushTool = new InputButton(drawingEditorsMap.BrushTool);
            eraserTool = new InputButton(drawingEditorsMap.EraserTool);
            fillTool = new InputButton(drawingEditorsMap.FillTool);
            pickerTool = new InputButton(drawingEditorsMap.PickerTool);
            clearCanvas = new InputButton(drawingEditorsMap.ClearCanvas);
            toggleGrid = new InputButton(drawingEditorsMap.ToggleGrid);
            
            tilesLayer = new InputButton(roomMap.TileLayer);
            decorLayer = new InputButton(roomMap.DecorLayer);
            objectsLayer = new InputButton(roomMap.ObjectLayer);
            enemiesLayer = new InputButton(roomMap.EnemyLayer);
            
            changePalette = new InputButton(spriteMap.ChangePalette);
            
            switchLeft = new InputButton(campaignSelectionMap.SwitchLeft);
            switchRight = new InputButton(campaignSelectionMap.SwitchRight);
            refreshCurrent = new InputButton(campaignSelectionMap.Refresh);
            refreshAll = new InputButton(campaignSelectionMap.RefreshAll);
            play = new InputButton(campaignSelectionMap.Play);
            
            showPalettes = new InputButton(selectionMenuMap.ShowPalettes);
            showSprites = new InputButton(selectionMenuMap.ShowSprites);
            showWeapons = new InputButton(selectionMenuMap.ShowWeapons);
            showProjectiles = new InputButton(selectionMenuMap.ShowProjectiles);
            showEnemies = new InputButton(selectionMenuMap.ShowEnemies);
            showRooms = new InputButton(selectionMenuMap.ShowRooms);
            showTiles = new InputButton(selectionMenuMap.ShowTiles);
            
            selectAll = new InputButton(campaignEditorMap.SelectAll);
            deselectAll = new InputButton(campaignEditorMap.DeselectAll);
            selectRandom = new InputButton(campaignEditorMap.SelectRandom);
        }

        protected override void WhenEnabled()
        {
            generalMap.Enable();
            selectionMenuMap.Enable();
            campaignSelectionMap.Enable();
            drawingEditorsMap.Enable();
            spriteMap.Enable();
            roomMap.Enable();
            campaignEditorMap.Enable();
            
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
            
            campaignEditorMap.Disable();
            roomMap.Disable();
            spriteMap.Disable();
            drawingEditorsMap.Disable();
            campaignSelectionMap.Disable();
            selectionMenuMap.Disable();
            generalMap.Disable();
        }

        public override bool IsMapEnabled { get => roomMap.enabled && campaignSelectionMap.enabled 
                                                                   && selectionMenuMap.enabled && generalMap.enabled 
                                                                   && drawingEditorsMap.enabled && spriteMap.enabled 
                                                                   && campaignEditorMap.enabled; }

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