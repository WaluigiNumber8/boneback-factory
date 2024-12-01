using System.Collections;
using System.Collections.ObjectModel;
using NUnit.Framework;
using RedRats.Systems.Themes;
using Rogium.Core;
using Rogium.Editors.Campaign;
using Rogium.Editors.Core;
using Rogium.Editors.NewAssetSelection;
using Rogium.Editors.Packs;
using Rogium.Editors.Weapons;
using Rogium.Systems.GASExtension;
using Rogium.Tests.Core;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
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
        private SelectionInfoColumn infoColumn;
        private PackAsset currentPack;

        public override IEnumerator Setup()
        {
            yield return base.Setup();
            OverseerLoader.LoadModalWindowBuilder();
            OverseerLoader.LoadUIBuilder();
            yield return MenuLoader.PrepareSelectionMenu();
            selectionMenu = SelectionMenuOverseerMono.GetInstance();
            infoColumn = selectionMenu.GetComponentInChildren<SelectionInfoColumn>();
            AddNewPackToLibrary();
            AddNewPackToLibrary();
            currentPack = PackEditorOverseer.Instance.CurrentPack;
            yield return null;
        }

        [UnityTest]
        public IEnumerator Should_ShowPaletteTitle_WhenPaletteCardClicked()
        {
            yield return OpenPackAndSelectPalette();
            Assert.That(infoColumn.Title, Is.EqualTo(currentPack.Palettes[0].Title));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowRoomTitle_WhenRoomCardClicked()
        {
            yield return OpenPackAndSelectRoom();
            Assert.That(infoColumn.Title, Is.EqualTo(currentPack.Rooms[0].Title));
        }

        [UnityTest]
        public IEnumerator Should_ShowPaletteIcon_WhenPaletteCardClicked()
        {
            yield return OpenPackAndSelectPalette();
            Assert.That(infoColumn.Icon, Is.EqualTo(currentPack.Palettes[0].Icon));
        }

        [UnityTest]
        public IEnumerator Should_ShowRoomBanner_WhenRoomCardClicked()
        {
            yield return OpenPackAndSelectRoom();
            Assert.That(infoColumn.BannerIcon, Is.EqualTo(currentPack.Rooms[0].Banner));
        }

        [UnityTest]
        public IEnumerator Should_ShowEmptyText_WhenSpriteMenuOpenedAndNothingSelected()
        {
            yield return OpenPackAndSelectPalette();
            selectionMenu.Open(AssetType.Sprite);
            yield return null;
            Assert.That(infoColumn.Title, Is.EqualTo("Select a sprite"));
        }

        [UnityTest]
        public IEnumerator Should_HideIconAndBanner_WhenSpriteMenuOpenedAndNothingSelected()
        {
            yield return OpenPackAndSelectPalette();
            selectionMenu.Open(AssetType.Sprite);
            yield return null;
            Assert.That(infoColumn.IconShown, Is.False);
            Assert.That(infoColumn.BannerShown, Is.False);
        }

        [UnityTest]
        public IEnumerator Should_RefreshInfo_WhenWeaponEdited()
        {
            yield return OpenPackAndSelectWeapon();
            PackEditorOverseer.Instance.ActivateWeaponEditor(0, false);
            WeaponEditorOverseer.Instance.CurrentAsset.UpdateBaseDamage(10);
            WeaponEditorOverseer.Instance.CompleteEditing();
            yield return null;
            Assert.That(infoColumn.GetProperty<string>(0).PropertyValue, Is.EqualTo("10"));
        }

        [UnityTest]
        public IEnumerator Should_RefreshInfo_WhenWeaponSelectionOpenAfterSpriteSelection()
        {
            yield return OpenPackAndSelectSprite();
            yield return OpenPackAndSelectWeapon();
            selectionMenu.Open(AssetType.Sprite);
            yield return null;
            Assert.That(infoColumn.Title, Is.EqualTo(currentPack.Sprites[0].Title));
        }

        [UnityTest]
        public IEnumerator Should_ShowEmpty_WhenSelectedAssetDeleted()
        {
            string title = ExternalLibraryOverseer.Instance.Packs[1].Title;
            yield return SelectPack(0);
            ExternalLibraryOverseer.Instance.DeletePack(0);
            yield return null;
            yield return SelectPack(0);
            Assert.That(infoColumn.Title, Is.EqualTo(title));
        }

        [UnityTest]
        public IEnumerator Should_KeepPropertyThemeToBlue_WhenCreatingPropertiesInColumn()
        {
            yield return OpenPackAndSelectEnemy();
            Assert.That(infoColumn.GetProperty<string>(0).GetComponentInChildren<TextMeshProUGUI>().color, Is.EqualTo(ThemeOverseerMono.GetInstance().GetThemeData(ThemeType.Blue).Fonts.general.color));
        }

        [UnityTest]
        public IEnumerator Should_MakeEnemyCardsRed_WhenEnemySelectionOpened()
        {
            yield return OpenPackAndSelectEnemy();
            Assert.That(selectionMenu.CurrentSelector.GetCard(0).GetComponentInChildren<Image>().sprite, Is.EqualTo(ThemeOverseerMono.GetInstance().GetThemeData(ThemeType.Red).Interactables.assetCard.normal));
        }

        [UnityTest]
        public IEnumerator Should_KeepPropertiesThemeBlue_WhenReturnFromEnemyEditor()
        {
            yield return OpenPackAndSelectEnemy();
            yield return MenuLoader.PrepareEnemyEditor(false);
            GASButtonActions.OpenEditorEnemy(0);
            GASButtonActions.CancelChangesEnemy();
            yield return null;
            Assert.That(infoColumn.GetProperty<string>(0).GetComponentInChildren<TextMeshProUGUI>().color, Is.EqualTo(ThemeOverseerMono.GetInstance().GetThemeData(ThemeType.Blue).Fonts.general.color));
        }

        [UnityTest]
        public IEnumerator Should_NotChangeCardsAccordingToTheme_WhenPackSelectionOpened()
        {
            yield return OpenPackAndSelectEnemy();
            yield return SelectPack(0);
            yield return null;
            Assert.That(selectionMenu.CurrentSelector.GetCard(0).GetComponentInChildren<Image>().sprite, Is.EqualTo(AssetDatabase.LoadAssetAtPath<EditableAssetCardController>("Assets/Rogium Legend/Prefabs/UI/Asset Selection Cards/prefvar_AssetCard_Pack.prefab").GetComponentInChildren<Image>().sprite));
        }
    
        #region Properties
        [UnityTest]
        public IEnumerator Should_ShowPackProperties_WhenPackCardClicked()
        {
            yield return SelectPack();
            Assert.That(infoColumn.PropertiesCount, Is.GreaterThan(0));
        }

        [UnityTest]
        public IEnumerator Should_ShowPackPalettesAmount_WhenPackCardClicked()
        {
            yield return SelectPack();
            Assert.That(infoColumn.GetProperty<string>(0).PropertyValue, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Palettes.Count.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowPackSpritesAmount_WhenPackCardClicked()
        {
            yield return SelectPack();
            Assert.That(infoColumn.GetProperty<string>(1).PropertyValue, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Sprites.Count.ToString()));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowPackWeaponsAmount_WhenPackCardClicked()
        {
            yield return SelectPack();
            Assert.That(infoColumn.GetProperty<string>(2).PropertyValue, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Weapons.Count.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowPackProjectilesAmount_WhenPackCardClicked()
        {
            yield return SelectPack();
            Assert.That(infoColumn.GetProperty<string>(3).PropertyValue, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Projectiles.Count.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowPackEnemiesAmount_WhenPackCardClicked()
        {
            yield return SelectPack();
            Assert.That(infoColumn.GetProperty<string>(4).PropertyValue, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Enemies.Count.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowPackRoomsAmount_WhenPackCardClicked()
        {
            yield return SelectPack();
            Assert.That(infoColumn.GetProperty<string>(5).PropertyValue, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Rooms.Count.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowPackTilesAmount_WhenPackCardClicked()
        {
            yield return SelectPack();
            Assert.That(infoColumn.GetProperty<string>(6).PropertyValue, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Tiles.Count.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowNoProperties_WhenPaletteCardClicked()
        {
            yield return OpenPackAndSelectPalette();
            Assert.That(infoColumn.PropertiesCount, Is.EqualTo(0));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowSpriteAssociatedPalette_WhenSpriteCardClicked()
        {
            currentPack.Sprites[0].UpdateAssociatedPaletteID(currentPack.Palettes[0].ID);
            yield return OpenPackAndSelectSprite();
            Assert.That(infoColumn.GetProperty<ReadOnlyCollection<Sprite>>(0).PropertyValue[0], Is.EqualTo(currentPack.Palettes[0].Icon));
        }

        [UnityTest]
        public IEnumerator Should_ShowWeaponProperties_WhenWeaponCardClicked()
        {
            yield return OpenPackAndSelectWeapon();
            Assert.That(infoColumn.PropertiesCount, Is.GreaterThan(0));
        }

        [UnityTest]
        public IEnumerator Should_ShowWeaponDamageProperty_WhenWeaponCardClicked()
        {
            yield return OpenPackAndSelectWeapon();
            Assert.That(infoColumn.GetProperty<string>(0).PropertyValue, Is.EqualTo(currentPack.Weapons[0].BaseDamage.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowWeaponTypeProperty_WhenWeaponCardClickedAndWeaponIsActive()
        {
            yield return OpenPackAndSelectWeapon();
            Assert.That(infoColumn.GetProperty<string>(1).PropertyValue, Is.EqualTo("Active"));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowWeaponTypeProperty_WhenWeaponCardClickedAndWeaponIsEvasive()
        {
            currentPack.Weapons[0].UpdateIsEvasive(true);
            yield return OpenPackAndSelectWeapon();
            Assert.That(infoColumn.GetProperty<string>(1).PropertyValue, Is.EqualTo("Evasive"));
        }

        [UnityTest]
        public IEnumerator Should_ShowWeaponProjectilesProperty_WhenWeaponCardClicked()
        {
            currentPack.Weapons[0].UpdateProjectileIDsLength(1);
            currentPack.Weapons[0].UpdateProjectileIDsPosID(0, currentPack.Projectiles[0].ID);
            yield return OpenPackAndSelectWeapon();
            Assert.That(infoColumn.GetProperty<ReadOnlyCollection<Sprite>>(2).PropertyValue[0], Is.EqualTo(currentPack.Projectiles[0].Icon));
        }

        [UnityTest]
        public IEnumerator Should_ShowProjectileProperties_WhenProjectileCardClicked()
        {
            yield return OpenPackAndSelectProjectile();
            Assert.That(infoColumn.PropertiesCount, Is.GreaterThan(0));
        }

        [UnityTest]
        public IEnumerator Should_ShowProjectileDamageProperty_WhenProjectileCardClicked()
        {
            yield return OpenPackAndSelectProjectile();
            Assert.That(infoColumn.GetProperty<string>(0).PropertyValue, Is.EqualTo(currentPack.Projectiles[0].BaseDamage.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowProjectileFlightSpeedProperty_WhenProjectileCardClicked()
        {
            yield return OpenPackAndSelectProjectile();
            Assert.That(infoColumn.GetProperty<string>(1).PropertyValue, Is.EqualTo(currentPack.Projectiles[0].FlightSpeed.ToString()));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowProjectilePierceTypeProperty_WhenProjectileCardClicked()
        {
            yield return OpenPackAndSelectProjectile();
            Assert.That(infoColumn.GetProperty<string>(2).PropertyValue, Is.EqualTo(currentPack.Projectiles[0].PierceType.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowEnemyProperties_WhenEnemyCardClicked()
        {
            yield return OpenPackAndSelectEnemy();
            Assert.That(infoColumn.PropertiesCount, Is.GreaterThan(0));
        }

        [UnityTest]
        public IEnumerator Should_ShowEnemyDamageProperty_WhenEnemyCardClicked()
        {
            yield return OpenPackAndSelectEnemy();
            Assert.That(infoColumn.GetProperty<string>(0).PropertyValue, Is.EqualTo(currentPack.Enemies[0].BaseDamage.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowEnemyHealthProperty_WhenEnemyCardClicked()
        {
            yield return OpenPackAndSelectEnemy();
            Assert.That(infoColumn.GetProperty<string>(1).PropertyValue, Is.EqualTo(currentPack.Enemies[0].MaxHealth.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowEnemyUsedWeaponsProperty_WhenEnemyCardClicked()
        {
            currentPack.Enemies[0].UpdateWeaponIDsLength(1);
            currentPack.Enemies[0].UpdateWeaponIDPos(0, currentPack.Weapons[0].ID);
            yield return OpenPackAndSelectEnemy();
            Assert.That(infoColumn.GetProperty<ReadOnlyCollection<Sprite>>(2).PropertyValue[0], Is.EqualTo(currentPack.Weapons[0].Icon));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowRoomProperties_WhenRoomCardClicked()
        {
            yield return OpenPackAndSelectRoom();
            Assert.That(infoColumn.PropertiesCount, Is.GreaterThan(0));
        }

        [UnityTest]
        public IEnumerator Should_ShowRoomTypeProperty_WhenRoomCardClicked()
        {
            yield return OpenPackAndSelectRoom();
            Assert.That(infoColumn.GetProperty<string>(0).PropertyValue, Is.EqualTo(currentPack.Rooms[0].Type.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowRoomTierProperty_WhenRoomCardClicked()
        {
            yield return OpenPackAndSelectRoom();
            Assert.That(infoColumn.GetProperty<string>(1).PropertyValue, Is.EqualTo(currentPack.Rooms[0].DifficultyLevel.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowTileProperties_WhenTileCardClicked()
        {
            yield return OpenPackAndSelectTile();
            Assert.That(infoColumn.PropertiesCount, Is.GreaterThan(0));
        }

        [UnityTest]
        public IEnumerator Should_ShowTileTypeProperty_WhenTileCardClicked()
        {
            yield return OpenPackAndSelectTile();
            Assert.That(infoColumn.GetProperty<string>(0).PropertyValue, Is.EqualTo(currentPack.Tiles[0].Type.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowTileLayerProperty_WhenTileCardClicked()
        {
            yield return OpenPackAndSelectTile();
            Assert.That(infoColumn.GetProperty<string>(1).PropertyValue, Is.EqualTo(currentPack.Tiles[0].LayerType.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowTileTerrainTypeProperty_WhenTileCardClicked()
        {
            yield return OpenPackAndSelectTile();
            Assert.That(infoColumn.GetProperty<string>(2).PropertyValue, Is.EqualTo(currentPack.Tiles[0].TerrainType.ToString()));
        }
        #endregion
    }
}