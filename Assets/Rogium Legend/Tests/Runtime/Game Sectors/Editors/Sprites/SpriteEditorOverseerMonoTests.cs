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
    public class SpriteEditorOverseerMonoTests : UITestBase
    {
        private SpriteEditorOverseerMono spriteEditor;

        [UnitySetUp]
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            PackAsset pack = AssetCreator.CreateAndAssignPack();
            
            MenuLoader.LoadSpriteEditor();
            spriteEditor = SpriteEditorOverseerMono.GetInstance();
            SpriteEditorOverseer.Instance.AssignAsset(pack.Sprites[0], 0);
            ActionHistorySystem.ClearHistory();
            yield return null;
        }

        [Test]
        public void SwitchPalette_Should_ChangeCurrentPalette()
        {
            PaletteAsset newPalette = new();
            newPalette.UpdateTitle("New Palette");
            
            spriteEditor.SwitchPalette(newPalette);
            
            Assert.That(spriteEditor.CurrentPalette, Is.EqualTo(newPalette));
        }
        
        [Test]
        public void ClearActiveGrid_Should_ClearDataGrid()
        {
            spriteEditor.ClearActiveGrid();

            Assert.That(spriteEditor.GetCurrentGridCopy.GetCellsCopy, Is.All.EqualTo(EditorConstants.EmptyColorID));
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
            SpriteEditorUtils.FillEntireGrid();
            ActionHistorySystem.ForceEndGrouping();
            spriteEditor.ClearActiveGrid();
            
            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(2));
        }
        
        [Test]
        public void UndoLast_Should_UndoClearActiveGrid()
        {
            SpriteEditorUtils.FillEntireGrid();
            spriteEditor.ClearActiveGrid();
            ActionHistorySystem.UndoLast();
            
            Assert.That(spriteEditor.GetCurrentGridCopy.GetCellsCopy, Is.Not.All.EqualTo(EditorConstants.EmptyColorID));
        }
    }
}