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
    public class PackAsset : IAsset
    {
        private PackInfoAsset packInfo;
        private IList<PaletteAsset> palettes = new List<PaletteAsset>();
        private IList<RoomAsset> rooms = new List<RoomAsset>();
        private IList<TileAsset> tiles = new List<TileAsset>();

        #region Constructors
        public PackAsset()
        {
            this.packInfo = new PackInfoAsset(EditorDefaults.packTitle,
                                              EditorDefaults.packIcon,
                                              EditorDefaults.author,
                                              EditorDefaults.packDescription);
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
        #endregion

        #region Update Values
        public void UpdateTitle(string newTitle)
        {
            packInfo.UpdateTitle(newTitle);
        }

        public void UpdateIcon(Sprite newIcon)
        {
            packInfo.UpdateIcon(newIcon);
        }

        public void UpdateAuthor(string newAuthor)
        {
            packInfo.UpdateAuthor(newAuthor);
        }

        public void UpdateCreationDate(DateTime newCreationDate)
        {
            packInfo.UpdateCreationDate(newCreationDate);
        }

        public void UpdateDescription(string newDescription)
        {
            packInfo.UpdateDescription(newDescription);
        }

        public void UpdatePackInfo(PackInfoAsset newPackInfo)
        {
            packInfo = new PackInfoAsset(newPackInfo);
        }
        #endregion

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
        public string Description { get => packInfo.Description; }
        public Sprite Icon { get => packInfo.Icon; }
        public string Author { get => packInfo.Author; }
        public DateTime CreationDate { get => packInfo.CreationDate; }

        public PackInfoAsset PackInfo { get => packInfo; }
        public IList<PaletteAsset> Palettes { get => palettes; }
        public IList<RoomAsset> Rooms { get => rooms; }
        public IList<TileAsset> Tiles { get => tiles; }
    }
}

