using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Palettes;
using Rogium.Editors.Rooms;
using Rogium.Editors.Tiles;
using Rogium.Editors.Sprites;
using System.Collections.Generic;

namespace Rogium.Editors.Packs
{
    /// <summary>
    /// Contains all important data for a given pack.
    /// </summary>
    public class PackAsset : AssetBase
    {
        private PackInfoAsset packInfo;
        private AssetList<PaletteAsset> palettes;
        private AssetList<SpriteAsset> sprites;
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
            this.sprites = new AssetList<SpriteAsset>(this);
            this.rooms = new AssetList<RoomAsset>(this);
            this.tiles = new AssetList<TileAsset>(this);
        }
        public PackAsset(PackInfoAsset packInfo)
        {
            this.packInfo = new PackInfoAsset(packInfo);
            
            GatherValuesFromInfo(packInfo);
            this.palettes = new AssetList<PaletteAsset>(this);
            this.sprites = new AssetList<SpriteAsset>(this);
            this.rooms = new AssetList<RoomAsset>(this);
            this.tiles = new AssetList<TileAsset>(this);
        }
        
        public PackAsset(PackAsset asset)
        {
            this.packInfo = new PackInfoAsset(asset.packInfo);
            
            GatherValuesFromInfo(packInfo);
            this.palettes = new AssetList<PaletteAsset>(this, asset.Palettes);
            this.sprites = new AssetList<SpriteAsset>(this, asset.Sprites);
            this.rooms = new AssetList<RoomAsset>(this, asset.Rooms);
            this.tiles = new AssetList<TileAsset>(this, asset.Tiles);
        }
        public PackAsset(PackInfoAsset packInfo, IList<PaletteAsset> palettes, IList<SpriteAsset> sprites,
                        IList<TileAsset> tiles, IList<RoomAsset> rooms)
        {
            this.packInfo = packInfo;
            
            //TODO Add all asset list generation in here.
            
            GatherValuesFromInfo(packInfo);
            this.palettes = new AssetList<PaletteAsset>(this, palettes);
            this.sprites = new AssetList<SpriteAsset>(this, sprites);
            this.rooms = new AssetList<RoomAsset>(this, rooms);
            this.tiles = new AssetList<TileAsset>(this, tiles);
        }
        #endregion

        private void GatherValuesFromInfo(PackInfoAsset info)
        {
            this.id = info.ID;
            this.title = info.Title;
            this.icon = info.Icon;
            this.author = info.Author;
            this.creationDate = info.CreationDate;
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
        public AssetList<SpriteAsset> Sprites { get => sprites; }
        public AssetList<RoomAsset> Rooms { get => rooms; }
        public AssetList<TileAsset> Tiles { get => tiles; }
    }
}

