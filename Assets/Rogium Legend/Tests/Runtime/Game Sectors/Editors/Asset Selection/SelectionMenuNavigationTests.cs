using System.Collections;
using NUnit.Framework;
using RedRats.Core;
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
    /// Tests for navigating the selection menu.
    /// </summary>
    public class SelectionMenuNavigationTests : MenuTestBase
    {
        private SelectionMenuOverseerMono selectionMenu;
        
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            OverseerLoader.LoadModalWindowBuilder();
            OverseerLoader.LoadUIBuilder();
            yield return MenuLoader.PrepareSelectionMenu();
            selectionMenu = SelectionMenuOverseerMono.GetInstance();
            AddNewPackToLibrary();
            AddNewPackToLibrary();
            ExternalLibraryOverseer.Instance.Packs[0].Palettes.Add(CreatePalette());
            ExternalLibraryOverseer.Instance.Packs[0].Palettes.Add(CreatePalette());
            ExternalLibraryOverseer.Instance.Packs[1].Sprites.Add(CreateSprite());
            yield return null;
        }

        [UnityTest]
        public IEnumerator Should_OpenForPalettes_WhenOpenPackThenPalettesThenPackThenPalettes()
        {
            GASButtonActions.OpenSelectionPack();
            GASButtonActions.OpenEditor(0);
            yield return null;
            GASButtonActions.OpenSelectionPack();
            GASButtonActions.OpenEditor(1);
            yield return null;
            Assert.That(selectionMenu.CurrentType, Is.EqualTo(AssetType.Palette));
        }
        
        [UnityTest]
        public IEnumerator Should_ContainOnlyPalettesFromCurrentPack_WhenOpenPackThenPalettesThenPackThenPalettes()
        {
            GASButtonActions.OpenSelectionPack();
            GASButtonActions.OpenEditor(0);
            yield return null;
            GASButtonActions.OpenSelectionPack();
            GASButtonActions.OpenEditor(1);
            yield return null;
            Assert.That(selectionMenu.CurrentSelector.Content.ActiveChildCount(), Is.EqualTo(PackEditorOverseer.Instance.CurrentPack.Palettes.Count + 1));
        }

        [UnityTest]
        public IEnumerator Should_OpenForPalettes_WhenOpenPackThenPalettesThenSpritesThenPackThenPalettes()
        {
            GASButtonActions.OpenSelectionPack();
            GASButtonActions.OpenEditor(0);
            GASButtonActions.OpenSelectionSprite();
            yield return null;
            GASButtonActions.OpenSelectionPack();
            GASButtonActions.OpenEditor(1);
            yield return null;
            Assert.That(selectionMenu.CurrentType, Is.EqualTo(AssetType.Palette));
        }
        
        [UnityTest]
        public IEnumerator Should_ContainOnlyPalettesFromCurrentPack_WhenOpenPackThenPalettesThenSpritesThenPackThenPalettes()
        {
            GASButtonActions.OpenSelectionPack();
            GASButtonActions.OpenEditor(0);
            GASButtonActions.OpenSelectionSprite();
            yield return null;
            GASButtonActions.OpenSelectionPack();
            GASButtonActions.OpenEditor(1);
            yield return null;
            Assert.That(selectionMenu.CurrentSelector.Content.ActiveChildCount(), Is.EqualTo(PackEditorOverseer.Instance.CurrentPack.Palettes.Count + 1));
        }
    }
}