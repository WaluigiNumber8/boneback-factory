using System.Collections.Generic;
using Rogium.Editors.Core;
using Rogium.Editors.Objects;
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
        [SerializeField] private ObjectMapLoader objectLoader;
        [SerializeField] private EnemyMapLoader enemyLoader;

        private IList<ObjectAsset> objects;

        private void Awake() => objects = InternalLibraryOverseer.GetInstance().GetObjectsCopy();

        /// <summary>
        /// Loads the next room.
        /// </summary>
        /// <param name="room"></param>
        /// <param name="dataPack"></param>
        public void Load(RoomAsset room, PackAsset dataPack)
        {
            tilemapLoader.Load(originPos, room.TileGrid, dataPack.Tiles);
            objectLoader.Load(originPos, room.ObjectGrid, objects);
            enemyLoader.Load(originPos, room.EnemyGrid, dataPack.Enemies);
        }
    }
}