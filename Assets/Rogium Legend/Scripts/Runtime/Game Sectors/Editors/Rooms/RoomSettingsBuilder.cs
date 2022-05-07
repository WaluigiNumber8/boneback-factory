using System;
using BoubakProductions.Core;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Editors.Rooms
{
    public class RoomSettingsBuilder
    {
        private readonly UIPropertyBuilder b;
        private readonly string[] difficulties;

        public RoomSettingsBuilder()
        {
            b = UIPropertyBuilder.GetInstance();
            difficulties = new[]
            {
                "Level 1",
                "Level 2",
                "Level 3",
                "Level 4",
                "Level 5"
            };
        }

        /// <summary>
        /// Build only teh essential setting properties for a room.
        /// </summary>
        /// <param name="content">The content to build under.</param>
        /// <param name="asset">The data to use.</param>
        /// <param name="clearContent">If on, all of contents children will be killed before building properties.</param>
        public void BuildEssentials(Transform content, RoomAsset asset, bool clearContent)
        {
            if (clearContent) content.gameObject.KillChildren();
            b.BuildDropdown("Type", Enum.GetNames(typeof(RoomType)), (int) asset.Type, content, asset.UpdateType);
            b.BuildDropdown("Tier", difficulties, asset.DifficultyLevel, content, asset.UpdateDifficultyLevel);
        }
        
        /// <summary>
        /// Build setting properties for a room.
        /// </summary>
        /// <param name="content">The content to build under.</param>
        /// <param name="asset">The data to use.</param>
        /// <param name="clearContent">If on, all of contents children will be killed before building properties.</param>
        public void Build(Transform content, RoomAsset asset, bool clearContent)
        {
            BuildEssentials(content, asset, clearContent);
            b.BuildSlider("Light", 0, 255, asset.Lightness, content, l => asset.UpdateLightness((int)l));
        }
        
    }
}