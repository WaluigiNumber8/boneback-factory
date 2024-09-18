using System.Collections;
using Rogium.Editors.Sprites;
using Rogium.Tests.Core;
using Rogium.Tests.UI.Interactables;
using Rogium.UserInterface.ModalWindows;
using UnityEngine;

namespace Rogium.Tests.Editors.Sprites
{
    public static class PaletteEditingTestsU
    {
        /// <summary>
        /// Right-Clicks the first color slot in the palette, assigns a color to ColorPicker and closes it.
        /// </summary>
        /// <param name="color">The color to assign.</param>
        /// <param name="slot">The slot to update</param>
        public static IEnumerator UpdateColorSlot(Color color, int slot = 0)
        {
            SpriteEditorOverseerMono.GetInstance().Palette.GetSlot(slot).OnPointerClick(PointerDataCreator.RightClick());
            ColorPickerWindow colorPicker = InteractableUtils.FindFirstColorPickerWindow();
            colorPicker.UpdateColor(color);
            colorPicker.Close();
            yield return null;
        }
    }
}