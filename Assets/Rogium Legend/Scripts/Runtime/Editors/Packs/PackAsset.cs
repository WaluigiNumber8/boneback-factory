using System;
using System.Collections.Generic;
using UnityEngine;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.PaletteData;
using Rogium.Editors.RoomData;
using Rogium.Editors.TileData;

namespace Rogium.Editors.PackData
{
    /// <summary>
    /// Contains all important data for a given pack.
    /// </summary>
    public class PackAsset : AssetBase
    {
        private PackInfoAsset packInfo;
        private AssetList<PaletteAsset> palettes;
        private AssetList<RoomAsset> rooms;
        private AssetList<TileAsset> tiles;

        #region Constructors
        public PackAsset()
        {
            
            this.packInfo = new PackInfoAsset(EditorDefaults.PackTitle,
                                              EditorDefaults.PackIcon,
                                              EditorDefaults.Author,
                                              EditorDefaults.PackDescription);
           
            GatherValuesFromInfo(packInfo);
            this.palettes = new AssetList<PaletteAsset>(this);
            this.rooms = new AssetList<RoomAsset>(this);
            this.tiles = new AssetList<TileAsset>(this);
        }
        public PackAsset(PackInfoAsset packInfo)
        {
            this.packInfo = new PackInfoAsset(packInfo);
            
            GatherValuesFromInfo(packInfo);
            this.palettes = new AssetList<PaletteAsset>(this);
            this.rooms = new AssetList<RoomAsset>(this);
            this.tiles = new AssetList<TileAsset>(this);
        }
        public PackAsset(PackInfoAsset packInfo, IList<RoomAsset> rooms, IList<TileAsset> tiles)
        {
            this.packInfo = packInfo;
            
            //TODO Add all asset list generation in here.
            
            GatherValuesFromInfo(packInfo);
            this.palettes = new AssetList<PaletteAsset>(this);
            this.rooms = new AssetList<RoomAsset>(this, rooms);
            this.tiles = new AssetList<TileAsset>(this, tiles);
        }
        #endregion

        private void GatherValuesFromInfo(PackInfoAsset info)
        {
            this.id = packInfo.ID;
            this.title = packInfo.Title;
            this.icon = packInfo.Icon;
            this.author = packInfo.Author;
            this.creationDate = packInfo.CreationDate;
        }
        
        #region Update Values
        public void UpdatePackInfo(PackInfoAsset newPackInfo)
        {
            packInfo = new PackInfoAsset(newPackInfo);
            GatherValuesFromInfo(packInfo);
        }
        #endregion

        public override bool Equals(object obj)
        {
            PackAsset pack = (PackAsset)obj;
            if (pack == null) return false;
            if (pack.packInfo.Title == packInfo.Title &&
                pack.packInfo.Author == packInfo.Author &&
                pack.packInfo.CreationDate == packInfo.CreationDate)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return int.Parse(packInfo.ID);
        }

        public PackInfoAsset PackInfo { get => packInfo; }
        public AssetList<PaletteAsset> Palettes { get => palettes; }
        public AssetList<RoomAsset> Rooms { get => rooms; }
        public AssetList<TileAsset> Tiles { get => tiles; }
    }
}

