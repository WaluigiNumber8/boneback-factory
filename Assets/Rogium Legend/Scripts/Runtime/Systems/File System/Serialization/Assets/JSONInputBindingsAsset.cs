using System;
using RedRats.Systems.FileSystem;
using Rogium.Options.Core;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="InputBindingsAsset"/>.
    /// </summary>
    [Serializable]
    public class JSONInputBindingsAsset : IEncodedObject<InputBindingsAsset>
    {
        public InputBindingData Keyboard, KeyboardAlt, Gamepad, GamepadAlt;
        
        public JSONInputBindingsAsset(InputBindingsAsset inputBindings)
        {
            Keyboard = inputBindings.Keyboard;
            KeyboardAlt = inputBindings.KeyboardAlt;
            Gamepad = inputBindings.Gamepad;
            GamepadAlt = inputBindings.GamepadAlt;
        }
        
        public InputBindingsAsset Decode()
        {
            return new InputBindingsAsset.Builder() 
                .WithKeyboard(Keyboard)
                .WithKeyboardAlt(KeyboardAlt)
                .WithGamepad(Gamepad)
                .WithGamepadAlt(GamepadAlt)
                .Build();
        }
    }
}