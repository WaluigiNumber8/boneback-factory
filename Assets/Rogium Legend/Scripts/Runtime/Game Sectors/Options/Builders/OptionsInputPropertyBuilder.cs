using Rogium.Systems.Input;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Builds properties for the Input section in the Options Menu.
    /// </summary>
    public class OptionsInputPropertyBuilder : UIPropertyContentBuilderBaseColumn2<InputBindingsAsset>
    {
        private readonly InputSystem input;

        public OptionsInputPropertyBuilder(Transform contentMain, Transform contentSecond) : base(contentMain, contentSecond)
        {
            input = InputSystem.GetInstance();
        }

        public override void BuildInternal(InputBindingsAsset asset)
        {
            BuildFor(InputDeviceType.Keyboard, contentMain);
            BuildFor(InputDeviceType.Gamepad, contentSecond);
        }

        private void BuildFor(InputDeviceType device, Transform parent)
        {
            //TODO: Enable input bindings when implemented
            b.BuildHeader("UI", parent);
            // b.BuildInputBinding(input.UI.Navigate.Action, device, parent);
            b.BuildInputBinding(input.UI.Select.Action, device, parent);
            b.BuildInputBinding(input.UI.Cancel.Action, device, parent);
            b.BuildInputBinding(input.UI.ContextSelect.Action, device, parent);
            // b.BuildInputBinding(input.UI.ShowTooltip.Action, device, parent);

            b.BuildHeader("Player", parent);
            b.BuildInputBinding(input.Player.Movement.Action, device, parent);
            b.BuildInputBinding(input.Player.ButtonMain.Action, device, parent);
            b.BuildInputBinding(input.Player.ButtonMainAlt.Action, device, parent);
            b.BuildInputBinding(input.Player.ButtonSub.Action, device, parent);
            b.BuildInputBinding(input.Player.ButtonSubAlt.Action, device, parent);
            b.BuildInputBinding(input.Player.ButtonDash.Action, device, parent);
            b.BuildInputBinding(input.Player.ButtonDashAlt.Action, device, parent);
            b.BuildInputBinding(input.Pause.Pause.Action, device, parent);
        }
    }
}