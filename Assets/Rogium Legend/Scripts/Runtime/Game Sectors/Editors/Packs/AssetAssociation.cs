using System;
using RedRats.Safety;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Palettes;
using Rogium.Editors.Sprites;

namespace Rogium.Editors.Packs
{
    /// <summary>
    /// Contains various methods for dealing with the sprite association system.
    /// </summary>
    public static class AssetAssociation
    {
        public static void RefreshSpriteForOtherAssets(SpriteAsset newAsset, PackAsset currentPack, Action whenSavePackChanges)
        {
            foreach (string id in newAsset.AssociatedAssetsIDs)
            {
                string identifier = id[..2];
                switch (identifier)
                {
                    case EditorAssetIDs.PackIdentifier:
                        currentPack.UpdateIcon(newAsset);
                        whenSavePackChanges.Invoke();
                        break;
                    case EditorAssetIDs.PaletteIdentifier:
                        RefreshSpriteAndSaveAsset(currentPack.Palettes, id, newAsset);
                        break;
                    case EditorAssetIDs.SpriteIdentifier:
                        RefreshSpriteAndSaveAsset(currentPack.Sprites, id, newAsset);
                        break;
                    case EditorAssetIDs.WeaponIdentifier:
                        RefreshSpriteAndSaveAsset(currentPack.Weapons, id, newAsset);
                        break;
                    case EditorAssetIDs.ProjectileIdentifier:
                        RefreshSpriteAndSaveAsset(currentPack.Projectiles, id, newAsset);
                        break;
                    case EditorAssetIDs.EnemyIdentifier:
                        RefreshSpriteAndSaveAsset(currentPack.Enemies, id, newAsset);
                        break;
                    case EditorAssetIDs.RoomIdentifier:
                        RefreshSpriteAndSaveAsset(currentPack.Rooms, id, newAsset);
                        break;
                    case EditorAssetIDs.TileIdentifier:
                        RefreshSpriteAndSaveAsset(currentPack.Tiles, id, newAsset);
                        break;
                }
            }
        }

        public static void RefreshPaletteForOtherAssets(PaletteAsset newAsset, PackAsset currentPack, Action savePackChanges)
        {
            foreach (string id in newAsset.AssociatedAssetsIDs)
            {
                string identifier = id[..2];
                if (identifier == EditorAssetIDs.SpriteIdentifier)
                {
                    RefreshPaletteAndSaveAsset(currentPack.Sprites, id, newAsset);
                    RefreshSpriteForOtherAssets(currentPack.Sprites.FindValueFirst(id), currentPack, savePackChanges);
                }
            }
        }
        
        public static void ProcessPaletteAssociations(PackAsset currentPack, SpriteAsset asset, string lastAssociatedPaletteID)
        {
            if (asset.AssociatedPaletteID == lastAssociatedPaletteID) return;
            if (string.IsNullOrEmpty(asset.AssociatedPaletteID) && !string.IsNullOrEmpty(lastAssociatedPaletteID)) return;
            
            //Remove association of older palette.
            if (!lastAssociatedPaletteID.IsEmpty())
            {
                currentPack.Palettes.FindValueFirst(lastAssociatedPaletteID).RemoveAssociation(asset.ID);
            }
            
            if (asset.AssociatedPaletteID.IsEmpty()) return;
            
            //Add association if possible.
            (PaletteAsset associatedPalette, int index) = currentPack.Palettes.FindValueAndIndexFirst(asset.AssociatedPaletteID);
            associatedPalette.AddAssociation(asset.ID);
            currentPack.Palettes.Update(index, associatedPalette);
        }
        
        /// <summary>
        /// Adds/Removes sprite associations if necessary.
        /// </summary>
        /// <param name="currentPack">The pack for which to process teh associations for.</param>
        /// <param name="asset">The asset to process the associations for.</param>
        /// <param name="lastAssociatedSpriteID">The asset's old sprite (given to it before editing).</param>
        public static void ProcessSpriteAssociations(PackAsset currentPack, AssetWithReferencedSpriteBase asset, string lastAssociatedSpriteID)
        {
            if (asset.AssociatedSpriteID == lastAssociatedSpriteID) return;
            if (string.IsNullOrEmpty(asset.AssociatedSpriteID) && !string.IsNullOrEmpty(lastAssociatedSpriteID)) return;
            
            //Remove association of older sprite.
            if (!lastAssociatedSpriteID.IsEmpty())
            {
                currentPack.Sprites.FindValueFirst(lastAssociatedSpriteID).RemoveAssociation(asset.ID);
            }
            
            if (asset.AssociatedSpriteID.IsEmpty()) return;
            
            //Add association if possible.
            (SpriteAsset associatedSprite, int index) = currentPack.Sprites.FindValueAndIndexFirst(asset.AssociatedSpriteID);
            associatedSprite.AddAssociation(asset.ID);
            currentPack.Sprites.Update(index, associatedSprite);
        }

        /// <summary>
        /// Updates the icon of an asset and calls for saving the asset to external storage.
        /// </summary>
        /// <param name="list">The list of assets the asset is located on.</param>
        /// <param name="id">The ID of the asset, who's sprite will be refreshed.</param>
        /// <param name="sprite">The new sprite to refresh with.</param>
        /// <typeparam name="T">Any type of <see cref="IAssetWithIcon"/>.</typeparam>
        public static void RefreshSpriteAndSaveAsset<T>(AssetList<T> list, string id, SpriteAsset sprite) where T : IAssetWithIcon
        {
            if (sprite == null) return;
            try
            {
                (T asset, int index) = list.FindValueAndIndexFirst(id);
                asset.UpdateIcon(sprite);
                list.Update(index, asset);
            }
            catch (PreconditionCollectionException)
            {
                sprite.RemoveAssociation(id);
            }
        }

        public static void RefreshPaletteAndSaveAsset<T>(AssetList<T> list, string id, PaletteAsset palette) where T : IAssetWithPalette
        {
            if (palette == null) return;
            try
            {
                (T asset, int index) = list.FindValueAndIndexFirst(id);
                asset.UpdatePalette(palette);
                list.Update(index, asset);
            }
            catch (PreconditionCollectionException)
            {
                palette.RemoveAssociation(id);
            }
        }

        /// <summary>
        /// Removes an associated sprite from any kind of <see cref="AssetWithReferencedSpriteBase"/>.
        /// </summary>
        /// <param name="list">The list of assets the asset is located on.</param>
        /// <param name="id">The ID of the asset to remove the reference from.</param>
        /// <typeparam name="T">Any type of <see cref="AssetWithReferencedSpriteBase"/>.</typeparam>
        public static void RemoveSpriteAssociationsAndSaveAsset<T>(AssetList<T> list, string id) where T : AssetWithReferencedSpriteBase
        {
            try
            {
                (T asset, int index) = list.FindValueAndIndexFirst(id);
                asset.ClearAssociatedSprite();
                list.Update(index, asset);
            }
            catch (PreconditionCollectionException)
            {
                //Intentionally blank (doesn't matter if asset does not exist, since we are removing the sprite anyway).
            }
        }

        public static void RemovePaletteAssociationsAndSaveAsset(AssetList<SpriteAsset> list, string id)
        {
            try
            {
                (SpriteAsset asset, int index) = list.FindValueAndIndexFirst(id);
                asset.ClearAssociatedPalette();
                list.Update(index, asset);
            }
            catch (PreconditionCollectionException)
            {
                //Intentionally blank (doesn't matter if asset does not exist, since we are removing the sprite anyway).
            }
        }

        /// <summary>
        /// Removes an association between asset and it's associated sprite.
        /// </summary>
        /// <param name="currentPack">The for which to process associations for.</param>
        /// <param name="asset">The asset, who's sprite association to sewer.</param>
        public static void RemoveSpriteAssociation(PackAsset currentPack, AssetWithReferencedSpriteBase asset)
        {
            if (asset.AssociatedSpriteID.IsEmpty()) return;
            currentPack.Sprites.FindValueFirst(asset.AssociatedSpriteID).RemoveAssociation(asset.ID);
        }
        
        public static void RemovePaletteAssociation(PackAsset currentPack, SpriteAsset asset)
        {
            if (asset.AssociatedPaletteID.IsEmpty()) return;
            currentPack.Palettes.FindValueFirst(asset.AssociatedPaletteID).RemoveAssociation(asset.ID);
        }

        
    }
}