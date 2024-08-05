using Rogium.Editors.Packs;
using Rogium.Editors.Rooms;
using Rogium.Editors.Sprites;
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
        /// Spawns & opens the Sprite Editor with a test sprite asset.
        /// </summary>
        public static void PrepareSpriteEditor()
        {
            PackAsset pack = AssetCreator.CreateAndAssignPack();
            Object.Instantiate(spriteEditorProperty);
            SpriteEditorOverseer.Instance.AssignAsset(pack.Sprites[0], 0);
        }

        /// <summary>
        /// Spawns & opens the Room Editor with a test room asset.
        /// </summary>
        public static void PrepareRoomEditor()
        {
            PackAsset pack = AssetCreator.CreateAndAssignPack();
            OverseerLoader.LoadInternalLibrary();
            OverseerLoader.LoadUIBuilder();
            OverseerLoader.LoadThemeOverseer();
            
            Object.Instantiate(roomEditorProperty);
            RoomEditorOverseer.Instance.AssignAsset(pack.Rooms[0], 0);
        }
    }
}