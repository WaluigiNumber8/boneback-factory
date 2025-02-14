using RedRats.Safety;
using Rogium.Options.Core;
using static Rogium.Systems.Input.InputSystemUtils;

namespace Rogium.Systems.Input
{
    /// <summary>
    /// Converts the <see cref="RogiumInputActions"/> asset to <see cref="InputBindingsAsset"/> and vice versa.
    /// </summary>
    public static class InputToAssetConverter
    {
        private static readonly InputSystem input = InputSystem.GetInstance();

        /// <summary>
        /// Builds an <see cref="InputBindingsAsset"/> from the current <see cref="RogiumInputActions"/> and returns it.
        /// </summary>
        public static InputBindingsAsset Get()
        {
            InputBindingData keyboard = new();
            // int navigateIndex = GetBindingIndexByDevice(input.UI.Navigate.Action, InputDeviceType.Keyboard);
            // keyboard.NavigateUp = input.UI.Navigate.Action.bindings[navigateIndex].effectivePath;
            // keyboard.NavigateDown = input.UI.Navigate.Action.bindings[navigateIndex + 1].effectivePath;
            // keyboard.NavigateLeft = input.UI.Navigate.Action.bindings[navigateIndex + 2].effectivePath;
            // keyboard.NavigateRight = input.UI.Navigate.Action.bindings[navigateIndex + 3].effectivePath;
            keyboard.Select = GetPath(input.UI.Select.Action, InputDeviceType.Keyboard);
            keyboard.ContextSelect = GetPath(input.UI.ContextSelect.Action, InputDeviceType.Keyboard);
            keyboard.Cancel = GetPath(input.UI.Cancel.Action, InputDeviceType.Keyboard);
            keyboard.ShowTooltip = GetPath(input.UI.ShowTooltip.Action, InputDeviceType.Keyboard);
            
            int movementIndex = GetBindingIndexByDevice(input.Player.Movement.Action, InputDeviceType.Keyboard);
            keyboard.MoveUp = GetPath(input.Player.Movement.Action, InputDeviceType.Keyboard, false, movementIndex);
            keyboard.MoveDown = GetPath(input.Player.Movement.Action, InputDeviceType.Keyboard, false, movementIndex + 1);
            keyboard.MoveLeft = GetPath(input.Player.Movement.Action, InputDeviceType.Keyboard, false, movementIndex + 2);
            keyboard.MoveRight = GetPath(input.Player.Movement.Action, InputDeviceType.Keyboard, false, movementIndex + 3);
            keyboard.ButtonMain = GetPath(input.Player.ButtonMain.Action, InputDeviceType.Keyboard);
            keyboard.ButtonMainAlt = GetPath(input.Player.ButtonMainAlt.Action, InputDeviceType.Keyboard);
            keyboard.ButtonSub = GetPath(input.Player.ButtonSub.Action, InputDeviceType.Keyboard);
            keyboard.ButtonSubAlt = GetPath(input.Player.ButtonSubAlt.Action, InputDeviceType.Keyboard);
            keyboard.ButtonDash = GetPath(input.Player.ButtonDash.Action, InputDeviceType.Keyboard);
            keyboard.ButtonDashAlt = GetPath(input.Player.ButtonDashAlt.Action, InputDeviceType.Keyboard);
            keyboard.Pause = GetPath(input.Pause.Pause.Action, InputDeviceType.Keyboard);

            InputBindingData keyboardAlt = new();
            // int navigateIndexAlt = GetBindingIndexByDevice(input.UI.Navigate.Action, InputDeviceType.Keyboard, true);
            // keyboardAlt.NavigateUp = input.UI.Navigate.Action.bindings[navigateIndexAlt].effectivePath;
            // keyboardAlt.NavigateDown = input.UI.Navigate.Action.bindings[navigateIndexAlt + 1].effectivePath;
            // keyboardAlt.NavigateLeft = input.UI.Navigate.Action.bindings[navigateIndexAlt + 2].effectivePath;
            // keyboardAlt.NavigateRight = input.UI.Navigate.Action.bindings[navigateIndexAlt + 3].effectivePath;
            keyboardAlt.Select = GetPath(input.UI.Select.Action, InputDeviceType.Keyboard, true);
            keyboardAlt.ContextSelect = GetPath(input.UI.ContextSelect.Action, InputDeviceType.Keyboard, true);
            keyboardAlt.Cancel = GetPath(input.UI.Cancel.Action, InputDeviceType.Keyboard, true);
            keyboardAlt.ShowTooltip = GetPath(input.UI.ShowTooltip.Action, InputDeviceType.Keyboard, true);
            
            int movementIndexAlt = GetBindingIndexByDevice(input.Player.Movement.Action, InputDeviceType.Keyboard, true);
            keyboardAlt.MoveUp = GetPath(input.Player.Movement.Action, InputDeviceType.Keyboard, true, movementIndexAlt);
            keyboardAlt.MoveDown = GetPath(input.Player.Movement.Action, InputDeviceType.Keyboard, true, movementIndexAlt + 1);
            keyboardAlt.MoveLeft = GetPath(input.Player.Movement.Action, InputDeviceType.Keyboard, true, movementIndexAlt + 2);
            keyboardAlt.MoveRight = GetPath(input.Player.Movement.Action, InputDeviceType.Keyboard, true, movementIndexAlt + 3);
            keyboardAlt.ButtonMain = GetPath(input.Player.ButtonMain.Action, InputDeviceType.Keyboard, true);
            keyboardAlt.ButtonMainAlt = GetPath(input.Player.ButtonMainAlt.Action, InputDeviceType.Keyboard, true);
            keyboardAlt.ButtonSub = GetPath(input.Player.ButtonSub.Action, InputDeviceType.Keyboard, true);
            keyboardAlt.ButtonSubAlt = GetPath(input.Player.ButtonSubAlt.Action, InputDeviceType.Keyboard, true);
            keyboardAlt.ButtonDash = GetPath(input.Player.ButtonDash.Action, InputDeviceType.Keyboard, true);
            keyboardAlt.ButtonDashAlt = GetPath(input.Player.ButtonDashAlt.Action, InputDeviceType.Keyboard, true);
            keyboardAlt.Pause = GetPath(input.Pause.Pause.Action, InputDeviceType.Keyboard, true);

            InputBindingData gamepad = new();
            // navigateIndex = GetBindingIndexByDevice(input.UI.Navigate.Action, InputDeviceType.Gamepad);
            // gamepad.NavigateUp = input.UI.Navigate.Action.bindings[navigateIndex].effectivePath;
            // gamepad.NavigateDown = input.UI.Navigate.Action.bindings[navigateIndex + 1].effectivePath;
            // gamepad.NavigateLeft = input.UI.Navigate.Action.bindings[navigateIndex + 2].effectivePath;
            // gamepad.NavigateRight = input.UI.Navigate.Action.bindings[navigateIndex + 3].effectivePath;
            gamepad.Select = GetPath(input.UI.Select.Action, InputDeviceType.Gamepad);
            gamepad.ContextSelect = GetPath(input.UI.ContextSelect.Action, InputDeviceType.Gamepad);
            gamepad.Cancel = GetPath(input.UI.Cancel.Action, InputDeviceType.Gamepad);
            gamepad.ShowTooltip = GetPath(input.UI.ShowTooltip.Action, InputDeviceType.Gamepad);
            
            movementIndex = GetBindingIndexByDevice(input.Player.Movement.Action, InputDeviceType.Gamepad);
            gamepad.MoveUp = GetPath(input.Player.Movement.Action, InputDeviceType.Gamepad, false, movementIndex);
            gamepad.MoveDown = GetPath(input.Player.Movement.Action, InputDeviceType.Gamepad, false, movementIndex + 1);
            gamepad.MoveLeft = GetPath(input.Player.Movement.Action, InputDeviceType.Gamepad, false, movementIndex + 2);
            gamepad.MoveRight = GetPath(input.Player.Movement.Action, InputDeviceType.Gamepad, false, movementIndex + 3);
            gamepad.ButtonMain = GetPath(input.Player.ButtonMain.Action, InputDeviceType.Gamepad);
            gamepad.ButtonMainAlt = GetPath(input.Player.ButtonMainAlt.Action, InputDeviceType.Gamepad);
            gamepad.ButtonSub = GetPath(input.Player.ButtonSub.Action, InputDeviceType.Gamepad);
            gamepad.ButtonSubAlt = GetPath(input.Player.ButtonSubAlt.Action, InputDeviceType.Gamepad);
            gamepad.ButtonDash = GetPath(input.Player.ButtonDash.Action, InputDeviceType.Gamepad);
            gamepad.ButtonDashAlt = GetPath(input.Player.ButtonDashAlt.Action, InputDeviceType.Gamepad);
            gamepad.Pause = GetPath(input.Pause.Pause.Action, InputDeviceType.Gamepad);

            InputBindingData gamepadAlt = new();
            // navigateIndexAlt = GetBindingIndexByDevice(input.UI.Navigate.Action, InputDeviceType.Gamepad, true);
            // gamepadAlt.NavigateUp = input.UI.Navigate.Action.bindings[navigateIndexAlt].effectivePath;
            // gamepadAlt.NavigateDown = input.UI.Navigate.Action.bindings[navigateIndexAlt + 1].effectivePath;
            // gamepadAlt.NavigateLeft = input.UI.Navigate.Action.bindings[navigateIndexAlt + 2].effectivePath;
            // gamepadAlt.NavigateRight = input.UI.Navigate.Action.bindings[navigateIndexAlt + 3].effectivePath;
            gamepadAlt.Select = GetPath(input.UI.Select.Action, InputDeviceType.Gamepad, true);
            gamepadAlt.ContextSelect = GetPath(input.UI.ContextSelect.Action, InputDeviceType.Gamepad, true);
            gamepadAlt.Cancel = GetPath(input.UI.Cancel.Action, InputDeviceType.Gamepad, true);
            gamepadAlt.ShowTooltip = GetPath(input.UI.ShowTooltip.Action, InputDeviceType.Gamepad, true);
            
            movementIndexAlt = GetBindingIndexByDevice(input.Player.Movement.Action, InputDeviceType.Gamepad, true);
            gamepadAlt.MoveUp = GetPath(input.Player.Movement.Action, InputDeviceType.Gamepad, true, movementIndexAlt);
            gamepadAlt.MoveDown = GetPath(input.Player.Movement.Action, InputDeviceType.Gamepad, true, movementIndexAlt + 1);
            gamepadAlt.MoveLeft = GetPath(input.Player.Movement.Action, InputDeviceType.Gamepad, true, movementIndexAlt + 2);
            gamepadAlt.MoveRight = GetPath(input.Player.Movement.Action, InputDeviceType.Gamepad, true, movementIndexAlt + 3);
            gamepadAlt.ButtonMain = GetPath(input.Player.ButtonMain.Action, InputDeviceType.Gamepad, true);
            gamepadAlt.ButtonMainAlt = GetPath(input.Player.ButtonMainAlt.Action, InputDeviceType.Gamepad, true);
            gamepadAlt.ButtonSub = GetPath(input.Player.ButtonSub.Action, InputDeviceType.Gamepad, true);
            gamepadAlt.ButtonSubAlt = GetPath(input.Player.ButtonSubAlt.Action, InputDeviceType.Gamepad, true);
            gamepadAlt.ButtonDash = GetPath(input.Player.ButtonDash.Action, InputDeviceType.Gamepad, true);
            gamepadAlt.ButtonDashAlt = GetPath(input.Player.ButtonDashAlt.Action, InputDeviceType.Gamepad, true);
            gamepadAlt.Pause = GetPath(input.Pause.Pause.Action, InputDeviceType.Gamepad, true);

            return new InputBindingsAsset.Builder()
                .WithKeyboard(keyboard)
                .WithKeyboardAlt(keyboardAlt)
                .WithGamepad(gamepad)
                .WithGamepadAlt(gamepadAlt)
                .Build();
        }

        /// <summary>
        /// Loads the <see cref="InputBindingsAsset"/> binding paths into the current <see cref="RogiumInputActions"/>.
        /// </summary>
        /// <param name="asset">The asset to load the paths from.</param>
        public static void Load(InputBindingsAsset asset)
        {
            Preconditions.IsNotNull(asset, nameof(asset));
            if (asset.Keyboard.Select == null || asset.Gamepad.Select == null) return;
            
            //inputSystem.UI - Keyboard
            // int navigateIndex = GetBindingIndexByDevice(input.UI.Navigate.Action, InputDeviceType.Keyboard);
            // input.UI.Navigate.Action.ApplyBindingOverride(navigateIndex, asset.Keyboard.NavigateUp);
            // input.UI.Navigate.Action.ApplyBindingOverride(navigateIndex + 1, asset.Keyboard.NavigateDown);
            // input.UI.Navigate.Action.ApplyBindingOverride(navigateIndex + 2, asset.Keyboard.NavigateLeft);
            // input.UI.Navigate.Action.ApplyBindingOverride(navigateIndex + 3, asset.Keyboard.NavigateRight);
            ApplyBindingOverride(asset.Keyboard.Select, input.UI.Select.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.Keyboard.ContextSelect, input.UI.ContextSelect.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.Keyboard.Cancel, input.UI.Cancel.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.Keyboard.ShowTooltip, input.UI.ShowTooltip.Action, InputDeviceType.Keyboard);

            // int navigateIndexAlt = GetBindingIndexByDevice(input.UI.Navigate.Action, InputDeviceType.Keyboard, true);
            // input.UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt, asset.KeyboardAlt.NavigateUp);
            // input.UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt + 1, asset.KeyboardAlt.NavigateDown);
            // input.UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt + 2, asset.KeyboardAlt.NavigateLeft);
            // input.UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt + 3, asset.KeyboardAlt.NavigateRight);
            ApplyBindingOverride(asset.KeyboardAlt.Select, input.UI.Select.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.KeyboardAlt.ContextSelect, input.UI.ContextSelect.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.KeyboardAlt.Cancel, input.UI.Cancel.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.KeyboardAlt.ShowTooltip, input.UI.ShowTooltip.Action, InputDeviceType.Keyboard, true);

            //inputSystem.UI - Gamepad
            // navigateIndex = GetBindingIndexByDevice(input.UI.Navigate.Action, InputDeviceType.Gamepad);
            // input.UI.Navigate.Action.ApplyBindingOverride(navigateIndex, asset.Gamepad.NavigateUp);
            // input.UI.Navigate.Action.ApplyBindingOverride(navigateIndex + 1, asset.Gamepad.NavigateDown);
            // input.UI.Navigate.Action.ApplyBindingOverride(navigateIndex + 2, asset.Gamepad.NavigateLeft);
            // input.UI.Navigate.Action.ApplyBindingOverride(navigateIndex + 3, asset.Gamepad.NavigateRight);
            ApplyBindingOverride(asset.Gamepad.Select, input.UI.Select.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.Gamepad.ContextSelect, input.UI.ContextSelect.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.Gamepad.Cancel, input.UI.Cancel.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.Gamepad.ShowTooltip, input.UI.ShowTooltip.Action, InputDeviceType.Gamepad);

            // navigateIndexAlt = GetBindingIndexByDevice(input.UI.Navigate.Action, InputDeviceType.Gamepad, true);
            // input.UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt, asset.GamepadAlt.NavigateUp);
            // input.UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt + 1, asset.GamepadAlt.NavigateDown);
            // input.UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt + 2, asset.GamepadAlt.NavigateLeft);
            // input.UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt + 3, asset.GamepadAlt.NavigateRight);
            ApplyBindingOverride(asset.GamepadAlt.Select, input.UI.Select.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.GamepadAlt.ContextSelect, input.UI.ContextSelect.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.GamepadAlt.Cancel, input.UI.Cancel.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.GamepadAlt.ShowTooltip, input.UI.ShowTooltip.Action, InputDeviceType.Gamepad, true);

            //inputSystem.Player - Keyboard
            int movementIndex = GetBindingIndexByDevice(input.Player.Movement.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.Keyboard.MoveUp, input.Player.Movement.Action, InputDeviceType.Keyboard, false, movementIndex);
            ApplyBindingOverride(asset.Keyboard.MoveDown, input.Player.Movement.Action, InputDeviceType.Keyboard, false, movementIndex + 1);
            ApplyBindingOverride(asset.Keyboard.MoveLeft, input.Player.Movement.Action, InputDeviceType.Keyboard, false, movementIndex + 2);
            ApplyBindingOverride(asset.Keyboard.MoveRight, input.Player.Movement.Action, InputDeviceType.Keyboard, false, movementIndex + 3);
            ApplyBindingOverride(asset.Keyboard.ButtonMain, input.Player.ButtonMain.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.Keyboard.ButtonMainAlt, input.Player.ButtonMainAlt.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.Keyboard.ButtonSub, input.Player.ButtonSub.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.Keyboard.ButtonSubAlt, input.Player.ButtonSubAlt.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.Keyboard.ButtonDash, input.Player.ButtonDash.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.Keyboard.ButtonDashAlt, input.Player.ButtonDashAlt.Action, InputDeviceType.Keyboard);

            int movementIndexAlt = GetBindingIndexByDevice(input.Player.Movement.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.KeyboardAlt.MoveUp, input.Player.Movement.Action, InputDeviceType.Keyboard, true, movementIndexAlt);
            ApplyBindingOverride(asset.KeyboardAlt.MoveDown, input.Player.Movement.Action, InputDeviceType.Keyboard, true, movementIndexAlt + 1);
            ApplyBindingOverride(asset.KeyboardAlt.MoveLeft, input.Player.Movement.Action, InputDeviceType.Keyboard, true, movementIndexAlt + 2);
            ApplyBindingOverride(asset.KeyboardAlt.MoveRight, input.Player.Movement.Action, InputDeviceType.Keyboard, true, movementIndexAlt + 3);
            ApplyBindingOverride(asset.KeyboardAlt.ButtonMain, input.Player.ButtonMain.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.KeyboardAlt.ButtonMainAlt, input.Player.ButtonMainAlt.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.KeyboardAlt.ButtonSub, input.Player.ButtonSub.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.KeyboardAlt.ButtonSubAlt, input.Player.ButtonSubAlt.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.KeyboardAlt.ButtonDash, input.Player.ButtonDash.Action, InputDeviceType.Keyboard, true);
            ApplyBindingOverride(asset.KeyboardAlt.ButtonDashAlt, input.Player.ButtonDashAlt.Action, InputDeviceType.Keyboard, true);

            //inputSystem.Player - Gamepad
            movementIndex = GetBindingIndexByDevice(input.Player.Movement.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.Gamepad.MoveUp, input.Player.Movement.Action, InputDeviceType.Gamepad, false, movementIndex);
            ApplyBindingOverride(asset.Gamepad.MoveDown, input.Player.Movement.Action, InputDeviceType.Gamepad, false, movementIndex + 1);
            ApplyBindingOverride(asset.Gamepad.MoveLeft, input.Player.Movement.Action, InputDeviceType.Gamepad, false, movementIndex + 2);
            ApplyBindingOverride(asset.Gamepad.MoveRight, input.Player.Movement.Action, InputDeviceType.Gamepad, false, movementIndex + 3);
            ApplyBindingOverride(asset.Gamepad.ButtonMain, input.Player.ButtonMain.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.Gamepad.ButtonMainAlt, input.Player.ButtonMainAlt.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.Gamepad.ButtonSub, input.Player.ButtonSub.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.Gamepad.ButtonSubAlt, input.Player.ButtonSubAlt.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.Gamepad.ButtonDash, input.Player.ButtonDash.Action, InputDeviceType.Gamepad);
            ApplyBindingOverride(asset.Gamepad.ButtonDashAlt, input.Player.ButtonDashAlt.Action, InputDeviceType.Gamepad);

            movementIndexAlt = GetBindingIndexByDevice(input.Player.Movement.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.GamepadAlt.MoveUp, input.Player.Movement.Action, InputDeviceType.Gamepad, true, movementIndexAlt);
            ApplyBindingOverride(asset.GamepadAlt.MoveDown, input.Player.Movement.Action, InputDeviceType.Gamepad, true, movementIndexAlt + 1);
            ApplyBindingOverride(asset.GamepadAlt.MoveLeft, input.Player.Movement.Action, InputDeviceType.Gamepad, true, movementIndexAlt + 2);
            ApplyBindingOverride(asset.GamepadAlt.MoveRight, input.Player.Movement.Action, InputDeviceType.Gamepad, true, movementIndexAlt + 3);
            ApplyBindingOverride(asset.GamepadAlt.ButtonMain, input.Player.ButtonMain.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.GamepadAlt.ButtonMainAlt, input.Player.ButtonMainAlt.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.GamepadAlt.ButtonSub, input.Player.ButtonSub.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.GamepadAlt.ButtonSubAlt, input.Player.ButtonSubAlt.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.GamepadAlt.ButtonDash, input.Player.ButtonDash.Action, InputDeviceType.Gamepad, true);
            ApplyBindingOverride(asset.GamepadAlt.ButtonDashAlt, input.Player.ButtonDashAlt.Action, InputDeviceType.Gamepad, true);

            // Pause
            ApplyBindingOverride(asset.Keyboard.Pause, input.Pause.Pause.Action, InputDeviceType.Keyboard);
            ApplyBindingOverride(asset.Gamepad.Pause, input.Pause.Pause.Action, InputDeviceType.Gamepad);
        }
    }
}