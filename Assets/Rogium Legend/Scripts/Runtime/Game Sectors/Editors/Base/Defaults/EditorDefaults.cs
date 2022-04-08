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
        
        //Campaign
        public const string CampaignTitle = "New Campaign";
        public static readonly Sprite CampaignIcon = Resources.Load<Sprite>("Defaults/spr_Default_Weapon_Filled");
        public const int CampaignLength = 25;

        //Palette
        public const string PaletteTitle = "New Palette";
        public const int PaletteSize = 9;
        
        //Sprite
        public const string SpriteTitle = "New Sprite";
        public static readonly Sprite SpriteIcon = Resources.Load<Sprite>("Defaults/spr_Grid_Blank");
        public const int SpriteSize = 16;
        
        //Weapon
        public const string WeaponTitle = "New Weapon";
        public static readonly Sprite WeaponIcon = Resources.Load<Sprite>("Defaults/spr_Default_Weapon");
        public const int WeaponBaseDamage = 2;
        public const float WeaponUseDelay = 0.1f;
        public const float WeaponKnockbackSelf = 0;
        public const float WeaponKnockbackOther = 1;
        
        //Projectile
        public const string ProjectileTitle = "New Projectile";
        public static readonly Sprite ProjectileIcon = Resources.Load<Sprite>("Defaults/spr_Default_Weapon_Filled");
        public const int ProjectileBaseDamage = 1;
        public const float ProjectileLifetime = 5;
        public const float ProjectileKnockbackSelf = 0;
        public const float ProjectileKnockbackOther = 1;
        
        //Enemy
        public const string EnemyTitle = "New Enemy";
        public static readonly Sprite EnemyIcon = Resources.Load<Sprite>("Defaults/spr_TestDummy");
        public const int EnemyBaseDamage = 1;
        public const float EnemyAttackDelay = 0.2f;
        public const float EnemyKnockbackForceSelf = 1;
        public const float EnemyKnockbackTimeSelf = 0.025f;
        public const float EnemyKnockbackTimeOther = 0.05f;
        public const float EnemyKnockbackForceOther = 2;
        public const int EnemyMaxHealth = 20;
        public const float EnemyInvincibilityTime = 0.05f;
        
        //Room
        public const string RoomTitle = "New Room";
        public static readonly Sprite RoomIcon = Resources.Load<Sprite>("Defaults/spr_Grid_Blank");
        public static readonly Vector2Int RoomSize = new Vector2Int(15, 10);
        public const int RoomDifficulty = 0;
        public const RoomType RoomType = Rooms.RoomType.Normal;
        public const int RoomLightness = 255;
        
        //Tile
        public const string TileTitle = "New Tile";
        public static readonly Sprite TileIcon = Resources.Load<Sprite>("Defaults/spr_Default_Tile");

        //Other
        public const string EmptyAssetID = "-1";
        public const int EmptyColorID = -1;
        public const string Author = "NO_AUTHOR";
        public const string AuthorGame = "Game";
        public const int PixelsPerUnit = 16;
        public static readonly Sprite EmptyGridSprite = Resources.Load<Sprite>("Defaults/spr_Grid_Blank");
        public static readonly Color EmptyGridColor = Color.black;
        public static readonly Color NoColor = new Color(0, 0, 0, 0);
        public static readonly Sprite MissingSprite = Resources.Load<Sprite>("Defaults/spr_Missing");
        public static readonly Color MissingColor = new Color(255, 88, 227);
        public static readonly Color DefaultColor = Color.white;
        
        //Asset SubMenu
        public const string AssetMenuBase = "Rogium Legend/";
        public const string AssetMenuAssets = "Rogium Legend/Assets/";
        public const string AssetMenuEditor = "Rogium Legend/Editor/";
        public const string AssetMenuGameplay = "Rogium Legend/Gameplay/";
    }
}