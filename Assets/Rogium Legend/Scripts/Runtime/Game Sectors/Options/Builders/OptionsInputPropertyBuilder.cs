using Rogium.Systems.Input;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Builds properties for the Input section in the Options Menu.
    /// </summary>
    public class OptionsInputPropertyBuilder : UIPropertyContentBuilderBaseColumn1<GameDataAsset>
    {
        private readonly InputSystem input;

        public OptionsInputPropertyBuilder(Transform contentMain) : base(contentMain) => input = InputSystem.GetInstance();

        public override void Build(GameDataAsset data)
        {
            Clear();
            
            b.BuildHeader("UI", contentMain);
            b.BuildInputBinding(input.UI.Click.Action, InputDeviceType.Keyboard, contentMain);
            b.BuildInputBinding(input.UI.ClickAlternative.Action, InputDeviceType.Keyboard, contentMain);
            
            b.BuildHeader("Player", contentMain);
            b.BuildInputBinding(input.Player.Movement.Action, InputDeviceType.Keyboard, contentMain);
            b.BuildInputBinding(input.Player.ButtonMain.Action, InputDeviceType.Keyboard, contentMain);
            b.BuildInputBinding(input.Player.ButtonMainAlt.Action, InputDeviceType.Keyboard, contentMain);
            b.BuildInputBinding(input.Player.ButtonSub.Action, InputDeviceType.Keyboard, contentMain);
            b.BuildInputBinding(input.Player.ButtonSubAlt.Action, InputDeviceType.Keyboard, contentMain);
            b.BuildInputBinding(input.Player.ButtonDash.Action, InputDeviceType.Keyboard, contentMain);
            b.BuildInputBinding(input.Player.ButtonDashAlt.Action, InputDeviceType.Keyboard, contentMain);
        }
    }
}