using System;
using System.Collections.Generic;
using System.Linq;
using Rogium.Options.OptionControllers;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Builds the properties of the options menu.
    /// </summary>
    public class OptionsGraphicsPropertyBuilder : UIPropertyContentBuilderBaseColumn1<GameDataAsset>
    {
        private readonly GraphicsOptionsController graphics;
        
        private readonly IList<Resolution> resolutions;
        private readonly IList<string> resolutionStrings;

        public OptionsGraphicsPropertyBuilder(Transform contentMain, GraphicsOptionsController graphics) : base(contentMain)
        {
            this.graphics = graphics;
            
            resolutions = Screen.resolutions.Where(r => Math.Abs(r.refreshRateRatio.value - Screen.currentResolution.refreshRateRatio.value) < 0.001).Reverse().ToList();
            resolutionStrings = resolutions.Select(r => $"{r.width}x{r.height}").ToList();
        }

        public override void Build(GameDataAsset gameData)
        {
            Clear();

            BuildResolutionsDropdown(gameData);
            b.BuildDropdown("Screen", Enum.GetNames(typeof(ScreenType)), (int)gameData.ScreenMode, contentMain, value =>
            {
                gameData.UpdateScreenMode(value);
                graphics.UpdateScreen((ScreenType) value);
            });
            b.BuildToggle("VSync", gameData.VSync, contentMain, value =>
            {
                gameData.UpdateVSync(value);
                graphics.UpdateVSync(value);
            });
        }

        /// <summary>
        /// Builds the dropdown for resolutions.
        /// </summary>
        /// <param name="gameData">The game settings asset.</param>
        private void BuildResolutionsDropdown(GameDataAsset gameData)
        {
            Resolution startingResolution;
            try
            {
                startingResolution = resolutions.First(r => r.width == gameData.Resolution.x && r.height == gameData.Resolution.y);
            }
            catch (InvalidOperationException)
            {
                startingResolution = Screen.currentResolution;
            }


            int wantedIndex = resolutions.IndexOf(startingResolution);
            wantedIndex = (wantedIndex == -1) ? 0 : wantedIndex;
            
            b.BuildDropdown("Resolution", resolutionStrings, wantedIndex, contentMain, value =>
            {
                Resolution resolution = resolutions[value];
                gameData.UpdateResolution(resolution);
                graphics.UpdateResolution(resolution);
            });
        }
    }
}