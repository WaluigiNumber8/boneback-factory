using System.Collections.Generic;
using Rogium.Editors.Packs;

namespace Rogium.Editors.Campaign
{
    /// <summary>
    /// Combines multiple packs into one.
    /// </summary>
    public class PackCombiner
    {
        /// <summary>
        /// Takes a list of packs and combines it into 1.
        /// </summary>
        /// <param name="packs">The list of packs to combine.</param>
        /// <returns>A pack containing everything</returns>
        public PackAsset Combine(IList<PackAsset> packs)
        {
            PackAsset ultimatePack = new PackAsset();
            foreach (PackAsset pack in packs)
            {
                ultimatePack.Palettes.AddAllWithoutSave(pack.Palettes);
                ultimatePack.Rooms.AddAllWithoutSave(pack.Rooms);
                ultimatePack.Tiles.AddAllWithoutSave(pack.Tiles);
            }
            return ultimatePack;
        }
    }
}