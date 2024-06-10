using RedRats.Core;
using Rogium.Editors.Enemies;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Rooms;
using Rogium.Editors.Sprites;
using Rogium.Editors.Tiles;
using Rogium.Editors.Weapons;
using UnityEngine;

namespace Rogium.Tests.Editors
{
    /// <summary>
    /// Creates assets for testing purposes.
    /// </summary>
    public static class AssetCreator
    {
        public static PackAsset CreateAndAssignPack()
        {
            PackAsset pack = CreatePack();
            PackEditorOverseer.Instance.AssignAsset(pack, 0);
            return pack;
        }
        
        public static PackAsset CreatePack()
        {
            
            PackAsset pack = new();
            pack.UpdateTitle("Test Pack");
            Sprite icon = RedRatBuilder.GenerateSprite(Color.red, 16, 16, 16);
            
            pack.Palettes.AddAllWithoutSave(new[] { CreatePalette() });
            pack.Sprites.AddAllWithoutSave(new[] { CreateSprite(icon) });
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
            palette.UpdateIcon(CreateSprite(RedRatBuilder.GenerateSprite(Color.magenta, 16, 16, 16)));
            return palette;
        }
        
        public static SpriteAsset CreateSprite(Sprite icon)
        {
            SpriteAsset sprite = new();
            sprite.UpdateTitle("Test Sprite");
            sprite.UpdateIcon(icon);
            return sprite;
        }

        public static WeaponAsset CreateWeapon()
        {
            WeaponAsset weapon = new();
            weapon.UpdateTitle("Test Weapon");
            weapon.UpdateIcon(CreateSprite(RedRatBuilder.GenerateSprite(Color.green, 16, 16, 16)));
            return weapon;
        }

        public static ProjectileAsset CreateProjectile()
        {
            ProjectileAsset projectile = new();
            projectile.UpdateTitle("Test Projectile");
            projectile.UpdateIcon(CreateSprite(RedRatBuilder.GenerateSprite(Color.white, 16, 16, 16)));
            return projectile;
        }

        public static EnemyAsset CreateEnemy()
        {
            EnemyAsset enemy = new();
            enemy.UpdateTitle("Test Enemy");
            enemy.UpdateIcon(CreateSprite(RedRatBuilder.GenerateSprite(Color.red, 16, 16, 16)));
            return enemy;
        }

        public static RoomAsset CreateRoom()
        {
            RoomAsset room = new();
            room.UpdateTitle("Test Room");
            room.UpdateIcon(CreateSprite(RedRatBuilder.GenerateSprite(Color.cyan, 16, 16, 16)));
            return room;
        }

        public static TileAsset CreateTile()
        {
            TileAsset tile = new();
            tile.UpdateTitle("Test Tile");
            tile.UpdateIcon(CreateSprite(RedRatBuilder.GenerateSprite(Color.yellow, 16, 16, 16)));
            return tile;
        }
    }
}