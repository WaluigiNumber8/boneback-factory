using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
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
        [SerializeField] private float newScrollSpeed = 1100f;
        [ButtonGroup, Button(ButtonSizes.Large), GUIColor("#569CD6")] public void CurrentScene() => UpdateScrollSensitivityInCurrentScene();
        
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
            foreach (ScrollRect rect in scrollRects)
            {
                rect.scrollSensitivity = newScrollSpeed;
            }
        }

    }
}