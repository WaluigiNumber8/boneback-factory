namespace Rogium.Options.Core
{
    [System.Serializable]
    public struct ShortcutBindingData
    {
        public string Undo;
        public string Redo;
        public string Save;
        
        public string New;
        public string Edit;
        public string EditProperties;
        public string Delete;
        
        public string ShowPalettes;
        public string ShowSprites;
        public string ShowWeapons;
        public string ShowProjectiles;
        public string ShowEnemies;
        public string ShowRooms;
        public string ShowTiles;
        
        public string SwitchLeft;
        public string SwitchRight;
        public string Refresh;
        public string RefreshAll;
        public string Play;
        
        public string SelectAll;
        public string DeselectAll;
        public string SelectRandom;
        
        public string SelectionTool;
        public string BrushTool;
        public string EraserTool;
        public string FillTool;
        public string PickerTool;
        public string ClearCanvas;
        public string ToggleGrid;
        
        public string TileLayer;
        public string DecorLayer;
        public string ObjectLayer;
        public string EnemyLayer;
        
        public string ChangePalette;
    }
}