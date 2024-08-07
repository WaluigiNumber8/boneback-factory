using RedRats.Core;
using Rogium.Editors.Campaign;
using Rogium.Editors.Enemies;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Rooms;
using Rogium.Editors.Sprites;
using Rogium.Editors.Tiles;
using Rogium.Editors.Weapons;
using Rogium.Systems.GridSystem;
using UnityEditor.Graphs;
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

        public static CampaignAsset CreateCampaign()
        {
            CampaignAsset campaign = new CampaignAsset.Builder()
                .WithTitle("Test Campaign")
                .WithIcon(RedRatBuilder.GenerateSprite(Color.black, 16, 16, 16))
                .WithDataPack(CreatePack())
                .Build();
            return campaign;
        }

        public static PackAsset CreatePack()
        {
            return new PackAsset.Builder()
                .WithTitle("Test Pack")
                .WithIcon(RedRatBuilder.GenerateSprite(Color.magenta, 16, 16, 16))
                .WithPalettes(new[] {CreatePalette()})
                .WithSprites(new[] {CreateSprite()})
                .WithWeapons(new[] {CreateWeapon()})
                .WithProjectiles(new[] {CreateProjectile()})
                .WithEnemies(new[] {CreateEnemy()})
                .WithRooms(new[] {CreateRoom()})
                .WithTiles(new[] {CreateTile()})
                .Build();
        }

        public static PaletteAsset CreatePalette()
        {
            return new PaletteAsset.Builder()
                .WithTitle("Test Palette")
                .WithIcon(RedRatBuilder.GenerateSprite(Color.magenta, 16, 16, 16))
                .Build();
        }

        public static SpriteAsset CreateSprite(Color color = new())
        {
            return new SpriteAsset.Builder()
                .WithTitle("Test Sprite")
                .WithIcon(RedRatBuilder.GenerateSprite(color, 16, 16, 16))
                .Build();
        }

        public static SpriteAsset CreateSpriteWithFirstPixelFromSlot1()
        {
            ObjectGrid<int> newSpriteData = new(16, 16, () => -1);
            newSpriteData.SetTo(0, 0, 0);
            SpriteAsset sprite = CreateSprite();
            sprite.UpdateSpriteData(newSpriteData);
            return sprite;
        }

        public static WeaponAsset CreateWeapon()
        {
            SpriteAsset s = CreateSprite();
            s.UpdateIcon(RedRatBuilder.GenerateSprite(Color.green, 16, 16, 16));
            WeaponAsset weapon = new WeaponAsset.Builder()
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
            return new RoomAsset.Builder()
                .WithTitle("Test Room")
                .WithIcon(RedRatBuilder.GenerateSprite(Color.blue, 16, 16, 16))
                .Build();
        }

        public static TileAsset CreateTile()
        {
            return new TileAsset.Builder()
                .WithTitle("Test Tile")
                .WithIcon(CreateSprite(Color.yellow))
                .Build();
        }
    }
}