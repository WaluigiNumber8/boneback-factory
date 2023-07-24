using Rogium.Editor.UI;
using UnityEditor;
using UnityEngine;

namespace Rogium.Editor.Core
{
    /// <summary>
    /// Autoruns scripts when the editor is opened.
    /// </summary>
    [InitializeOnLoad]
    public class Autorun
    {
        static Autorun() => EditorApplication.update += InitWhenEditorLoad;

        private static void InitWhenEditorLoad()
        {
            RogiumScreenSwitcher screenSwitcher = ScriptableObject.CreateInstance<RogiumScreenSwitcher>();
            screenSwitcher.SwitchSceneToDefault();
            EditorApplication.update -= InitWhenEditorLoad;
        }
    }
}