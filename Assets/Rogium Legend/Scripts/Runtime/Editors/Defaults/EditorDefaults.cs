using Rogium.Editors.Rooms;
using UnityEngine;

namespace Rogium.Editors.Core.Defaults
{
    /// <summary>
    /// Contains all default values used by the editor.
    /// </summary>
    public static class EditorDefaults
    {
        //Pack
        public const string PackTitle = "New Pack";
        public const string PackDescription = "A new pack filled with adventure!";
        public static readonly Sprite PackIcon = Resources.Load<Sprite>("Defaults/spr_Default_Weapon_Filled");
        public const string Author = "NO_AUTHOR";
        
        //Campaign
        public const string CampaignTitle = "New Campaign";
        public static readonly Sprite CampaignIcon = Resources.Load<Sprite>("Defaults/spr_Default_Weapon_Filled");

        //Palette
        public const string PaletteName = "New Palette";
        public const int PaletteSize = 9;
        
        //Sprite
        public const string SpriteName = "New Sprite";
        public static readonly Sprite SpriteIcon = Resources.Load<Sprite>("Defaults/spr_Grid_Blank");
        public const int SpriteSize = 16;
        
        //Tile
        public const string TileTitle = "New Tile";
        public static readonly Sprite TileIcon = Resources.Load<Sprite>("Defaults/spr_Default_Tile");

        //Room
        public const string RoomTitle = "New Room";
        public static readonly Sprite RoomIcon = Resources.Load<Sprite>("Defaults/spr_Grid_Blank");
        public static readonly Vector2Int RoomSize = new Vector2Int(15, 10);
        public const int RoomDifficulty = 0;
        public const RoomType RoomType = Rooms.RoomType.Normal;

        //Other
        public const string EmptyAssetID = "-1";
        public const int EmptyColorID = -1;
        public const int PixelsPerUnit = 16;
        public static readonly Sprite EmptyGridSprite = Resources.Load<Sprite>("Defaults/spr_Grid_Blank");
        public static readonly Color EmptyGridColor = Color.black;
        public static readonly Color NoColor = new Color(0, 0, 0, 0);
        public static readonly Sprite MissingSprite = Resources.Load<Sprite>("Defaults/spr_Missing");
        public static readonly Color MissingColor = new Color(255, 88, 227);
    }
}