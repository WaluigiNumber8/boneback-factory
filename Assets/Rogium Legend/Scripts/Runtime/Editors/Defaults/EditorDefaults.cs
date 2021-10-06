using UnityEditor;
using UnityEngine;

namespace Rogium.Editors.Core.Defaults
{
    /// <summary>
    /// Contains all default values used by the editor.
    /// </summary>
    public static class EditorDefaults
    {
        public const string PackTitle = "New Pack";
        public const string PackDescription = "A new pack filled with adventure!";
        public static readonly Sprite PackIcon = Resources.Load<Sprite>("Defaults/spr_Default_Weapon_Filled");
        public const string Author = "NO_AUTHOR";

        public const string TileTitle = "New Tile";
        public static readonly Sprite TileIcon = Resources.Load<Sprite>("Defaults/spr_Default_Tile");

        public const string RoomTitle = "New Room";
        public const int RoomDifficulty = 0;
        public static readonly Sprite RoomIcon = Resources.Load<Sprite>("Defaults/spr_Default_Weapon_Filled");
        
        public const string EmptyID = "-1";
        public static readonly Sprite EmptyGridSprite = Resources.Load<Sprite>("Defaults/spr_Grid_Blank");
        public static readonly Sprite MissingSprite = Resources.Load<Sprite>("Defaults/spr_Missing");
    }
}