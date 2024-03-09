using System;
using System.Collections.Generic;
using System.Linq;
using Rogium.Editors.Tiles;
using Rogium.Gameplay.Core;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Rogium.Gameplay.TilemapInteractions
{
    /// <summary>
    /// Triggers floors effects for entities that stepped on the floor.
    /// </summary>
    public class FloorInteractionService : MonoBehaviour
    {
        public event Action<TerrainType> OnFootstep;
        
        [SerializeField] private Tilemap floorMap;
        
        private IDictionary<Tile, FloorTileInfo> floorTiles;

        private void Start()
        {
            IList<TileAsset> allTiles = GameplayOverseerMono.GetInstance().CurrentCampaign.DataPack.Tiles;
            floorTiles = allTiles.ToDictionary(t => t.Tile, t => new FloorTileInfo(t.TerrainType));
        }

        /// <summary>
        /// Triggers all floor effects for an entity that stepped on a specific tile.
        /// </summary>
        public void TriggerFloorEffect(Vector3 worldPosition)
        {
            Tile tile = GetTileFromCurrentPosition(worldPosition);
            FloorTileInfo data = floorTiles[tile];
            
            //Apply effects
            OnFootstep?.Invoke(data.terrainType);
        }

        private Tile GetTileFromCurrentPosition(Vector3 worldPosition)
        {
            Vector3Int position = floorMap.WorldToCell(worldPosition);
            return (Tile)floorMap.GetTile(position);
        }
    }
}