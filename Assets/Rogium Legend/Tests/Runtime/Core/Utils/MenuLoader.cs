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
        private static readonly GameObject propertyEditorProperty = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/UI/Menus/pref_Menu_PropertyEditor.prefab");
        private static readonly GameObject selectionMenuProperty = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/UI/Menus/pref_Menu_SelectionMenu.prefab");

        public static IEnumerator PrepareSelectionMenu()
        {
            OverseerLoader.LoadInternalLibrary();
            OverseerLoader.LoadThemeOverseer();
            Object.Instantiate(selectionMenuProperty);
            yield return null;
            GASButtonActions.OpenSelectionPack();
        }

        /// <summary>
        /// Spawns & opens the Palette Editor with a test palette asset.
        /// </summary>
        public static IEnumerator PreparePaletteEditor()
        {
            Object.Instantiate(paletteEditorProperty);
            yield return null;
            PackEditorOverseer.Instance.ActivatePaletteEditor(0);
        }
        
        /// <summary>
        /// Spawns & opens the Sprite Editor with a test sprite asset.
        /// </summary>
        public static IEnumerator PrepareSpriteEditor()
        {
            OverseerLoader.LoadThemeOverseer();
            Object.Instantiate(spriteEditorProperty);
            yield return null;
            PackEditorOverseer.Instance.ActivateSpriteEditor(0);
        }

        /// <summary>
        /// Spawns & opens the Weapon Editor with a test weapon asset.
        /// </summary>
        public static IEnumerator PrepareWeaponEditor()
        {
            OverseerLoader.LoadInternalLibrary();
            OverseerLoader.LoadUIBuilder();
            OverseerLoader.LoadThemeOverseer();
            
            Object.Instantiate(propertyEditorProperty);
            yield return null;
            PackEditorOverseer.Instance.ActivateWeaponEditor(0);
        }

        /// <summary>
        /// Spawns & opens the Projectile Editor with a test projectile asset.
        /// </summary>
        public static IEnumerator PrepareProjectileEditor()
        {
            OverseerLoader.LoadInternalLibrary();
            OverseerLoader.LoadUIBuilder();
            OverseerLoader.LoadThemeOverseer();
            
            Object.Instantiate(propertyEditorProperty);
            yield return null;
            PackEditorOverseer.Instance.ActivateProjectileEditor(0);
        }

        /// <summary>
        /// Spawns & opens the Enemy Editor with a test enemy asset.
        /// </summary>
        public static IEnumerator PrepareEnemyEditor()
        {
            OverseerLoader.LoadInternalLibrary();
            OverseerLoader.LoadUIBuilder();
            OverseerLoader.LoadThemeOverseer();
            
            Object.Instantiate(propertyEditorProperty);
            yield return null;
            PackEditorOverseer.Instance.ActivateEnemyEditor(0);
        }
        
        /// <summary>
        /// Spawns & opens the Room Editor with a test room asset.
        /// </summary>
        public static IEnumerator PrepareRoomEditor()
        {
            OverseerLoader.LoadInternalLibrary();
            OverseerLoader.LoadUIBuilder();
            OverseerLoader.LoadThemeOverseer();
            
            Object.Instantiate(roomEditorProperty);
            yield return null;
            PackEditorOverseer.Instance.ActivateRoomEditor(0);
        }

        /// <summary>
        /// Spawns & opens the Tile Editor with a test tile asset.
        /// </summary>
        public static IEnumerator PrepareTileEditor()
        {
            OverseerLoader.LoadInternalLibrary();
            OverseerLoader.LoadUIBuilder();
            OverseerLoader.LoadThemeOverseer();

            Object.Instantiate(propertyEditorProperty);
            yield return null;
            PackEditorOverseer.Instance.ActivateTileEditor(0);
        }
    }
}