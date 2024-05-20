using System;
using Rogium.Editors.Palettes;
using Rogium.Editors.Sprites;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// An action that switches the editor's currently used palette.
    /// </summary>
    public class SwitchSpriteEditorPaletteAction : ActionBase<PaletteAsset>
    {
        private readonly SpriteEditorOverseerMono editor;
        private readonly PaletteAsset value;
        private readonly PaletteAsset lastValue;
        
        public SwitchSpriteEditorPaletteAction(PaletteAsset value, PaletteAsset lastValue, Action<PaletteAsset> fallback) : base(fallback)
        {
            this.editor = SpriteEditorOverseerMono.GetInstance();
            this.value = value;
            this.lastValue = lastValue;
        }
        
        protected override void ExecuteSelf() => editor.SwitchPalette(value);

        protected override void UndoSelf() => editor.SwitchPalette(lastValue);

        public override bool NothingChanged() => value.Equals(lastValue);

        public override string ToString() => $"Sprite Editor - Switched palette from {lastValue} to {value}";

        public override object AffectedConstruct { get => editor; }
        public override PaletteAsset Value { get => value; }
        public override PaletteAsset LastValue { get => lastValue; }
    }
}