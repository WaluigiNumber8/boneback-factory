using RedRats.Safety;
using Rogium.Options.Core;
using static Rogium.Systems.Input.InputSystemUtils;

namespace Rogium.Systems.Input
{
    /// <summary>
    /// Converts a <see cref="RogiumInputActions"/> to an <see cref="ShortcutBindingsAsset"/>.
    /// </summary>
    public static class ShortcutToAssetConverter
    {
        private static readonly InputSystem input = InputSystem.Instance;

        /// <summary>
        /// Builds a <see cref="ShortcutBindingsAsset"/> from the current <see cref="RogiumInputActions"/> and returns it.
        /// </summary>
        public static ShortcutBindingsAsset Get()
        {
            ShortcutBindingData keyboard = new();
            ShortcutBindingData keyboardAlt = new();
            ShortcutBindingData gamepad = new();
            ShortcutBindingData gamepadAlt = new();
            
            keyboard.Undo = GetPath(input.Shortcuts.Undo.Action, InputDeviceType.Keyboard);
            keyboardAlt.Undo = GetPath(input.Shortcuts.Undo.Action, InputDeviceType.Keyboard, true);
            gamepad.Undo = GetPath(input.Shortcuts.Undo.Action, InputDeviceType.Gamepad);
            gamepadAlt.Undo = GetPath(input.Shortcuts.Undo.Action, InputDeviceType.Gamepad, true);
            keyboard.Redo = GetPath(input.Shortcuts.Redo.Action, InputDeviceType.Keyboard);
            keyboardAlt.Redo = GetPath(input.Shortcuts.Redo.Action, InputDeviceType.Keyboard, true);
            gamepad.Redo = GetPath(input.Shortcuts.Redo.Action, InputDeviceType.Gamepad);
            gamepadAlt.Redo = GetPath(input.Shortcuts.Redo.Action, InputDeviceType.Gamepad, true);
            keyboard.Save = GetPath(input.Shortcuts.Save.Action, InputDeviceType.Keyboard);
            keyboardAlt.Save = GetPath(input.Shortcuts.Save.Action, InputDeviceType.Keyboard, true);
            gamepad.Save = GetPath(input.Shortcuts.Save.Action, InputDeviceType.Gamepad);
            gamepadAlt.Save = GetPath(input.Shortcuts.Save.Action, InputDeviceType.Gamepad, true);
            
            keyboard.New = GetPath(input.Shortcuts.NewAsset.Action, InputDeviceType.Keyboard);
            keyboardAlt.New = GetPath(input.Shortcuts.NewAsset.Action, InputDeviceType.Keyboard, true);
            gamepad.New = GetPath(input.Shortcuts.NewAsset.Action, InputDeviceType.Gamepad);
            gamepadAlt.New = GetPath(input.Shortcuts.NewAsset.Action, InputDeviceType.Gamepad, true);
            keyboard.Edit = GetPath(input.Shortcuts.Edit.Action, InputDeviceType.Keyboard);
            keyboardAlt.Edit = GetPath(input.Shortcuts.Edit.Action, InputDeviceType.Keyboard, true);
            gamepad.Edit = GetPath(input.Shortcuts.Edit.Action, InputDeviceType.Gamepad);
            gamepadAlt.Edit = GetPath(input.Shortcuts.Edit.Action, InputDeviceType.Gamepad, true);
            keyboard.EditProperties = GetPath(input.Shortcuts.EditProperties.Action, InputDeviceType.Keyboard);
            keyboardAlt.EditProperties = GetPath(input.Shortcuts.EditProperties.Action, InputDeviceType.Keyboard, true);
            gamepad.EditProperties = GetPath(input.Shortcuts.EditProperties.Action, InputDeviceType.Gamepad);
            gamepadAlt.EditProperties = GetPath(input.Shortcuts.EditProperties.Action, InputDeviceType.Gamepad, true);
            keyboard.Delete = GetPath(input.Shortcuts.Delete.Action, InputDeviceType.Keyboard);
            keyboardAlt.Delete = GetPath(input.Shortcuts.Delete.Action, InputDeviceType.Keyboard, true);
            gamepad.Delete = GetPath(input.Shortcuts.Delete.Action, InputDeviceType.Gamepad);
            gamepadAlt.Delete = GetPath(input.Shortcuts.Delete.Action, InputDeviceType.Gamepad, true);
            
            keyboard.ShowPalettes = GetPath(input.Shortcuts.ShowPalettes.Action, InputDeviceType.Keyboard);
            keyboardAlt.ShowPalettes = GetPath(input.Shortcuts.ShowPalettes.Action, InputDeviceType.Keyboard, true);
            gamepad.ShowPalettes = GetPath(input.Shortcuts.ShowPalettes.Action, InputDeviceType.Gamepad);
            gamepadAlt.ShowPalettes = GetPath(input.Shortcuts.ShowPalettes.Action, InputDeviceType.Gamepad, true);
            keyboard.ShowSprites = GetPath(input.Shortcuts.ShowSprites.Action, InputDeviceType.Keyboard);
            keyboardAlt.ShowSprites = GetPath(input.Shortcuts.ShowSprites.Action, InputDeviceType.Keyboard, true);
            gamepad.ShowSprites = GetPath(input.Shortcuts.ShowSprites.Action, InputDeviceType.Gamepad);
            gamepadAlt.ShowSprites = GetPath(input.Shortcuts.ShowSprites.Action, InputDeviceType.Gamepad, true);
            keyboard.ShowWeapons = GetPath(input.Shortcuts.ShowWeapons.Action, InputDeviceType.Keyboard);
            keyboardAlt.ShowWeapons = GetPath(input.Shortcuts.ShowWeapons.Action, InputDeviceType.Keyboard, true);
            gamepad.ShowWeapons = GetPath(input.Shortcuts.ShowWeapons.Action, InputDeviceType.Gamepad);
            gamepadAlt.ShowWeapons = GetPath(input.Shortcuts.ShowWeapons.Action, InputDeviceType.Gamepad, true);
            keyboard.ShowProjectiles = GetPath(input.Shortcuts.ShowProjectiles.Action, InputDeviceType.Keyboard);
            keyboardAlt.ShowProjectiles = GetPath(input.Shortcuts.ShowProjectiles.Action, InputDeviceType.Keyboard, true);
            gamepad.ShowProjectiles = GetPath(input.Shortcuts.ShowProjectiles.Action, InputDeviceType.Gamepad);
            gamepadAlt.ShowProjectiles = GetPath(input.Shortcuts.ShowProjectiles.Action, InputDeviceType.Gamepad, true);
            keyboard.ShowEnemies = GetPath(input.Shortcuts.ShowEnemies.Action, InputDeviceType.Keyboard);
            keyboardAlt.ShowEnemies = GetPath(input.Shortcuts.ShowEnemies.Action, InputDeviceType.Keyboard, true);
            gamepad.ShowEnemies = GetPath(input.Shortcuts.ShowEnemies.Action, InputDeviceType.Gamepad);
            gamepadAlt.ShowEnemies = GetPath(input.Shortcuts.ShowEnemies.Action, InputDeviceType.Gamepad, true);
            keyboard.ShowRooms = GetPath(input.Shortcuts.ShowRooms.Action, InputDeviceType.Keyboard);
            keyboardAlt.ShowRooms = GetPath(input.Shortcuts.ShowRooms.Action, InputDeviceType.Keyboard, true);
            gamepad.ShowRooms = GetPath(input.Shortcuts.ShowRooms.Action, InputDeviceType.Gamepad);
            gamepadAlt.ShowRooms = GetPath(input.Shortcuts.ShowRooms.Action, InputDeviceType.Gamepad, true);
            keyboard.ShowTiles = GetPath(input.Shortcuts.ShowTiles.Action, InputDeviceType.Keyboard);
            keyboardAlt.ShowTiles = GetPath(input.Shortcuts.ShowTiles.Action, InputDeviceType.Keyboard, true);
            gamepad.ShowTiles = GetPath(input.Shortcuts.ShowTiles.Action, InputDeviceType.Gamepad);
            gamepadAlt.ShowTiles = GetPath(input.Shortcuts.ShowTiles.Action, InputDeviceType.Gamepad, true);
            
            keyboard.SwitchLeft = GetPath(input.Shortcuts.SwitchLeft.Action, InputDeviceType.Keyboard);
            keyboardAlt.SwitchLeft = GetPath(input.Shortcuts.SwitchLeft.Action, InputDeviceType.Keyboard, true);
            gamepad.SwitchLeft = GetPath(input.Shortcuts.SwitchLeft.Action, InputDeviceType.Gamepad);
            gamepadAlt.SwitchLeft = GetPath(input.Shortcuts.SwitchLeft.Action, InputDeviceType.Gamepad, true);
            keyboard.SwitchRight = GetPath(input.Shortcuts.SwitchRight.Action, InputDeviceType.Keyboard);
            keyboardAlt.SwitchRight = GetPath(input.Shortcuts.SwitchRight.Action, InputDeviceType.Keyboard, true);
            gamepad.SwitchRight = GetPath(input.Shortcuts.SwitchRight.Action, InputDeviceType.Gamepad);
            gamepadAlt.SwitchRight = GetPath(input.Shortcuts.SwitchRight.Action, InputDeviceType.Gamepad, true);
            keyboard.Refresh = GetPath(input.Shortcuts.RefreshCurrent.Action, InputDeviceType.Keyboard);
            keyboardAlt.Refresh = GetPath(input.Shortcuts.RefreshCurrent.Action, InputDeviceType.Keyboard, true);
            gamepad.Refresh = GetPath(input.Shortcuts.RefreshCurrent.Action, InputDeviceType.Gamepad);
            gamepadAlt.Refresh = GetPath(input.Shortcuts.RefreshCurrent.Action, InputDeviceType.Gamepad, true);
            keyboard.RefreshAll = GetPath(input.Shortcuts.RefreshAll.Action, InputDeviceType.Keyboard);
            keyboardAlt.RefreshAll = GetPath(input.Shortcuts.RefreshAll.Action, InputDeviceType.Keyboard, true);
            gamepad.RefreshAll = GetPath(input.Shortcuts.RefreshAll.Action, InputDeviceType.Gamepad);
            gamepadAlt.RefreshAll = GetPath(input.Shortcuts.RefreshAll.Action, InputDeviceType.Gamepad, true);
            keyboard.Play = GetPath(input.Shortcuts.Play.Action, InputDeviceType.Keyboard);
            keyboardAlt.Play = GetPath(input.Shortcuts.Play.Action, InputDeviceType.Keyboard, true);
            gamepad.Play = GetPath(input.Shortcuts.Play.Action, InputDeviceType.Gamepad);
            gamepadAlt.Play = GetPath(input.Shortcuts.Play.Action, InputDeviceType.Gamepad, true);
            
            keyboard.SelectAll = GetPath(input.Shortcuts.SelectAll.Action, InputDeviceType.Keyboard);
            keyboardAlt.SelectAll = GetPath(input.Shortcuts.SelectAll.Action, InputDeviceType.Keyboard, true);
            gamepad.SelectAll = GetPath(input.Shortcuts.SelectAll.Action, InputDeviceType.Gamepad);
            gamepadAlt.SelectAll = GetPath(input.Shortcuts.SelectAll.Action, InputDeviceType.Gamepad, true);
            keyboard.DeselectAll = GetPath(input.Shortcuts.DeselectAll.Action, InputDeviceType.Keyboard);
            keyboardAlt.DeselectAll = GetPath(input.Shortcuts.DeselectAll.Action, InputDeviceType.Keyboard, true);
            gamepad.DeselectAll = GetPath(input.Shortcuts.DeselectAll.Action, InputDeviceType.Gamepad);
            gamepadAlt.DeselectAll = GetPath(input.Shortcuts.DeselectAll.Action, InputDeviceType.Gamepad, true);
            keyboard.SelectRandom = GetPath(input.Shortcuts.SelectRandom.Action, InputDeviceType.Keyboard);
            keyboardAlt.SelectRandom = GetPath(input.Shortcuts.SelectRandom.Action, InputDeviceType.Keyboard, true);
            gamepad.SelectRandom = GetPath(input.Shortcuts.SelectRandom.Action, InputDeviceType.Gamepad);
            gamepadAlt.SelectRandom = GetPath(input.Shortcuts.SelectRandom.Action, InputDeviceType.Gamepad, true);
            
            keyboard.SelectionTool = GetPath(input.Shortcuts.SelectionTool.Action, InputDeviceType.Keyboard);
            keyboardAlt.SelectionTool = GetPath(input.Shortcuts.SelectionTool.Action, InputDeviceType.Keyboard, true);
            gamepad.SelectionTool = GetPath(input.Shortcuts.SelectionTool.Action, InputDeviceType.Gamepad);
            gamepadAlt.SelectionTool = GetPath(input.Shortcuts.SelectionTool.Action, InputDeviceType.Gamepad, true);
            keyboard.BrushTool = GetPath(input.Shortcuts.BrushTool.Action, InputDeviceType.Keyboard);
            keyboardAlt.BrushTool = GetPath(input.Shortcuts.BrushTool.Action, InputDeviceType.Keyboard, true);
            gamepad.BrushTool = GetPath(input.Shortcuts.BrushTool.Action, InputDeviceType.Gamepad);
            gamepadAlt.BrushTool = GetPath(input.Shortcuts.BrushTool.Action, InputDeviceType.Gamepad, true);
            keyboard.EraserTool = GetPath(input.Shortcuts.EraserTool.Action, InputDeviceType.Keyboard);
            keyboardAlt.EraserTool = GetPath(input.Shortcuts.EraserTool.Action, InputDeviceType.Keyboard, true);
            gamepad.EraserTool = GetPath(input.Shortcuts.EraserTool.Action, InputDeviceType.Gamepad);
            gamepadAlt.EraserTool = GetPath(input.Shortcuts.EraserTool.Action, InputDeviceType.Gamepad, true);
            keyboard.FillTool = GetPath(input.Shortcuts.FillTool.Action, InputDeviceType.Keyboard);
            keyboardAlt.FillTool = GetPath(input.Shortcuts.FillTool.Action, InputDeviceType.Keyboard, true);
            gamepad.FillTool = GetPath(input.Shortcuts.FillTool.Action, InputDeviceType.Gamepad);
            gamepadAlt.FillTool = GetPath(input.Shortcuts.FillTool.Action, InputDeviceType.Gamepad, true);
            keyboard.PickerTool = GetPath(input.Shortcuts.PickerTool.Action, InputDeviceType.Keyboard);
            keyboardAlt.PickerTool = GetPath(input.Shortcuts.PickerTool.Action, InputDeviceType.Keyboard, true);
            gamepad.PickerTool = GetPath(input.Shortcuts.PickerTool.Action, InputDeviceType.Gamepad);
            gamepadAlt.PickerTool = GetPath(input.Shortcuts.PickerTool.Action, InputDeviceType.Gamepad, true);
            keyboard.ClearCanvas = GetPath(input.Shortcuts.ClearCanvas.Action, InputDeviceType.Keyboard);
            keyboardAlt.ClearCanvas = GetPath(input.Shortcuts.ClearCanvas.Action, InputDeviceType.Keyboard, true);
            gamepad.ClearCanvas = GetPath(input.Shortcuts.ClearCanvas.Action, InputDeviceType.Gamepad);
            gamepadAlt.ClearCanvas = GetPath(input.Shortcuts.ClearCanvas.Action, InputDeviceType.Gamepad, true);
            keyboard.ToggleGrid = GetPath(input.Shortcuts.ToggleGrid.Action, InputDeviceType.Keyboard);
            keyboardAlt.ToggleGrid = GetPath(input.Shortcuts.ToggleGrid.Action, InputDeviceType.Keyboard, true);
            gamepad.ToggleGrid = GetPath(input.Shortcuts.ToggleGrid.Action, InputDeviceType.Gamepad);
            gamepadAlt.ToggleGrid = GetPath(input.Shortcuts.ToggleGrid.Action, InputDeviceType.Gamepad, true);
            
            keyboard.TileLayer = GetPath(input.Shortcuts.TilesLayer.Action, InputDeviceType.Keyboard);
            keyboardAlt.TileLayer = GetPath(input.Shortcuts.TilesLayer.Action, InputDeviceType.Keyboard, true);
            gamepad.TileLayer = GetPath(input.Shortcuts.TilesLayer.Action, InputDeviceType.Gamepad);
            gamepadAlt.TileLayer = GetPath(input.Shortcuts.TilesLayer.Action, InputDeviceType.Gamepad, true);
            keyboard.DecorLayer = GetPath(input.Shortcuts.DecorLayer.Action, InputDeviceType.Keyboard);
            keyboardAlt.DecorLayer = GetPath(input.Shortcuts.DecorLayer.Action, InputDeviceType.Keyboard, true);
            gamepad.DecorLayer = GetPath(input.Shortcuts.DecorLayer.Action, InputDeviceType.Gamepad);
            gamepadAlt.DecorLayer = GetPath(input.Shortcuts.DecorLayer.Action, InputDeviceType.Gamepad, true);
            keyboard.ObjectLayer = GetPath(input.Shortcuts.ObjectsLayer.Action, InputDeviceType.Keyboard);
            keyboardAlt.ObjectLayer = GetPath(input.Shortcuts.ObjectsLayer.Action, InputDeviceType.Keyboard, true);
            gamepad.ObjectLayer = GetPath(input.Shortcuts.ObjectsLayer.Action, InputDeviceType.Gamepad);
            gamepadAlt.ObjectLayer = GetPath(input.Shortcuts.ObjectsLayer.Action, InputDeviceType.Gamepad, true);
            keyboard.EnemyLayer = GetPath(input.Shortcuts.EnemiesLayer.Action, InputDeviceType.Keyboard);
            keyboardAlt.EnemyLayer = GetPath(input.Shortcuts.EnemiesLayer.Action, InputDeviceType.Keyboard, true);
            gamepad.EnemyLayer = GetPath(input.Shortcuts.EnemiesLayer.Action, InputDeviceType.Gamepad);
            gamepadAlt.EnemyLayer = GetPath(input.Shortcuts.EnemiesLayer.Action, InputDeviceType.Gamepad, true);
            
            keyboard.ChangePalette = GetPath(input.Shortcuts.ChangePalette.Action, InputDeviceType.Keyboard);
            keyboardAlt.ChangePalette = GetPath(input.Shortcuts.ChangePalette.Action, InputDeviceType.Keyboard, true);
            gamepad.ChangePalette = GetPath(input.Shortcuts.ChangePalette.Action, InputDeviceType.Gamepad);
            gamepadAlt.ChangePalette = GetPath(input.Shortcuts.ChangePalette.Action, InputDeviceType.Gamepad, true);
            
            return new ShortcutBindingsAsset.Builder()
                .WithKeyboard(keyboard)
                .WithKeyboardAlt(keyboardAlt)
                .WithGamepad(gamepad)
                .WithGamepadAlt(gamepadAlt)
                .Build();
        }

        /// <summary>
        /// Loads <see cref="ShortcutBindingsAsset"/> paths into the current <see cref="RogiumInputActions"/>.
        /// </summary>
        /// <param name="asset">The asset to load the paths from.</param>
        public static void Load(ShortcutBindingsAsset asset)
        {
            Preconditions.IsNotNull(asset, nameof(asset));
            if (asset.Keyboard.Undo == null || asset.Gamepad.Undo == null) return;

            ApplyBindingOverride(asset.Keyboard.Undo, input.Shortcuts.Undo.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.Undo, input.Shortcuts.Undo.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.Undo, input.Shortcuts.Undo.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.Undo, input.Shortcuts.Undo.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.Redo, input.Shortcuts.Redo.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.Redo, input.Shortcuts.Redo.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.Redo, input.Shortcuts.Redo.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.Redo, input.Shortcuts.Redo.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.Save, input.Shortcuts.Save.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.Save, input.Shortcuts.Save.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.Save, input.Shortcuts.Save.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.Save, input.Shortcuts.Save.Action, InputDeviceType.Gamepad, true);

            ApplyBindingOverride(asset.Keyboard.New, input.Shortcuts.NewAsset.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.New, input.Shortcuts.NewAsset.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.New, input.Shortcuts.NewAsset.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.New, input.Shortcuts.NewAsset.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.Edit, input.Shortcuts.Edit.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.Edit, input.Shortcuts.Edit.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.Edit, input.Shortcuts.Edit.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.Edit, input.Shortcuts.Edit.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.EditProperties, input.Shortcuts.EditProperties.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.EditProperties, input.Shortcuts.EditProperties.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.EditProperties, input.Shortcuts.EditProperties.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.EditProperties, input.Shortcuts.EditProperties.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.Delete, input.Shortcuts.Delete.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.Delete, input.Shortcuts.Delete.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.Delete, input.Shortcuts.Delete.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.Delete, input.Shortcuts.Delete.Action, InputDeviceType.Gamepad, true);

            ApplyBindingOverride(asset.Keyboard.ShowPalettes, input.Shortcuts.ShowPalettes.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.ShowPalettes, input.Shortcuts.ShowPalettes.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.ShowPalettes, input.Shortcuts.ShowPalettes.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.ShowPalettes, input.Shortcuts.ShowPalettes.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.ShowSprites, input.Shortcuts.ShowSprites.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.ShowSprites, input.Shortcuts.ShowSprites.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.ShowSprites, input.Shortcuts.ShowSprites.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.ShowSprites, input.Shortcuts.ShowSprites.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.ShowWeapons, input.Shortcuts.ShowWeapons.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.ShowWeapons, input.Shortcuts.ShowWeapons.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.ShowWeapons, input.Shortcuts.ShowWeapons.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.ShowWeapons, input.Shortcuts.ShowWeapons.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.ShowProjectiles, input.Shortcuts.ShowProjectiles.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.ShowProjectiles, input.Shortcuts.ShowProjectiles.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.ShowProjectiles, input.Shortcuts.ShowProjectiles.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.ShowProjectiles, input.Shortcuts.ShowProjectiles.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.ShowEnemies, input.Shortcuts.ShowEnemies.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.ShowEnemies, input.Shortcuts.ShowEnemies.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.ShowEnemies, input.Shortcuts.ShowEnemies.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.ShowEnemies, input.Shortcuts.ShowEnemies.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.ShowRooms, input.Shortcuts.ShowRooms.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.ShowRooms, input.Shortcuts.ShowRooms.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.ShowRooms, input.Shortcuts.ShowRooms.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.ShowRooms, input.Shortcuts.ShowRooms.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.ShowTiles, input.Shortcuts.ShowTiles.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.ShowTiles, input.Shortcuts.ShowTiles.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.ShowTiles, input.Shortcuts.ShowTiles.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.ShowTiles, input.Shortcuts.ShowTiles.Action, InputDeviceType.Gamepad, true);

            ApplyBindingOverride(asset.Keyboard.SwitchLeft, input.Shortcuts.SwitchLeft.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.SwitchLeft, input.Shortcuts.SwitchLeft.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.SwitchLeft, input.Shortcuts.SwitchLeft.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.SwitchLeft, input.Shortcuts.SwitchLeft.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.SwitchRight, input.Shortcuts.SwitchRight.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.SwitchRight, input.Shortcuts.SwitchRight.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.SwitchRight, input.Shortcuts.SwitchRight.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.SwitchRight, input.Shortcuts.SwitchRight.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.Refresh, input.Shortcuts.RefreshCurrent.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.Refresh, input.Shortcuts.RefreshCurrent.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.Refresh, input.Shortcuts.RefreshCurrent.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.Refresh, input.Shortcuts.RefreshCurrent.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.RefreshAll, input.Shortcuts.RefreshAll.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.RefreshAll, input.Shortcuts.RefreshAll.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.RefreshAll, input.Shortcuts.RefreshAll.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.RefreshAll, input.Shortcuts.RefreshAll.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.Play, input.Shortcuts.Play.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.Play, input.Shortcuts.Play.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.Play, input.Shortcuts.Play.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.Play, input.Shortcuts.Play.Action, InputDeviceType.Gamepad, true);

            ApplyBindingOverride(asset.Keyboard.SelectAll, input.Shortcuts.SelectAll.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.SelectAll, input.Shortcuts.SelectAll.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.SelectAll, input.Shortcuts.SelectAll.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.SelectAll, input.Shortcuts.SelectAll.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.DeselectAll, input.Shortcuts.DeselectAll.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.DeselectAll, input.Shortcuts.DeselectAll.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.DeselectAll, input.Shortcuts.DeselectAll.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.DeselectAll, input.Shortcuts.DeselectAll.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.SelectRandom, input.Shortcuts.SelectRandom.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.SelectRandom, input.Shortcuts.SelectRandom.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.SelectRandom, input.Shortcuts.SelectRandom.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.SelectRandom, input.Shortcuts.SelectRandom.Action, InputDeviceType.Gamepad, true);

            ApplyBindingOverride(asset.Keyboard.SelectionTool, input.Shortcuts.SelectionTool.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.SelectionTool, input.Shortcuts.SelectionTool.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.SelectionTool, input.Shortcuts.SelectionTool.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.SelectionTool, input.Shortcuts.SelectionTool.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.BrushTool, input.Shortcuts.BrushTool.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.BrushTool, input.Shortcuts.BrushTool.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.BrushTool, input.Shortcuts.BrushTool.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.BrushTool, input.Shortcuts.BrushTool.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.EraserTool, input.Shortcuts.EraserTool.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.EraserTool, input.Shortcuts.EraserTool.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.EraserTool, input.Shortcuts.EraserTool.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.EraserTool, input.Shortcuts.EraserTool.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.FillTool, input.Shortcuts.FillTool.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.FillTool, input.Shortcuts.FillTool.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.FillTool, input.Shortcuts.FillTool.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.FillTool, input.Shortcuts.FillTool.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.PickerTool, input.Shortcuts.PickerTool.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.PickerTool, input.Shortcuts.PickerTool.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.PickerTool, input.Shortcuts.PickerTool.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.PickerTool, input.Shortcuts.PickerTool.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.ClearCanvas, input.Shortcuts.ClearCanvas.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.ClearCanvas, input.Shortcuts.ClearCanvas.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.ClearCanvas, input.Shortcuts.ClearCanvas.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.ClearCanvas, input.Shortcuts.ClearCanvas.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.ToggleGrid, input.Shortcuts.ToggleGrid.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.ToggleGrid, input.Shortcuts.ToggleGrid.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.ToggleGrid, input.Shortcuts.ToggleGrid.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.ToggleGrid, input.Shortcuts.ToggleGrid.Action, InputDeviceType.Gamepad, true);

            ApplyBindingOverride(asset.Keyboard.TileLayer, input.Shortcuts.TilesLayer.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.TileLayer, input.Shortcuts.TilesLayer.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.TileLayer, input.Shortcuts.TilesLayer.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.TileLayer, input.Shortcuts.TilesLayer.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.DecorLayer, input.Shortcuts.DecorLayer.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.DecorLayer, input.Shortcuts.DecorLayer.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.DecorLayer, input.Shortcuts.DecorLayer.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.DecorLayer, input.Shortcuts.DecorLayer.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.ObjectLayer, input.Shortcuts.ObjectsLayer.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.ObjectLayer, input.Shortcuts.ObjectsLayer.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.ObjectLayer, input.Shortcuts.ObjectsLayer.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.ObjectLayer, input.Shortcuts.ObjectsLayer.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.Keyboard.EnemyLayer, input.Shortcuts.EnemiesLayer.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.EnemyLayer, input.Shortcuts.EnemiesLayer.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.EnemyLayer, input.Shortcuts.EnemiesLayer.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.EnemyLayer, input.Shortcuts.EnemiesLayer.Action, InputDeviceType.Gamepad, true);

            ApplyBindingOverride(asset.Keyboard.ChangePalette, input.Shortcuts.ChangePalette.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.KeyboardAlt.ChangePalette, input.Shortcuts.ChangePalette.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.Gamepad.ChangePalette, input.Shortcuts.ChangePalette.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.GamepadAlt.ChangePalette, input.Shortcuts.ChangePalette.Action, InputDeviceType.Gamepad, true);
        }
    }
}