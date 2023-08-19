using System;
using Rogium.Options.OptionControllers;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Builds the properties of the options menu.
    /// </summary>
    public class OptionsGraphicsPropertyBuilder : UIPropertyContentBuilderBaseColumn1
    {
        private readonly GraphicsOptionsController graphics;
        public OptionsGraphicsPropertyBuilder(Transform contentMain, GraphicsOptionsController graphics) : base(contentMain)
        {
            this.graphics = graphics;
        }

        public void Build(GameDataAsset gameData)
        {
            Clear();
            b.BuildDropdown("Screen", Enum.GetNames(typeof(FullScreenMode)), (int)gameData.ScreenMode, contentMain, (value) =>
            {
                gameData.UpdateScreenMode(value);
                graphics.SetScreen((FullScreenMode) value);
            });
        }
    }
}