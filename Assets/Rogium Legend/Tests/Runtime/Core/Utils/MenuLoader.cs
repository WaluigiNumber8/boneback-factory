using System.Collections;
using Rogium.Editors.Packs;
using Rogium.Systems.GASExtension;
using Rogium.Tests.Editors;
using UnityEditor;
using UnityEngine;

namespace Rogium.Tests.Core
{
    /// <summary>
    /// Loads in different menus from the game.
    /// </summary>
    public static class MenuLoader
    {
        private static readonly GameObject paletteEditorProperty = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/UI/Menus/pref_Menu_PaletteEditor.prefab");
        private static readonly GameObject spriteEditorProperty = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/UI/Menus/pref_Menu_SpriteEditor.prefab");
        private static readonly GameObject roomEditorProperty = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/UI/Menus/pref_Menu_RoomEditor.prefab");
        private static readonly GameObject selectionMenuProperty = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/UI/Menus/pref_Menu_SelectionMenu.prefab");

        public static void PrepareSelectionMenu()
        {
            OverseerLoader.LoadInternalLibrary();
            OverseerLoader.LoadThemeOverseer();
            Object.Instantiate(selectionMenuProperty);
            GASButtonActions.OpenSelectionPack();
        }

        /// <summary>
        /// Spawns & opens the Palette Editor with a test palette asset.
        /// </summary>
        public static IEnumerator PreparePaletteEditor()
        {
            PackAsset pack = AssetCreator.CreateAndAssignPack();
            Object.Instantiate(paletteEditorProperty);
            yield return null;
            PackEditorOverseer.Instance.AssignAsset(pack, 0);
            PackEditorOverseer.Instance.ActivatePaletteEditor(0);
        }
        
        /// <summary>
        /// Spawns & opens the Sprite Editor with a test sprite asset.
        /// </summary>
        public static IEnumerator PrepareSpriteEditor()
        {
            PackAsset pack = AssetCreator.CreateAndAssignPack();
            Object.Instantiate(spriteEditorProperty);
            yield return null;
            PackEditorOverseer.Instance.AssignAsset(pack, 0);
            PackEditorOverseer.Instance.ActivateSpriteEditor(0);
        }

        /// <summary>
        /// Spawns & opens the Room Editor with a test room asset.
        /// </summary>
        public static IEnumerator PrepareRoomEditor()
        {
            PackAsset pack = AssetCreator.CreateAndAssignPack();
            OverseerLoader.LoadInternalLibrary();
            OverseerLoader.LoadUIBuilder();
            OverseerLoader.LoadThemeOverseer();
            
            Object.Instantiate(roomEditorProperty);
            yield return null;
            PackEditorOverseer.Instance.AssignAsset(pack, 0);
            PackEditorOverseer.Instance.ActivateRoomEditor(0);
        }
    }
}