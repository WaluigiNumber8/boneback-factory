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
            PaletteAsset palette = TUtilsAssetCreator.CreatePalette();
            Assert.That(palette.AssociatedAssetsIDs, Is.Empty);
        }

        [Test]
        public void Should_AddAssociationToPaletteAsset_WhenSpriteIsSavedAndPaletteExists()
        {
            SwitchToPalette();
            spriteEditor.CompleteEditing();
            Assert.That(packEditor.CurrentPack.Palettes[0].AssociatedAssetsIDs.Contains(packEditor.CurrentPack.Sprites[0].ID));
        }

        [Test]
        public void Should_AddPaletteAssociationToCurrentlyEditedSpriteAsset_WhenPaletteSwitched()
        {
            SwitchToPalette();
            Assert.That(spriteEditor.CurrentAsset.AssociatedPaletteID, Is.EqualTo(packEditor.CurrentPack.Palettes[0].ID));
        }
        
        [Test]
        public void Should_AddPaletteAssociationToSpriteAsset_WhenSpriteIsSavedAndPaletteExists()
        {
            SwitchToPalette();
            spriteEditor.CompleteEditing();
            Assert.That(packEditor.CurrentPack.Sprites[0].AssociatedPaletteID, Is.EqualTo(packEditor.CurrentPack.Palettes[0].ID));
        }

        [UnityTest]
        public IEnumerator Should_UpdateIconColorOfSprite_WhenAssociatedPaletteWasUpdated()
        {
            yield return SwitchPaletteAndFillForSprite();

            yield return UpdatePaletteColorInPaletteEditor(Color.blue);
            Color color = packEditor.CurrentPack.Sprites[0].Icon.texture.GetPixel(0, 0);
            Assert.That(color, Is.EqualTo(Color.blue));
        }
        
        [UnityTest]
        public IEnumerator Should_UpdateIconColorOfWeapon_WhenAssociatedPaletteWasUpdated()
        {
            yield return SwitchPaletteAndFillForSprite();
            
            yield return UpdateSpriteOfWeaponInEditor();
            yield return UpdatePaletteColorInPaletteEditor(Color.blue);
            
            Color color = packEditor.CurrentPack.Weapons[0].Icon.texture.GetPixel(0, 0);
            Assert.That(color, Is.EqualTo(Color.blue));
        }

        [UnityTest]
        public IEnumerator Should_UpdateIconColorOfSprite_WhenAssociatedPaletteUpdatedInSpriteEditor()
        {
            yield return SwitchPaletteAndFillForSprite();
            yield return UpdatePaletteColorInSpriteEditor(Color.green);

            Color color = packEditor.CurrentPack.Sprites[0].Icon.texture.GetPixel(0, 0);
            Assert.That(color, Is.EqualTo(Color.green));
        }
        
        [UnityTest]
        public IEnumerator Should_UpdateIconColorOfAllSprites_WhenAssociatedPaletteUpdatedInPaletteEditor()
        {
            packEditor.CreateNewSprite(TUtilsAssetCreator.CreateSprite(Color.blue));
            packEditor.CreateNewSprite(TUtilsAssetCreator.CreateSprite(Color.yellow));
            for (int i = 0; i < 3; i++)
            {
                yield return SwitchPaletteAndFillForSprite(i);
            }

            yield return UpdatePaletteColorInPaletteEditor(Color.green);

            for (int i = 0; i < 3; i++)
            {
                Color color = packEditor.CurrentPack.Sprites[i].Icon.texture.GetPixel(0, 0);
                Assert.That(color, Is.EqualTo(Color.green));
            }
        }
        
        [UnityTest]
        public IEnumerator Should_UpdateIconColorOfAllSprites_WhenAssociatedPaletteUpdatedInSpriteEditor()
        {
            packEditor.CreateNewSprite(TUtilsAssetCreator.CreateSprite(Color.blue));
            packEditor.CreateNewSprite(TUtilsAssetCreator.CreateSprite(Color.yellow));
            for (int i = 0; i < 3; i++)
            {
                yield return SwitchPaletteAndFillForSprite(i);
            }

            yield return UpdatePaletteColorInSpriteEditor(Color.green);

            for (int i = 0; i < 3; i++)
            {
                Color color = packEditor.CurrentPack.Sprites[i].Icon.texture.GetPixel(0, 0);
                Assert.That(color, Is.EqualTo(Color.green));
            }
        }

        [UnityTest]
        public IEnumerator Should_RemoveSpriteAssociationFromPalette_WhenSpritePaletteIsSwitched()
        {
            packEditor.CreateNewPalette(TUtilsAssetCreator.CreatePalette());
            SwitchToPalette(0);
            spriteEditor.CompleteEditing();
            
            packEditor.ActivateSpriteEditor(0);
            yield return null;
            SwitchToPalette(1);
            spriteEditor.CompleteEditing();
            
            Assert.That(packEditor.CurrentPack.Palettes[0].AssociatedAssetsIDs, Is.Empty);
        }
    }
}