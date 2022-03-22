using System;
using System.Collections.Generic;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Editors.Rooms
{
    public class RoomSettingsBuilder
    {
        private readonly UIPropertyBuilder b;
        private readonly IList<string> difficulties;

        public RoomSettingsBuilder()
        {
            b = UIPropertyBuilder.GetInstance();
            
            difficulties = new List<string>();
            difficulties.Add("Level 1");
            difficulties.Add("Level 2");
            difficulties.Add("Level 3");
            difficulties.Add("Level 4");
            difficulties.Add("Level 5");
        }
        
        /// <summary>
        /// Build setting properties for a room.
        /// </summary>
        /// <param name="content">The content to build under.</param>
        /// <param name="data">The data to use.</param>
        public void Build(Transform content, RoomAsset data)
        {
            b.BuildDropdown("Type", Enum.GetNames(typeof(RoomType)), (int) data.Type, content, data.UpdateType);
            b.BuildDropdown("Tier", difficulties, data.DifficultyLevel, content, data.UpdateDifficultyLevel);
        }
        
    }
}