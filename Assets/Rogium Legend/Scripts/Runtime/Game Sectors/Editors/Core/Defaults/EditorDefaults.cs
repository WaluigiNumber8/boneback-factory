using Rogium.Editors.Rooms;
using Rogium.Editors.Weapons;
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
        public static readonly Sprite PackIcon = Resources.Load<Sprite>("Defaults/spr_Default_Weapon");
        
        //Campaign
        public const string CampaignTitle = "New Campaign";
        public static readonly Sprite CampaignIcon = Resources.Load<Sprite>("Defaults/spr_Default_Weapon");
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
        
        public const AnimationType WeaponAnimationType = AnimationType.NoAnimation;
        public const int WeaponFrameDuration = 40;
        public static readonly Sprite WeaponIconAlt = Resources.Load<Sprite>("Defaults/spr_Grid_Blank");
        
        public const int WeaponBaseDamage = 2;
        public const float WeaponUseDelay = 0.1f;
        public const float WeaponKnockbackForceSelf = 0;
        public const float WeaponKnockbackTimeSelf = 0f;
        public const bool WeaponKnockbackLockDirectionSelf = true;
        public const float WeaponKnockbackForceOther = 0.5f;
        public const float WeaponKnockbackTimeOther = 0.05f;
        public const bool WeaponKnockbackLockDirectionOther = true;

        public const WeaponUseType WeaponUseType = Weapons.WeaponUseType.PopUp;
        public const float WeaponUseDuration = 0.05f;
        public const float WeaponUseStartDelay = 0f;
        public const bool WeaponIsEvasive = false;
        public const bool WeaponFreezeUser = true;
        public const int WeaponProjectileMaxCount = 12;
        
        //Projectile
        public const string ProjectileTitle = "New Projectile";
        public static readonly Sprite ProjectileIcon = Resources.Load<Sprite>("Defaults/spr_Default_Weapon");
        
        public const AnimationType ProjectileAnimationType = AnimationType.NoAnimation;
        public const int ProjectileFrameDuration = 40;
        public static readonly Sprite ProjectileIconAlt = Resources.Load<Sprite>("Defaults/spr_Grid_Blank");
        
        public const int ProjectileBaseDamage = 1;
        public const float ProjectileLifetime = 5;
        public const float ProjectileKnockbackForceSelf = 0;
        public const float ProjectileKnockbackTimeSelf = 0f;
        public const bool ProjectileKnockbackLockDirectionSelf = true;
        public const float ProjectileKnockbackForceOther = 2f;
        public const float ProjectileKnockbackTimeOther = 0.075f;
        public const bool ProjectileKnockbackLockDirectionOther = true;
        
        //Enemy
        public const string EnemyTitle = "New Enemy";
        public static readonly Sprite EnemyIcon = Resources.Load<Sprite>("Defaults/spr_TestDummy");

        public const AnimationType EnemyAnimationType = AnimationType.HorizontalFlip;
        public const int EnemyFrameDuration = 50;
        public static readonly Sprite EnemyIconAlt = Resources.Load<Sprite>("Defaults/spr_Grid_Blank");
        
        public const int EnemyBaseDamage = 1;
        public const float EnemyAttackDelay = 2f;
        public const float EnemyKnockbackForceSelf = 1;
        public const float EnemyKnockbackTimeSelf = 0.025f;
        public const bool EnemyKnockbackLockDirectionSelf = true;
        public const float EnemyKnockbackForceOther = 2;
        public const float EnemyKnockbackTimeOther = 0.075f;
        public const bool EnemyKnockbackLockDirectionOther = true;
        
        public const int EnemyMaxHealth = 20;
        public const float EnemyAttackProbability = 100;
        public const float EnemyInvincibilityTime = 0.05f;
        public const int EnemyWeaponMaxCount = 4;
        
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