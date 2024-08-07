using NUnit.Framework;
using Rogium.Editors.Weapons;

namespace Rogium.Tests.Editors.AssetManipulation
{
    public class AssetCreationTests
    {
        [Test]
        public void Copy_Should_CreateAssetWithSameID()
        {
            WeaponAsset original = PrepareWeapon();
            WeaponAsset copy = new WeaponAsset.WeaponBuilder().AsCopy(original).Build();
            Assert.That(copy.ID, Is.EqualTo(original.ID));
        }

        [Test]
        public void Copy_Should_CreateAssetWithSameParameters()
        {
            WeaponAsset original = PrepareWeapon();
            WeaponAsset copy = new WeaponAsset.WeaponBuilder().AsCopy(original).Build();
            Assert.That(copy.Title, Is.EqualTo(original.Title));
            Assert.That(copy.Author, Is.EqualTo(original.Author));
            Assert.That(copy.CreationDate, Is.EqualTo(original.CreationDate));
        }

        [Test]
        public void Copy_Should_CreateWeaponWithSameParameters()
        {
            WeaponAsset original = PrepareWeapon();
            WeaponAsset copy = new WeaponAsset.WeaponBuilder().AsCopy(original).Build();
            Assert.That(copy.UseType, Is.EqualTo(original.UseType));
        }

        [Test]
        public void Clone_Should_CreateAssetWithDifferentID()
        {
            WeaponAsset original = PrepareWeapon();
            WeaponAsset clone = new WeaponAsset.WeaponBuilder().AsClone(original).Build();
            Assert.That(clone.ID, Is.Not.EqualTo(original.ID));
        }

        [Test]
        public void Clone_Should_CreateAssetWithSameParameters()
        {
            WeaponAsset original = PrepareWeapon();
            WeaponAsset clone = new WeaponAsset.WeaponBuilder().AsClone(original).Build();
            Assert.That(clone.Title, Is.EqualTo(original.Title));
            Assert.That(clone.Author, Is.EqualTo(original.Author));
            Assert.That(clone.CreationDate, Is.EqualTo(original.CreationDate));
        }

        [Test]
        public void Clone_Should_CreateWeaponWithSameParameters()
        {
            WeaponAsset original = PrepareWeapon();
            WeaponAsset clone = new WeaponAsset.WeaponBuilder().AsClone(original).Build();
            Assert.That(clone.UseType, Is.EqualTo(original.UseType));
        }
        
        private static WeaponAsset PrepareWeapon()
        {
            WeaponAsset weapon = AssetCreator.CreateWeapon();
            weapon.UpdateUseType(WeaponUseType.Hidden);
            return weapon;
        }
    }
}