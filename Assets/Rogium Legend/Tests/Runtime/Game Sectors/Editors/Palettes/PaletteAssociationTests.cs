using System.Collections;
using NUnit.Framework;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Sprites;
using Rogium.Tests.Core;
using UnityEngine;

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
            PaletteAsset palette = packEditor.CurrentPack.Palettes[0];
            SpriteEditorOverseerMono.GetInstance().SwitchPalette(palette);
            spriteEditor.CompleteEditing();
            Assert.That(palette.AssociatedAssetsIDs.Contains(packEditor.CurrentPack.Sprites[0].ID));
        }

        [Test]
        public void Should_AddPaletteAssociationToCurrentlyEditedSpriteAsset_WhenPaletteSwitched()
        {
            PaletteAsset palette = packEditor.CurrentPack.Palettes[0];
            SpriteEditorOverseerMono.GetInstance().SwitchPalette(palette);
            Assert.That(spriteEditor.CurrentAsset.AssociatedPaletteID, Is.EqualTo(palette.ID));
        }
        
        [Test]
        public void Should_AddPaletteAssociationToSpriteAsset_WhenSpriteIsSavedAndPaletteExists()
        {
            PaletteAsset palette = packEditor.CurrentPack.Palettes[0];
            SpriteEditorOverseerMono.GetInstance().SwitchPalette(palette);
            spriteEditor.CompleteEditing();
            Assert.That(packEditor.CurrentPack.Sprites[0].AssociatedPaletteID, Is.EqualTo(palette.ID));
        }

        [Test]
        public void Should_UpdateSpriteColor_WhenAssociatedPaletteWasUpdated()
        {
            // Assign Palette to Sprite
            PaletteAsset palette = packEditor.CurrentPack.Palettes[0];
            SpriteEditorOverseerMono.GetInstance().SwitchPalette(palette);
            SpriteEditorOverseerMono.GetInstance().UpdateGridCell(Vector2Int.zero);
            spriteEditor.CompleteEditing();
            Color startColor = packEditor.CurrentPack.Sprites[0].Icon.texture.GetPixel(0, 0);
            
            packEditor.ActivatePaletteEditor(0, false);
            PaletteEditorOverseer.Instance.UpdateColor(Color.magenta, 0);
            PaletteEditorOverseer.Instance.CompleteEditing();
            Color endColor = packEditor.CurrentPack.Sprites[0].Icon.texture.GetPixel(0, 0);
            Assert.That(startColor, Is.Not.EqualTo(endColor));
        }

        [Test]
        public void Should_UpdateIcon_WhenUpdateGridCellAndSave()
        {
            PaletteAsset palette = packEditor.CurrentPack.Palettes[0];
            SpriteEditorOverseerMono s = SpriteEditorOverseerMono.GetInstance();
            s.SwitchPalette(palette);
            
            Color colorBefore = packEditor.CurrentPack.Sprites[0].Icon.texture.GetPixel(0, packEditor.CurrentPack.Sprites[0].Icon.texture.height - 1);
            s.UpdateGridCell(Vector2Int.zero);
            Color colorAfter = packEditor.CurrentPack.Sprites[0].Icon.texture.GetPixel(0, packEditor.CurrentPack.Sprites[0].Icon.texture.height - 1);
            
            Assert.That(colorBefore, Is.Not.EqualTo(colorAfter));
        }
    }
}