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
            yield return null;
        }

        [UnityTest]
        public IEnumerator Should_ShowPaletteTitle_WhenPaletteCardClicked()
        {
            yield return OpenPackAndSelectPalette();
            Assert.That(selectionInfoColumn.Title, Is.EqualTo(PackEditorOverseer.Instance.CurrentPack.Palettes[0].Title));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowRoomTitle_WhenRoomCardClicked()
        {
            yield return OpenPackAndSelectRoom();
            Assert.That(selectionInfoColumn.Title, Is.EqualTo(PackEditorOverseer.Instance.CurrentPack.Rooms[0].Title));
        }

        [UnityTest]
        public IEnumerator Should_ShowPaletteIcon_WhenPaletteCardClicked()
        {
            yield return OpenPackAndSelectPalette();
            Assert.That(selectionInfoColumn.Icon, Is.EqualTo(PackEditorOverseer.Instance.CurrentPack.Palettes[0].Icon));
        }

        [UnityTest]
        public IEnumerator Should_ShowRoomBanner_WhenRoomCardClicked()
        {
            yield return OpenPackAndSelectRoom();
            Assert.That(selectionInfoColumn.BannerIcon, Is.EqualTo(PackEditorOverseer.Instance.CurrentPack.Rooms[0].Icon));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowSpriteAssociatedPalette_WhenSpriteCardClicked()
        {
            PackEditorOverseer.Instance.CurrentPack.Sprites[0].UpdateAssociatedPaletteID(PackEditorOverseer.Instance.CurrentPack.Palettes[0].ID);
            yield return OpenPackAndSelectSprite();
            Assert.That(selectionInfoColumn.GetProperty<ReadOnlyCollection<Sprite>>(0).PropertyValue[0], Is.EqualTo(PackEditorOverseer.Instance.CurrentPack.Palettes[0].Icon));
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
            Assert.That(selectionInfoColumn.GetProperty<string>(0).PropertyValue, Is.EqualTo(PackEditorOverseer.Instance.CurrentPack.Rooms[0].Type.ToString()));
        }

        [UnityTest]
        public IEnumerator Should_ShowRoomTierProperty_WhenRoomCardClicked()
        {
            yield return OpenPackAndSelectRoom();
            Assert.That(selectionInfoColumn.GetProperty<string>(1).PropertyValue, Is.EqualTo(PackEditorOverseer.Instance.CurrentPack.Rooms[0].DifficultyLevel.ToString()));
        }
    }
}