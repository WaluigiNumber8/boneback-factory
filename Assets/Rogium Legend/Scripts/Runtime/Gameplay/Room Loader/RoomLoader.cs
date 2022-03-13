using Rogium.Editors.Packs;
using Rogium.Editors.Rooms;
using UnityEngine;

namespace Rogium.Gameplay.DataLoading
{
    /// <summary>
    /// Turns a <see cref="RoomAsset"/> into a Gameplay Form.
    /// </summary>
    public class RoomLoader : MonoBehaviour
    {
        [SerializeField] private Vector3Int originPos;
        [SerializeField] private TileMapLoader tilemapLoader;
        [SerializeField] private EnemyMapLoader enemyLoader;
        

        /// <summary>
        /// Loads the room with Tile, Object & Enemy data.
        /// </summary>
        /// <param name="room">The room to load.</param>
        /// <param name="dataPack">The pack to take data from.</param>
        public void Load(RoomAsset room, PackAsset dataPack)
        {
            tilemapLoader.Load(originPos, room.TileGrid, dataPack.Tiles);
            enemyLoader.Load(originPos, room.EnemyGrid, dataPack.Enemies);
        }
        
        
    }
}