using Rogium.Editors.Tiles;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Editors.AssetSelection
{
    /// <summary>
    /// Builds the <see cref="SelectionInfoColumn"/> for a <see cref="TileAsset"/>.
    /// </summary>
    public class SelectionInfoColumnPropertyBuilderTile : IPContentBuilderBaseColumn1<TileAsset>
    {
        public SelectionInfoColumnPropertyBuilderTile(Transform contentMain) : base(contentMain) { }

        /// <summary>
        /// Build <see cref="SelectionInfoColumn"/> properties for a tile.
        /// </summary>
        /// <param name="asset">The tile to build for.</param>
        public override void BuildInternal(TileAsset asset)
        {
            b.BuildPlainText("Type", asset.Type.ToString(), contentMain);
            b.BuildPlainText("Layer", asset.LayerType.ToString(), contentMain);
            b.BuildPlainText("Terrain", asset.TerrainType.ToString(), contentMain);
        }
    }
}