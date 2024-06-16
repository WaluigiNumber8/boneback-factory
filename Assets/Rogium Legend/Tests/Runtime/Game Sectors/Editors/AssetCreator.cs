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
            PackAsset pack = new("Test Pack", RedRatBuilder.GenerateSprite(Color.black, 16, 16, 16));
            
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
            PaletteAsset palette = new("Test Palette", RedRatBuilder.GenerateSprite(Color.magenta, 16, 16, 16));
            return palette;
        }
        
        public static SpriteAsset CreateSprite()
        {
            SpriteAsset sprite = new("Test Sprite", RedRatBuilder.GenerateSprite(Color.white, 16, 16, 16));
            return sprite;
        }

        public static WeaponAsset CreateWeapon()
        {
            WeaponAsset weapon = new("Test Weapon", RedRatBuilder.GenerateSprite(Color.green, 16, 16, 16));
            return weapon;
        }

        public static ProjectileAsset CreateProjectile()
        {
            ProjectileAsset projectile = new("Test Projectile", RedRatBuilder.GenerateSprite(Color.cyan, 16, 16, 16));
            return projectile;
        }

        public static EnemyAsset CreateEnemy()
        {
            EnemyAsset enemy = new("Test Enemy", RedRatBuilder.GenerateSprite(Color.red, 16, 16, 16));
            return enemy;
        }

        public static RoomAsset CreateRoom()
        {
            RoomAsset room = new("Test Room", RedRatBuilder.GenerateSprite(Color.blue, 16, 16, 16));
            return room;
        }

        public static TileAsset CreateTile()
        {
            TileAsset tile = new("Test Tile", RedRatBuilder.GenerateSprite(Color.yellow, 16, 16, 16));
            return tile;
        }
    }
}