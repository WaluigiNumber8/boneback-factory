using System.Collections;
using NUnit.Framework;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Sprites;
using Rogium.Tests.Core;

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
            MenuLoader.PrepareSpriteEditor();
            PaletteAsset palette = packEditor.CurrentPack.Palettes[0];
            SpriteEditorOverseerMono.GetInstance().SwitchPalette(palette);
            spriteEditor.CompleteEditing();
            Assert.That(palette.AssociatedAssetsIDs.Contains(packEditor.CurrentPack.Sprites[0].ID));
        }


        [Test]
        public void Should_AddPaletteAssociationToCurrentlyEditedSpriteAsset_WhenPaletteSwitched()
        {
            MenuLoader.PrepareSpriteEditor();
            PaletteAsset palette = packEditor.CurrentPack.Palettes[0];
            SpriteEditorOverseerMono.GetInstance().SwitchPalette(palette);
            Assert.That(spriteEditor.CurrentAsset.AssociatedPaletteID, Is.EqualTo(palette.ID));
        }
        
        [Test]
        public void Should_AddPaletteAssociationToSpriteAsset_WhenSpriteIsSavedAndPaletteExists()
        {
            MenuLoader.PrepareSpriteEditor();
            PaletteAsset palette = packEditor.CurrentPack.Palettes[0];
            SpriteEditorOverseerMono.GetInstance().SwitchPalette(palette);
            spriteEditor.CompleteEditing();
            Assert.That(packEditor.CurrentPack.Sprites[0].AssociatedPaletteID, Is.EqualTo(palette.ID));
        }
    }
}