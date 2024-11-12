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
            b.BuildInputBinding(input.UI.Click.Action, contentMain);
            b.BuildInputBinding(input.UI.ClickAlternative.Action, contentMain);
            
            b.BuildHeader("Player", contentMain);
            b.BuildInputBinding(input.Player.ButtonMain.Action, contentMain);
            b.BuildInputBinding(input.Player.ButtonMainAlt.Action, contentMain);
            b.BuildInputBinding(input.Player.ButtonSub.Action, contentMain);
            b.BuildInputBinding(input.Player.ButtonSubAlt.Action, contentMain);
            b.BuildInputBinding(input.Player.ButtonDash.Action, contentMain);
            b.BuildInputBinding(input.Player.ButtonDashAlt.Action, contentMain);
        }
    }
}