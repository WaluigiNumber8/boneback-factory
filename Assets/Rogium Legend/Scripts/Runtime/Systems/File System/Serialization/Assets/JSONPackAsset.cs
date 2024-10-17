using System;
using Rogium.Editors.Packs;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="PackAsset"/>.
    /// </summary>
    [Serializable]
    public class JSONPackAsset : JSONAssetWithReferencedSpriteBase<PackAsset>
    {
        public string description;
        public int paletteCount, spriteCount, weaponCount, projectileCount, enemyCount, roomCount, tileCount;

        public JSONPackAsset(PackAsset asset) : base(asset)
        {
            description = asset.Description;
            paletteCount = asset.PaletteCount;
            spriteCount = asset.SpriteCount;
            weaponCount = asset.WeaponCount;
            projectileCount = asset.ProjectileCount;
            enemyCount = asset.EnemyCount;
            roomCount = asset.RoomCount;
            tileCount = asset.TileCount;
        }

        public override PackAsset Decode()
        {
            return new PackAsset.Builder()
                .WithID(id)
                .WithTitle(title)
                .WithIcon(icon.Decode())
                .WithAuthor(author)
                .WithCreationDate(DateTime.Parse(creationDate))
                .WithAssociatedSpriteID(associatedSpriteID)
                .WithDescription(description)
                .WithCounts(paletteCount, spriteCount, weaponCount, projectileCount, enemyCount, roomCount, tileCount)
                .Build();
        }
        
    }
}