using RedRats.Safety;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Sprites;

namespace Rogium.Editors.Packs
{
    /// <summary>
    /// Contains various methods for dealing with the sprite association system.
    /// </summary>
    public static class SpriteAssociation
    {
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
            if (!string.IsNullOrEmpty(lastAssociatedSpriteID))
            {
                currentPack.Sprites.FindValueFirst(lastAssociatedSpriteID).TryRemoveAssociation(asset);
            }
            
            if (asset.AssociatedSpriteID == EditorDefaults.EmptyAssetID) return;
            
            //Add association if possible.
            (SpriteAsset associatedSprite, int index) = currentPack.Sprites.FindValueAndIndexFirst(asset.AssociatedSpriteID);
            associatedSprite.TryAddAssociation(asset);
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
            catch (SafetyNetCollectionException)
            {
                sprite.TryRemoveAssociation(id);
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
            catch (SafetyNetCollectionException)
            {
                //Intentionally blank (doesn't matter if asset does not exist, since we are removing the sprite anyway).
            }
        }

        /// <summary>
        /// Removes an association between asset and it's associated sprite.
        /// </summary>
        /// <param name="currentPack">The for which to process associations for.</param>
        /// <param name="asset">The asset, who's sprite association to sewer.</param>
        public static void RemoveAssociation(PackAsset currentPack, AssetWithReferencedSpriteBase asset)
        {
            if (string.IsNullOrEmpty(asset.AssociatedSpriteID)) return;
            currentPack.Sprites.FindValueFirst(asset.AssociatedSpriteID).TryRemoveAssociation(asset);
        }
    }
}