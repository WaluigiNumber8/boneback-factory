using RedRats.Core;
using Rogium.Editors.Enemies;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Rooms;
using Rogium.Editors.Sprites;
using Rogium.Editors.Tiles;
using Rogium.Editors.Weapons;
using Rogium.Systems.GridSystem;
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
            palette.UpdateIcon(RedRatBuilder.GenerateSprite(Color.magenta, 16, 16, 16));
            return palette;
        }
        
        public static SpriteAsset CreateSprite()
        {
            SpriteAsset sprite = new();
            sprite.UpdateTitle("Test Palette");
            sprite.UpdateIcon(RedRatBuilder.GenerateSprite(Color.white, 16, 16, 16));
            return sprite;
        }

        public static SpriteAsset CreateSpriteWithFirstPixelFromSlot1()
        {
            SpriteAsset sprite = new();
            ObjectGrid<int> newSpriteData = new(16, 16, () => -1);
            newSpriteData.SetTo(0, 0, 0);
            sprite.UpdateSpriteData(newSpriteData);
            return sprite;
        }
        
        public static WeaponAsset CreateWeapon()
        {
            SpriteAsset s = CreateSprite();
            s.UpdateIcon(RedRatBuilder.GenerateSprite(Color.green, 16, 16, 16));
            WeaponAsset weapon = new WeaponAsset.WeaponBuilder()
                .WithTitle("Test Weapon")
                .WithIcon(s)
                .Build();
            return weapon;
        }

        public static ProjectileAsset CreateProjectile()
        {
            ProjectileAsset projectile = new();
            projectile.UpdateTitle("Test Projectile");
            SpriteAsset s = CreateSprite();
            s.UpdateIcon(RedRatBuilder.GenerateSprite(Color.cyan, 16, 16, 16));
            projectile.UpdateIcon(s);
            return projectile;
        }

        public static EnemyAsset CreateEnemy()
        {
            EnemyAsset enemy = new();
            enemy.UpdateTitle("Test Enemy");
            SpriteAsset s = CreateSprite();
            s.UpdateIcon(RedRatBuilder.GenerateSprite(Color.red, 16, 16, 16));
            enemy.UpdateIcon(s);
            return enemy;
        }

        public static RoomAsset CreateRoom()
        {
            RoomAsset room = new();
            room.UpdateTitle("Test Room");
            SpriteAsset s = CreateSprite();
            s.UpdateIcon(RedRatBuilder.GenerateSprite(Color.blue, 16, 16, 16));
            room.UpdateIcon(s);
            return room;
        }

        public static TileAsset CreateTile()
        {
            TileAsset tile = new();
            tile.UpdateTitle("Test Tile");
            SpriteAsset s = CreateSprite();
            s.UpdateIcon(RedRatBuilder.GenerateSprite(Color.yellow, 16, 16, 16));
            tile.UpdateIcon(s);
            return tile;
        }
    }
}