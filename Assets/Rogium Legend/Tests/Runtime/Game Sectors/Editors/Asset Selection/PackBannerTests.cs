using System.Collections;
using NSubstitute.Extensions;
using NUnit.Framework;
using RedRats.UI.ModalWindows;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.NewAssetSelection;
using Rogium.Editors.NewAssetSelection.UI;
using Rogium.Editors.Packs;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables;
using Rogium.UserInterface.ModalWindows;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Rogium.Tests.Editors.AssetSelection
{
    /// <summary>
    /// Tests for the <see cref="PackBanner"/> that info about the currently open pack.
    /// </summary>
    public class PackBannerTests : MenuTestBase
    {
        private SelectionMenuOverseerMono selectionMenu;
        private PackBanner packBanner;
        private PackAsset currentPack;

        public override IEnumerator Setup()
        {
            yield return base.Setup();
            OverseerLoader.LoadModalWindowBuilder();
            OverseerLoader.LoadUIBuilder();
            yield return MenuLoader.PrepareSelectionMenuV2();
            selectionMenu = SelectionMenuOverseerMono.GetInstance();
            packBanner = selectionMenu.GetComponentInChildren<PackBanner>();
            currentPack = ExternalLibraryOverseer.Instance.Packs[0];
            AssetCreator.AddNewPackToLibrary();
            yield return null;
            selectionMenu.Open(AssetType.Pack);
            ((EditableAssetCardControllerV2) selectionMenu.CurrentSelector.GetCard(0)).Edit();
            yield return null;
        }

        [Test]
        public void Should_DisplayPackTitle_WhenLoaded()
        {
            Assert.That(packBanner.Title, Is.EqualTo(currentPack.Title));
        }

        [UnityTest]
        public IEnumerator Should_DisplayPackTitle_WhenLoadedThenLoadedADifferentPack()
        {
            selectionMenu.Open(AssetType.Pack);
            ((EditableAssetCardControllerV2) selectionMenu.CurrentSelector.GetCard(1)).Edit();
            yield return null;
            Assert.That(packBanner.Title, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[1].Title));
        }

        [Test]
        public void Should_DisplayPackIcon_WhenLoaded()
        {
            Assert.That(packBanner.Icon, Is.EqualTo(currentPack.Icon));
        }

        [UnityTest]
        public IEnumerator Should_DisplayPackIcon_WhenLoadedThenLoadedADifferentPack()
        {
            selectionMenu.Open(AssetType.Pack);
            ((EditableAssetCardControllerV2) selectionMenu.CurrentSelector.GetCard(1)).Edit();
            yield return null;
            Assert.That(packBanner.Icon, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[1].Icon));
        }

        [UnityTest]
        public IEnumerator Should_DisplayPackConfigWindow_WhenBannerClicked()
        {
            packBanner.Config();
            yield return null;
            Assert.That(ModalWindowBuilder.GetInstance().GenericActiveWindows, Is.GreaterThan(0));
        }

        [UnityTest]
        public IEnumerator Should_RefreshBannerTitle_WhenPackTitleEdited()
        {
            packBanner.Config();
            yield return null;
            ModalWindow window = Object.FindFirstObjectByType<ModalWindow>();
            window.GetComponentInChildren<TMP_InputField>().text = "New Fred";
            yield return new WaitForSeconds(5f);
            window.OnAccept();
            yield return new WaitForSeconds(2f);
            Assert.That(packBanner.Title, Is.EqualTo("New Fred"));
        }
        
        [UnityTest]
        public IEnumerator Should_RefreshBannerIcon_WhenPackTitleEdited()
        {
            PackEditorOverseer.Instance.CurrentPack.Sprites.Add(AssetCreator.CreateSprite(Color.green));
            packBanner.Config();
            yield return null;
            ModalWindow window = Object.FindFirstObjectByType<ModalWindow>();
            window.GetComponentInChildren<AssetField>().OnPointerClick(PointerDataCreator.LeftClick());
            yield return null;
            AssetPickerWindow picker = Object.FindFirstObjectByType<AssetPickerWindow>();
            picker.Select(1);
            picker.ConfirmSelection();
            yield return new WaitForSeconds(5f);
            window.OnAccept();
            yield return new WaitForSeconds(2f);
            Assert.That(packBanner.Icon, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Sprites[1].Icon));
        }
    }
}