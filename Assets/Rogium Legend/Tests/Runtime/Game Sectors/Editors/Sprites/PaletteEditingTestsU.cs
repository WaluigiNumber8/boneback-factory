using System.Collections;
using RedRats.UI.ModalWindows;
using Rogium.Editors.Packs;
using Rogium.Editors.Sprites;
using Rogium.Systems.GASExtension;
using Rogium.Systems.Toolbox;
using Rogium.Tests.Core;
using Rogium.Tests.UI.Interactables.Properties;
using Rogium.UserInterface.ModalWindows;
using UnityEngine;

namespace Rogium.Tests.Editors.Sprites
{
    public static class PaletteEditingTestsU
    {
        public static IEnumerator UpdateColorSlot(Color color, int slot = 0)
        {
            SpriteEditorOverseerMono.Instance.Palette.GetSlot(slot).OnPointerClick(TUtilsPointerDataCreator.RightClick());
            ColorPickerWindow colorPicker = InteractableUtils.FindFirstColorPickerWindow();
            colorPicker.UpdateColor(color);
            colorPicker.Close();
            yield return null;
        }

        public static IEnumerator SavePaletteAsNewAndConfirm()
        {
            yield return UpdateColorSlot(Color.blue);
            GASActions.SavePaletteAsNew();
            yield return null;
            Object.FindFirstObjectByType<ModalWindow>()?.OnAccept();
        }

        public static IEnumerator UpdateColorSlotForSpriteWithMissingPalette()
        {
            SpriteEditorOverseer.Instance.UpdateAsset(TUtilsAssetCreator.CreateSpriteFromSlot1());
            SpriteEditorOverseer.Instance.CompleteEditing();
            PackEditorOverseer.Instance.CurrentPack.Sprites[0].ClearAssociatedPalette();
            PackEditorOverseer.Instance.ActivateSpriteEditor(0);
            yield return null;
            yield return UpdateColorSlot(Color.blue);
            GASActions.SaveChangesSprite();
            Object.FindFirstObjectByType<ModalWindow>()?.OnAccept();
        }
        
        public static void FillSpriteEditorGrid()
        {
            SpriteEditorOverseerMono.Instance.Toolbox.SwitchTool(ToolType.Fill);
            SpriteEditorOverseerMono.Instance.UpdateCell(Vector2Int.zero);
        }
    }
}