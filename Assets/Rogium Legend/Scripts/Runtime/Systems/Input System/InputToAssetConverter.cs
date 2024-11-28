using RedRats.Safety;
using Rogium.Options.Core;
using UnityEngine.InputSystem;

namespace Rogium.Systems.Input
{
    /// <summary>
    /// Converts the <see cref="RogiumInputActions"/> asset to <see cref="InputBindingsAsset"/> and vice versa.
    /// </summary>
    public static class InputToAssetConverter
    {
        private static readonly InputSystem input = InputSystem.GetInstance();

        public static InputBindingsAsset Get()
        {
            InputBindingData keyboard = new();
            int navigateIndex = input.GetBindingIndexByDevice(input.UI.Navigate.Action, InputDeviceType.Keyboard);
            // keyboard.NavigateUp = inputSystem.UI.Navigate.Action.bindings[navigateIndex].effectivePath;
            // keyboard.NavigateDown = inputSystem.UI.Navigate.Action.bindings[navigateIndex + 1].effectivePath;
            // keyboard.NavigateLeft = inputSystem.UI.Navigate.Action.bindings[navigateIndex + 2].effectivePath;
            // keyboard.NavigateRight = inputSystem.UI.Navigate.Action.bindings[navigateIndex + 3].effectivePath;
            keyboard.Select = input.UI.Select.Action.bindings[input.GetBindingIndexByDevice(input.UI.Select.Action, InputDeviceType.Keyboard)].effectivePath;
            keyboard.ContextSelect = input.UI.ContextSelect.Action.bindings[input.GetBindingIndexByDevice(input.UI.ContextSelect.Action, InputDeviceType.Keyboard)].effectivePath;
            keyboard.Cancel = input.UI.Cancel.Action.bindings[input.GetBindingIndexByDevice(input.UI.Cancel.Action, InputDeviceType.Keyboard)].effectivePath;
            keyboard.ShowTooltip = input.UI.ShowTooltip.Action.bindings[input.GetBindingIndexByDevice(input.UI.ShowTooltip.Action, InputDeviceType.Keyboard)].effectivePath;
            int movementIndex = input.GetBindingIndexByDevice(input.Player.Movement.Action, InputDeviceType.Keyboard);
            keyboard.MoveUp = input.Player.Movement.Action.bindings[movementIndex].effectivePath;
            keyboard.MoveDown = input.Player.Movement.Action.bindings[movementIndex + 1].effectivePath;
            keyboard.MoveLeft = input.Player.Movement.Action.bindings[movementIndex + 2].effectivePath;
            keyboard.MoveRight = input.Player.Movement.Action.bindings[movementIndex + 3].effectivePath;
            keyboard.ButtonMain = input.Player.ButtonMain.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonMain.Action, InputDeviceType.Keyboard)].effectivePath;
            keyboard.ButtonMainAlt = input.Player.ButtonMainAlt.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonMainAlt.Action, InputDeviceType.Keyboard)].effectivePath;
            keyboard.ButtonSub = input.Player.ButtonSub.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonSub.Action, InputDeviceType.Keyboard)].effectivePath;
            keyboard.ButtonSubAlt = input.Player.ButtonSubAlt.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonSubAlt.Action, InputDeviceType.Keyboard)].effectivePath;
            keyboard.ButtonDash = input.Player.ButtonDash.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonDash.Action, InputDeviceType.Keyboard)].effectivePath;
            keyboard.ButtonDashAlt = input.Player.ButtonDashAlt.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonDashAlt.Action, InputDeviceType.Keyboard)].effectivePath;
            keyboard.Pause = input.Pause.Pause.Action.bindings[input.GetBindingIndexByDevice(input.Pause.Pause.Action, InputDeviceType.Keyboard)].effectivePath;

            InputBindingData keyboardAlt = new();
            int navigateIndexAlt = input.GetBindingIndexByDevice(input.UI.Navigate.Action, InputDeviceType.Keyboard, true);
            // keyboardAlt.NavigateUp = inputSystem.UI.Navigate.Action.bindings[navigateIndexAlt].effectivePath;
            // keyboardAlt.NavigateDown = inputSystem.UI.Navigate.Action.bindings[navigateIndexAlt + 1].effectivePath;
            // keyboardAlt.NavigateLeft = inputSystem.UI.Navigate.Action.bindings[navigateIndexAlt + 2].effectivePath;
            // keyboardAlt.NavigateRight = inputSystem.UI.Navigate.Action.bindings[navigateIndexAlt + 3].effectivePath;
            keyboardAlt.Select = input.UI.Select.Action.bindings[input.GetBindingIndexByDevice(input.UI.Select.Action, InputDeviceType.Keyboard, true)].effectivePath;
            keyboardAlt.ContextSelect = input.UI.ContextSelect.Action.bindings[input.GetBindingIndexByDevice(input.UI.ContextSelect.Action, InputDeviceType.Keyboard, true)].effectivePath;
            keyboardAlt.Cancel = input.UI.Cancel.Action.bindings[input.GetBindingIndexByDevice(input.UI.Cancel.Action, InputDeviceType.Keyboard, true)].effectivePath;
            keyboardAlt.ShowTooltip = input.UI.ShowTooltip.Action.bindings[input.GetBindingIndexByDevice(input.UI.ShowTooltip.Action, InputDeviceType.Keyboard, true)].effectivePath;
            int movementIndexAlt = input.GetBindingIndexByDevice(input.Player.Movement.Action, InputDeviceType.Keyboard, true);
            keyboardAlt.MoveUp = input.Player.Movement.Action.bindings[movementIndexAlt].effectivePath;
            keyboardAlt.MoveDown = input.Player.Movement.Action.bindings[movementIndexAlt + 1].effectivePath;
            keyboardAlt.MoveLeft = input.Player.Movement.Action.bindings[movementIndexAlt + 2].effectivePath;
            keyboardAlt.MoveRight = input.Player.Movement.Action.bindings[movementIndexAlt + 3].effectivePath;
            keyboardAlt.ButtonMain = input.Player.ButtonMain.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonMain.Action, InputDeviceType.Keyboard, true)].effectivePath;
            keyboardAlt.ButtonMainAlt = input.Player.ButtonMainAlt.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonMainAlt.Action, InputDeviceType.Keyboard, true)].effectivePath;
            keyboardAlt.ButtonSub = input.Player.ButtonSub.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonSub.Action, InputDeviceType.Keyboard, true)].effectivePath;
            keyboardAlt.ButtonSubAlt = input.Player.ButtonSubAlt.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonSubAlt.Action, InputDeviceType.Keyboard, true)].effectivePath;
            keyboardAlt.ButtonDash = input.Player.ButtonDash.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonDash.Action, InputDeviceType.Keyboard, true)].effectivePath;
            keyboardAlt.ButtonDashAlt = input.Player.ButtonDashAlt.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonDashAlt.Action, InputDeviceType.Keyboard, true)].effectivePath;
            keyboardAlt.Pause = input.Pause.Pause.Action.bindings[input.GetBindingIndexByDevice(input.Pause.Pause.Action, InputDeviceType.Keyboard, true)].effectivePath;

            InputBindingData gamepad = new();
            navigateIndex = input.GetBindingIndexByDevice(input.UI.Navigate.Action, InputDeviceType.Gamepad);
            // gamepad.NavigateUp = inputSystem.UI.Navigate.Action.bindings[navigateIndex].effectivePath;
            // gamepad.NavigateDown = inputSystem.UI.Navigate.Action.bindings[navigateIndex + 1].effectivePath;
            // gamepad.NavigateLeft = inputSystem.UI.Navigate.Action.bindings[navigateIndex + 2].effectivePath;
            // gamepad.NavigateRight = inputSystem.UI.Navigate.Action.bindings[navigateIndex + 3].effectivePath;
            gamepad.Select = input.UI.Select.Action.bindings[input.GetBindingIndexByDevice(input.UI.Select.Action, InputDeviceType.Gamepad)].effectivePath;
            gamepad.ContextSelect = input.UI.ContextSelect.Action.bindings[input.GetBindingIndexByDevice(input.UI.ContextSelect.Action, InputDeviceType.Gamepad)].effectivePath;
            gamepad.Cancel = input.UI.Cancel.Action.bindings[input.GetBindingIndexByDevice(input.UI.Cancel.Action, InputDeviceType.Gamepad)].effectivePath;
            gamepad.ShowTooltip = input.UI.ShowTooltip.Action.bindings[input.GetBindingIndexByDevice(input.UI.ShowTooltip.Action, InputDeviceType.Gamepad)].effectivePath;
            movementIndex = input.GetBindingIndexByDevice(input.Player.Movement.Action, InputDeviceType.Gamepad);
            gamepad.MoveUp = input.Player.Movement.Action.bindings[movementIndex].effectivePath;
            gamepad.MoveDown = input.Player.Movement.Action.bindings[movementIndex + 1].effectivePath;
            gamepad.MoveLeft = input.Player.Movement.Action.bindings[movementIndex + 2].effectivePath;
            gamepad.MoveRight = input.Player.Movement.Action.bindings[movementIndex + 3].effectivePath;
            gamepad.ButtonMain = input.Player.ButtonMain.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonMain.Action, InputDeviceType.Gamepad)].effectivePath;
            gamepad.ButtonMainAlt = input.Player.ButtonMainAlt.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonMainAlt.Action, InputDeviceType.Gamepad)].effectivePath;
            gamepad.ButtonSub = input.Player.ButtonSub.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonSub.Action, InputDeviceType.Gamepad)].effectivePath;
            gamepad.ButtonSubAlt = input.Player.ButtonSubAlt.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonSubAlt.Action, InputDeviceType.Gamepad)].effectivePath;
            gamepad.ButtonDash = input.Player.ButtonDash.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonDash.Action, InputDeviceType.Gamepad)].effectivePath;
            gamepad.ButtonDashAlt = input.Player.ButtonDashAlt.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonDashAlt.Action, InputDeviceType.Gamepad)].effectivePath;
            gamepad.Pause = input.Pause.Pause.Action.bindings[input.GetBindingIndexByDevice(input.Pause.Pause.Action, InputDeviceType.Gamepad)].effectivePath;

            InputBindingData gamepadAlt = new();
            navigateIndexAlt = input.GetBindingIndexByDevice(input.UI.Navigate.Action, InputDeviceType.Gamepad, true);
            // gamepadAlt.NavigateUp = inputSystem.UI.Navigate.Action.bindings[navigateIndexAlt].effectivePath;
            // gamepadAlt.NavigateDown = inputSystem.UI.Navigate.Action.bindings[navigateIndexAlt + 1].effectivePath;
            // gamepadAlt.NavigateLeft = inputSystem.UI.Navigate.Action.bindings[navigateIndexAlt + 2].effectivePath;
            // gamepadAlt.NavigateRight = inputSystem.UI.Navigate.Action.bindings[navigateIndexAlt + 3].effectivePath;
            gamepadAlt.Select = input.UI.Select.Action.bindings[input.GetBindingIndexByDevice(input.UI.Select.Action, InputDeviceType.Gamepad, true)].effectivePath;
            gamepadAlt.ContextSelect = input.UI.ContextSelect.Action.bindings[input.GetBindingIndexByDevice(input.UI.ContextSelect.Action, InputDeviceType.Gamepad, true)].effectivePath;
            gamepadAlt.Cancel = input.UI.Cancel.Action.bindings[input.GetBindingIndexByDevice(input.UI.Cancel.Action, InputDeviceType.Gamepad, true)].effectivePath;
            gamepadAlt.ShowTooltip = input.UI.ShowTooltip.Action.bindings[input.GetBindingIndexByDevice(input.UI.ShowTooltip.Action, InputDeviceType.Gamepad, true)].effectivePath;
            movementIndexAlt = input.GetBindingIndexByDevice(input.Player.Movement.Action, InputDeviceType.Gamepad, true);
            gamepadAlt.MoveUp = input.Player.Movement.Action.bindings[movementIndexAlt].effectivePath;
            gamepadAlt.MoveDown = input.Player.Movement.Action.bindings[movementIndexAlt + 1].effectivePath;
            gamepadAlt.MoveLeft = input.Player.Movement.Action.bindings[movementIndexAlt + 2].effectivePath;
            gamepadAlt.MoveRight = input.Player.Movement.Action.bindings[movementIndexAlt + 3].effectivePath;
            gamepadAlt.ButtonMain = input.Player.ButtonMain.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonMain.Action, InputDeviceType.Gamepad, true)].effectivePath;
            gamepadAlt.ButtonMainAlt = input.Player.ButtonMainAlt.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonMainAlt.Action, InputDeviceType.Gamepad, true)].effectivePath;
            gamepadAlt.ButtonSub = input.Player.ButtonSub.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonSub.Action, InputDeviceType.Gamepad, true)].effectivePath;
            gamepadAlt.ButtonSubAlt = input.Player.ButtonSubAlt.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonSubAlt.Action, InputDeviceType.Gamepad, true)].effectivePath;
            gamepadAlt.ButtonDash = input.Player.ButtonDash.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonDash.Action, InputDeviceType.Gamepad, true)].effectivePath;
            gamepadAlt.ButtonDashAlt = input.Player.ButtonDashAlt.Action.bindings[input.GetBindingIndexByDevice(input.Player.ButtonDashAlt.Action, InputDeviceType.Gamepad, true)].effectivePath;
            gamepadAlt.Pause = input.Pause.Pause.Action.bindings[input.GetBindingIndexByDevice(input.Pause.Pause.Action, InputDeviceType.Gamepad, true)].effectivePath;

            return new InputBindingsAsset.Builder()
                .WithKeyboard(keyboard)
                .WithKeyboardAlt(keyboardAlt)
                .WithGamepad(gamepad)
                .WithGamepadAlt(gamepadAlt)
                .Build();
        }

        public static void Load(InputBindingsAsset asset)
        {
            SafetyNet.EnsureIsNotNull(asset, nameof(asset));
            if (asset.Keyboard.Select == null || asset.Gamepad.Select == null) return;
            
            //inputSystem.UI - Keyboard
            // int navigateIndex = inputSystem.GetBindingIndexByDevice(inputSystem.UI.Navigate.Action, InputDeviceType.Keyboard);
            // inputSystem.UI.Navigate.Action.ApplyBindingOverride(navigateIndex, asset.Keyboard.MoveUp);
            // inputSystem.UI.Navigate.Action.ApplyBindingOverride(navigateIndex + 1, asset.Keyboard.MoveDown);
            // inputSystem.UI.Navigate.Action.ApplyBindingOverride(navigateIndex + 2, asset.Keyboard.MoveLeft);
            // inputSystem.UI.Navigate.Action.ApplyBindingOverride(navigateIndex + 3, asset.Keyboard.MoveRight);
            input.UI.Select.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.UI.Select.Action, InputDeviceType.Keyboard), asset.Keyboard.Select);
            input.UI.ContextSelect.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.UI.ContextSelect.Action, InputDeviceType.Keyboard), asset.Keyboard.ContextSelect);
            input.UI.Cancel.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.UI.Cancel.Action, InputDeviceType.Keyboard), asset.Keyboard.Cancel);
            input.UI.ShowTooltip.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.UI.ShowTooltip.Action, InputDeviceType.Keyboard), asset.Keyboard.ShowTooltip);

            // int navigateIndexAlt = inputSystem.GetBindingIndexByDevice(inputSystem.UI.Navigate.Action, InputDeviceType.Keyboard, true);
            // inputSystem.UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt, asset.KeyboardAlt.MoveUp);
            // inputSystem.UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt + 1, asset.KeyboardAlt.MoveDown);
            // inputSystem.UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt + 2, asset.KeyboardAlt.MoveLeft);
            // inputSystem.UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt + 3, asset.KeyboardAlt.MoveRight);
            input.UI.Select.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.UI.Select.Action, InputDeviceType.Keyboard, true), asset.KeyboardAlt.Select);
            input.UI.ContextSelect.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.UI.ContextSelect.Action, InputDeviceType.Keyboard, true), asset.KeyboardAlt.ContextSelect);
            input.UI.Cancel.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.UI.Cancel.Action, InputDeviceType.Keyboard, true), asset.KeyboardAlt.Cancel);
            input.UI.ShowTooltip.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.UI.ShowTooltip.Action, InputDeviceType.Keyboard, true), asset.KeyboardAlt.ShowTooltip);

            //inputSystem.UI - Gamepad
            // navigateIndex = inputSystem.GetBindingIndexByDevice(inputSystem.UI.Navigate.Action, InputDeviceType.Gamepad);
            // inputSystem.UI.Navigate.Action.ApplyBindingOverride(navigateIndex, asset.Gamepad.MoveUp);
            // inputSystem.UI.Navigate.Action.ApplyBindingOverride(navigateIndex + 1, asset.Gamepad.MoveDown);
            // inputSystem.UI.Navigate.Action.ApplyBindingOverride(navigateIndex + 2, asset.Gamepad.MoveLeft);
            // inputSystem.UI.Navigate.Action.ApplyBindingOverride(navigateIndex + 3, asset.Gamepad.MoveRight);
            input.UI.Select.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.UI.Select.Action, InputDeviceType.Gamepad), asset.Gamepad.Select);
            input.UI.ContextSelect.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.UI.ContextSelect.Action, InputDeviceType.Gamepad), asset.Gamepad.ContextSelect);
            input.UI.Cancel.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.UI.Cancel.Action, InputDeviceType.Gamepad), asset.Gamepad.Cancel);
            input.UI.ShowTooltip.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.UI.ShowTooltip.Action, InputDeviceType.Gamepad), asset.Gamepad.ShowTooltip);

            // navigateIndexAlt = inputSystem.GetBindingIndexByDevice(inputSystem.UI.Navigate.Action, InputDeviceType.Gamepad, true);
            // inputSystem.UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt, asset.GamepadAlt.MoveUp);
            // inputSystem.UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt + 1, asset.GamepadAlt.MoveDown);
            // inputSystem.UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt + 2, asset.GamepadAlt.MoveLeft);
            // inputSystem.UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt + 3, asset.GamepadAlt.MoveRight);
            input.UI.Select.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.UI.Select.Action, InputDeviceType.Gamepad, true), asset.GamepadAlt.Select);
            input.UI.ContextSelect.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.UI.ContextSelect.Action, InputDeviceType.Gamepad, true), asset.GamepadAlt.ContextSelect);
            input.UI.Cancel.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.UI.Cancel.Action, InputDeviceType.Gamepad, true), asset.GamepadAlt.Cancel);
            input.UI.ShowTooltip.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.UI.ShowTooltip.Action, InputDeviceType.Gamepad, true), asset.GamepadAlt.ShowTooltip);

            //inputSystem.Player - Keyboard
            int movementIndex = input.GetBindingIndexByDevice(input.Player.Movement.Action, InputDeviceType.Keyboard);
            input.Player.Movement.Action.ApplyBindingOverride(movementIndex, asset.Keyboard.MoveUp);
            input.Player.Movement.Action.ApplyBindingOverride(movementIndex + 1, asset.Keyboard.MoveDown);
            input.Player.Movement.Action.ApplyBindingOverride(movementIndex + 2, asset.Keyboard.MoveLeft);
            input.Player.Movement.Action.ApplyBindingOverride(movementIndex + 3, asset.Keyboard.MoveRight);
            input.Player.ButtonMain.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonMain.Action, InputDeviceType.Keyboard), asset.Keyboard.ButtonMain);
            input.Player.ButtonMainAlt.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonMainAlt.Action, InputDeviceType.Keyboard), asset.Keyboard.ButtonMainAlt);
            input.Player.ButtonSub.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonSub.Action, InputDeviceType.Keyboard), asset.Keyboard.ButtonSub);
            input.Player.ButtonSubAlt.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonSubAlt.Action, InputDeviceType.Keyboard), asset.Keyboard.ButtonSubAlt);
            input.Player.ButtonDash.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonDash.Action, InputDeviceType.Keyboard), asset.Keyboard.ButtonDash);
            input.Player.ButtonDashAlt.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonDashAlt.Action, InputDeviceType.Keyboard), asset.Keyboard.ButtonDashAlt);

            int movementIndexAlt = input.GetBindingIndexByDevice(input.Player.Movement.Action, InputDeviceType.Keyboard, true);
            input.Player.Movement.Action.ApplyBindingOverride(movementIndexAlt, asset.KeyboardAlt.MoveUp);
            input.Player.Movement.Action.ApplyBindingOverride(movementIndexAlt + 1, asset.KeyboardAlt.MoveDown);
            input.Player.Movement.Action.ApplyBindingOverride(movementIndexAlt + 2, asset.KeyboardAlt.MoveLeft);
            input.Player.Movement.Action.ApplyBindingOverride(movementIndexAlt + 3, asset.KeyboardAlt.MoveRight);
            input.Player.ButtonMain.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonMain.Action, InputDeviceType.Keyboard, true), asset.KeyboardAlt.ButtonMain);
            input.Player.ButtonMainAlt.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonMainAlt.Action, InputDeviceType.Keyboard, true), asset.KeyboardAlt.ButtonMainAlt);
            input.Player.ButtonSub.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonSub.Action, InputDeviceType.Keyboard, true), asset.KeyboardAlt.ButtonSub);
            input.Player.ButtonSubAlt.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonSubAlt.Action, InputDeviceType.Keyboard, true), asset.KeyboardAlt.ButtonSubAlt);
            input.Player.ButtonDash.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonDash.Action, InputDeviceType.Keyboard, true), asset.KeyboardAlt.ButtonDash);
            input.Player.ButtonDashAlt.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonDashAlt.Action, InputDeviceType.Keyboard, true), asset.KeyboardAlt.ButtonDashAlt);

            //inputSystem.Player - Gamepad
            movementIndex = input.GetBindingIndexByDevice(input.Player.Movement.Action, InputDeviceType.Gamepad);
            input.Player.Movement.Action.ApplyBindingOverride(movementIndex, asset.Gamepad.MoveUp);
            input.Player.Movement.Action.ApplyBindingOverride(movementIndex + 1, asset.Gamepad.MoveDown);
            input.Player.Movement.Action.ApplyBindingOverride(movementIndex + 2, asset.Gamepad.MoveLeft);
            input.Player.Movement.Action.ApplyBindingOverride(movementIndex + 3, asset.Gamepad.MoveRight);
            input.Player.ButtonMain.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonMain.Action, InputDeviceType.Gamepad), asset.Gamepad.ButtonMain);
            input.Player.ButtonMainAlt.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonMainAlt.Action, InputDeviceType.Gamepad), asset.Gamepad.ButtonMainAlt);
            input.Player.ButtonSub.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonSub.Action, InputDeviceType.Gamepad), asset.Gamepad.ButtonSub);
            input.Player.ButtonSubAlt.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonSubAlt.Action, InputDeviceType.Gamepad), asset.Gamepad.ButtonSubAlt);
            input.Player.ButtonDash.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonDash.Action, InputDeviceType.Gamepad), asset.Gamepad.ButtonDash);
            input.Player.ButtonDashAlt.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonDashAlt.Action, InputDeviceType.Gamepad), asset.Gamepad.ButtonDashAlt);

            movementIndexAlt = input.GetBindingIndexByDevice(input.Player.Movement.Action, InputDeviceType.Gamepad, true);
            input.Player.Movement.Action.ApplyBindingOverride(movementIndexAlt, asset.GamepadAlt.MoveUp);
            input.Player.Movement.Action.ApplyBindingOverride(movementIndexAlt + 1, asset.GamepadAlt.MoveDown);
            input.Player.Movement.Action.ApplyBindingOverride(movementIndexAlt + 2, asset.GamepadAlt.MoveLeft);
            input.Player.Movement.Action.ApplyBindingOverride(movementIndexAlt + 3, asset.GamepadAlt.MoveRight);
            input.Player.ButtonMain.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonMain.Action, InputDeviceType.Gamepad, true), asset.GamepadAlt.ButtonMain);
            input.Player.ButtonMainAlt.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonMainAlt.Action, InputDeviceType.Gamepad, true), asset.GamepadAlt.ButtonMainAlt);
            input.Player.ButtonSub.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonSub.Action, InputDeviceType.Gamepad, true), asset.GamepadAlt.ButtonSub);
            input.Player.ButtonSubAlt.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonSubAlt.Action, InputDeviceType.Gamepad, true), asset.GamepadAlt.ButtonSubAlt);
            input.Player.ButtonDash.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonDash.Action, InputDeviceType.Gamepad, true), asset.GamepadAlt.ButtonDash);
            input.Player.ButtonDashAlt.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Player.ButtonDashAlt.Action, InputDeviceType.Gamepad, true), asset.GamepadAlt.ButtonDashAlt);

            // Pause
            input.Pause.Pause.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Pause.Pause.Action, InputDeviceType.Keyboard), asset.Keyboard.Pause);
            input.Pause.Pause.Action.ApplyBindingOverride(input.GetBindingIndexByDevice(input.Pause.Pause.Action, InputDeviceType.Gamepad), asset.Gamepad.Pause);
        }
    }
}