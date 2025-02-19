using System.Collections;
using System.Linq;
using NUnit.Framework;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Sprites;
using Rogium.Systems.ActionHistory;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.TestTools;

namespace Rogium.Tests.Editors.Sprites
{
    /// <summary>
    /// Tests for the <see cref="SpriteEditorOverseerMono"/>.
    /// </summary>
    public class SpriteEditorOverseerMonoTests : MenuTestBase
    {
        private SpriteEditorOverseerMono spriteEditor;

        [UnitySetUp]
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            
            yield return TUtilsMenuLoader.PrepareSpriteEditor();
            spriteEditor = SpriteEditorOverseerMono.Instance;
            ActionHistorySystem.ClearHistory();
            yield return null;
        }

        [Test]
        public void SwitchPalette_Should_ChangeCurrentPalette()
        {
            PaletteAsset newPalette = TUtilsAssetCreator.CreatePalette();
            spriteEditor.SwitchPalette(newPalette);
            Assert.That(spriteEditor.CurrentPaletteAsset, Is.EqualTo(newPalette));
        }
        
        [Test]
        public void ClearActiveGrid_Should_ClearDataGrid()
        {
            spriteEditor.ClearActiveGrid();

            Assert.That(spriteEditor.GetCurrentGridCopy.GetCellsCopy, Is.All.EqualTo(EditorDefaults.EmptyColorID));
        }
        
        [Test]
        public void ClearActiveGrid_Should_ClearVisualGrid()
        {
            spriteEditor.ClearActiveGrid();
            Color[] pixels = spriteEditor.CurrentGridSprite.texture.GetPixels();
            bool allTransparent = pixels.All(pixel => pixel.a == 0);

            Assert.That(allTransparent, Is.True);
        }
        
        [Test]
        public void ClearActiveGrid_Should_AddToUndoHistory()
        {
            SpriteEditorOverseerMonoTestsU.FillEntireGrid();
            ActionHistorySystem.ForceEndGrouping();
            spriteEditor.ClearActiveGrid();
            
            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(2));
        }
        
        [Test]
        public void UndoLast_Should_UndoClearActiveGrid()
        {
            SpriteEditorOverseerMonoTestsU.FillEntireGrid();
            spriteEditor.ClearActiveGrid();
            ActionHistorySystem.Undo();
            
            Assert.That(spriteEditor.GetCurrentGridCopy.GetCellsCopy, Is.Not.All.EqualTo(EditorDefaults.EmptyColorID));
        }
    }
}