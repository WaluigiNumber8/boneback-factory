using System.Collections;
using NUnit.Framework;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.NewAssetSelection;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Tests.Core;
using Rogium.Tests.UI.Interactables.Properties;
using Rogium.UserInterface.Interactables;
using UnityEngine;
using UnityEngine.TestTools;
using static Rogium.Tests.Editors.AssetCreator;
using static Rogium.Tests.Editors.AssetSelection.SelectionMenuOverseerMonoTestsU;

namespace Rogium.Tests.Editors.AssetSelection
{
    /// <summary>
    /// Tests for working with the Asset Selection Menu.
    /// </summary>
    public class SelectionMenuOverseerMonoTests : MenuTestBase
    {
        private SelectionMenuOverseerMono selectionMenu;

        public override IEnumerator Setup()
        {
            yield return base.Setup();
            OverseerLoader.LoadModalWindowBuilder();
            OverseerLoader.LoadUIBuilder();
            yield return MenuLoader.PrepareSelectionMenuV2();
            selectionMenu = SelectionMenuOverseerMono.GetInstance();
            AddNewPackToLibrary();
            AddNewPackToLibrary();
            yield return null;
        }

        [Test]
        public void Should_FillWithPackAssetCards_WhenOpenForPacks()
        {
            selectionMenu.Open(AssetType.Pack);
            Assert.That(selectionMenu.CurrentSelector.Content.childCount, Is.EqualTo(4));
        }

        [Test]
        public void Should_OpenSelectionMenuForPalettes_WhenClickEditOnPackCard()
        {
            OpenPackSelectionAndEditFirstPack();
            Assert.That(selectionMenu.CurrentType, Is.EqualTo(AssetType.Palette));
        }

        [Test]
        public void Should_FillWithPaletteAssetCards_WhenClickEditOnPackCard()
        {
            OpenPackSelectionAndEditFirstPack();
            Assert.That(selectionMenu.CurrentSelector.Content.childCount, Is.EqualTo(2));
        }
        
        [UnityTest]
        public IEnumerator Should_NotDuplicatePaletteAssetCards_WhenEditPackLeaveAndEditAgain()
        {
            OpenPackSelectionAndEditFirstPack();
            OpenPackSelectionAndEditFirstPack();
            yield return null;
            Assert.That(selectionMenu.CurrentSelector.Content.childCount, Is.EqualTo(2));
        }

        [UnityTest]
        public IEnumerator Should_RefreshPaletteAssetCardTitle_WhenPaletteEditedAndSelectionMenuLoaded()
        {
            OpenPackSelectionAndEditFirstPack();
            PackEditorOverseer.Instance.ActivatePaletteEditor(0);
            PaletteEditorOverseer.Instance.CurrentAsset.UpdateTitle("Fred");
            PaletteEditorOverseer.Instance.CompleteEditing();
            selectionMenu.Open(AssetType.Palette);
            yield return null;
            Assert.That(selectionMenu.CurrentSelector.Content.GetChild(1).GetComponent<EditableAssetCardControllerV2>().Title, Is.EqualTo("Fred"));
        }

        [UnityTest]
        public IEnumerator Should_RefreshPaletteAssetCardIcon_WhenPaletteEditedAndSelectionMenuLoaded()
        {
            OpenPackSelectionAndEditFirstPack();
            PackEditorOverseer.Instance.ActivatePaletteEditor(0);
            PaletteEditorOverseer.Instance.CurrentAsset.Colors[0] = Color.blue;
            PaletteEditorOverseer.Instance.CompleteEditing();
            selectionMenu.Open(AssetType.Palette);
            yield return null;
            Texture2D icon = selectionMenu.CurrentSelector.Content.GetChild(1).GetComponent<EditableAssetCardControllerV2>().Icon.texture;
            Assert.That(icon.GetPixel(0, icon.height - 1), Is.EqualTo(Color.blue));
        }

        [UnityTest]
        public IEnumerator Should_SpawnPackCreateButtonAtStartOfAssetList_WhenOpenForPacks()
        {
            selectionMenu.Open(AssetType.Pack);
            yield return null;
            Assert.That(selectionMenu.CurrentSelector.Content.GetChild(0).GetComponent<InteractableButton>(), Is.Not.Null);
        }

        [UnityTest]
        public IEnumerator Should_CreateNewPackAsset_WhenClickCreatePackButton()
        {
            selectionMenu.Open(AssetType.Pack);
            yield return null;
            InteractableButton createButton = selectionMenu.CurrentSelector.Content.GetChild(0).GetComponent<InteractableButton>();
            createButton.Click();
            yield return null;
            InteractableUtils.FindFirstModalWindow().OnAccept();
            yield return null;
            Assert.That(ExternalLibraryOverseer.Instance.Packs.Count, Is.EqualTo(4));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowCreatedNewPackAssetInSelection_WhenClickCreatePackButton()
        {
            selectionMenu.Open(AssetType.Pack);
            yield return null;
            InteractableButton createButton = selectionMenu.CurrentSelector.Content.GetChild(0).GetComponent<InteractableButton>();
            createButton.Click();
            yield return null;
            InteractableUtils.FindFirstModalWindow().OnAccept();
            yield return null;
            Assert.That(selectionMenu.CurrentSelector.Content.childCount, Is.EqualTo(5));
        }
    }
}