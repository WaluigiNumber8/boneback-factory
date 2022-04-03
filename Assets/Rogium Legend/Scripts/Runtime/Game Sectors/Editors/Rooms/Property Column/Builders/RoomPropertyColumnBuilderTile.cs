using System.Collections.Generic;
using BoubakProductions.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Tiles;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Editors.Rooms.PropertyColumn
{
    /// <summary>
    /// Builds the Room Property Column for tiles.
    /// </summary>
    public class RoomPropertyColumnBuilderTile : UIPropertyContentBuilderBaseColumn1
    {
        private readonly IList<string> tileTypes;

        public RoomPropertyColumnBuilderTile(Transform contentMain) : base(contentMain)
        {
            tileTypes = EnumUtils.ToStringList(typeof(TileType));
        }

        /// <summary>
        /// Build properties.
        /// </summary>
        /// <param name="data">The data of the individual asset.</param>
        public void Build(AssetData data)
        {
            EmptyContent();
            
            b.BuildDropdown("Type", tileTypes, data.Parameters.intValue1, contentMain, data.UpdateIntValue1, true);
        }

    }
}