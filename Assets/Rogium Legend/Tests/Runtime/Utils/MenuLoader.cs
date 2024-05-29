using UnityEditor;
using UnityEngine;

namespace Rogium.Tests.Core
{
    /// <summary>
    /// Loads in different menus from the game.
    /// </summary>
    public static class MenuLoader
    {
        private static readonly GameObject spriteEditorProperty = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/UI/Menus/pref_Menu_SpriteEditor.prefab");

        public static void LoadSpriteEditor()
        {
            Object.Instantiate(spriteEditorProperty);
        }
    }
}