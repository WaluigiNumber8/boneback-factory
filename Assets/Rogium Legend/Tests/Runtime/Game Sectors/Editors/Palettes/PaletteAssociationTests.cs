using System.Collections;
using NUnit.Framework;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Sprites;
using Rogium.Systems.Toolbox;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.TestTools;

namespace Rogium.Tests.Editors.Palettes
{
    /// <summary>
    /// Tests for <see cref="PaletteAsset"/>s being associated with other assets.
    /// </summary>
    public class PaletteAssociationTests : MenuTestBase
    {
        private PackEditorOverseer packEditor;
        private SpriteEditorOverseer spriteEditor;

        public override IEnumerator Setup()
        {
            yield return base.Setup();
            packEditor = PackEditorOverseer.Instance;
            spriteEditor = SpriteEditorOverseer.Instance;
            
            yield return MenuLoader.PrepareSpriteEditor();
            yield return null;
        }

        [Test]
        public void Should_CreatePaletteAssetWithNoAssociations()
        {
            PaletteAsset palette = AssetCreator.CreatePalette();
            Assert.That(palette.AssociatedAssetsIDs, Is.Empty);
        }

        [Test]
        public void Should_AddAssociationToPaletteAsset_WhenSpriteIsSavedAndPaletteExists()
        {
            PaletteAssociationTestsU.SwitchToFirstPalette();
            spriteEditor.CompleteEditing();
            Assert.That(packEditor.CurrentPack.Palettes[0].AssociatedAssetsIDs.Contains(packEditor.CurrentPack.Sprites[0].ID));
        }

        [Test]
        public void Should_AddPaletteAssociationToCurrentlyEditedSpriteAsset_WhenPaletteSwitched()
        {
            PaletteAssociationTestsU.SwitchToFirstPalette();
            Assert.That(spriteEditor.CurrentAsset.AssociatedPaletteID, Is.EqualTo(packEditor.CurrentPack.Palettes[0].ID));
        }
        
        [Test]
        public void Should_AddPaletteAssociationToSpriteAsset_WhenSpriteIsSavedAndPaletteExists()
        {
            PaletteAssociationTestsU.SwitchToFirstPalette();
            spriteEditor.CompleteEditing();
            Assert.That(packEditor.CurrentPack.Sprites[0].AssociatedPaletteID, Is.EqualTo(packEditor.CurrentPack.Palettes[0].ID));
        }

        [UnityTest]
        public IEnumerator Should_UpdateSpriteColor_WhenAssociatedPaletteWasUpdated()
        {
            // Assign Palette to Sprite
            PaletteAssociationTestsU.SwitchToFirstPalette();
            PaletteAssociationTestsU.FillSpriteEditorGrid();
            spriteEditor.CompleteEditing();
            Color startColor = packEditor.CurrentPack.Sprites[0].Icon.texture.GetPixel(0, 0);
            
            yield return MenuLoader.PreparePaletteEditor();
            PaletteEditorOverseerMono.GetInstance().UpdateColorSlotColor(Color.blue, 0);
            PaletteEditorOverseer.Instance.CompleteEditing();
            Color endColor = packEditor.CurrentPack.Sprites[0].Icon.texture.GetPixel(0, 0);
            Assert.That(startColor, Is.Not.EqualTo(endColor));
        }
    }
}