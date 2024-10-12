using System.Collections;
using System.Collections.ObjectModel;
using NUnit.Framework;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.NewAssetSelection;
using Rogium.Editors.Packs;
using Rogium.Systems.GASExtension;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.TestTools;
using static Rogium.Tests.Editors.AssetCreator;
using static Rogium.Tests.Editors.AssetSelection.SelectionInfoColumnTestsU;

namespace Rogium.Tests.Editors.AssetSelection
{
    /// <summary>
    /// Tests for the <see cref="SelectionInfoColumn"/> class.
    /// </summary>
    public class SelectionInfoColumnTests : MenuTestBase
    {
        private SelectionMenuOverseerMono selectionMenu;
        private SelectionInfoColumn selectionInfoColumn;
        private PackAsset currentPack;

        public override IEnumerator Setup()
        {
            yield return base.Setup();
            OverseerLoader.LoadModalWindowBuilder();
            OverseerLoader.LoadUIBuilder();
            yield return MenuLoader.PrepareSelectionMenuV2();
            selectionMenu = SelectionMenuOverseerMono.GetInstance();
            selectionInfoColumn = selectionMenu.GetComponentInChildren<SelectionInfoColumn>();
            AddNewPackToLibrary();
            AddNewPackToLibrary();
            currentPack = PackEditorOverseer.Instance.CurrentPack;
            yield return null;
        }

        [UnityTest]
        public IEnumerator Should_ShowPaletteTitle_WhenPaletteCardClicked()
        {
            yield return OpenPackAndSelectPalette();
            Assert.That(selectionInfoColumn.Title, Is.EqualTo(currentPack.Palettes[0].Title));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowRoomTitle_WhenRoomCardClicked()
        {
            yield return OpenPackAndSelectRoom();
            Assert.That(selectionInfoColumn.Title, Is.EqualTo(currentPack.Rooms[0].Title));
        }

        [UnityTest]
        public IEnumerator Should_ShowPackProperties_WhenPackCardClicked()
        {
            yield return SelectPack();
            Assert.That(selectionInfoColumn.PropertiesCount, Is.GreaterThan(0));
        }

        [UnityTest]
        public IEnumerator Should_ShowPackPalettesAmount_WhenPackCardClicked()
        {
            yield return SelectPack();
            Assert.That(selectionInfoColumn.GetProperty<string>(0).PropertyValue, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Palettes.Count.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowPackSpritesAmount_WhenPackCardClicked()
        {
            yield return SelectPack();
            Assert.That(selectionInfoColumn.GetProperty<string>(1).PropertyValue, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Sprites.Count.ToString()));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowPackWeaponsAmount_WhenPackCardClicked()
        {
            yield return SelectPack();
            Assert.That(selectionInfoColumn.GetProperty<string>(2).PropertyValue, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Weapons.Count.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowPackProjectilesAmount_WhenPackCardClicked()
        {
            yield return SelectPack();
            Assert.That(selectionInfoColumn.GetProperty<string>(3).PropertyValue, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Projectiles.Count.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowPackEnemiesAmount_WhenPackCardClicked()
        {
            yield return SelectPack();
            Assert.That(selectionInfoColumn.GetProperty<string>(4).PropertyValue, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Enemies.Count.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowPackRoomsAmount_WhenPackCardClicked()
        {
            yield return SelectPack();
            Assert.That(selectionInfoColumn.GetProperty<string>(5).PropertyValue, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Rooms.Count.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowPackTilesAmount_WhenPackCardClicked()
        {
            yield return SelectPack();
            Assert.That(selectionInfoColumn.GetProperty<string>(6).PropertyValue, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Tiles.Count.ToString()));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowPaletteIcon_WhenPaletteCardClicked()
        {
            yield return OpenPackAndSelectPalette();
            Assert.That(selectionInfoColumn.Icon, Is.EqualTo(currentPack.Palettes[0].Icon));
        }

        [UnityTest]
        public IEnumerator Should_ShowRoomBanner_WhenRoomCardClicked()
        {
            yield return OpenPackAndSelectRoom();
            Assert.That(selectionInfoColumn.BannerIcon, Is.EqualTo(currentPack.Rooms[0].Icon));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowSpriteAssociatedPalette_WhenSpriteCardClicked()
        {
            currentPack.Sprites[0].UpdateAssociatedPaletteID(currentPack.Palettes[0].ID);
            yield return OpenPackAndSelectSprite();
            Assert.That(selectionInfoColumn.GetProperty<ReadOnlyCollection<Sprite>>(0).PropertyValue[0], Is.EqualTo(currentPack.Palettes[0].Icon));
        }

        [UnityTest]
        public IEnumerator Should_ShowWeaponProperties_WhenWeaponCardClicked()
        {
            yield return OpenPackAndSelectWeapon();
            Assert.That(selectionInfoColumn.PropertiesCount, Is.GreaterThan(0));
        }

        [UnityTest]
        public IEnumerator Should_ShowWeaponDamageProperty_WhenWeaponCardClicked()
        {
            yield return OpenPackAndSelectWeapon();
            Assert.That(selectionInfoColumn.GetProperty<string>(0).PropertyValue, Is.EqualTo(currentPack.Weapons[0].BaseDamage.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowWeaponTypeProperty_WhenWeaponCardClickedAndWeaponIsActive()
        {
            yield return OpenPackAndSelectWeapon();
            Assert.That(selectionInfoColumn.GetProperty<string>(1).PropertyValue, Is.EqualTo("Active"));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowWeaponTypeProperty_WhenWeaponCardClickedAndWeaponIsEvasive()
        {
            currentPack.Weapons[0].UpdateIsEvasive(true);
            yield return OpenPackAndSelectWeapon();
            Assert.That(selectionInfoColumn.GetProperty<string>(1).PropertyValue, Is.EqualTo("Evasive"));
        }

        [UnityTest]
        public IEnumerator Should_ShowWeaponProjectilesProperty_WhenWeaponCardClicked()
        {
            currentPack.Weapons[0].UpdateProjectileIDsLength(1);
            currentPack.Weapons[0].UpdateProjectileIDsPosID(0, currentPack.Projectiles[0].ID);
            yield return OpenPackAndSelectWeapon();
            Assert.That(selectionInfoColumn.GetProperty<ReadOnlyCollection<Sprite>>(2).PropertyValue[0], Is.EqualTo(currentPack.Projectiles[0].Icon));
        }

        [UnityTest]
        public IEnumerator Should_ShowProjectileProperties_WhenProjectileCardClicked()
        {
            yield return OpenPackAndSelectProjectile();
            Assert.That(selectionInfoColumn.PropertiesCount, Is.GreaterThan(0));
        }

        [UnityTest]
        public IEnumerator Should_ShowProjectileDamageProperty_WhenProjectileCardClicked()
        {
            yield return OpenPackAndSelectProjectile();
            Assert.That(selectionInfoColumn.GetProperty<string>(0).PropertyValue, Is.EqualTo(currentPack.Projectiles[0].BaseDamage.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowProjectileFlightSpeedProperty_WhenProjectileCardClicked()
        {
            yield return OpenPackAndSelectProjectile();
            Assert.That(selectionInfoColumn.GetProperty<string>(1).PropertyValue, Is.EqualTo(currentPack.Projectiles[0].FlightSpeed.ToString()));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowProjectilePierceTypeProperty_WhenProjectileCardClicked()
        {
            yield return OpenPackAndSelectProjectile();
            Assert.That(selectionInfoColumn.GetProperty<string>(2).PropertyValue, Is.EqualTo(currentPack.Projectiles[0].PierceType.ToString()));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowRoomProperties_WhenRoomCardClicked()
        {
            yield return OpenPackAndSelectRoom();
            Assert.That(selectionInfoColumn.PropertiesCount, Is.GreaterThan(0));
        }

        [UnityTest]
        public IEnumerator Should_ShowRoomTypeProperty_WhenRoomCardClicked()
        {
            yield return OpenPackAndSelectRoom();
            Assert.That(selectionInfoColumn.GetProperty<string>(0).PropertyValue, Is.EqualTo(currentPack.Rooms[0].Type.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowRoomTierProperty_WhenRoomCardClicked()
        {
            yield return OpenPackAndSelectRoom();
            Assert.That(selectionInfoColumn.GetProperty<string>(1).PropertyValue, Is.EqualTo(currentPack.Rooms[0].DifficultyLevel.ToString()));
        }
    }
}