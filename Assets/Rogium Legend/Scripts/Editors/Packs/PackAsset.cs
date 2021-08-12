using RogiumLegend.Editors.PaletteData;
using RogiumLegend.Editors.RoomData;
using System.Collections.Generic;

namespace RogiumLegend.Editors.PackData
{
    public class PackAsset
    {
        private PackInfoAsset packInfo;
        private List<PaletteAsset> palettes;
        private List<RoomAsset> rooms;

        public PackAsset(PackInfoAsset packInfo)
        {
            this.packInfo = packInfo;
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

        public PackInfoAsset PackInfo { get => packInfo; }
        public List<PaletteAsset> Palettes { get => palettes; }
        public List<RoomAsset> Rooms { get => rooms; }
    }
}

