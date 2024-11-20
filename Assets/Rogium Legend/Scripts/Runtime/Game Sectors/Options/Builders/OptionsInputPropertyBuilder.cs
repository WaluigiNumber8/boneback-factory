using Rogium.Systems.Input;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Builds properties for the Input section in the Options Menu.
    /// </summary>
    public class OptionsInputPropertyBuilder : UIPropertyContentBuilderBaseColumn2<GameDataAsset>
    {
        private readonly InputSystem input;

        public OptionsInputPropertyBuilder(Transform contentMain, Transform contentSecond) : base(contentMain, contentSecond)
        {
            input = InputSystem.GetInstance();
        }

        public override void Build(GameDataAsset data)
        {
            Clear();
            
            BuildFor(InputDeviceType.Keyboard, contentMain);
            BuildFor(InputDeviceType.Gamepad, contentSecond);
        }

        private void BuildFor(InputDeviceType device, Transform parent)
        {
            b.BuildHeader("UI", parent);
            b.BuildInputBinding(input.UI.Click.Action, device, parent);
            b.BuildInputBinding(input.UI.ClickAlternative.Action, device, parent);

            b.BuildHeader("Player", parent);
            b.BuildInputBinding(input.Player.Movement.Action, device, parent);
            b.BuildInputBinding(input.Player.ButtonMain.Action, device, parent);
            b.BuildInputBinding(input.Player.ButtonMainAlt.Action, device, parent);
            b.BuildInputBinding(input.Player.ButtonSub.Action, device, parent);
            b.BuildInputBinding(input.Player.ButtonSubAlt.Action, device, parent);
            b.BuildInputBinding(input.Player.ButtonDash.Action, device, parent);
            b.BuildInputBinding(input.Player.ButtonDashAlt.Action, device, parent);
        }
    }
}