using Rogium.Systems.Input;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Builds properties for the Input section in the Options Menu.
    /// </summary>
    public class OptionsShortcutPropertyBuilder : IPContentBuilderBaseColumn2<ShortcutBindingsAsset>
    {
        private readonly InputSystem input;

        public OptionsShortcutPropertyBuilder(Transform contentMain, Transform contentSecond) : base(contentMain, contentSecond)
        {
            input = InputSystem.Instance;
        }

        public override void BuildInternal(ShortcutBindingsAsset asset)
        {
            BuildFor(InputDeviceType.Keyboard, contentMain);
            BuildFor(InputDeviceType.Gamepad, contentSecond);
        }

        private void BuildFor(InputDeviceType device, Transform parent)
        {
            b.BuildHeader("General", parent);
            b.BuildInputBinding(input.Shortcuts.Undo.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.Redo.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.Save.Action, device, parent);
            b.BuildHeader("General Selection", parent);
            b.BuildInputBinding(input.Shortcuts.NewAsset.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.Edit.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.EditProperties.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.Delete.Action, device, parent);
            b.BuildHeader("Asset Selection", parent);
            b.BuildInputBinding(input.Shortcuts.ShowPalettes.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.ShowSprites.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.ShowWeapons.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.ShowProjectiles.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.ShowEnemies.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.ShowRooms.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.ShowTiles.Action, device, parent);
            b.BuildHeader("Campaign Selection", parent);
            b.BuildInputBinding(input.Shortcuts.SwitchLeft.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.SwitchRight.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.RefreshCurrent.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.RefreshAll.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.Play.Action, device, parent);
            b.BuildHeader("Campaign Editor", parent);
            b.BuildInputBinding(input.Shortcuts.SelectAll.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.DeselectAll.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.SelectRandom.Action, device, parent);
            b.BuildHeader("Drawing Editors", parent);
            b.BuildInputBinding(input.Shortcuts.SelectionTool.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.BrushTool.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.EraserTool.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.FillTool.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.PickerTool.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.ClearCanvas.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.ToggleGrid.Action, device, parent);
            b.BuildHeader("Sprite Editor", parent);
            b.BuildInputBinding(input.Shortcuts.ChangePalette.Action, device, parent);
            b.BuildHeader("Room Editor", parent);
            b.BuildInputBinding(input.Shortcuts.TilesLayer.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.DecorLayer.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.ObjectsLayer.Action, device, parent);
            b.BuildInputBinding(input.Shortcuts.EnemiesLayer.Action, device, parent);
        }
    }
}