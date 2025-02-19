using System.Collections.Generic;
using RedRats.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Objects;
using Rogium.Editors.Packs;
using Rogium.Editors.Rooms;
using Rogium.Gameplay.Core;
using Rogium.Gameplay.Core.Lighting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rogium.Gameplay.DataLoading
{
    /// <summary>
    /// Controls and decides, which room will be loaded next.
    /// </summary>
    public class RoomLoader : MonoBehaviour
    {
        [SerializeField] private Vector3Int originPos;
        [SerializeField, FormerlySerializedAs("tilemapBuilder")] private TileMapDataBuilder tileBuilder;
        [SerializeField] private TileMapDataBuilder decorBuilder;
        [SerializeField] private ObjectMapDataBuilder objectBuilder;
        [SerializeField] private EnemyMapDataBuilder enemyBuilder;
        
        private RoomLight roomLight;
        private IList<ObjectAsset> objects;

        private PackAsset dataPack;

        public void Init()
        {
            roomLight = RoomLight.Instance;
            objects = InternalLibraryOverseer.Instance.Objects;
            dataPack = GameplayOverseerMono.Instance.CurrentCampaign.DataPack;
        }

        /// <summary>
        /// Loads the next room.
        /// </summary>
        /// <param name="roomIndex">The index of the room to load.</param>
        public void LoadNext(int roomIndex)
        {
            RoomAsset room = dataPack.Rooms[roomIndex];
            tileBuilder.Load(originPos, room.TileGrid, dataPack.Tiles);
            decorBuilder.Load(originPos, room.DecorGrid, dataPack.Tiles);
            objectBuilder.Load(originPos, room.ObjectGrid, objects);
            enemyBuilder.Load(originPos, room.EnemyGrid, dataPack.Enemies);

            roomLight.Construct(ColorUtils.ConvertTo01(room.Lightness), room.LightnessColor);
        }

        /// <summary>
        /// Clears all data of the previous room.
        /// </summary>
        public void Clear()
        {
            tileBuilder.Clear();
            decorBuilder.Clear();
            objectBuilder.Clear();
            enemyBuilder.Clear();
        }
    }
}