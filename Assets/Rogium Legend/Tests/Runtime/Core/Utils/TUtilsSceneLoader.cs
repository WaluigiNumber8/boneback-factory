using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Rogium.Tests.Core
{
    /// <summary>
    /// Loads scenes for testing.
    /// </summary>
    public static class TUtilsSceneLoader
    {
        public static void LoadMenuTestingScene() => EditorSceneManager.LoadSceneInPlayMode("Assets/Rogium Legend/Scenes/Testing/scene_UnitTesting_Menu.unity", new LoadSceneParameters(LoadSceneMode.Single));

        public static void LoadGameplayTestingScene() => EditorSceneManager.LoadSceneInPlayMode("Assets/Rogium Legend/Scenes/Testing/scene_UnitTesting_Gameplay.unity", new LoadSceneParameters(LoadSceneMode.Single));
    }
}