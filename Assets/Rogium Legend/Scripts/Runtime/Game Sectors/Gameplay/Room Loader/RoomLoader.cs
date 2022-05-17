using System.Collections.Generic;
using RedRats.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Objects;
using Rogium.Editors.Packs;
using Rogium.Editors.Rooms;
using Rogium.Gameplay.Core;
using Rogium.Gameplay.Core.Lighting;
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
        [Space] 
        [SerializeField] private TilemapShadowCaster2D wallShadowCaster; 
        
        private RoomLight roomLight;
        private IList<ObjectAsset> objects;

        private PackAsset dataPack;

        public void Init()
        {
            roomLight = RoomLight.GetInstance();
            objects = InternalLibraryOverseer.GetInstance().GetObjectsCopy();
            dataPack = GameplayOverseerMono.GetInstance().CurrentCampaign.DataPack;
        }

        /// <summary>
        /// Loads the next room.
        /// </summary>
        /// <param name="roomIndex">The index of the room to load.</param>
        public void LoadNext(int roomIndex)
        {
            RoomAsset room = dataPack.Rooms[roomIndex];
            tilemapBuilder.Load(originPos, room.TileGrid, dataPack.Tiles);
            objectBuilder.Load(originPos, room.ObjectGrid, objects);
            enemyBuilder.Load(originPos, room.EnemyGrid, dataPack.Enemies);

            roomLight.UpdateIntensity(ColorUtils.ConvertTo01(room.Lightness));
            Invoke(nameof(GenerateShadows), 0.5f);
        }

        /// <summary>
        /// Clears all data of the previous room.
        /// </summary>
        public void Clear()
        {
            tilemapBuilder.Clear();
            objectBuilder.Clear();
            enemyBuilder.Clear();
        }
        
        private void GenerateShadows() => wallShadowCaster.Generate();
    }
}