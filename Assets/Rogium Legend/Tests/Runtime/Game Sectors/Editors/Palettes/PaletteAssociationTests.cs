using System.Collections;
using NUnit.Framework;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Sprites;
using Rogium.Systems.Toolbox;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.TestTools;
using static Rogium.Tests.Editors.Palettes.PaletteAssociationTestsU;

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
            SwitchToFirstPalette();
            spriteEditor.CompleteEditing();
            Assert.That(packEditor.CurrentPack.Palettes[0].AssociatedAssetsIDs.Contains(packEditor.CurrentPack.Sprites[0].ID));
        }

        [Test]
        public void Should_AddPaletteAssociationToCurrentlyEditedSpriteAsset_WhenPaletteSwitched()
        {
            SwitchToFirstPalette();
            Assert.That(spriteEditor.CurrentAsset.AssociatedPaletteID, Is.EqualTo(packEditor.CurrentPack.Palettes[0].ID));
        }
        
        [Test]
        public void Should_AddPaletteAssociationToSpriteAsset_WhenSpriteIsSavedAndPaletteExists()
        {
            SwitchToFirstPalette();
            spriteEditor.CompleteEditing();
            Assert.That(packEditor.CurrentPack.Sprites[0].AssociatedPaletteID, Is.EqualTo(packEditor.CurrentPack.Palettes[0].ID));
        }

        [UnityTest]
        public IEnumerator Should_UpdateSpriteIconColor_WhenAssociatedPaletteWasUpdated()
        {
            SwitchToFirstPalette();
            FillSpriteEditorGrid();
            spriteEditor.CompleteEditing();

            yield return UpdatePaletteColorInPaletteEditor(Color.blue);
            Color color = packEditor.CurrentPack.Sprites[0].Icon.texture.GetPixel(0, 0);
            Assert.That(color, Is.EqualTo(Color.blue));
        }
        
        [UnityTest]
        public IEnumerator Should_UpdateWeaponIconColor_WhenAssociatedPaletteWasUpdated()
        {
            SwitchToFirstPalette();
            FillSpriteEditorGrid();
            spriteEditor.CompleteEditing();
            
            yield return UpdateSpriteOfWeaponInEditor();
            yield return UpdatePaletteColorInPaletteEditor(Color.blue);
            
            Color color = packEditor.CurrentPack.Weapons[0].Icon.texture.GetPixel(0, 0);
            Assert.That(color, Is.EqualTo(Color.blue));
        }

        [Test]
        public void Should_UpdateSpriteIconColor_WhenAssociatedPaletteUpdatedInSpriteEditor()
        {
            SwitchToFirstPalette();
            FillSpriteEditorGrid();
            spriteEditor.CompleteEditing();

            UpdatePaletteColorInSpriteEditor();

            Color color = packEditor.CurrentPack.Sprites[0].Icon.texture.GetPixel(0, 0);
            Assert.That(color, Is.EqualTo(Color.yellow));
        }
    }
}