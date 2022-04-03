using System;
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

        private PackAsset dataPack;
        private RRG rrg;

        private IList<ObjectAsset> objects;

        private void Start()
        {
            objects = InternalLibraryOverseer.GetInstance().GetObjectsCopy();
            dataPack = GameplayOverseerMono.GetInstance().CurrentCampaign.DataPack;
            rrg = new RRG(dataPack.Rooms);
        }

        /// <summary>
        /// Loads the next room based on <see cref="RoomType"/>.
        /// </summary>
        /// <param name="type">The type of room to load next.</param>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when the <see cref="RRG"/> does not support the given type.</exception>
        public void LoadNext(RoomType type)
        {
            switch (type)
            {
                case RoomType.Normal:
                    LoadNextNormalRoom();
                    break;
                case RoomType.Rare:
                    LoadNextRareRoom();
                    break;
                case RoomType.Entrance:
                    LoadEntranceRoom();
                    break;
                case RoomType.Shop:
                    LoadShopRoom();
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"The Room Type of '{type}' is not supported by the RRG.");
            }
        }
        
        /// <summary>
        /// Loads the next normal type room.
        /// </summary>
        public void LoadNextNormalRoom() => LoadRoom(rrg.NextNormalRoom());

        /// <summary>
        /// Loads the next rare type room.
        /// </summary>
        public void LoadNextRareRoom() => LoadRoom(rrg.NextRareRoom());

        /// <summary>
        /// Loads the entrance room, chosen for this campaign.
        /// </summary>
        public void LoadEntranceRoom() => LoadRoom(rrg.ChosenEntranceRoom());

        /// <summary>
        /// Loads the shop room, chosen for this campaign.
        /// </summary>
        public void LoadShopRoom() => LoadRoom(rrg.ChosenShopRoom());

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