using Rogium.Editors.Palettes;
using Rogium.Editors.Sprites;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// An action that switches the editor's currently used palette.
    /// </summary>
    public class SwitchSpriteEditorPaletteAction : IAction
    {
        private readonly SpriteEditorOverseerMono editor;
        private readonly PaletteAsset value;
        private readonly PaletteAsset lastValue;
        
        public SwitchSpriteEditorPaletteAction(PaletteAsset value, PaletteAsset lastValue)
        {
            this.editor = SpriteEditorOverseerMono.GetInstance();
            this.value = value;
            this.lastValue = lastValue;
        }
        
        public void Execute() => editor.SwitchPalette(value);

        public void Undo() => editor.SwitchPalette(lastValue);

        public bool NothingChanged() => value.Equals(lastValue);

        public override string ToString() => $"Sprite Editor - Switched palette from {lastValue} to {value}";

        public object AffectedConstruct { get => editor; }
        public object Value { get => value; }
        public object LastValue { get => lastValue; }
    }
}