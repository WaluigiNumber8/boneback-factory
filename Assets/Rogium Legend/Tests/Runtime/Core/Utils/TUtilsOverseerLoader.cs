using Rogium.UserInterface.ModalWindows;
using UnityEditor;
using UnityEngine;

namespace Rogium.Tests.Core
{
    /// <summary>
    /// Loads in different overseers for testing.
    /// </summary>
    public static class TUtilsOverseerLoader
    {
        private static readonly GameObject internalLibraryPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/Global/pref_InternalLibrary.prefab");
        private static readonly GameObject uiBuilderPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/Global/Builders/pref_Builder_InteractableProperties.prefab");
        private static readonly ModalWindowBuilder modalWindowBuilderPrefab = AssetDatabase.LoadAssetAtPath<ModalWindowBuilder>("Assets/Rogium Legend/Prefabs/Global/Builders/pref_Builder_ModalWindows.prefab");
        private static readonly GameObject themeOverseerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/Global/Overseers/pref_Overseer_Themes.prefab");
        private static readonly GameObject backgroundOverseerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/Global/Overseers/pref_Overseer_Backgrounds.prefab");
        private static readonly GameObject inputSystemPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/Global/Systems/pref_System_Input.prefab");

        public static void LoadInternalLibrary() => Object.Instantiate(internalLibraryPrefab);
        public static void LoadThemeOverseer() => Object.Instantiate(themeOverseerPrefab);
        public static void LoadUIBuilder() => Object.Instantiate(uiBuilderPrefab, GetCanvasTransform());
        public static void LoadBackgroundOverseer() => Object.Instantiate(backgroundOverseerPrefab);
        public static void LoadModalWindowBuilder()
        {
            LoadThemeOverseer();
            LoadInternalLibrary();
            Object.Instantiate(modalWindowBuilderPrefab, GetCanvasTransform());
        }
        public static void LoadInputSystem() => Object.Instantiate(inputSystemPrefab);

        private static Transform GetCanvasTransform() => Object.FindFirstObjectByType<Canvas>().transform;
    }
}