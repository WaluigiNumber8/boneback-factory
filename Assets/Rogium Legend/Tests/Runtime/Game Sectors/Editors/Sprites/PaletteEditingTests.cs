using System.Collections;
using NUnit.Framework;
using Rogium.Editors.Sprites;
using Rogium.Tests.Core;
using Rogium.UserInterface.ModalWindows;
using UnityEngine.TestTools;
using static Rogium.Tests.Core.PointerDataCreator;
using static Rogium.Tests.UI.Interactables.InteractableUtils;

namespace Rogium.Tests.Editors.Sprites
{
    /// <summary>
    /// Tests for editing the palette in the Sprite Editor.
    /// </summary>
    public class PaletteEditingTests : MenuTestBase
    {
        private SpriteEditorOverseerMono spriteEditor;
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            OverseerLoader.LoadModalWindowBuilder();
            yield return null;
            MenuLoader.PrepareSpriteEditor();
            spriteEditor = SpriteEditorOverseerMono.GetInstance();
        }

        [UnityTest]
        public IEnumerator Should_OpenColorPicker_WhenPaletteSlotRightClicked()
        {
            spriteEditor.Palette.GetSlot(0).OnPointerClick(RightClick());
            yield return null;
            ColorPickerWindow colorPicker = FindFirstColorPickerWindow();
            
            Assert.That(colorPicker.gameObject.activeSelf, Is.True);
        }
    }
}