using Rogium.Editors.Core;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Holds input bindings for the game.
    /// </summary>
    public class InputBindingsAsset : IDataAsset
    {
        private InputBindingData keyboard;
        private InputBindingData keyboardAlt;
        private InputBindingData gamepad;
        private InputBindingData gamepadAlt;

        private InputBindingsAsset() { }
        
        public override string ToString() => $"{Title}";
        
        public string ID { get => "ZY"; }
        public string Title { get => "Input"; }
        public InputBindingData Keyboard { get => keyboard; }
        public InputBindingData KeyboardAlt { get => keyboardAlt; }
        public InputBindingData Gamepad { get => gamepad; }
        public InputBindingData GamepadAlt { get => gamepadAlt; }
        
        public class Builder
        {
            private readonly InputBindingsAsset Asset = new();
            
            public Builder()
            {
                Asset.keyboard = new InputBindingData();
                Asset.keyboardAlt = new InputBindingData();
                Asset.gamepad = new InputBindingData();
                Asset.gamepadAlt = new InputBindingData();
            }
        
            public Builder WithKeyboard(InputBindingData data)
            {
                Asset.keyboard = data;
                return this;
            }
            
            public Builder WithKeyboardAlt(InputBindingData data)
            {
                Asset.keyboardAlt = data;
                return this;
            }
            
            public Builder WithGamepad(InputBindingData data)
            {
                Asset.gamepad = data;
                return this;
            }
            
            public Builder WithGamepadAlt(InputBindingData data)
            {
                Asset.gamepadAlt = data;
                return this;
            }
            
            public Builder AsCopy(InputBindingsAsset asset)
            {
                return WithKeyboard(asset.Keyboard)
                      .WithKeyboardAlt(asset.KeyboardAlt)
                      .WithGamepad(asset.Gamepad)
                      .WithGamepadAlt(asset.GamepadAlt);
            }
        
            public InputBindingsAsset Build() => Asset;
        }
    }
}