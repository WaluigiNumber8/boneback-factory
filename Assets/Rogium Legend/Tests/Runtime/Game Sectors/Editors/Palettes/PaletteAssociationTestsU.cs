using System.Collections;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Sprites;
using Rogium.Editors.Weapons;
using Rogium.Systems.Toolbox;
using Rogium.Tests.Core;
using UnityEngine;

namespace Rogium.Tests.Editors.Palettes
{
    /// <summary>
    /// Utils for <see cref="PaletteAssociationTests"/>.
    /// </summary>
    public static class PaletteAssociationTestsU
    {
        public static void SwitchToFirstPalette()
        {
            PaletteAsset palette = PackEditorOverseer.Instance.CurrentPack.Palettes[0];
            SpriteEditorOverseerMono.GetInstance().SwitchPalette(palette);
        }

        public static void FillSpriteEditorGrid()
        {
            SpriteEditorOverseerMono.GetInstance().Toolbox.SwitchTool(ToolType.Fill);
            SpriteEditorOverseerMono.GetInstance().UpdateCell(Vector2Int.zero);
        }

        public static IEnumerator UpdatePaletteColorInPaletteEditor(Color color)
        {
            yield return MenuLoader.PreparePaletteEditor();
            PaletteEditorOverseerMono.GetInstance().UpdateColorSlotColor(color, 0);
            PaletteEditorOverseer.Instance.CompleteEditing();
        }
        
        public static IEnumerator UpdateSpriteOfWeaponInEditor()
        {
            yield return MenuLoader.PrepareWeaponEditor();
            WeaponEditorOverseer.Instance.CurrentAsset.UpdateIcon(PackEditorOverseer.Instance.CurrentPack.Sprites[0]);
            WeaponEditorOverseer.Instance.CompleteEditing();
        }

        public static void UpdatePaletteColorInSpriteEditor()
        {
            PackEditorOverseer.Instance.ActivateSpriteEditor(0);
            SpriteEditorOverseerMono.GetInstance().Palette.GetSlot(0).UpdateColor(Color.yellow);
            SpriteEditorOverseerMono.GetInstance().Palette.Select(0);
            SpriteEditorOverseer.Instance.CompleteEditing();
        }
    }
}