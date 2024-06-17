using System.Collections;
using NUnit.Framework;
using Rogium.Editors.Sprites;
using Rogium.Systems.ActionHistory;
using Rogium.Tests.Core;
using Rogium.UserInterface.ModalWindows;
using UnityEngine;
using UnityEngine.TestTools;
using static Rogium.Tests.Core.PointerDataCreator;
using static Rogium.Tests.Editors.Sprites.SpriteEditorUtils;
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

        [UnityTest]
        public IEnumerator Should_ChangeColorSlotColor_WhenColorPickerColorChanged()
        {
            UpdateColorSlotColor(Color.blue);
            yield return null;
            
            Assert.That(spriteEditor.Palette.GetSlot(0).CurrentColor, Is.EqualTo(Color.blue));
        }

        [UnityTest]
        public IEnumerator Should_UpdateCurrentBrushColor_WhenChangedColorOfCurrentColorSlot()
        {
            UpdateColorSlotColor(Color.blue);
            yield return null;
            
            Assert.That(spriteEditor.CurrentBrushColor, Is.EqualTo(Color.blue));
        }

        [UnityTest]
        public IEnumerator Should_AddSlotColorChangeToActionHistory()
        {
            ActionHistorySystem.ClearHistory();
            
            UpdateColorSlotColor(Color.blue);
            ActionHistorySystem.ForceEndGrouping();
            yield return null;

            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(1));
        }

        [UnityTest]
        public IEnumerator Should_RevertColorSlotChange_WhenUndoIsCalled()
        {
            UpdateColorSlotColor(Color.blue);
            ActionHistorySystem.ForceEndGrouping();
            yield return null;
            UpdateColorSlotColor(Color.red);
            ActionHistorySystem.ForceEndGrouping();
            yield return null;
            ActionHistorySystem.UndoLast();
            
            Assert.That(spriteEditor.Palette.GetSlot(0).CurrentColor, Is.EqualTo(Color.blue));
        }

        //TODO Add test if ColorSlot current color is set to the color that is loaded into it on palette refresh.
        
    }
}