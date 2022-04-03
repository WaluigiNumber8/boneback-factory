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

            difficulties = new string[5];
            difficulties[0] = "Level 1";
            difficulties[1] = "Level 2";
            difficulties[2] = "Level 3";
            difficulties[3] = "Level 4";
            difficulties[4] = "Level 5";
        }

        /// <summary>
        /// Build setting properties for a room.
        /// </summary>
        /// <param name="content">The content to build under.</param>
        /// <param name="data">The data to use.</param>
        /// <param name="clearContent">If on, all of contents children will be killed before building properties.</param>
        public void Build(Transform content, RoomAsset data, bool clearContent)
        {
            if (clearContent) content.gameObject.KillChildren();
            b.BuildDropdown("Type", Enum.GetNames(typeof(RoomType)), (int) data.Type, content, data.UpdateType);
            b.BuildDropdown("Tier", difficulties, data.DifficultyLevel, content, data.UpdateDifficultyLevel);
        }
        
    }
}