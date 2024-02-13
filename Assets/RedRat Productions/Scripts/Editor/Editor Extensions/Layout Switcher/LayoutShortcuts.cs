using UnityEditor.ShortcutManagement;
using UnityEngine;

namespace RedRats.Editor.UnityEditorExtensions.LayoutSwitching
{
    /// <summary>
    /// Contains shortcuts to the different layouts.
    /// </summary>
    public static class LayoutShortcuts
    {
        [Shortcut("Layout Switcher/John Neutral", KeyCode.Alpha1, ShortcutModifiers.Control | ShortcutModifiers.Shift | ShortcutModifiers.Alt)]
        public static void SwitchLayoutToDefault() => LayoutSwitcher.OpenLayout("John Neutral");
        
        [Shortcut("Layout Switcher/Shadergraph", KeyCode.Alpha2, ShortcutModifiers.Control | ShortcutModifiers.Shift | ShortcutModifiers.Alt)]
        public static void SwitchLayoutToShaders() => LayoutSwitcher.OpenLayout("Shadergraph");
        
        [Shortcut("Layout Switcher/Audio Editing", KeyCode.Alpha3, ShortcutModifiers.Control | ShortcutModifiers.Shift | ShortcutModifiers.Alt)]
        public static void SwitchLayoutToAudioMixing() => LayoutSwitcher.OpenLayout("Audio Editing");
        
        [Shortcut("Layout Switcher/Prefab Editing", KeyCode.Alpha4, ShortcutModifiers.Control | ShortcutModifiers.Shift | ShortcutModifiers.Alt)]
        public static void SwitchLayoutToPrefabEditing() => LayoutSwitcher.OpenLayout("Prefab Editing");
    }
}