using NUnit.Framework;
using Rogium.Editors.Campaign;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Palettes;
using Rogium.Editors.Rooms;
using Rogium.Editors.Sprites;
using Rogium.Editors.Weapons;

namespace Rogium.Tests.Editors.AssetManipulation
{
    public class AssetCreationTests
    {
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
    }
}