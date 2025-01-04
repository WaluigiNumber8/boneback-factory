using Rogium.Systems.Input;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Builds properties for the Input section in the Options Menu.
    /// </summary>
    public class OptionsShortcutPropertyBuilder : UIPropertyContentBuilderBaseColumn2<PreferencesAsset>
    {
        private readonly InputSystem input;

        public OptionsShortcutPropertyBuilder(Transform contentMain, Transform contentSecond) : base(contentMain, contentSecond)
        {
            input = InputSystem.GetInstance();
        }

        public override void Build(PreferencesAsset asset)
        {
            Clear();
            
            BuildFor(InputDeviceType.Keyboard, contentMain);
            BuildFor(InputDeviceType.Gamepad, contentSecond);
        }

        private void BuildFor(InputDeviceType device, Transform parent)
        {
            b.BuildHeader("General", parent);
            // b.BuildInputBinding(input.Shortcuts.Undo.Action, device, parent);
            // b.BuildInputBinding(input.Shortcuts.Redo.Action, device, parent);
            // b.BuildInputBinding(input.Shortcuts.Save.Action, device, parent);
            // b.BuildInputBinding(input.Shortcuts.Cancel.Action, device, parent);
        }
    }
}