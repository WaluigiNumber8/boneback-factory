using System.Collections;
using NUnit.Framework;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Sprites;
using Rogium.Systems.ActionHistory;
using Rogium.Tests.Core;
using UnityEngine.TestTools;

namespace Rogium.Tests.Editors.Sprite
{
    /// <summary>
    /// Tests for the <see cref="SpriteEditorOverseerMono"/>.
    /// </summary>
    [RequiresPlayMode]
    public class SpriteEditorOverseerMonoTests
    {
        private SpriteEditorOverseerMono spriteEditor;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            SceneLoader.LoadUIScene();
            yield return null;
            PackAsset pack = AssetCreator.CreatePack();
            PackEditorOverseer.Instance.AssignAsset(pack, 0);
            SpriteAsset sprite = pack.Sprites[0];
            
            MenuLoader.LoadSpriteEditor();
            ActionHistorySystem.ClearHistory();
            spriteEditor = SpriteEditorOverseerMono.GetInstance();
            spriteEditor.PrepareEditor(sprite);
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
    }
}