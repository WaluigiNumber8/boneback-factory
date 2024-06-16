using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Rogium.Tests.Core
{
    /// <summary>
    /// Loads scenes for testing.
    /// </summary>
    public static class SceneLoader
    {
        /// <summary>
        /// Loads the scene for testing the UI.
        /// </summary>
        public static void LoadUIScene() => EditorSceneManager.LoadSceneInPlayMode("Assets/Rogium Legend/Scenes/Testing/scene_UnitTesting_UI.unity", new LoadSceneParameters(LoadSceneMode.Single));

        /// <summary>
        /// Unloads the scene for testing the UI.
        /// </summary>
        public static void UnloadUIScene() => EditorSceneManager.CloseScene(EditorSceneManager.GetSceneByPath("Assets/Rogium Legend/Scenes/Testing/scene_UnitTesting_UI.unity"), true);
    }
}