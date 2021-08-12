using RogiumLegend.Global.SafetyChecks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RogiumLegend.Editors.RoomData
{
    public class RoomAsset
    {
        private string roomName;
        private int difficultyLevel;

        public RoomAsset()
        {
            roomName = "New Room";
            difficultyLevel = 1;
        }

        public RoomAsset(string roomName, int difficultyLevel)
        {
            SafetyNet.EnsureIntIsBiggerThan(difficultyLevel, 0, "New Room Difficulty Level");

            this.roomName = roomName;
            this.difficultyLevel = difficultyLevel;
        }

        public string RoomName { get => roomName;  }
        public int DifficultyLevel { get => difficultyLevel; }
    }
}