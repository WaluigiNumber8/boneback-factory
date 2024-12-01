using System.Collections;
using Rogium.Editors.Core;
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
        private static readonly GameObject campaignSelectionProperty = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/UI/Menus/pref_Menu_CampaignSelection.prefab");
        private static readonly GameObject campaignEditorProperty = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/UI/Menus/pref_Menu_CampaignEditor.prefab");
        private static readonly GameObject selectionMenuProperty = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/UI/Menus/pref_Menu_SelectionMenu.prefab");
        private static readonly GameObject mainMenuProperty = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/UI/Menus/pref_Menu_Main.prefab");
        private static readonly GameObject optionsMenuProperty = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/UI/Menus/pref_Menu_Options.prefab");
        
        public static IEnumerator PrepareSelectionMenu()
        {
            OverseerLoader.LoadBackgroundOverseer();
            OverseerLoader.LoadInternalLibrary();
            OverseerLoader.LoadThemeOverseer();
            OverseerLoader.LoadUIBuilder();
            Object.Instantiate(selectionMenuProperty, Object.FindFirstObjectByType<Canvas>().transform);
            yield return null;
        }
        
        public static IEnumerator PrepareMainMenu()
        {
            OverseerLoader.LoadBackgroundOverseer();
            OverseerLoader.LoadInternalLibrary();
            OverseerLoader.LoadThemeOverseer();
            Object.Instantiate(mainMenuProperty, Object.FindFirstObjectByType<Canvas>().transform);
            yield return null;
        }
        
        public static IEnumerator PrepareOptionsMenu()
        {
            OverseerLoader.LoadBackgroundOverseer();
            OverseerLoader.LoadInternalLibrary();
            OverseerLoader.LoadThemeOverseer();
            OverseerLoader.LoadModalWindowBuilder();
            OverseerLoader.LoadUIBuilder();
            Object.Instantiate(optionsMenuProperty, Object.FindFirstObjectByType<Canvas>().transform);
            yield return null;
        }

        /// <summary>
        /// Spawns & opens the Palette Editor with a test palette asset.
        /// </summary>
        public static IEnumerator PreparePaletteEditor(bool openEditor = true)
        {
            Object.Instantiate(paletteEditorProperty, Object.FindFirstObjectByType<Canvas>().transform);
            yield return null;
            if (!openEditor) yield break; 
            PackEditorOverseer.Instance.ActivatePaletteEditor(0);
        }

        /// <summary>
        /// Spawns & opens the Sprite Editor with a test sprite asset.
        /// </summary>
        /// <param name="openEditor"></param>
        public static IEnumerator PrepareSpriteEditor(bool openEditor = true)
        {
            OverseerLoader.LoadThemeOverseer();
            Object.Instantiate(spriteEditorProperty, Object.FindFirstObjectByType<Canvas>().transform);
            yield return null;
            if (!openEditor) yield break; 
            PackEditorOverseer.Instance.ActivateSpriteEditor(0);
        }

        /// <summary>
        /// Spawns & opens the Weapon Editor with a test weapon asset.
        /// </summary>
        /// <param name="openEditor"></param>
        public static IEnumerator PrepareWeaponEditor(bool openEditor = true)
        {
            OverseerLoader.LoadInternalLibrary();
            OverseerLoader.LoadUIBuilder();
            OverseerLoader.LoadThemeOverseer();
            
            Object.Instantiate(propertyEditorProperty, Object.FindFirstObjectByType<Canvas>().transform);
            yield return null;
            if (!openEditor) yield break; 
            PackEditorOverseer.Instance.ActivateWeaponEditor(0);
        }

        /// <summary>
        /// Spawns & opens the Projectile Editor with a test projectile asset.
        /// </summary>
        /// <param name="openEditor"></param>
        public static IEnumerator PrepareProjectileEditor(bool openEditor = true)
        {
            OverseerLoader.LoadInternalLibrary();
            OverseerLoader.LoadUIBuilder();
            OverseerLoader.LoadThemeOverseer();
            
            Object.Instantiate(propertyEditorProperty, Object.FindFirstObjectByType<Canvas>().transform);
            yield return null;
            if (!openEditor) yield break; 
            PackEditorOverseer.Instance.ActivateProjectileEditor(0);
        }

        /// <summary>
        /// Spawns & opens the Enemy Editor with a test enemy asset.
        /// </summary>
        public static IEnumerator PrepareEnemyEditor(bool openEditor = true)
        {
            OverseerLoader.LoadInternalLibrary();
            OverseerLoader.LoadUIBuilder();
            OverseerLoader.LoadThemeOverseer();
            
            Object.Instantiate(propertyEditorProperty, Object.FindFirstObjectByType<Canvas>().transform);
            yield return null;
            if (!openEditor) yield break; 
            PackEditorOverseer.Instance.ActivateEnemyEditor(0);
        }

        /// <summary>
        /// Spawns & opens the Room Editor with a test room asset.
        /// </summary>
        /// <param name="openEditor"></param>
        public static IEnumerator PrepareRoomEditor(bool openEditor = true)
        {
            OverseerLoader.LoadInternalLibrary();
            OverseerLoader.LoadUIBuilder();
            OverseerLoader.LoadThemeOverseer();
            
            Object.Instantiate(roomEditorProperty, Object.FindFirstObjectByType<Canvas>().transform);
            yield return null;
            if (!openEditor) yield break; 
            PackEditorOverseer.Instance.ActivateRoomEditor(0);
        }

        /// <summary>
        /// Spawns & opens the Tile Editor with a test tile asset.
        /// </summary>
        /// <param name="openEditor"></param>
        public static IEnumerator PrepareTileEditor(bool openEditor = true)
        {
            OverseerLoader.LoadInternalLibrary();
            OverseerLoader.LoadUIBuilder();
            OverseerLoader.LoadThemeOverseer();

            Object.Instantiate(propertyEditorProperty, Object.FindFirstObjectByType<Canvas>().transform);
            yield return null;
            if (!openEditor) yield break; 
            PackEditorOverseer.Instance.ActivateTileEditor(0);
        }

        public static IEnumerator PrepareCampaignEditor(bool openEditor = true)
        {
            OverseerLoader.LoadUIBuilder();
            OverseerLoader.LoadThemeOverseer();
            
            Object.Instantiate(campaignEditorProperty, Object.FindFirstObjectByType<Canvas>().transform);
            yield return null;
            if (!openEditor) yield break;
            yield return AssetCreator.CreateAndAssignCampaign();
        }
        
        public static IEnumerator PrepareCampaignSelection()
        {
            OverseerLoader.LoadBackgroundOverseer();
            OverseerLoader.LoadUIBuilder();
            OverseerLoader.LoadThemeOverseer();
            
            Object.Instantiate(campaignSelectionProperty, Object.FindFirstObjectByType<Canvas>().transform);
            yield return null;
        }
    }
}