using NUnit.Framework;
using Rogium.Editors.Campaign;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Weapons;

namespace Rogium.Tests.Editors.AssetManipulation
{
    public class AssetCreationTests
    {
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