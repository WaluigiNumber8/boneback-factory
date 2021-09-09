using BoubakProductions.Safety;
using RogiumLegend.Editors.Core;
using RogiumLegend.Editors.PaletteData;
using RogiumLegend.Editors.RoomData;
using RogiumLegend.Editors.TileData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RogiumLegend.Editors.PackData
{
    /// <summary>
    /// Contains all important data for a given pack.
    /// </summary>
    public class PackAsset : IAsset
    {
        private PackInfoAsset packInfo;
        private IList<PaletteAsset> palettes = new List<PaletteAsset>();
        private IList<RoomAsset> rooms = new List<RoomAsset>();
        private IList<TileAsset> tiles = new List<TileAsset>();

        public PackAsset()
        {
            //TODO - Plug in Default Data.
            this.packInfo = new PackInfoAsset("New Pack",
                                              Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f)),
                                              "NO_AUTHOR",
                                              "A Pack filled with adventure!");
        }
        public PackAsset(PackInfoAsset packInfo)
        {
            this.packInfo = packInfo;
        }
        public PackAsset(PackInfoAsset packInfo, IList<RoomAsset> rooms, IList<TileAsset> tiles)
        {
            this.packInfo = packInfo;
            this.rooms = new List<RoomAsset>(rooms);
            this.tiles = new List<TileAsset>(tiles);
        }

        public override bool Equals(object obj)
        {
            PackAsset pack = (PackAsset)obj;
            if (pack.packInfo.Title == packInfo.Title &&
                pack.packInfo.Author == packInfo.Author &&
                pack.packInfo.CreationDate == packInfo.CreationDate)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            string hash = packInfo.Title + packInfo.Author + packInfo.CreationDate;
            return hash.GetHashCode();
        }

        public string Title { get => packInfo.Title; }
        public Sprite Icon { get => packInfo.Icon; }
        public string Author { get => packInfo.Author; }
        public DateTime CreationDate { get => packInfo.CreationDate; }
        public PackInfoAsset PackInfo { get => packInfo; set => packInfo = value; }
        public string Description { get => packInfo.Description; }
        public IList<PaletteAsset> Palettes { get => palettes; }
        public IList<RoomAsset> Rooms { get => rooms; }
        public IList<TileAsset> Tiles { get => tiles; }

    }
}

