using System;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Builds the properties of the options menu.
    /// </summary>
    public class OptionsGraphicsPropertyBuilder : UIPropertyContentBuilderBaseColumn1
    {
        public OptionsGraphicsPropertyBuilder(Transform contentMain) : base(contentMain) { }

        public void Build(GameDataAsset gameData)
        {
            b.BuildDropdown("Screen", Enum.GetNames(typeof(FullScreenMode)), (int)gameData.ScreenMode, contentMain, gameData.UpdateScreenMode);
        }
    }
}