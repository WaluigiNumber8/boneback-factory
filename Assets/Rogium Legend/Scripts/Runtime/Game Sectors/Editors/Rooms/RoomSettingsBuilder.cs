using System;
using Rogium.Editors.Core.Defaults;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Editors.Rooms
{
    /// <summary>
    /// Builds settings for a room.
    /// </summary>
    public class RoomSettingsBuilder : UIPropertyContentBuilderBaseColumn1<RoomAsset>
    {
        public RoomSettingsBuilder(Transform contentMain) : base(contentMain) { }

        /// <summary>
        /// Build only teh essential setting properties for a room.
        /// </summary>
        /// <param name="asset">The data to use.</param>
        public void BuildEssentials(RoomAsset asset)
        {
            b.BuildDropdown("Type", Enum.GetNames(typeof(RoomType)), (int) asset.Type, contentMain, asset.UpdateType);
            b.BuildDropdown("Tier", EditorDefaults.Instance.RoomDifficultyTitles, asset.DifficultyLevel, contentMain, asset.UpdateDifficultyLevel);
        }
        
        public override void BuildInternal(RoomAsset asset)
        {
            BuildEssentials(asset);
            b.BuildSlider("Light", 0, 255, asset.Lightness, contentMain, l => asset.UpdateLightness((int)l));
            b.BuildColorField("Light Color", asset.LightnessColor, contentMain, asset.UpdateLightnessColor);
        }
    }
}