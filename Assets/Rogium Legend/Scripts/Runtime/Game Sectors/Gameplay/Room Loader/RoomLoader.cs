using System;
using System.Collections;
using System.Collections.Generic;
using BoubakProductions.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Objects;
using Rogium.Editors.Packs;
using Rogium.Editors.Rooms;
using Rogium.Gameplay.AssetRandomGenerator;
using Rogium.Gameplay.Core;
using UnityEngine;

namespace Rogium.Gameplay.DataLoading
{
    /// <summary>
    /// Controls and decides, which room will be loaded next.
    /// </summary>
    public class RoomLoader : MonoBehaviour
    {
        [SerializeField] private Vector3Int originPos;
        [SerializeField] private TileMapDataBuilder tilemapBuilder;
        [SerializeField] private ObjectMapDataBuilder objectBuilder;
        [SerializeField] private EnemyMapDataBuilder enemyBuilder;
        
        private IList<ObjectAsset> objects;

        private PackAsset dataPack;
        private RRG rrg;

        public void Init()
        {
            objects = InternalLibraryOverseer.GetInstance().GetObjectsCopy();
            dataPack = GameplayOverseerMono.GetInstance().CurrentCampaign.DataPack;
            rrg = new RRG(dataPack.Rooms, 0);
        }

        /// <summary>
        /// Loads the next room based on <see cref="RoomType"/>.
        /// </summary>
        /// <param name="type">The type of room to load next.</param>
        /// <param name="difficultyTier">The difficulty tier of the room.</param>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when the <see cref="RRG"/> does not support the given type.</exception>
        public void LoadNext(RoomType type, int difficultyTier) => LoadRoom(rrg.LoadNext(type, difficultyTier));

        /// <summary>
        /// Clears all data of the previous room.
        /// </summary>
        public void Clear()
        {
            tilemapBuilder.Clear();
            objectBuilder.Clear();
            enemyBuilder.Clear();
        }
        
        /// <summary>
        /// Loads the next room.
        /// </summary>
        /// <param name="roomIndex">The index of the room to load.</param>
        private void LoadRoom(int roomIndex)
        {
            SafetyNet.EnsureIndexWithingCollectionRange(roomIndex, dataPack.Rooms, "List of Rooms");
            
            RoomAsset room = dataPack.Rooms[roomIndex];
            tilemapBuilder.Load(originPos, room.TileGrid, dataPack.Tiles);
            objectBuilder.Load(originPos, room.ObjectGrid, objects);
            enemyBuilder.Load(originPos, room.EnemyGrid, dataPack.Enemies);
        }
    }
}