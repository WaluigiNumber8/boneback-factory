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
        public event Action<Transform, TerrainType> OnFootstep;
        
        [SerializeField] private Tilemap floorMap;
        
        private IDictionary<Tile, FloorTileInfo> floorTiles;

        private void Start()
        {
            IList<TileAsset> allTiles = GameplayOverseerMono.GetInstance().CurrentCampaign.DataPack.Tiles;
            floorTiles = allTiles.Where(t => t.Tile != null).ToDictionary(t => t.Tile, t => new FloorTileInfo(t.TerrainType));
        }

        /// <summary>
        /// Triggers all floor effects for an object that stepped on a specific tile.
        /// </summary>
        public void TriggerFloorEffect(Transform entity)
        {
            Tile tile = GetTileFromCurrentPosition(entity.position);
            if (tile == null) return;
            
            //Apply effects
            FloorTileInfo data = floorTiles[tile];
            OnFootstep?.Invoke(entity, data.terrainType);
        }

        private Tile GetTileFromCurrentPosition(Vector3 worldPosition)
        {
            Vector3Int position = floorMap.WorldToCell(worldPosition);
            return (Tile)floorMap.GetTile(position);
        }
    }
}