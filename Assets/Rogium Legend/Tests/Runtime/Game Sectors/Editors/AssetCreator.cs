using Rogium.Editors.Core;
using Rogium.Editors.Enemies;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Rooms;
using Rogium.Editors.Sprites;
using Rogium.Editors.Tiles;
using Rogium.Editors.Weapons;

namespace Rogium.Tests.Editors
{
    /// <summary>
    /// Creates assets for testing purposes.
    /// </summary>
    public static class AssetCreator
    {
        public static PackAsset CreatePack()
        {
            PackAsset pack = new();
            pack.UpdateTitle("Test Pack");
            
            pack.Palettes.AddAllWithoutSave(new[] { CreatePalette() });
            pack.Sprites.AddAllWithoutSave(new[] { CreateSprite() });
            pack.Weapons.AddAllWithoutSave(new[] { CreateWeapon() });
            pack.Projectiles.AddAllWithoutSave(new[] { CreateProjectile() });
            pack.Enemies.AddAllWithoutSave(new[] { CreateEnemy() });
            pack.Rooms.AddAllWithoutSave(new[] { CreateRoom() });
            pack.Tiles.AddAllWithoutSave(new[] { CreateTile() });

            return pack;
        }

        public static PaletteAsset CreatePalette()
        {
            PaletteAsset palette = new();
            palette.UpdateTitle("Test Palette");
            return palette;
        }
        
        public static SpriteAsset CreateSprite()
        {
            SpriteAsset sprite = new();
            sprite.UpdateTitle("Test Sprite");
            return sprite;
        }

        public static WeaponAsset CreateWeapon()
        {
            WeaponAsset weapon = new();
            weapon.UpdateTitle("Test Weapon");
            return weapon;
        }

        public static ProjectileAsset CreateProjectile()
        {
            ProjectileAsset projectile = new();
            projectile.UpdateTitle("Test Projectile");
            return projectile;
        }

        public static EnemyAsset CreateEnemy()
        {
            EnemyAsset enemy = new();
            enemy.UpdateTitle("Test Enemy");
            return enemy;
        }

        public static RoomAsset CreateRoom()
        {
            RoomAsset room = new();
            room.UpdateTitle("Test Room");
            return room;
        }

        public static TileAsset CreateTile()
        {
            TileAsset tile = new();
            tile.UpdateTitle("Test Tile");
            return tile;
        }
    }
}