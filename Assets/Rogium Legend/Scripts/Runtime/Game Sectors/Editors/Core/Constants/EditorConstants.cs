using RedRats.Core;
using Rogium.Editors.Enemies;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Rooms;
using Rogium.Editors.Weapons;
using UnityEngine;

namespace Rogium.Editors.Core.Defaults
{
    /// <summary>
    /// Contains all default values used by the editor.
    /// </summary>
    public static class EditorConstants
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
        public const int WeaponFrameDurationMax = 1200;
        public static readonly Sprite WeaponIconAlt = Resources.Load<Sprite>("Defaults/spr_Grid_Blank");
        
        public const int WeaponBaseDamage = 2;
        public const float WeaponUseDelay = 1.2f;
        public const float WeaponUseCooldownMax = 1.2f;
        public const float WeaponKnockbackForceSelf = 0f;
        public const float WeaponKnockbackTimeSelf = 0f;
        public const bool WeaponKnockbackLockDirectionSelf = true;
        public const float WeaponKnockbackForceOther = 0.5f;
        public const float WeaponKnockbackTimeOther = 0.05f;
        public const bool WeaponKnockbackLockDirectionOther = true;
        public const float WeaponKnockbackForceMax = 10f;
        public const float WeaponKnockbackTimeMax = 0.5f;

        public const WeaponUseType WeaponUseType = Weapons.WeaponUseType.PopUp;
        public const float WeaponUseDuration = 0.2f;
        public const float WeaponUseDurationMax = 20f;
        public const float WeaponUseStartDelay = 0f;
        public const float WeaponUseStartDelayMax = 20f;
        public const bool WeaponIsEvasive = false;
        public const bool WeaponFreezeUser = true;
        
        public const int WeaponProjectileMaxCount = 12;
        public const int WeaponProjectileAngleOffset = 0;
        public const int WeaponProjectileAngleOffsetMax = 180;
        public const float WeaponProjectileSpawnDelay = 0;
        public const float WeaponProjectileSpawnDelayMax = 20f;
        
        //Projectile
        public const string ProjectileTitle = "New Projectile";
        public static readonly Sprite ProjectileIcon = Resources.Load<Sprite>("Defaults/spr_Default_Weapon");
        
        public const AnimationType ProjectileAnimationType = AnimationType.NoAnimation;
        public const int ProjectileFrameDuration = 40;
        public const int ProjectileFrameDurationMax = 1200;
        public static readonly Sprite ProjectileIconAlt = Resources.Load<Sprite>("Defaults/spr_Grid_Blank");
        
        public const int ProjectileBaseDamage = 1;
        public const float ProjectileLifetime = 0.5f;
        public const float ProjectileLifetimeMax = 120f;
        public const float ProjectileKnockbackForceSelf = 0;
        public const float ProjectileKnockbackTimeSelf = 0f;
        public const bool ProjectileKnockbackLockDirectionSelf = true;
        public const float ProjectileKnockbackForceOther = 2f;
        public const float ProjectileKnockbackTimeOther = 0.075f;
        public const bool ProjectileKnockbackLockDirectionOther = true;
        public const float ProjectileKnockbackForceMax = 10f;
        public const float ProjectileKnockbackTimeMax = 20f;

        public const float ProjectileFlightSpeed = 20;
        public const float ProjectileAcceleration = 0.2f;
        public const float ProjectileBrakeForce = 0.5f;
        public const PierceType ProjectilePierceType = PierceType.None;
        public const float ProjectileMaxAcceleration = 0.5f;
        public const float ProjectileMaxBrakeForce = 0.5f;
        
        //Enemy
        public const string EnemyTitle = "New Enemy";
        public static readonly Sprite EnemyIcon = Resources.Load<Sprite>("Defaults/spr_TestDummy");

        public const AnimationType EnemyAnimationType = AnimationType.HorizontalFlip;
        public const int EnemyFrameDuration = 50;
        public const int EnemyFrameDurationMax = 1200;
        public static readonly Sprite EnemyIconAlt = Resources.Load<Sprite>("Defaults/spr_Grid_Blank");
        
        public const int EnemyBaseDamage = 1;
        public const float EnemyInvincibilityTimeMax = 20;
        public const float EnemyAttackDelay = 2f;
        public const float EnemyAttackDelayMax = 20f;
        public const float EnemyKnockbackForceSelf = 1;
        public const float EnemyKnockbackTimeSelf = 0.025f;
        public const bool EnemyKnockbackLockDirectionSelf = true;
        public const float EnemyKnockbackForceOther = 1.75f;
        public const float EnemyKnockbackTimeOther = 0.1f;
        public const bool EnemyKnockbackLockDirectionOther = true;
        public const float EnemyKnockbackForceMax = 10f;
        public const float EnemyKnockbackTimeMax = 0.5f;

        public const int EnemyMaxHealth = 5;
        public const float EnemyAttackProbability = 100;
        public const float EnemyInvincibilityTime = 0.25f;
        public const int EnemyWeaponMaxCount = 4;
        
        public const AIType EnemyAI = AIType.LookInDirection;
        public const DirectionType EnemyStartingDirection = DirectionType.Down;
        public const bool EnemySeamlessMovement = false;
        public const float EnemyNextStepTime = 0.25f;
        public const float EnemyNextStepTimeMax = 20f;
        
        //Room
        public const string RoomTitle = "New Room";
        public static readonly Sprite RoomIcon = Resources.Load<Sprite>("Defaults/spr_Grid_Blank");
        public static readonly Vector2Int RoomSize = new Vector2Int(15, 10);
        public const int RoomDifficulty = 0;
        public const RoomType RoomType = Rooms.RoomType.Common;
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
        
        //Tools SubMenu
        public const string ToolMenuBase = "Tools/Rogium Legend/";
    }
}