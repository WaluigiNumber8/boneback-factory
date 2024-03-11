using Rogium.Editors.Tiles;

namespace Rogium.Gameplay.TilemapInteractions
{
    /// <summary>
    /// Holds data for a floor tile.
    /// </summary>
    public struct FloorTileInfo
    {
        public TerrainType terrainType;
        
        public FloorTileInfo(TerrainType terrainType)
        {
            this.terrainType = terrainType;
        }
    }
}