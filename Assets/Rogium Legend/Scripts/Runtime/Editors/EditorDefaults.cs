using UnityEditor;
using UnityEngine;

namespace Rogium.Editors.Core.Defaults
{
    /// <summary>
    /// Contains all default valuesc used by the editor.
    /// </summary>
    public static class EditorDefaults
    {
        public static readonly string packTitle = "New Pack";
        public static readonly string packDescription = "A new pack filled with adventure!";
        public static readonly Sprite packIcon = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Rogium Legend/Sprites/Defaults/spr_Default_Weapon_Filled.png");
        public static readonly string author = "NO_AUTHOR";

        public static readonly string tileTitle = "New Tile";
        public static readonly Sprite tileSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Rogium Legend/Sprites/Defaults/spr_Default_Weapon_Filled.png");

        public static readonly string roomTitle = "New Room";
        public static readonly Sprite roomSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Rogium Legend/Sprites/Defaults/spr_Default_Weapon_Filled.png");
    }
}