using Rogium.Editors.Rooms;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.UserInterface.Editors.AssetSelection
{
    /// <summary>
    /// Builds the <see cref="SelectionInfoColumn"/> for a <see cref="RoomAsset"/>.
    /// </summary>
    public class SelectionInfoColumnPropertyBuilderRoom : UIPropertyContentBuilderBaseColumn1
    {
        public SelectionInfoColumnPropertyBuilderRoom(Transform contentMain) : base(contentMain) { }

        /// <summary>
        /// Build <see cref="SelectionInfoColumn"/> properties for a room.
        /// </summary>
        /// <param name="room">The room to build for.</param>
        public void Build(RoomAsset room)
        {
            Clear();
            b.BuildPlainText("Type", room.Type.ToString(), contentMain);
            b.BuildPlainText("Difficulty", room.DifficultyLevel.ToString(), contentMain);
        }
        
    }
}