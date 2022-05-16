using System;
using System.Collections.Generic;
using RedRats.Core;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Objects;
using Rogium.Gameplay.InteractableObjects;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Gameplay.DataLoading
{
    /// <summary>
    /// Handles loading interactable object data into rooms.
    /// </summary>
    public class ObjectMapDataBuilder : MonoBehaviour
    {
        [SerializeField] private Vector2 spawnOffset = new Vector2(0.5f, 0.5f);
        [SerializeField] private Transform content;

        /// <summary>
        /// Load a new grid tilemap of interactable objects.
        /// </summary>
        /// <param name="offsetPos">The offset from center, where to start spawning.</param>
        /// <param name="IDGrid">The grid object IDs.</param>
        /// <param name="dataList">The list of assets to take data from.</param>
        public void Load(Vector3Int offsetPos, ObjectGrid<AssetData> IDGrid, IList<ObjectAsset> dataList)
        {
            Clear();
            AssetUtils.UpdateFromGridByList(IDGrid, dataList,
                                            (x, y, asset, data) => Spawn(offsetPos, x, y, asset, data.Parameters),
                                            (x, y) => throw new InvalidOperationException("This interactable object does not exist."));
        }

        /// <summary>
        /// Clears all objects.
        /// </summary>
        public void Clear() => content.KillChildren();

        /// <summary>
        /// Spawns a new interactable object and fills it with data.
        /// </summary>
        /// <param name="offsetPos">The offset position from center.</param>
        /// <param name="x">The X offset from the content position.</param>
        /// <param name="y">The Y offset from the content position.</param>
        /// <param name="asset">The data to spawn the object with.</param>
        /// <param name="parameters">The parameter data of teh current object.</param>
        private void Spawn(Vector3Int offsetPos, int x, int y, ObjectAsset asset, ParameterInfo parameters)
        {
            Vector3 spawnPos = new Vector3(offsetPos.x + x + spawnOffset.x,
                                           offsetPos.y + y + spawnOffset.y);
            
            IInteractObject en = Instantiate(asset.Prefab, spawnPos, Quaternion.identity, content).GetComponent<IInteractObject>();
            en.Construct(asset, parameters);
        }

    }
}