using System.Collections.Generic;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Palettes;
using Rogium.Editors.Rooms;
using Rogium.Editors.Sprites;
using Rogium.Editors.Tiles;
using Rogium.ExternalStorage;

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

        private ExternalStorageOverseer ex = ExternalStorageOverseer.Instance;
        
        #region Constructors
        public PackAsset()
        {
            
            packInfo = new PackInfoAsset(EditorDefaults.PackTitle,
                                         EditorDefaults.PackIcon,
                                         EditorDefaults.Author,
                                         EditorDefaults.PackDescription);
           
            GatherValuesFromInfo(packInfo);
            palettes = new AssetList<PaletteAsset>(ex.Palettes.Save, ex.Palettes.UpdateTitle, ex.Palettes.Delete);
            sprites = new AssetList<SpriteAsset>(ex.Sprites.Save, ex.Sprites.UpdateTitle, ex.Sprites.Delete);
            rooms = new AssetList<RoomAsset>(ex.Rooms.Save, ex.Rooms.UpdateTitle, ex.Rooms.Delete);
            tiles = new AssetList<TileAsset>(ex.Tiles.Save, ex.Tiles.UpdateTitle, ex.Tiles.Delete);
        }
        public PackAsset(PackInfoAsset packInfo)
        {
            this.packInfo = new PackInfoAsset(packInfo);
            
            GatherValuesFromInfo(packInfo);
            palettes = new AssetList<PaletteAsset>(ex.Palettes.Save, ex.Palettes.UpdateTitle, ex.Palettes.Delete);
            sprites = new AssetList<SpriteAsset>(ex.Sprites.Save, ex.Sprites.UpdateTitle, ex.Sprites.Delete);
            rooms = new AssetList<RoomAsset>(ex.Rooms.Save, ex.Rooms.UpdateTitle, ex.Rooms.Delete);
            tiles = new AssetList<TileAsset>(ex.Tiles.Save, ex.Tiles.UpdateTitle, ex.Tiles.Delete);
        }
        
        public PackAsset(PackAsset asset)
        {
            packInfo = new PackInfoAsset(asset.packInfo);
            
            GatherValuesFromInfo(packInfo);
            palettes = new AssetList<PaletteAsset>(ex.Palettes.Save, ex.Palettes.UpdateTitle, ex.Palettes.Delete, asset.Palettes);
            sprites = new AssetList<SpriteAsset>(ex.Sprites.Save, ex.Sprites.UpdateTitle, ex.Sprites.Delete, asset.Sprites);
            rooms = new AssetList<RoomAsset>(ex.Rooms.Save, ex.Rooms.UpdateTitle, ex.Rooms.Delete, asset.Rooms);
            tiles = new AssetList<TileAsset>(ex.Tiles.Save, ex.Tiles.UpdateTitle, ex.Tiles.Delete, asset.Tiles);
        }
        public PackAsset(PackInfoAsset packInfo, IList<PaletteAsset> palettes, IList<SpriteAsset> sprites,
                        IList<TileAsset> tiles, IList<RoomAsset> rooms)
        {
            this.packInfo = packInfo;
            
            //TODO Add all asset list generation in here.
            
            GatherValuesFromInfo(packInfo);
            this.palettes = new AssetList<PaletteAsset>(ex.Palettes.Save, ex.Palettes.UpdateTitle, ex.Palettes.Delete, palettes);
            this.sprites = new AssetList<SpriteAsset>(ex.Sprites.Save, ex.Sprites.UpdateTitle, ex.Sprites.Delete, sprites);
            this.rooms = new AssetList<RoomAsset>(ex.Rooms.Save, ex.Rooms.UpdateTitle, ex.Rooms.Delete, rooms);
            this.tiles = new AssetList<TileAsset>(ex.Tiles.Save, ex.Tiles.UpdateTitle, ex.Tiles.Delete, tiles);
        }
        #endregion

        private void GatherValuesFromInfo(PackInfoAsset info)
        {
            id = info.ID;
            title = info.Title;
            icon = info.Icon;
            author = info.Author;
            creationDate = info.CreationDate;
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

