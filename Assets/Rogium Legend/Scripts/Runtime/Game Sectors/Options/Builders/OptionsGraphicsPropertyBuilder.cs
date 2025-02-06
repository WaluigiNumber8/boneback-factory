using System;
using System.Collections.Generic;
using System.Linq;
using Rogium.Options.OptionControllers;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Builds properties for the Graphics section in the Options Menu.
    /// </summary>
    public class OptionsGraphicsPropertyBuilder : UIPropertyContentBuilderBaseColumn1<PreferencesAsset>
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

        public override void BuildInternal(PreferencesAsset preferences)
        {
            BuildResolutionsDropdown(preferences);
            b.BuildDropdown("Screen", Enum.GetNames(typeof(ScreenType)), (int)preferences.ScreenMode, contentMain, value =>
            {
                preferences.UpdateScreenMode(value);
                graphics.UpdateScreen((ScreenType) value);
            });
            b.BuildToggle("VSync", preferences.VSync, contentMain, value =>
            {
                preferences.UpdateVSync(value);
                graphics.UpdateVSync(value);
            });
        }

        /// <summary>
        /// Builds the dropdown for resolutions.
        /// </summary>
        /// <param name="preferences">The game settings asset.</param>
        private void BuildResolutionsDropdown(PreferencesAsset preferences)
        {
            Resolution startingResolution;
            try
            {
                startingResolution = resolutions.First(r => r.width == preferences.Resolution.x && r.height == preferences.Resolution.y);
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
                preferences.UpdateResolution(resolution);
                graphics.UpdateResolution(resolution);
            });
        }
    }
}