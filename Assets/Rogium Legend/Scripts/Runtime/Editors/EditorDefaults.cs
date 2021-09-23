using UnityEditor;
using UnityEngine;

namespace Rogium.Editors.Core.Defaults
{
    /// <summary>
    /// Contains all default valuesc used by the editor.
    /// </summary>
    public static class EditorDefaults
    {
        public const string PackTitle = "New Pack";
        public const string PackDescription = "A new pack filled with adventure!";
        public static readonly Sprite PackIcon = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Rogium Legend/Sprites/Defaults/spr_Default_Weapon_Filled.png");
        public const string Author = "NO_AUTHOR";

        public const string TileTitle = "New Tile";
        public static readonly Sprite TileSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Rogium Legend/Sprites/Tiles/spr_tile_FloorTile.png");

        public const string RoomTitle = "New Room";
        public const int RoomDifficulty = 0;
        public static readonly Sprite RoomSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Rogium Legend/Sprites/Defaults/spr_Default_Weapon_Filled.png");
        
        public const int DefaultTileIndex = -1;
    }
}