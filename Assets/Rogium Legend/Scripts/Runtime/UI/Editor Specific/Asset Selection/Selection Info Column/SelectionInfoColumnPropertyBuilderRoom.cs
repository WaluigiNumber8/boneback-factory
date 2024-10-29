using Rogium.Editors.Rooms;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.UserInterface.Editors.AssetSelection
{
    /// <summary>
    /// Builds the <see cref="SelectionInfoColumn"/> for a <see cref="RoomAsset"/>.
    /// </summary>
    public class SelectionInfoColumnPropertyBuilderRoom : UIPropertyContentBuilderBaseColumn1<RoomAsset>
    {
        public SelectionInfoColumnPropertyBuilderRoom(Transform contentMain) : base(contentMain) { }

        /// <summary>
        /// Build <see cref="SelectionInfoColumn"/> properties for a room.
        /// </summary>
        /// <param name="asset">The room to build for.</param>
        public override void Build(RoomAsset asset)
        {
            Clear();
            b.BuildPlainText("Type", asset.Type.ToString(), contentMain);
            b.BuildPlainText("Difficulty", asset.DifficultyLevel.ToString(), contentMain);
        }
        
    }
}