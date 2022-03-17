using System.Collections.Generic;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
using Rogium.ExternalStorage;

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
                PackAsset p = ExternalStorageOverseer.Instance.LoadPack(pack);
                ultimatePack.Palettes.AddAllWithoutSave(p.Palettes);
                ultimatePack.Sprites.AddAllWithoutSave(p.Sprites);
                ultimatePack.Weapons.AddAllWithoutSave(p.Weapons);
                ultimatePack.Projectiles.AddAllWithoutSave(p.Projectiles);
                ultimatePack.Enemies.AddAllWithoutSave(p.Enemies);
                ultimatePack.Rooms.AddAllWithoutSave(p.Rooms);
                ultimatePack.Tiles.AddAllWithoutSave(p.Tiles);
            }
            return ultimatePack;
        }

        /// <summary>
        /// Based on entered information, combines a list of packs into a single one..
        /// </summary>
        /// <param name="packs">The list of import information about packs.</param>
        /// <returns>A single combined pack.</returns>
        public PackAsset Combine(IList<PackImportInfo> packs)
        {
            PackAsset ultimatePack = new PackAsset();
            IList<PackAsset> allPacks = ExternalLibraryOverseer.Instance.GetPacksCopy;

            foreach (PackImportInfo importInfo in packs)
            {
                PackAsset foundPack = allPacks[allPacks.FindIndexFirst(importInfo.ID)];
                CombinePack(ultimatePack, foundPack, importInfo);
            }

            return ultimatePack;
        }

        /// <summary>
        /// Imports a given pack into a another one.
        /// </summary>
        /// <param name="ultimatePack">The pack to import into.</param>
        /// <param name="packToImport">The pack that will be imported.</param>
        /// <param name="importInfo">The data containing what will be imported.</param>
        private void CombinePack(PackAsset ultimatePack, PackAsset packToImport, PackImportInfo importInfo)
        {
            ultimatePack.Palettes.AddAllWithoutSave(packToImport.Palettes);
            ultimatePack.Sprites.AddAllWithoutSave(packToImport.Sprites);
            ultimatePack.Projectiles.AddAllWithoutSave(packToImport.Projectiles);
            
            if (importInfo.weapons)
            {
                ultimatePack.Weapons.AddAllWithoutSave(packToImport.Weapons);
            }

            if (importInfo.enemies)
            {
                ultimatePack.Enemies.AddAllWithoutSave(packToImport.Enemies);
            }

            if (importInfo.rooms)
            {
                ultimatePack.Tiles.AddAllWithoutSave(packToImport.Tiles);
                ultimatePack.Rooms.AddAllWithoutSave(packToImport.Rooms);
            }
        }
        
    }
}