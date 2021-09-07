using BoubakProductions.Safety;
using RogiumLegend.Editors.PaletteData;
using RogiumLegend.Editors.RoomData;
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

        public PackAsset()
        {
            //TODO - Plug in Default Data.
            this.packInfo = new PackInfoAsset("New Pack", "", "ME", null);
        }
        public PackAsset(PackInfoAsset packInfo)
        {
            SafetyNet.EnsureStringInRange(packInfo.packName, 4, 30, "name");
            SafetyNet.EnsureStringInRange(packInfo.description, 0, 2000, "description");

            this.packInfo = packInfo;
        }
        public PackAsset(PackInfoAsset packInfo, IList<RoomAsset> rooms)
        {
            SafetyNet.EnsureStringInRange(packInfo.packName, 4, 30, "name");
            SafetyNet.EnsureStringInRange(packInfo.description, 0, 2000, "description");

            this.packInfo = packInfo;
            this.rooms = new List<RoomAsset>(rooms);
        }

        /// <summary>
        /// Updates the packs Pack Information.
        /// </summary>
        /// <param name="packinfo">New Pack Information Container.</param>
        public void UpdatePackInfo(PackInfoAsset packinfo)
        {
            this.packInfo = new PackInfoAsset(packInfo.packName, packinfo.description, packinfo.author, packInfo.icon, packInfo.creationDateTime);
        }

        public override bool Equals(object obj)
        {
            PackAsset pack = (PackAsset)obj;
            if (pack.packInfo.packName == packInfo.packName &&
                pack.packInfo.author == packInfo.author &&
                pack.packInfo.creationDateTime == packInfo.creationDateTime)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            string hash = packInfo.packName + packInfo.author + packInfo.creationDateTime;
            return hash.GetHashCode();
        }

        public string Title { get => packInfo.packName; }
        public Sprite Icon { get => packInfo.icon; }
        public PackInfoAsset PackInfo { get => packInfo; }
        public IList<PaletteAsset> Palettes { get => palettes; }
        public IList<RoomAsset> Rooms { get => rooms; }

    }
}

