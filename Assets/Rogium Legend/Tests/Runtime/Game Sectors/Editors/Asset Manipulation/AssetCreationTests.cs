using NUnit.Framework;
using Rogium.Editors.Campaign;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Enemies;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Rooms;
using Rogium.Editors.Sprites;
using Rogium.Editors.Tiles;
using Rogium.Editors.Weapons;
using Rogium.Options.Core;

namespace Rogium.Tests.Editors.AssetManipulation
{
    public class AssetCreationTests
    {
        #region Packs

        [Test]
        public void Build_Should_GivePackProperIdentifier()
        {
            PackAsset pack = new PackAsset.Builder().Build();
            Assert.That(pack.ID[..2], Is.EqualTo(EditorAssetIDs.PackIdentifier));
        }
        
        [Test]
        public void Copy_Should_CreatePackWithSameID()
        {
            PackAsset original = AssetCreator.CreatePack();
            PackAsset copy = new PackAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.ID, Is.EqualTo(original.ID));
        }
        
        [Test]
        public void Copy_Should_CreatePackWithSameParameters()
        {
            PackAsset original = AssetCreator.CreatePack();
            PackAsset copy = new PackAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.Palettes, Is.EqualTo(original.Palettes));
            Assert.That(copy.Sprites, Is.EqualTo(original.Sprites));
            Assert.That(copy.Weapons, Is.EqualTo(original.Weapons));
            Assert.That(copy.Projectiles, Is.EqualTo(original.Projectiles));
            Assert.That(copy.Enemies, Is.EqualTo(original.Enemies));
            Assert.That(copy.Rooms, Is.EqualTo(original.Rooms));
            Assert.That(copy.Tiles, Is.EqualTo(original.Tiles));
        }
        
        [Test]
        public void Copy_Should_CreatePackBaseWithSameParameters()
        {
            PackAsset original = AssetCreator.CreatePack();
            PackAsset copy = new PackAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.Title, Is.EqualTo(original.Title));
            Assert.That(copy.Author, Is.EqualTo(original.Author));
            Assert.That(copy.CreationDate, Is.EqualTo(original.CreationDate));
        }
        
        [Test]
        public void Clone_Should_CreatePackWithDifferentID()
        {
            PackAsset original = AssetCreator.CreatePack();
            PackAsset clone = new PackAsset.Builder().AsClone(original).Build();
            Assert.That(clone.ID, Is.Not.EqualTo(original.ID));
        }
        
        [Test]
        public void Clone_Should_CreatePackBaseWithSameParameters()
        {
            PackAsset original = AssetCreator.CreatePack();
            PackAsset clone = new PackAsset.Builder().AsClone(original).Build();
            Assert.That(clone.Title, Is.EqualTo(original.Title));
            Assert.That(clone.Author, Is.EqualTo(original.Author));
            Assert.That(clone.CreationDate, Is.EqualTo(original.CreationDate));
        }
        
        [Test]
        public void Clone_Should_CreatePackWithSameParameters()
        {
            PackAsset original = AssetCreator.CreatePack();
            PackAsset clone = new PackAsset.Builder().AsClone(original).Build();
            Assert.That(clone.Palettes, Is.EqualTo(original.Palettes));
            Assert.That(clone.Sprites, Is.EqualTo(original.Sprites));
            Assert.That(clone.Weapons, Is.EqualTo(original.Weapons));
            Assert.That(clone.Projectiles, Is.EqualTo(original.Projectiles));
            Assert.That(clone.Enemies, Is.EqualTo(original.Enemies));
            Assert.That(clone.Rooms, Is.EqualTo(original.Rooms));
            Assert.That(clone.Tiles, Is.EqualTo(original.Tiles));
        }

        #endregion
        
        #region Palettes

        [Test]
        public void Build_Should_GivePaletteProperIdentifier()
        {
            PaletteAsset palette = new PaletteAsset.Builder().Build();
            Assert.That(palette.ID[..2], Is.EqualTo(EditorAssetIDs.PaletteIdentifier));
        }
        
        [Test]
        public void Copy_Should_CreatePaletteWithSameID()
        {
            PaletteAsset original = AssetCreator.CreatePalette();
            PaletteAsset copy = new PaletteAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.ID, Is.EqualTo(original.ID));
        }
        
        [Test]
        public void Copy_Should_CreatePaletteWithSameParameters()
        {
            PaletteAsset original = AssetCreator.CreatePalette();
            PaletteAsset copy = new PaletteAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.Colors, Is.EqualTo(original.Colors));
        }
        
        [Test]
        public void Copy_Should_CreatePaletteBaseWithSameParameters()
        {
            PaletteAsset original = AssetCreator.CreatePalette();
            PaletteAsset copy = new PaletteAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.Title, Is.EqualTo(original.Title));
            Assert.That(copy.Author, Is.EqualTo(original.Author));
            Assert.That(copy.CreationDate, Is.EqualTo(original.CreationDate));
        }
        
        [Test]
        public void Clone_Should_CreatePaletteWithDifferentID()
        {
            PaletteAsset original = AssetCreator.CreatePalette();
            PaletteAsset clone = new PaletteAsset.Builder().AsClone(original).Build();
            Assert.That(clone.ID, Is.Not.EqualTo(original.ID));
        }
        
        [Test]
        public void Clone_Should_CreatePaletteBaseWithSameParameters()
        {
            PaletteAsset original = AssetCreator.CreatePalette();
            PaletteAsset clone = new PaletteAsset.Builder().AsClone(original).Build();
            Assert.That(clone.Title, Is.EqualTo(original.Title));
            Assert.That(clone.Author, Is.EqualTo(original.Author));
            Assert.That(clone.CreationDate, Is.EqualTo(original.CreationDate));
        }
        
        [Test]
        public void Clone_Should_CreatePaletteWithSameParameters()
        {
            PaletteAsset original = AssetCreator.CreatePalette();
            PaletteAsset clone = new PaletteAsset.Builder().AsClone(original).Build();
            Assert.That(clone.Colors, Is.EqualTo(original.Colors));
        }
        #endregion

        #region Sprites

        [Test]
        public void Build_Should_GiveSpriteProperIdentifier()
        {
            SpriteAsset sprite = new SpriteAsset.Builder().Build();
            Assert.That(sprite.ID[..2], Is.EqualTo(EditorAssetIDs.SpriteIdentifier));
        }
        
        [Test]
        public void Copy_Should_CreateSpriteWithSameID()
        {
            SpriteAsset original = AssetCreator.CreateSprite();
            SpriteAsset copy = new SpriteAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.ID, Is.EqualTo(original.ID));
        }
        
        [Test]
        public void Copy_Should_CreateSpriteWithSameParameters()
        {
            SpriteAsset original = AssetCreator.CreateSprite();
            SpriteAsset copy = new SpriteAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.SpriteData, Is.EqualTo(original.SpriteData));
        }
        
        [Test]
        public void Copy_Should_CreateSpriteBaseWithSameParameters()
        {
            SpriteAsset original = AssetCreator.CreateSprite();
            SpriteAsset copy = new SpriteAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.Title, Is.EqualTo(original.Title));
            Assert.That(copy.Author, Is.EqualTo(original.Author));
            Assert.That(copy.CreationDate, Is.EqualTo(original.CreationDate));
        }
        
        [Test]
        public void Clone_Should_CreateSpriteWithDifferentID()
        {
            SpriteAsset original = AssetCreator.CreateSprite();
            SpriteAsset clone = new SpriteAsset.Builder().AsClone(original).Build();
            Assert.That(clone.ID, Is.Not.EqualTo(original.ID));
        }
        
        [Test]
        public void Clone_Should_CreateSpriteBaseWithSameParameters()
        {
            SpriteAsset original = AssetCreator.CreateSprite();
            SpriteAsset clone = new SpriteAsset.Builder().AsClone(original).Build();
            Assert.That(clone.Title, Is.EqualTo(original.Title));
            Assert.That(clone.Author, Is.EqualTo(original.Author));
            Assert.That(clone.CreationDate, Is.EqualTo(original.CreationDate));
        }
        
        [Test]
        public void Clone_Should_CreateSpriteWithSameParameters()
        {
            SpriteAsset original = AssetCreator.CreateSprite();
            SpriteAsset clone = new SpriteAsset.Builder().AsClone(original).Build();
            Assert.That(clone.SpriteData, Is.EqualTo(original.SpriteData));
        }

        #endregion
        
        #region Weapons

        [Test]
        public void Build_Should_GiveWeaponProperIdentifier()
        {
            WeaponAsset weapon = new WeaponAsset.Builder().Build();
            Assert.That(weapon.ID[..2], Is.EqualTo(EditorAssetIDs.WeaponIdentifier));
        }
        
        [Test]
        public void Copy_Should_CreateWeaponWithSameID()
        {
            WeaponAsset original = AssetCreator.CreateWeapon();
            WeaponAsset copy = new WeaponAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.ID, Is.EqualTo(original.ID));
        }
        
        [Test]
        public void Copy_Should_CreateWeaponWithSameParameters()
        {
            WeaponAsset original = AssetCreator.CreateWeapon();
            WeaponAsset copy = new WeaponAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.UseType, Is.EqualTo(original.UseType));
        }
        
        [Test]
        public void Copy_Should_CreateWeaponBaseWithSameParameters()
        {
            WeaponAsset original = AssetCreator.CreateWeapon();
            WeaponAsset copy = new WeaponAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.Title, Is.EqualTo(original.Title));
            Assert.That(copy.Author, Is.EqualTo(original.Author));
            Assert.That(copy.CreationDate, Is.EqualTo(original.CreationDate));
        }

        [Test]
        public void Clone_Should_CreateWeaponWithDifferentID()
        {
            WeaponAsset original = AssetCreator.CreateWeapon();
            WeaponAsset clone = new WeaponAsset.Builder().AsClone(original).Build();
            Assert.That(clone.ID, Is.Not.EqualTo(original.ID));
        }

        [Test]
        public void Clone_Should_CreateWeaponBaseWithSameParameters()
        {
            WeaponAsset original = AssetCreator.CreateWeapon();
            WeaponAsset clone = new WeaponAsset.Builder().AsClone(original).Build();
            Assert.That(clone.Title, Is.EqualTo(original.Title));
            Assert.That(clone.Author, Is.EqualTo(original.Author));
            Assert.That(clone.CreationDate, Is.EqualTo(original.CreationDate));
        }

        [Test]
        public void Clone_Should_CreateWeaponWithSameParameters()
        {
            WeaponAsset original = AssetCreator.CreateWeapon();
            WeaponAsset clone = new WeaponAsset.Builder().AsClone(original).Build();
            Assert.That(clone.UseType, Is.EqualTo(original.UseType));
        }

        #endregion

        #region Projectiles

        [Test]
        public void Build_Should_GiveProjectileProperIdentifier()
        {
            ProjectileAsset projectile = new ProjectileAsset.Builder().Build();
            Assert.That(projectile.ID[..2], Is.EqualTo(EditorAssetIDs.ProjectileIdentifier));
        }

        [Test]
        public void Copy_Should_CreateProjectileWithSameID()
        {
            ProjectileAsset original = AssetCreator.CreateProjectile();
            ProjectileAsset copy = new ProjectileAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.ID, Is.EqualTo(original.ID));
        }
        
        [Test]
        public void Copy_Should_CreateProjectileWithSameParameters()
        {
            ProjectileAsset original = AssetCreator.CreateProjectile();
            ProjectileAsset copy = new ProjectileAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.BaseDamage, Is.EqualTo(original.BaseDamage));
        }
        
        [Test]
        public void Copy_Should_CreateProjectileBaseWithSameParameters()
        {
            ProjectileAsset original = AssetCreator.CreateProjectile();
            ProjectileAsset copy = new ProjectileAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.Title, Is.EqualTo(original.Title));
            Assert.That(copy.Author, Is.EqualTo(original.Author));
            Assert.That(copy.CreationDate, Is.EqualTo(original.CreationDate));
        }
        
        [Test]
        public void Clone_Should_CreateProjectileWithDifferentID()
        {
            ProjectileAsset original = AssetCreator.CreateProjectile();
            ProjectileAsset clone = new ProjectileAsset.Builder().AsClone(original).Build();
            Assert.That(clone.ID, Is.Not.EqualTo(original.ID));
        }
        
        [Test]
        public void Clone_Should_CreateProjectileBaseWithSameParameters()
        {
            ProjectileAsset original = AssetCreator.CreateProjectile();
            ProjectileAsset clone = new ProjectileAsset.Builder().AsClone(original).Build();
            Assert.That(clone.Title, Is.EqualTo(original.Title));
            Assert.That(clone.Author, Is.EqualTo(original.Author));
            Assert.That(clone.CreationDate, Is.EqualTo(original.CreationDate));
        }
        
        [Test]
        public void Clone_Should_CreateProjectileWithSameParameters()
        {
            ProjectileAsset original = AssetCreator.CreateProjectile();
            ProjectileAsset clone = new ProjectileAsset.Builder().AsClone(original).Build();
            Assert.That(clone.BaseDamage, Is.EqualTo(original.BaseDamage));
        }
        #endregion

        #region Enemies

        [Test]
        public void Build_Should_GiveEnemyProperIdentifier()
        {
            EnemyAsset enemy = new EnemyAsset.Builder().Build();
            Assert.That(enemy.ID[..2], Is.EqualTo(EditorAssetIDs.EnemyIdentifier));
        }
        
        [Test]
        public void Copy_Should_CreateEnemyWithSameID()
        {
            EnemyAsset original = AssetCreator.CreateEnemy();
            EnemyAsset copy = new EnemyAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.ID, Is.EqualTo(original.ID));
        }
        
        [Test]
        public void Copy_Should_CreateEnemyWithSameParameters()
        {
            EnemyAsset original = AssetCreator.CreateEnemy();
            EnemyAsset copy = new EnemyAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.MaxHealth, Is.EqualTo(original.MaxHealth));
        }
        
        [Test]
        public void Copy_Should_CreateEnemyBaseWithSameParameters()
        {
            EnemyAsset original = AssetCreator.CreateEnemy();
            EnemyAsset copy = new EnemyAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.Title, Is.EqualTo(original.Title));
            Assert.That(copy.Author, Is.EqualTo(original.Author));
            Assert.That(copy.CreationDate, Is.EqualTo(original.CreationDate));
        }
        
        [Test]
        public void Clone_Should_CreateEnemyWithDifferentID()
        {
            EnemyAsset original = AssetCreator.CreateEnemy();
            EnemyAsset clone = new EnemyAsset.Builder().AsClone(original).Build();
            Assert.That(clone.ID, Is.Not.EqualTo(original.ID));
        }
        
        [Test]
        public void Clone_Should_CreateEnemyBaseWithSameParameters()
        {
            EnemyAsset original = AssetCreator.CreateEnemy();
            EnemyAsset clone = new EnemyAsset.Builder().AsClone(original).Build();
            Assert.That(clone.Title, Is.EqualTo(original.Title));
            Assert.That(clone.Author, Is.EqualTo(original.Author));
            Assert.That(clone.CreationDate, Is.EqualTo(original.CreationDate));
        }
        
        [Test]
        public void Clone_Should_CreateEnemyWithSameParameters()
        {
            EnemyAsset original = AssetCreator.CreateEnemy();
            EnemyAsset clone = new EnemyAsset.Builder().AsClone(original).Build();
            Assert.That(clone.MaxHealth, Is.EqualTo(original.MaxHealth));
        }
        #endregion
        
        #region Rooms

        [Test]
        public void Build_Should_GiveRoomProperIdentifier()
        {
            RoomAsset room = new RoomAsset.Builder().Build();
            Assert.That(room.ID[..2], Is.EqualTo(EditorAssetIDs.RoomIdentifier));
            
        }
        
        [Test]
        public void Copy_Should_CreateRoomWithSameID()
        {
            RoomAsset original = AssetCreator.CreateRoom();
            RoomAsset copy = new RoomAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.ID, Is.EqualTo(original.ID));
        }
        
        [Test]
        public void Copy_Should_CreateRoomWithSameParameters()
        {
            RoomAsset original = AssetCreator.CreateRoom();
            RoomAsset copy = new RoomAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.TileGrid, Is.EqualTo(original.TileGrid));
        }
        
        [Test]
        public void Copy_Should_CreateRoomBaseWithSameParameters()
        {
            RoomAsset original = AssetCreator.CreateRoom();
            RoomAsset copy = new RoomAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.Title, Is.EqualTo(original.Title));
            Assert.That(copy.Author, Is.EqualTo(original.Author));
            Assert.That(copy.CreationDate, Is.EqualTo(original.CreationDate));
        }
        
        [Test]
        public void Clone_Should_CreateRoomWithDifferentID()
        {
            RoomAsset original = AssetCreator.CreateRoom();
            RoomAsset clone = new RoomAsset.Builder().AsClone(original).Build();
            Assert.That(clone.ID, Is.Not.EqualTo(original.ID));
        }
        
        [Test]
        public void Clone_Should_CreateRoomBaseWithSameParameters()
        {
            RoomAsset original = AssetCreator.CreateRoom();
            RoomAsset clone = new RoomAsset.Builder().AsClone(original).Build();
            Assert.That(clone.Title, Is.EqualTo(original.Title));
            Assert.That(clone.Author, Is.EqualTo(original.Author));
            Assert.That(clone.CreationDate, Is.EqualTo(original.CreationDate));
        }
        
        [Test]
        public void Clone_Should_CreateRoomWithSameParameters()
        {
            RoomAsset original = AssetCreator.CreateRoom();
            RoomAsset clone = new RoomAsset.Builder().AsClone(original).Build();
            Assert.That(clone.TileGrid, Is.EqualTo(original.TileGrid));
        }

        #endregion

        #region Tiles

        [Test]
        public void Build_Should_GiveTileProperIdentifier()
        {
            TileAsset tile = new TileAsset.Builder().Build();
            Assert.That(tile.ID[..2], Is.EqualTo(EditorAssetIDs.TileIdentifier));
        }
        
        [Test]
        public void Copy_Should_CreateTileWithSameID()
        {
            TileAsset original = AssetCreator.CreateTile();
            TileAsset copy = new TileAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.ID, Is.EqualTo(original.ID));
        }
        
        [Test]
        public void Copy_Should_CreateTileWithSameParameters()
        {
            TileAsset original = AssetCreator.CreateTile();
            TileAsset copy = new TileAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.Tile, Is.EqualTo(original.Tile));
        }
        
        [Test]
        public void Copy_Should_CreateTileBaseWithSameParameters()
        {
            TileAsset original = AssetCreator.CreateTile();
            TileAsset copy = new TileAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.Title, Is.EqualTo(original.Title));
            Assert.That(copy.Author, Is.EqualTo(original.Author));
            Assert.That(copy.CreationDate, Is.EqualTo(original.CreationDate));
        }
        
        [Test]
        public void Clone_Should_CreateTileWithDifferentID()
        {
            TileAsset original = AssetCreator.CreateTile();
            TileAsset clone = new TileAsset.Builder().AsClone(original).Build();
            Assert.That(clone.ID, Is.Not.EqualTo(original.ID));
        }
        
        [Test]
        public void Clone_Should_CreateTileBaseWithSameParameters()
        {
            TileAsset original = AssetCreator.CreateTile();
            TileAsset clone = new TileAsset.Builder().AsClone(original).Build();
            Assert.That(clone.Title, Is.EqualTo(original.Title));
            Assert.That(clone.Author, Is.EqualTo(original.Author));
            Assert.That(clone.CreationDate, Is.EqualTo(original.CreationDate));
        }
        
        [Test]
        public void Clone_Should_CreateTileWithSameParameters()
        {
            TileAsset original = AssetCreator.CreateTile();
            TileAsset clone = new TileAsset.Builder().AsClone(original).Build();
            Assert.That(clone.Tile, Is.EqualTo(original.Tile));
        }
        #endregion
        
        #region Campaigns

        [Test]
        public void Build_Should_GiveCampaignProperIdentifier()
        {
            CampaignAsset campaign = new CampaignAsset.Builder().Build();
            Assert.That(campaign.ID[..2], Is.EqualTo(EditorAssetIDs.CampaignIdentifier));
        }
        
        [Test]
        public void Copy_Should_CreateCampaignWithSameID()
        {
            CampaignAsset original = AssetCreator.CreateCampaign();
            CampaignAsset copy = new CampaignAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.ID, Is.EqualTo(original.ID));
        }
        
        [Test]
        public void Copy_Should_CreateCampaignWithSameParameters()
        {
            CampaignAsset original = AssetCreator.CreateCampaign();
            CampaignAsset copy = new CampaignAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.AdventureLength, Is.EqualTo(original.AdventureLength));
        }
        
        [Test]
        public void Copy_Should_CreateCampaignBaseWithSameParameters()
        {
            CampaignAsset original = AssetCreator.CreateCampaign();
            CampaignAsset copy = new CampaignAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.Title, Is.EqualTo(original.Title));
            Assert.That(copy.Author, Is.EqualTo(original.Author));
            Assert.That(copy.CreationDate, Is.EqualTo(original.CreationDate));
        }
        
        [Test]
        public void Clone_Should_CreateCampaignWithDifferentID()
        {
            CampaignAsset original = AssetCreator.CreateCampaign();
            CampaignAsset clone = new CampaignAsset.Builder().AsClone(original).Build();
            Assert.That(clone.ID, Is.Not.EqualTo(original.ID));
        }
        
        [Test]
        public void Clone_Should_CreateCampaignBaseWithSameParameters()
        {
            CampaignAsset original = AssetCreator.CreateCampaign();
            CampaignAsset clone = new CampaignAsset.Builder().AsClone(original).Build();
            Assert.That(clone.Title, Is.EqualTo(original.Title));
            Assert.That(clone.Author, Is.EqualTo(original.Author));
            Assert.That(clone.CreationDate, Is.EqualTo(original.CreationDate));
        }
        
        [Test]
        public void Clone_Should_CreateCampaignWithSameParameters()
        {
            CampaignAsset original = AssetCreator.CreateCampaign();
            CampaignAsset clone = new CampaignAsset.Builder().AsClone(original).Build();
            Assert.That(clone.AdventureLength, Is.EqualTo(original.AdventureLength));
        }

        #endregion

        #region Preferences

        [Test]
        public void Copy_Should_CreatePreferencesWithSameParameters()
        {
            PreferencesAsset original = new PreferencesAsset.Builder().Build();
            PreferencesAsset copy = new PreferencesAsset.Builder().AsCopy(original).Build();
            Assert.That(copy.MasterVolume, Is.EqualTo(original.MasterVolume));
            Assert.That(copy.MusicVolume, Is.EqualTo(original.MusicVolume));
            Assert.That(copy.SoundVolume, Is.EqualTo(original.SoundVolume));
            Assert.That(copy.UIVolume, Is.EqualTo(original.UIVolume));
            Assert.That(copy.Resolution, Is.EqualTo(original.Resolution));
            Assert.That(copy.ScreenMode, Is.EqualTo(original.ScreenMode));
            Assert.That(copy.VSync, Is.EqualTo(original.VSync));
        }

        #endregion
    }
}