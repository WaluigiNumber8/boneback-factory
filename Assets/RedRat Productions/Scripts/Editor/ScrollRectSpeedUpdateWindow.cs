using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace RedRats.Editor
{
    /// <summary>
    /// A helper tool for updating the ScrollArea scrolling speed.
    /// </summary>
    public class ScrollRectSpeedUpdateWindow : OdinEditorWindow
    {
        [Space]
        [SerializeField] 
        private float newScrollSpeed = 56;
        [ButtonGroup, Button(ButtonSizes.Large), GUIColor("#569CD6")] 
        public void CurrentScene() => UpdateScrollSensitivityInCurrentScene();
        [ButtonGroup, Button(ButtonSizes.Large), GUIColor("#D65377"), EnableIf("IsPrefabStageOpen")]
        public void CurrentPrefab() => UpdateScrollSensitivityInOpenPrefab();
        
        [MenuItem( "Tools/RedRat Productions/ScrollRect Speed Updater")]
        private static void ShowWindow()
        {
            ScrollRectSpeedUpdateWindow window = GetWindow<ScrollRectSpeedUpdateWindow>();
            window.titleContent = new GUIContent("ScrollRect Speed Updater");
            window.maxSize = new Vector2(320, 110);
            window.Show();
        }
        
        private void UpdateScrollSensitivityInCurrentScene()
        {
            ScrollRect[] scrollRects = FindObjectsByType<ScrollRect>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            UpdateScrollRects(scrollRects);
        }

        private void UpdateScrollSensitivityInOpenPrefab()
        {
            PrefabStage stage = PrefabStageUtility.GetCurrentPrefabStage();
            if (stage == null) return;
            ScrollRect[] scrollRects = stage.prefabContentsRoot.GetComponentsInChildren<ScrollRect>(true);
            UpdateScrollRects(scrollRects);
        }
        
        private void UpdateScrollRects(ScrollRect[] scrollRects)
        {
            foreach (ScrollRect rect in scrollRects)
            {
                rect.scrollSensitivity = newScrollSpeed;
                EditorUtility.SetDirty(rect);
            }
        }
        
        private static bool IsPrefabStageOpen => PrefabStageUtility.GetCurrentPrefabStage() != null;
    }
}