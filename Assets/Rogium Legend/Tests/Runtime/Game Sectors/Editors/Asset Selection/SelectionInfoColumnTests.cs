using System.Collections;
using NUnit.Framework;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.NewAssetSelection;
using Rogium.Editors.Packs;
using Rogium.Systems.GASExtension;
using Rogium.Tests.Core;
using UnityEngine.TestTools;
using static Rogium.Tests.Editors.AssetCreator;

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
            GASButtonActions.OpenSelectionPack();
            GASButtonActions.OpenEditor(0);
            selectionMenu.CurrentSelector.GetCard(0).SetToggle(true);
            yield return null;
            Assert.That(selectionInfoColumn.Title, Is.EqualTo(PackEditorOverseer.Instance.CurrentPack.Palettes[0].Title));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowRoomTitle_WhenRoomCardClicked()
        {
            GASButtonActions.OpenSelectionPack();
            GASButtonActions.OpenEditor(0);
            GASButtonActions.OpenSelectionRoom();
            selectionMenu.CurrentSelector.GetCard(0).SetToggle(true);
            yield return null;
            Assert.That(selectionInfoColumn.Title, Is.EqualTo(PackEditorOverseer.Instance.CurrentPack.Rooms[0].Title));
        }

        [UnityTest]
        public IEnumerator Should_ShowPaletteIcon_WhenPaletteCardClicked()
        {
            GASButtonActions.OpenSelectionPack();
            GASButtonActions.OpenEditor(0);
            selectionMenu.CurrentSelector.GetCard(0).SetToggle(true);
            yield return null;
            Assert.That(selectionInfoColumn.Icon, Is.EqualTo(PackEditorOverseer.Instance.CurrentPack.Palettes[0].Icon));
        }

        [UnityTest]
        public IEnumerator Should_ShowRoomBanner_WhenRoomCardClicked()
        {
            GASButtonActions.OpenSelectionPack();
            GASButtonActions.OpenEditor(0);
            GASButtonActions.OpenSelectionRoom();
            selectionMenu.CurrentSelector.GetCard(0).SetToggle(true);
            yield return null;
            Assert.That(selectionInfoColumn.BannerIcon, Is.EqualTo(PackEditorOverseer.Instance.CurrentPack.Rooms[0].Icon));
        }
    }
}