using System;
using System.Collections.Generic;
using BoubakProductions.Core;
using Rogium.Core;
using Rogium.Editors.Objects;
using Rogium.Gameplay.InteractableObjects;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Gameplay.DataLoading
{
    /// <summary>
    /// Handles loading interactable object data into rooms.
    /// </summary>
    public class ObjectMapLoader : MonoBehaviour
    {
        [SerializeField] private Transform content;

        /// <summary>
        /// Load a new grid tilemap of interactable objects.
        /// </summary>
        /// <param name="offsetPos">The offset from center, where to start spawning.</param>
        /// <param name="IDGrid">The grid object IDs.</param>
        /// <param name="dataList">The list of assets to take data from.</param>
        public void Load(Vector3Int offsetPos, ObjectGrid<string> IDGrid, IList<ObjectAsset> dataList)
        {
            content.gameObject.KillChildren();
            AssetUtils.UpdateFromGridByList(IDGrid, dataList,
                                            (x, y, asset) => Spawn(offsetPos, x, y, asset),
                                            (x, y) => throw new InvalidOperationException("This interactable object does not exist."));
        }

        /// <summary>
        /// Spawns a new interactable object and fills it with data.
        /// </summary>
        /// <param name="offsetPos">The offset position from center.</param>
        /// <param name="x">The X offset from the content position.</param>
        /// <param name="y">The Y offset from the content position.</param>
        /// <param name="data">The data to spawn the object with.</param>
        private void Spawn(Vector3Int offsetPos, int x, int y, ObjectAsset data)
        {
            IInteractObject en = Instantiate(data.Prefab, offsetPos + new Vector3(x, y, 0) + new Vector3(0.5f, 0.5f, 0), Quaternion.identity, content).GetComponent<IInteractObject>();
            en.Construct(data);
        }

    }
}