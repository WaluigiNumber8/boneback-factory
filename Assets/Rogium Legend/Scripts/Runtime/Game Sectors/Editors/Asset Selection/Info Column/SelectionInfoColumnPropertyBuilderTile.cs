using Rogium.Editors.Tiles;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// Builds the <see cref="SelectionInfoColumn"/> for a <see cref="TileAsset"/>.
    /// </summary>
    public class SelectionInfoColumnPropertyBuilderTile : UIPropertyContentBuilderBaseColumn1<TileAsset>
    {
        public SelectionInfoColumnPropertyBuilderTile(Transform contentMain) : base(contentMain) { }

        /// <summary>
        /// Build <see cref="SelectionInfoColumn"/> properties for a tile.
        /// </summary>
        /// <param name="asset">The tile to build for.</param>
        public override void Build(TileAsset asset)
        {
            Clear();
            b.BuildPlainText("Type", asset.Type.ToString(), contentMain);
            b.BuildPlainText("Layer", asset.LayerType.ToString(), contentMain);
            b.BuildPlainText("Terrain", asset.TerrainType.ToString(), contentMain);
        }
    }
}