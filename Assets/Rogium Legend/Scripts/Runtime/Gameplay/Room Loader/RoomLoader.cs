using Rogium.Editors.Packs;
using Rogium.Editors.Rooms;
using UnityEngine;

namespace Rogium.Gameplay.DataLoading
{
    /// <summary>
    /// Turns a <see cref="RoomAsset"/> into a Gameplay Form.
    /// </summary>
    public class RoomLoader
    {
        private readonly TilemapLoader tilemapLoader;
        
        public RoomLoader()
        {
            tilemapLoader = new TilemapLoader();
        }
        
        /// <summary>
        /// Loads the room with Tile, Object & Enemy data.
        /// </summary>
        /// <param name="room">The room to load.</param>
        /// <param name="data">The pack to take data from.</param>
        public void Load(TilemapLayer[] tilemaps, Vector3Int originPosition, RoomAsset room, PackAsset data)
        {
            //Clean the room
            ClearAllTiles(tilemaps);
            
            //Place new tiles.
            tilemapLoader.LoadTiles(tilemaps, room.TileGrid, originPosition, data.Tiles);
            
        }
        
        /// <summary>
        /// Cleans the entire room.
        /// </summary>
        /// <param name="tilemaps">Cleans all layers in the room.</param>
        private void ClearAllTiles(TilemapLayer[] tilemaps)
        {
            foreach (TilemapLayer layer in tilemaps)
            {
                layer.Tilemap.ClearAllTiles();
                layer.Positions.Clear();
            }
        }
    }
}