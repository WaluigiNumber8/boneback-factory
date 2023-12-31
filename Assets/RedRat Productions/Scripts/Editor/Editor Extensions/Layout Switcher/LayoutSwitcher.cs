using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using RedRats.Systems.FileSystem;
using UnityEditor;
using UnityEditorInternal;

namespace RedRats.Editor.UnityEditorExtensions.LayoutSwitching
{
    /// <summary>
    /// Allows for switching between different editor layouts.
    /// </summary>
    public static class LayoutSwitcher
    {
        public static bool OpenLayout(string name)
        {
            string path = GetWindowLayoutPath(name);
            if (string.IsNullOrWhiteSpace(path)) return false;

            Type windowLayoutType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.WindowLayout");

            if (windowLayoutType == null) return false;

            MethodInfo tryLoadWindowLayoutMethod = windowLayoutType.GetMethod("LoadWindowLayout",
                BindingFlags.Public | BindingFlags.Static, 
                null, 
                new[] {typeof(string), typeof(bool)}, 
                null);
            
            if (tryLoadWindowLayoutMethod == null) return false;

            object[] parameters = {path, false};
            bool result = (bool) tryLoadWindowLayoutMethod.Invoke(null, parameters);
            return result;
        }

        /// <summary>
        /// Gets the path of a wanted layout.
        /// </summary>
        /// <param name="name">The name of the layout we want.</param>
        private static string GetWindowLayoutPath(string name)
        {
            string layoutPreferencesPath = Path.Combine(InternalEditorUtility.unityPreferencesFolder, "Layouts");
            string layoutModePreferencesPath = Path.Combine(layoutPreferencesPath, ModeService.currentId);

            if (!Directory.Exists(layoutModePreferencesPath)) return null;
            IList<string> layoutPaths = FileSystem.LoadFilePaths(layoutModePreferencesPath, ".wlt");

            if (layoutPaths == null || layoutPaths.Count == 0) return null;
            return layoutPaths.FirstOrDefault(path => string.CompareOrdinal(name, Path.GetFileNameWithoutExtension(path)) == 0);
        }
    }
}