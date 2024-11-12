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
        public OptionsInputPropertyBuilder(Transform contentMain) : base(contentMain) { }

        public override void Build(GameDataAsset data)
        {
            Clear();
            
            b.BuildHeader("UI", contentMain);
            b.BuildInputBinding(InputSystem.GetInstance().UI.ClickAlternative.Action, contentMain);
        }
    }
}