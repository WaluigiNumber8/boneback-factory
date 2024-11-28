using System.Collections;
using RedRats.Core;
using Rogium.Options.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

namespace Rogium.Systems.Input
{
    /// <summary>
    /// Overseers all input profiles and deals with their switching.
    /// </summary>
    public class InputSystem : PersistentMonoSingleton<InputSystem>
    {
        private EventSystem eventSystem;
        private RogiumInputActions input;
        private InputProfilePlayer inputPlayer;
        private InputProfileUI inputUI;
        private InputProfilePause inputPause;
        
        private Vector2 pointerPosition;

        protected override void Awake()
        {
            base.Awake();
            ClearAllInput();
            SceneManager.sceneLoaded += (_, __) => eventSystem = FindFirstObjectByType<EventSystem>();
            inputUI.PointerPosition.OnPressed += UpdatePointerPosition;
            EnablePauseMap();
        }

        public void ClearAllInput()
        {
            input = new RogiumInputActions();
            inputPlayer = new InputProfilePlayer(input);
            inputUI = new InputProfileUI(input);
            inputPause = new InputProfilePause(input);
        }

        /// <summary>
        /// Enables the UI Action Map.
        /// </summary>
        public void EnableUIMap()
        {
            DisableAll();
            inputUI.Enable();
        }
        
        /// <summary>
        /// Enables the Player Action Map.
        /// </summary>
        public void EnablePlayerMap()
        {
            DisableAll();
            inputPlayer.Enable();
        }
        
        public void EnablePauseMap()
        {
            DisableAll();
            inputPause.Enable();
        }
        
        public (InputAction, int) FindDuplicateBinding(InputAction action, int bindingIndex)
        {
            InputBinding newBinding = action.bindings[bindingIndex];
            foreach (InputBinding binding in action.actionMap.bindings)
            {
                if (binding.effectivePath.Equals(newBinding.effectivePath) && binding.id != newBinding.id)
                {
                    InputAction foundAction = input.FindAction(binding.action);
                    int foundIndex = foundAction.bindings.IndexOf(b => b.id == binding.id);
                    return (foundAction, foundIndex);
                }
            }
            return (null, -1);
        }
        
        /// <summary>
        /// Disables all input for a specified amount of time.
        /// </summary>
        /// <param name="caller">The <see cref="MonoBehaviour"/> that called for this method.</param>
        /// <param name="delay">How long to suspend all input for.</param>
        public void DisableInput(MonoBehaviour caller, float delay)
        {
            caller.StartCoroutine(DisableAllCoroutine(delay));
            IEnumerator DisableAllCoroutine(float delayTime)
            {
                eventSystem.sendNavigationEvents = false;
                yield return new WaitForSecondsRealtime(delayTime);
                eventSystem.sendNavigationEvents = true;
            }
        }

        public InputBindingsAsset GetInputAsAsset()
        {
            InputBindingData keyboard = new InputBindingData();
            int navigateIndex = GetBindingIndexByDevice(inputUI.Navigate.Action, InputDeviceType.Keyboard);
            // keyboard.NavigateUp = inputUI.Navigate.Action.bindings[navigateIndex].effectivePath;
            // keyboard.NavigateDown = inputUI.Navigate.Action.bindings[navigateIndex + 1].effectivePath;
            // keyboard.NavigateLeft = inputUI.Navigate.Action.bindings[navigateIndex + 2].effectivePath;
            // keyboard.NavigateRight = inputUI.Navigate.Action.bindings[navigateIndex + 3].effectivePath;
            keyboard.Select = inputUI.Select.Action.bindings[GetBindingIndexByDevice(inputUI.Select.Action, InputDeviceType.Keyboard)].effectivePath;
            keyboard.ContextSelect = inputUI.ContextSelect.Action.bindings[GetBindingIndexByDevice(inputUI.ContextSelect.Action, InputDeviceType.Keyboard)].effectivePath;
            keyboard.Cancel = inputUI.Cancel.Action.bindings[GetBindingIndexByDevice(inputUI.Cancel.Action, InputDeviceType.Keyboard)].effectivePath;
            keyboard.ShowTooltip = inputUI.ShowTooltip.Action.bindings[GetBindingIndexByDevice(inputUI.ShowTooltip.Action, InputDeviceType.Keyboard)].effectivePath;
            int movementIndex = GetBindingIndexByDevice(inputPlayer.Movement.Action, InputDeviceType.Keyboard);
            keyboard.MoveUp = inputPlayer.Movement.Action.bindings[movementIndex].effectivePath;
            keyboard.MoveDown = inputPlayer.Movement.Action.bindings[movementIndex + 1].effectivePath;
            keyboard.MoveLeft = inputPlayer.Movement.Action.bindings[movementIndex + 2].effectivePath;
            keyboard.MoveRight = inputPlayer.Movement.Action.bindings[movementIndex + 3].effectivePath;
            keyboard.ButtonMain = inputPlayer.ButtonMain.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonMain.Action, InputDeviceType.Keyboard)].effectivePath;
            keyboard.ButtonMainAlt = inputPlayer.ButtonMainAlt.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonMainAlt.Action, InputDeviceType.Keyboard)].effectivePath;
            keyboard.ButtonSub = inputPlayer.ButtonSub.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonSub.Action, InputDeviceType.Keyboard)].effectivePath;
            keyboard.ButtonSubAlt = inputPlayer.ButtonSubAlt.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonSubAlt.Action, InputDeviceType.Keyboard)].effectivePath;
            keyboard.ButtonDash = inputPlayer.ButtonDash.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonDash.Action, InputDeviceType.Keyboard)].effectivePath;
            keyboard.ButtonDashAlt = inputPlayer.ButtonDashAlt.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonDashAlt.Action, InputDeviceType.Keyboard)].effectivePath;
            keyboard.Pause = inputPause.Pause.Action.bindings[GetBindingIndexByDevice(inputPause.Pause.Action, InputDeviceType.Keyboard)].effectivePath;
            
            InputBindingData keyboardAlt = new InputBindingData();
            int navigateIndexAlt = GetBindingIndexByDevice(inputUI.Navigate.Action, InputDeviceType.Keyboard, true);
            // keyboardAlt.NavigateUp = inputUI.Navigate.Action.bindings[navigateIndexAlt].effectivePath;
            // keyboardAlt.NavigateDown = inputUI.Navigate.Action.bindings[navigateIndexAlt + 1].effectivePath;
            // keyboardAlt.NavigateLeft = inputUI.Navigate.Action.bindings[navigateIndexAlt + 2].effectivePath;
            // keyboardAlt.NavigateRight = inputUI.Navigate.Action.bindings[navigateIndexAlt + 3].effectivePath;
            keyboardAlt.Select = inputUI.Select.Action.bindings[GetBindingIndexByDevice(inputUI.Select.Action, InputDeviceType.Keyboard, true)].effectivePath;
            keyboardAlt.ContextSelect = inputUI.ContextSelect.Action.bindings[GetBindingIndexByDevice(inputUI.ContextSelect.Action, InputDeviceType.Keyboard, true)].effectivePath;
            keyboardAlt.Cancel = inputUI.Cancel.Action.bindings[GetBindingIndexByDevice(inputUI.Cancel.Action, InputDeviceType.Keyboard, true)].effectivePath;
            keyboardAlt.ShowTooltip = inputUI.ShowTooltip.Action.bindings[GetBindingIndexByDevice(inputUI.ShowTooltip.Action, InputDeviceType.Keyboard, true)].effectivePath;
            int movementIndexAlt = GetBindingIndexByDevice(inputPlayer.Movement.Action, InputDeviceType.Keyboard, true);
            keyboardAlt.MoveUp = inputPlayer.Movement.Action.bindings[movementIndexAlt].effectivePath;
            keyboardAlt.MoveDown = inputPlayer.Movement.Action.bindings[movementIndexAlt + 1].effectivePath;
            keyboardAlt.MoveLeft = inputPlayer.Movement.Action.bindings[movementIndexAlt + 2].effectivePath;
            keyboardAlt.MoveRight = inputPlayer.Movement.Action.bindings[movementIndexAlt + 3].effectivePath;
            keyboardAlt.ButtonMain = inputPlayer.ButtonMain.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonMain.Action, InputDeviceType.Keyboard, true)].effectivePath;
            keyboardAlt.ButtonMainAlt = inputPlayer.ButtonMainAlt.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonMainAlt.Action, InputDeviceType.Keyboard, true)].effectivePath;
            keyboardAlt.ButtonSub = inputPlayer.ButtonSub.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonSub.Action, InputDeviceType.Keyboard, true)].effectivePath;
            keyboardAlt.ButtonSubAlt = inputPlayer.ButtonSubAlt.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonSubAlt.Action, InputDeviceType.Keyboard, true)].effectivePath;
            keyboardAlt.ButtonDash = inputPlayer.ButtonDash.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonDash.Action, InputDeviceType.Keyboard, true)].effectivePath;
            keyboardAlt.ButtonDashAlt = inputPlayer.ButtonDashAlt.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonDashAlt.Action, InputDeviceType.Keyboard, true)].effectivePath;
            keyboardAlt.Pause = inputPause.Pause.Action.bindings[GetBindingIndexByDevice(inputPause.Pause.Action, InputDeviceType.Keyboard, true)].effectivePath;
            
            InputBindingData gamepad = new InputBindingData();
            navigateIndex = GetBindingIndexByDevice(inputUI.Navigate.Action, InputDeviceType.Gamepad);
            // gamepad.NavigateUp = inputUI.Navigate.Action.bindings[navigateIndex].effectivePath;
            // gamepad.NavigateDown = inputUI.Navigate.Action.bindings[navigateIndex + 1].effectivePath;
            // gamepad.NavigateLeft = inputUI.Navigate.Action.bindings[navigateIndex + 2].effectivePath;
            // gamepad.NavigateRight = inputUI.Navigate.Action.bindings[navigateIndex + 3].effectivePath;
            gamepad.Select = inputUI.Select.Action.bindings[GetBindingIndexByDevice(inputUI.Select.Action, InputDeviceType.Gamepad)].effectivePath;
            gamepad.ContextSelect = inputUI.ContextSelect.Action.bindings[GetBindingIndexByDevice(inputUI.ContextSelect.Action, InputDeviceType.Gamepad)].effectivePath;
            gamepad.Cancel = inputUI.Cancel.Action.bindings[GetBindingIndexByDevice(inputUI.Cancel.Action, InputDeviceType.Gamepad)].effectivePath;
            gamepad.ShowTooltip = inputUI.ShowTooltip.Action.bindings[GetBindingIndexByDevice(inputUI.ShowTooltip.Action, InputDeviceType.Gamepad)].effectivePath;
            movementIndex = GetBindingIndexByDevice(inputPlayer.Movement.Action, InputDeviceType.Gamepad);
            gamepad.MoveUp = inputPlayer.Movement.Action.bindings[movementIndex].effectivePath;
            gamepad.MoveDown = inputPlayer.Movement.Action.bindings[movementIndex + 1].effectivePath;
            gamepad.MoveLeft = inputPlayer.Movement.Action.bindings[movementIndex + 2].effectivePath;
            gamepad.MoveRight = inputPlayer.Movement.Action.bindings[movementIndex + 3].effectivePath;
            gamepad.ButtonMain = inputPlayer.ButtonMain.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonMain.Action, InputDeviceType.Gamepad)].effectivePath;
            gamepad.ButtonMainAlt = inputPlayer.ButtonMainAlt.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonMainAlt.Action, InputDeviceType.Gamepad)].effectivePath;
            gamepad.ButtonSub = inputPlayer.ButtonSub.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonSub.Action, InputDeviceType.Gamepad)].effectivePath;
            gamepad.ButtonSubAlt = inputPlayer.ButtonSubAlt.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonSubAlt.Action, InputDeviceType.Gamepad)].effectivePath;
            gamepad.ButtonDash = inputPlayer.ButtonDash.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonDash.Action, InputDeviceType.Gamepad)].effectivePath;
            gamepad.ButtonDashAlt = inputPlayer.ButtonDashAlt.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonDashAlt.Action, InputDeviceType.Gamepad)].effectivePath;
            gamepad.Pause = inputPause.Pause.Action.bindings[GetBindingIndexByDevice(inputPause.Pause.Action, InputDeviceType.Gamepad)].effectivePath;
            
            InputBindingData gamepadAlt = new InputBindingData();
            navigateIndexAlt = GetBindingIndexByDevice(inputUI.Navigate.Action, InputDeviceType.Gamepad, true);
            // gamepadAlt.NavigateUp = inputUI.Navigate.Action.bindings[navigateIndexAlt].effectivePath;
            // gamepadAlt.NavigateDown = inputUI.Navigate.Action.bindings[navigateIndexAlt + 1].effectivePath;
            // gamepadAlt.NavigateLeft = inputUI.Navigate.Action.bindings[navigateIndexAlt + 2].effectivePath;
            // gamepadAlt.NavigateRight = inputUI.Navigate.Action.bindings[navigateIndexAlt + 3].effectivePath;
            gamepadAlt.Select = inputUI.Select.Action.bindings[GetBindingIndexByDevice(inputUI.Select.Action, InputDeviceType.Gamepad, true)].effectivePath;
            gamepadAlt.ContextSelect = inputUI.ContextSelect.Action.bindings[GetBindingIndexByDevice(inputUI.ContextSelect.Action, InputDeviceType.Gamepad, true)].effectivePath;
            gamepadAlt.Cancel = inputUI.Cancel.Action.bindings[GetBindingIndexByDevice(inputUI.Cancel.Action, InputDeviceType.Gamepad, true)].effectivePath;
            gamepadAlt.ShowTooltip = inputUI.ShowTooltip.Action.bindings[GetBindingIndexByDevice(inputUI.ShowTooltip.Action, InputDeviceType.Gamepad, true)].effectivePath;
            movementIndexAlt = GetBindingIndexByDevice(inputPlayer.Movement.Action, InputDeviceType.Gamepad, true);
            gamepadAlt.MoveUp = inputPlayer.Movement.Action.bindings[movementIndexAlt].effectivePath;
            gamepadAlt.MoveDown = inputPlayer.Movement.Action.bindings[movementIndexAlt + 1].effectivePath;
            gamepadAlt.MoveLeft = inputPlayer.Movement.Action.bindings[movementIndexAlt + 2].effectivePath;
            gamepadAlt.MoveRight = inputPlayer.Movement.Action.bindings[movementIndexAlt + 3].effectivePath;
            gamepadAlt.ButtonMain = inputPlayer.ButtonMain.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonMain.Action, InputDeviceType.Gamepad, true)].effectivePath;
            gamepadAlt.ButtonMainAlt = inputPlayer.ButtonMainAlt.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonMainAlt.Action, InputDeviceType.Gamepad, true)].effectivePath;
            gamepadAlt.ButtonSub = inputPlayer.ButtonSub.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonSub.Action, InputDeviceType.Gamepad, true)].effectivePath;
            gamepadAlt.ButtonSubAlt = inputPlayer.ButtonSubAlt.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonSubAlt.Action, InputDeviceType.Gamepad, true)].effectivePath;
            gamepadAlt.ButtonDash = inputPlayer.ButtonDash.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonDash.Action, InputDeviceType.Gamepad, true)].effectivePath;
            gamepadAlt.ButtonDashAlt = inputPlayer.ButtonDashAlt.Action.bindings[GetBindingIndexByDevice(inputPlayer.ButtonDashAlt.Action, InputDeviceType.Gamepad, true)].effectivePath;
            gamepadAlt.Pause = inputPause.Pause.Action.bindings[GetBindingIndexByDevice(inputPause.Pause.Action, InputDeviceType.Gamepad, true)].effectivePath;
            
            return new InputBindingsAsset.Builder()
                .WithKeyboard(keyboard)
                .WithKeyboardAlt(keyboardAlt)
                .WithGamepad(gamepad)
                .WithGamepadAlt(gamepadAlt)
                .Build();
        }
        
        public void ApplyInput(InputBindingsAsset asset)
        {
            //UI - Keyboard
            // int navigateIndex = GetBindingIndexByDevice(inputUI.Navigate.Action, InputDeviceType.Keyboard);
            // UI.Navigate.Action.ApplyBindingOverride(navigateIndex, asset.Keyboard.MoveUp);
            // UI.Navigate.Action.ApplyBindingOverride(navigateIndex + 1, asset.Keyboard.MoveDown);
            // UI.Navigate.Action.ApplyBindingOverride(navigateIndex + 2, asset.Keyboard.MoveLeft);
            // UI.Navigate.Action.ApplyBindingOverride(navigateIndex + 3, asset.Keyboard.MoveRight);
            UI.Select.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputUI.Select.Action, InputDeviceType.Keyboard), asset.Keyboard.Select);
            UI.ContextSelect.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputUI.ContextSelect.Action, InputDeviceType.Keyboard), asset.Keyboard.ContextSelect);
            UI.Cancel.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputUI.Cancel.Action, InputDeviceType.Keyboard), asset.Keyboard.Cancel);
            UI.ShowTooltip.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputUI.ShowTooltip.Action, InputDeviceType.Keyboard), asset.Keyboard.ShowTooltip);
            
            // int navigateIndexAlt = GetBindingIndexByDevice(inputUI.Navigate.Action, InputDeviceType.Keyboard, true);
            // UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt, asset.KeyboardAlt.MoveUp);
            // UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt + 1, asset.KeyboardAlt.MoveDown);
            // UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt + 2, asset.KeyboardAlt.MoveLeft);
            // UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt + 3, asset.KeyboardAlt.MoveRight);
            UI.Select.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputUI.Select.Action, InputDeviceType.Keyboard, true), asset.KeyboardAlt.Select);
            UI.ContextSelect.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputUI.ContextSelect.Action, InputDeviceType.Keyboard, true), asset.KeyboardAlt.ContextSelect);
            UI.Cancel.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputUI.Cancel.Action, InputDeviceType.Keyboard, true), asset.KeyboardAlt.Cancel);
            UI.ShowTooltip.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputUI.ShowTooltip.Action, InputDeviceType.Keyboard, true), asset.KeyboardAlt.ShowTooltip);
            
            //UI - Gamepad
            // navigateIndex = GetBindingIndexByDevice(inputUI.Navigate.Action, InputDeviceType.Gamepad);
            // UI.Navigate.Action.ApplyBindingOverride(navigateIndex, asset.Gamepad.MoveUp);
            // UI.Navigate.Action.ApplyBindingOverride(navigateIndex + 1, asset.Gamepad.MoveDown);
            // UI.Navigate.Action.ApplyBindingOverride(navigateIndex + 2, asset.Gamepad.MoveLeft);
            // UI.Navigate.Action.ApplyBindingOverride(navigateIndex + 3, asset.Gamepad.MoveRight);
            UI.Select.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputUI.Select.Action, InputDeviceType.Gamepad), asset.Gamepad.Select);
            UI.ContextSelect.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputUI.ContextSelect.Action, InputDeviceType.Gamepad), asset.Gamepad.ContextSelect);
            UI.Cancel.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputUI.Cancel.Action, InputDeviceType.Gamepad), asset.Gamepad.Cancel);
            UI.ShowTooltip.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputUI.ShowTooltip.Action, InputDeviceType.Gamepad), asset.Gamepad.ShowTooltip);
            
            // navigateIndexAlt = GetBindingIndexByDevice(inputUI.Navigate.Action, InputDeviceType.Gamepad, true);
            // UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt, asset.GamepadAlt.MoveUp);
            // UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt + 1, asset.GamepadAlt.MoveDown);
            // UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt + 2, asset.GamepadAlt.MoveLeft);
            // UI.Navigate.Action.ApplyBindingOverride(navigateIndexAlt + 3, asset.GamepadAlt.MoveRight);
            UI.Select.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputUI.Select.Action, InputDeviceType.Gamepad, true), asset.GamepadAlt.Select);
            UI.ContextSelect.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputUI.ContextSelect.Action, InputDeviceType.Gamepad, true), asset.GamepadAlt.ContextSelect);
            UI.Cancel.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputUI.Cancel.Action, InputDeviceType.Gamepad, true), asset.GamepadAlt.Cancel);
            UI.ShowTooltip.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputUI.ShowTooltip.Action, InputDeviceType.Gamepad, true), asset.GamepadAlt.ShowTooltip);
            
            //Player - Keyboard
            int movementIndex = GetBindingIndexByDevice(inputPlayer.Movement.Action, InputDeviceType.Keyboard);
            Player.Movement.Action.ApplyBindingOverride(movementIndex, asset.Keyboard.MoveUp);
            Player.Movement.Action.ApplyBindingOverride(movementIndex + 1, asset.Keyboard.MoveDown);
            Player.Movement.Action.ApplyBindingOverride(movementIndex + 2, asset.Keyboard.MoveLeft);
            Player.Movement.Action.ApplyBindingOverride(movementIndex + 3, asset.Keyboard.MoveRight);
            Player.ButtonMain.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonMain.Action, InputDeviceType.Keyboard), asset.Keyboard.ButtonMain);
            Player.ButtonMainAlt.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonMainAlt.Action, InputDeviceType.Keyboard), asset.Keyboard.ButtonMainAlt);
            Player.ButtonSub.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonSub.Action, InputDeviceType.Keyboard), asset.Keyboard.ButtonSub);
            Player.ButtonSubAlt.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonSubAlt.Action, InputDeviceType.Keyboard), asset.Keyboard.ButtonSubAlt);
            Player.ButtonDash.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonDash.Action, InputDeviceType.Keyboard), asset.Keyboard.ButtonDash);
            Player.ButtonDashAlt.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonDashAlt.Action, InputDeviceType.Keyboard), asset.Keyboard.ButtonDashAlt);
            
            int movementIndexAlt = GetBindingIndexByDevice(inputPlayer.Movement.Action, InputDeviceType.Keyboard, true);
            Player.Movement.Action.ApplyBindingOverride(movementIndexAlt, asset.KeyboardAlt.MoveUp);
            Player.Movement.Action.ApplyBindingOverride(movementIndexAlt + 1, asset.KeyboardAlt.MoveDown);
            Player.Movement.Action.ApplyBindingOverride(movementIndexAlt + 2, asset.KeyboardAlt.MoveLeft);
            Player.Movement.Action.ApplyBindingOverride(movementIndexAlt + 3, asset.KeyboardAlt.MoveRight);
            Player.ButtonMain.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonMain.Action, InputDeviceType.Keyboard, true), asset.KeyboardAlt.ButtonMain);
            Player.ButtonMainAlt.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonMainAlt.Action, InputDeviceType.Keyboard, true), asset.KeyboardAlt.ButtonMainAlt);
            Player.ButtonSub.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonSub.Action, InputDeviceType.Keyboard, true), asset.KeyboardAlt.ButtonSub);
            Player.ButtonSubAlt.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonSubAlt.Action, InputDeviceType.Keyboard, true), asset.KeyboardAlt.ButtonSubAlt);
            Player.ButtonDash.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonDash.Action, InputDeviceType.Keyboard, true), asset.KeyboardAlt.ButtonDash);
            Player.ButtonDashAlt.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonDashAlt.Action, InputDeviceType.Keyboard, true), asset.KeyboardAlt.ButtonDashAlt);
            
            //Player - Gamepad
            movementIndex = GetBindingIndexByDevice(inputPlayer.Movement.Action, InputDeviceType.Gamepad);
            Player.Movement.Action.ApplyBindingOverride(movementIndex, asset.Gamepad.MoveUp);
            Player.Movement.Action.ApplyBindingOverride(movementIndex + 1, asset.Gamepad.MoveDown);
            Player.Movement.Action.ApplyBindingOverride(movementIndex + 2, asset.Gamepad.MoveLeft);
            Player.Movement.Action.ApplyBindingOverride(movementIndex + 3, asset.Gamepad.MoveRight);
            Player.ButtonMain.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonMain.Action, InputDeviceType.Gamepad), asset.Gamepad.ButtonMain);
            Player.ButtonMainAlt.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonMainAlt.Action, InputDeviceType.Gamepad), asset.Gamepad.ButtonMainAlt);
            Player.ButtonSub.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonSub.Action, InputDeviceType.Gamepad), asset.Gamepad.ButtonSub);
            Player.ButtonSubAlt.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonSubAlt.Action, InputDeviceType.Gamepad), asset.Gamepad.ButtonSubAlt);
            Player.ButtonDash.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonDash.Action, InputDeviceType.Gamepad), asset.Gamepad.ButtonDash);
            Player.ButtonDashAlt.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonDashAlt.Action, InputDeviceType.Gamepad), asset.Gamepad.ButtonDashAlt);
            
            movementIndexAlt = GetBindingIndexByDevice(inputPlayer.Movement.Action, InputDeviceType.Gamepad, true);
            Player.Movement.Action.ApplyBindingOverride(movementIndexAlt, asset.GamepadAlt.MoveUp);
            Player.Movement.Action.ApplyBindingOverride(movementIndexAlt + 1, asset.GamepadAlt.MoveDown);
            Player.Movement.Action.ApplyBindingOverride(movementIndexAlt + 2, asset.GamepadAlt.MoveLeft);
            Player.Movement.Action.ApplyBindingOverride(movementIndexAlt + 3, asset.GamepadAlt.MoveRight);
            Player.ButtonMain.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonMain.Action, InputDeviceType.Gamepad, true), asset.GamepadAlt.ButtonMain);
            Player.ButtonMainAlt.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonMainAlt.Action, InputDeviceType.Gamepad, true), asset.GamepadAlt.ButtonMainAlt);
            Player.ButtonSub.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonSub.Action, InputDeviceType.Gamepad, true), asset.GamepadAlt.ButtonSub);
            Player.ButtonSubAlt.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonSubAlt.Action, InputDeviceType.Gamepad, true), asset.GamepadAlt.ButtonSubAlt);
            Player.ButtonDash.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonDash.Action, InputDeviceType.Gamepad, true), asset.GamepadAlt.ButtonDash);
            Player.ButtonDashAlt.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPlayer.ButtonDashAlt.Action, InputDeviceType.Gamepad, true), asset.GamepadAlt.ButtonDashAlt);
            
            // Pause
            Pause.Pause.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPause.Pause.Action, InputDeviceType.Keyboard), asset.Keyboard.Pause);
            Pause.Pause.Action.ApplyBindingOverride(GetBindingIndexByDevice(inputPause.Pause.Action, InputDeviceType.Gamepad), asset.Gamepad.Pause);
        }
        
        public int GetBindingIndexByDevice(InputAction action, InputDeviceType device, bool getSecondary = false)
        {
            return device switch
            {
                InputDeviceType.Keyboard => GetBindingIndex(new InputBinding(groups: KeyboardSchemeGroup, path: default)),
                InputDeviceType.Gamepad => GetBindingIndex(new InputBinding(groups: GamepadSchemeGroup, path: default)),
                _ => throw new System.ArgumentOutOfRangeException(nameof(device), device, null)
            };

            int GetBindingIndex(InputBinding group)
            {
                ReadOnlyArray<InputBinding> bindings = action.bindings;
                bool waitForComposite = false;
                for (int i = 0; i < bindings.Count; ++i)
                {
                    InputBinding b = bindings[i];
                    if (b.isComposite) waitForComposite = false;
                    if (!group.Matches(b)) continue;
                    if (waitForComposite) continue;
                    if (getSecondary)
                    {
                        getSecondary = false;
                        if (b.isPartOfComposite) waitForComposite = true;
                        continue;
                    }
                    return i;
                }
                return -1;
            }
        }
        
        /// <summary>
        /// Disables all Action Maps except UI.
        /// </summary>
        private void DisableAll()
        {
            inputPlayer.Disable();
        }
        
        private void UpdatePointerPosition(Vector2 value) => pointerPosition = value;

        public Vector2 PointerPosition { get => pointerPosition; }
        public InputProfilePlayer Player { get => inputPlayer; }
        public InputProfileUI UI { get => inputUI; }
        public InputProfilePause Pause { get => inputPause; }
        public string KeyboardSchemeGroup { get => input.KeyboardMouseScheme.bindingGroup; }
        public string GamepadSchemeGroup { get => input.GamepadScheme.bindingGroup; }
    }
}