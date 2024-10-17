using System;
using System.Collections.Generic;
using Rogium.Editors.Core;
using Rogium.Editors.Tiles;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Editors.Rooms.PropertyColumn
{
    /// <summary>
    /// Builds the Room Property Column for tiles.
    /// </summary>
    public class RoomPropertyColumnBuilderTile : UIPropertyContentBuilderBaseColumn1<AssetData>
    {
        public RoomPropertyColumnBuilderTile(Transform contentMain) : base(contentMain) { }

        /// <summary>
        /// Build properties.
        /// </summary>
        /// <param name="data">The data of the individual asset.</param>
        public override void Build(AssetData data)
        {
            Clear();
            
            b.BuildPlainText("Layer", Enum.GetNames(typeof(TileLayerType))[data.Parameters.intValue1], contentMain);
        }

    }
}