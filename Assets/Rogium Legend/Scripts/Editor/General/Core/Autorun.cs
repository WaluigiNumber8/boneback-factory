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
        static Autorun() => EditorApplication.delayCall += InitWhenEditorLoad;

        private static void InitWhenEditorLoad()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode) return;
            
            RogiumScreenSwitcher screenSwitcher = ScriptableObject.CreateInstance<RogiumScreenSwitcher>();
            screenSwitcher.SwitchSceneToDefault();
            EditorApplication.delayCall -= InitWhenEditorLoad;
        }
    }
}