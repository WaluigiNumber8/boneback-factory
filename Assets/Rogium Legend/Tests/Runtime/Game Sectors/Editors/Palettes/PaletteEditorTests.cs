using System.Collections;
using NUnit.Framework;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.TestTools;

namespace Rogium.Tests.Editors.Palettes
{
    /// <summary>
    /// Tests focused on <see cref="PaletteEditorOverseer"/> and <see cref="PaletteEditorOverseerMono"/>.
    /// </summary>
    public class PaletteEditorTests : MenuTestBase
    {
        private PaletteEditorOverseer editor;
        
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            yield return TUtilsMenuLoader.PreparePaletteEditor();
            editor = PaletteEditorOverseer.Instance;
            yield return null;
        }

        [Test]
        public void UpdateColor_Should_UpdateColorOfAsset()
        {
            editor.UpdateColor(Color.red, 0);
            Assert.That(editor.CurrentAsset.Colors[0], Is.EqualTo(Color.red));
        }

        [UnityTest]
        public IEnumerator CompleteEditing_Should_UpdateColorOfAsset()
        {
            PaletteEditorOverseerMono.Instance.UpdateColorSlotColor(Color.red, 0);
            PaletteEditorOverseerMono.Instance.SelectSlot(0);
            yield return null;
            editor.CompleteEditing();
            Assert.That(PackEditorOverseer.Instance.CurrentPack.Palettes[0].Colors[0], Is.EqualTo(Color.red));
        }
    }
}