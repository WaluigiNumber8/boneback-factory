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
        
        //Tile
        public const string TileTitle = "New Tile";
        public static readonly Sprite TileIcon = Resources.Load<Sprite>("Defaults/spr_Default_Tile");

        //Room
        public const string RoomTitle = "New Room";
        public static readonly Sprite RoomIcon = Resources.Load<Sprite>("Defaults/spr_Default_Weapon_Filled");
        public const int RoomDifficulty = 0;
        public const RoomType RoomType = Rooms.RoomType.Normal;

        //Other
        public const string EmptyID = "-1";
        public static readonly Sprite EmptyGridSprite = Resources.Load<Sprite>("Defaults/spr_Grid_Blank");
        public static readonly Sprite MissingSprite = Resources.Load<Sprite>("Defaults/spr_Missing");
    }
}