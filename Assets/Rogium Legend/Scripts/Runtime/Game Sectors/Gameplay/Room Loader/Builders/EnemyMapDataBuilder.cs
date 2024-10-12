using System.Collections.Generic;
using RedRats.Core;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Enemies;
using Rogium.Editors.Weapons;
using Rogium.Gameplay.Core;
using Rogium.Gameplay.Entities.Enemy;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Gameplay.DataLoading
{
    /// <summary>
    /// Handles loading enemy data into rooms.
    /// </summary>
    public class EnemyMapDataBuilder : MonoBehaviour
    {
        [SerializeField] private EnemyController vessel;
        [SerializeField] private GameObject missingEnemyObject;
        [SerializeField] private Transform content;

        private IList<WeaponAsset> weapons;

        private void Start() => weapons = GameplayOverseerMono.GetInstance().CurrentCampaign.DataPack.Weapons;

        /// <summary>
        /// Load a new grid tilemap of enemies.
        /// </summary>
        /// <param name="offsetPos">The offset from center, where to start spawning.</param>
        /// <param name="IDGrid">The grid enemy IDs.</param>
        /// <param name="dataList">The list of assets to take data from.</param>
        public void Load(Vector3Int offsetPos, ObjectGrid<AssetData> IDGrid, IList<EnemyAsset> dataList)
        {
            Clear();
            AssetUtils.UpdateFromGridByList(IDGrid, dataList,
                                            (x, y, asset) => Spawn(offsetPos, x, y, asset),
                                            (x, y) => SpawnMissing(offsetPos, x, y));
        }

        /// <summary>
        /// Clears all objects.
        /// </summary>
        public void Clear() => content.KillChildren();
        
        /// <summary>
        /// Spawns a new enemy object and fills it with data.
        /// </summary>
        /// <param name="offsetPos">The offset position from center.</param>
        /// <param name="x">The X offset from the content position.</param>
        /// <param name="y">The Y offset from the content position.</param>
        /// <param name="data">The data to spawn the object with.</param>
        private void Spawn(Vector3Int offsetPos, int x, int y, EnemyAsset data)
        {
            EnemyController en = Instantiate(vessel, offsetPos + new Vector3(x, y, 0) + new Vector3(0.5f, 0.5f, 0), Quaternion.identity, content);
            IList<WeaponAsset> weapons = data.WeaponIDs.ConvertToAssets(this.weapons);
            en.Construct(data, weapons);
        }

        /// <summary>
        /// Spawns the missing enemy object.
        /// </summary>
        /// <param name="offsetPos">The offset position from center.</param>
        /// <param name="x">The X offset from the content position.</param>
        /// <param name="y">The Y offset from the content position.</param>
        private void SpawnMissing(Vector3Int offsetPos, int x, int y)
        {
            Instantiate(missingEnemyObject, offsetPos + new Vector3(x, y, 0) + new Vector3(0.5f, 0.5f, 0), Quaternion.identity, content);
        }
        
    }
}