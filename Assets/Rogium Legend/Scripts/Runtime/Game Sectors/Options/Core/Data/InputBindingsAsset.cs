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
            private readonly InputBindingsAsset asset = new();
            
            public Builder()
            {
                asset.keyboard = new InputBindingData();
                asset.keyboardAlt = new InputBindingData();
                asset.gamepad = new InputBindingData();
                asset.gamepadAlt = new InputBindingData();
            }
        
            public Builder WithKeyboard(InputBindingData data)
            {
                asset.keyboard = data;
                return this;
            }
            
            public Builder WithKeyboardAlt(InputBindingData data)
            {
                asset.keyboardAlt = data;
                return this;
            }
            
            public Builder WithGamepad(InputBindingData data)
            {
                asset.gamepad = data;
                return this;
            }
            
            public Builder WithGamepadAlt(InputBindingData data)
            {
                asset.gamepadAlt = data;
                return this;
            }
            
            public Builder AsCopy(InputBindingsAsset asset)
            {
                this.asset.keyboard = asset.keyboard;
                this.asset.keyboardAlt = asset.keyboardAlt;
                this.asset.gamepad = asset.gamepad;
                this.asset.gamepadAlt = asset.gamepadAlt;
                return this;
            }
        
            public InputBindingsAsset Build() => asset;
        }
    }
}