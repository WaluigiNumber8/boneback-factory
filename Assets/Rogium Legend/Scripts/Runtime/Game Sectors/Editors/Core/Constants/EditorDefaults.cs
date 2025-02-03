using RedRats.Core;
using Rogium.Editors.Enemies;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Rooms;
using Rogium.Editors.Tiles;
using Rogium.Editors.Weapons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Editors.Core.Defaults
{
    [CreateAssetMenu(fileName = "asset_Defaults_Editor", menuName = EditorAssetPaths.AssetMenuEditor + "Default Editor Constants", order = 500)]
    [ResourcesAssetPath("Defaults/asset_Defaults_Editor")]
    public class EditorDefaults : ScriptableObjectSingleton<EditorDefaults>
    {
        public const string EmptyAssetID = "-1";
        public const int EmptyColorID = -1;
        
        [SerializeField, Required, FoldoutGroup("General")] private int pixelsPerUnit = 16;
        [SerializeField, Required, FoldoutGroup("General"), PreviewField] private Sprite emptySprite;
        [SerializeField, Required, FoldoutGroup("General"), PreviewField] private Sprite missingSprite;
        [SerializeField, Required, FoldoutGroup("General")] private Color emptyGridColor = Color.black;
        [SerializeField, Required, FoldoutGroup("General")] private Color noColor = Color.clear;
        [SerializeField, Required, FoldoutGroup("General")] private Color missingColor = new(255, 88, 227);
        [SerializeField, Required, FoldoutGroup("General")] private Color defaultColor = Color.white;
        [SerializeField, Required, FoldoutGroup("General")] private string author = "NO_AUTHOR";
        [SerializeField, Required, FoldoutGroup("General")] private string authorGame = "Game";
        
        [SerializeField, Required, FoldoutGroup("Input"), Range(0.1f, 16f)] private float inputTimeout = 5f;
        [SerializeField, Required, FoldoutGroup("Input"), Range(0f, 1f)] private float inputWaitForAnother = 0.16f;
        [SerializeField, Required, FoldoutGroup("Input")] private string inputEmptyText = "-";
        
        [SerializeField, Required, FoldoutGroup("Packs")] private string packTitle = "New Pack";
        [SerializeField, Required, FoldoutGroup("Packs"), PreviewField] private Sprite packIcon;
        [SerializeField, Required, FoldoutGroup("Packs"), TextArea] private string packDescription = "A new pack filled with adventure!";
        
        [SerializeField, Required, FoldoutGroup("Campaigns")] private string campaignTitle = "New Campaign";
        [SerializeField, Required, FoldoutGroup("Campaigns")] private int campaignLength = 25;
        
        [SerializeField, Required, FoldoutGroup("Palettes")] private string paletteTitle = "New Palette";
        [SerializeField, Required, FoldoutGroup("Palettes")] private int paletteSize = 9;
        [SerializeField, Required, FoldoutGroup("Palettes")] private Color[] missingPalette;
        
        [SerializeField, Required, FoldoutGroup("Sprites")] private string spriteTitle = "New Sprite";
        [SerializeField, Required, FoldoutGroup("Sprites")] private int spriteSize = 16;
        
        [SerializeField, Required, FoldoutGroup("Weapons")] private string weaponTitle = "New Weapon";
        [SerializeField, Required, FoldoutGroup("Weapons"), PreviewField] private Sprite weaponIcon;
        [SerializeField, Required, FoldoutGroup("Weapons")] private Color weaponColor = Color.green;
        [SerializeField, Required, FoldoutGroup("Weapons")] private AnimationType weaponAnimationType = AnimationType.NoAnimation;
        [SerializeField, Required, FoldoutGroup("Weapons"), PropertyRange(0, "weaponFrameDurationMax")] private int weaponFrameDuration = 40;
        [SerializeField, Required, FoldoutGroup("Weapons"), MinValue(0)] private int weaponFrameDurationMax = 1200;
        [SerializeField, Required, FoldoutGroup("Weapons")] private int weaponBaseDamage = 2;
        [SerializeField, Required, FoldoutGroup("Weapons")] private float weaponUseDelay = 1.2f;
        [SerializeField, Required, FoldoutGroup("Weapons")] private float weaponUseCooldownMax = 1.2f;
        [SerializeField, Required, FoldoutGroup("Weapons")] private float weaponKnockbackForceSelf;
        [SerializeField, Required, FoldoutGroup("Weapons")] private bool weaponKnockbackLockDirectionSelf = true;
        [SerializeField, Required, FoldoutGroup("Weapons")] private float weaponKnockbackForceOther = 10f;
        [SerializeField, Required, FoldoutGroup("Weapons")] private bool weaponKnockbackLockDirectionOther = true;
        [SerializeField, Required, FoldoutGroup("Weapons")] private float weaponKnockbackForceMax = 60f;
        [SerializeField, Required, FoldoutGroup("Weapons"), EnumToggleButtons] private WeaponUseType weaponUseType = WeaponUseType.PopUp;
        [SerializeField, Required, FoldoutGroup("Weapons"), PropertyRange(0f, "weaponUseDurationMax")] private float weaponUseDuration = 0.2f;
        [SerializeField, Required, FoldoutGroup("Weapons"), MinValue(0f)] private float weaponUseDurationMax = 20f;
        [SerializeField, Required, FoldoutGroup("Weapons"), PropertyRange(0f, "weaponUseStartDelayMax")] private float weaponUseStartDelay;
        [SerializeField, Required, FoldoutGroup("Weapons"), MinValue(0f)] private float weaponUseStartDelayMax = 20f;
        [SerializeField, Required, FoldoutGroup("Weapons")] private bool weaponIsEvasive;
        [SerializeField, Required, FoldoutGroup("Weapons")] private bool weaponFreezeUser = true;
        [SerializeField, Required, FoldoutGroup("Weapons"), MinValue(0)] private int weaponProjectileMaxCount = 12;
        [SerializeField, Required, FoldoutGroup("Weapons")] private int weaponProjectileAngleOffset;
        [SerializeField, Required, FoldoutGroup("Weapons")] private int weaponProjectileAngleOffsetMax = 180;
        [SerializeField, Required, FoldoutGroup("Weapons"), PropertyRange(0f, "weaponProjectileSpawnDelayMax")] private float weaponProjectileSpawnDelay;
        [SerializeField, Required, FoldoutGroup("Weapons"), MinValue(0f)] private float weaponProjectileSpawnDelayMax = 20f;
        
        [SerializeField, Required, FoldoutGroup("Projectiles")] private string projectileTitle = "New Projectile";
        [SerializeField, Required, FoldoutGroup("Projectiles"), PreviewField] private Sprite projectileIcon;
        [SerializeField, Required, FoldoutGroup("Projectiles")] private Color projectileColor = Color.yellow;
        [SerializeField, Required, FoldoutGroup("Projectiles")] private AnimationType projectileAnimationType = AnimationType.NoAnimation;
        [SerializeField, Required, FoldoutGroup("Projectiles"), PropertyRange(0, "projectileFrameDurationMax")] private int projectileFrameDuration = 40;
        [SerializeField, Required, FoldoutGroup("Projectiles"), MinValue(0)] private int projectileFrameDurationMax = 1200;
        [SerializeField, Required, FoldoutGroup("Projectiles")] private int projectileBaseDamage = 1;
        [SerializeField, Required, FoldoutGroup("Projectiles"), PropertyRange(0f, "projectileLifetimeMax")] private float projectileLifetime = 0.5f;
        [SerializeField, Required, FoldoutGroup("Projectiles"), MinValue(0f)] private float projectileLifetimeMax = 120f;
        [SerializeField, Required, FoldoutGroup("Projectiles")] private float projectileKnockbackForceSelf;
        [SerializeField, Required, FoldoutGroup("Projectiles")] private bool projectileKnockbackLockDirectionSelf = true;
        [SerializeField, Required, FoldoutGroup("Projectiles")] private float projectileKnockbackForceOther = 5f;
        [SerializeField, Required, FoldoutGroup("Projectiles")] private bool projectileKnockbackLockDirectionOther = true;
        [SerializeField, Required, FoldoutGroup("Projectiles")] private float projectileKnockbackForceMax = 60f;
        [SerializeField, Required, FoldoutGroup("Projectiles")] private float projectileFlightSpeed = 16;
        [SerializeField, Required, FoldoutGroup("Projectiles"), PropertyRange(0f, "projectileAccelerationMax")] private float projectileAcceleration = 0.5f;
        [SerializeField, Required, FoldoutGroup("Projectiles"), MinValue(0f)] private float projectileAccelerationMax = 1f;
        [SerializeField, Required, FoldoutGroup("Projectiles"), PropertyRange(0f, "projectileBrakeForceMax")] private float projectileBrakeForce = 0.01f;
        [SerializeField, Required, FoldoutGroup("Projectiles"), MinValue(0f)] private float projectileBrakeForceMax = 2f;
        [SerializeField, Required, FoldoutGroup("Projectiles"), EnumToggleButtons] private PierceType projectilePierceType = PierceType.None;
        
        [SerializeField, Required, FoldoutGroup("Enemies")] private string enemyTitle = "New Enemy";
        [SerializeField, Required, FoldoutGroup("Enemies"), PreviewField] private Sprite enemyIcon;
        [SerializeField, Required, FoldoutGroup("Enemies")] private Color enemyColor = Color.red;
        [SerializeField, Required, FoldoutGroup("Enemies")] private AnimationType enemyAnimationType = AnimationType.HorizontalFlip;
        [SerializeField, Required, FoldoutGroup("Enemies"), PropertyRange(0f, "enemyFrameDurationMax")] private int enemyFrameDuration = 50;
        [SerializeField, Required, FoldoutGroup("Enemies"), MinValue(0)] private int enemyFrameDurationMax = 1200;
        [SerializeField, Required, FoldoutGroup("Enemies")] private int enemyBaseDamage = 1;
        [SerializeField, Required, FoldoutGroup("Enemies")] private float enemyInvincibilityTimeMax = 20;
        [SerializeField, Required, FoldoutGroup("Enemies"), PropertyRange(0f, "enemyAttackDelayMax")] private float enemyAttackDelay = 2f;
        [SerializeField, Required, FoldoutGroup("Enemies"), MinValue(0f)] private float enemyAttackDelayMax = 20f;
        [SerializeField, Required, FoldoutGroup("Enemies")] private float enemyKnockbackForceSelf = 1f;
        [SerializeField, Required, FoldoutGroup("Enemies")] private bool enemyKnockbackLockDirectionSelf = true;
        [SerializeField, Required, FoldoutGroup("Enemies")] private float enemyKnockbackForceOther = 10f;
        [SerializeField, Required, FoldoutGroup("Enemies")] private bool enemyKnockbackLockDirectionOther = true;
        [SerializeField, Required, FoldoutGroup("Enemies")] private float enemyKnockbackForceMax = 60f;
        [SerializeField, Required, FoldoutGroup("Enemies")] private int enemyMaxHealth = 5;
        [SerializeField, Required, FoldoutGroup("Enemies")] private float enemyAttackProbability = 100;
        [SerializeField, Required, FoldoutGroup("Enemies")] private float enemyInvincibilityTime = 0.25f;
        [SerializeField, Required, FoldoutGroup("Enemies"), MinValue(0)] private int enemyWeaponMaxCount = 9;
        [SerializeField, Required, FoldoutGroup("Enemies")] private AIType enemyAI = AIType.LookInDirection;
        [SerializeField, Required, FoldoutGroup("Enemies")] private DirectionType enemyStartingDirection = DirectionType.Down;
        [SerializeField, Required, FoldoutGroup("Enemies")] private bool enemySeamlessMovement;
        [SerializeField, Required, FoldoutGroup("Enemies"), PropertyRange(0.01f, "enemyNextStepTimeMax")] private float enemyNextStepTime = 0.25f;
        [SerializeField, Required, FoldoutGroup("Enemies"), MinValue(0.01f)] private float enemyNextStepTimeMax = 20f;
        
        [SerializeField, Required, FoldoutGroup("Rooms")] private string roomTitle = "New Room";
        [SerializeField, Required, FoldoutGroup("Rooms")] private Vector2Int roomSize = new(15, 10);
        [SerializeField, Required, FoldoutGroup("Rooms")] private int roomDifficulty;
        [SerializeField, Required, FoldoutGroup("Rooms")] private string[] roomDifficultyTitles;
        [SerializeField, Required, FoldoutGroup("Rooms")] private RoomType roomType = RoomType.Common;
        [SerializeField, Required, FoldoutGroup("Rooms"), PropertyRange(0, 255)] private int roomLightness = 255;
        [SerializeField, Required, FoldoutGroup("Rooms")] private Color roomLightnessColor = new(1, 1, 1, 1);
        
        [SerializeField, Required, FoldoutGroup("Tiles")] private string tileTitle = "New Tile";
        [SerializeField, Required, FoldoutGroup("Tiles"), PreviewField] private Sprite tileIcon;
        [SerializeField, Required, FoldoutGroup("Tiles"), EnumToggleButtons] private TileType tileType = TileType.Tile;
        [SerializeField, Required, FoldoutGroup("Tiles"), EnumToggleButtons] private TileLayerType tileLayer = TileLayerType.Wall;
        [SerializeField, Required, FoldoutGroup("Tiles")] private TerrainType tileTerrainType = TerrainType.Tile;
        
        [SerializeField, Required, FoldoutGroup("Sounds"), MinValue(0f)] private float soundPitchOffset = 0.05f;
        
        public float InputTimeout { get => inputTimeout; }
        public float InputWaitForAnother { get => inputWaitForAnother; }
        public string InputEmptyText { get => inputEmptyText; }
        public string PackTitle { get => packTitle; }
        public string PackDescription { get => packDescription; }
        public string CampaignTitle { get => campaignTitle; }
        public int CampaignLength { get => campaignLength; }
        public string PaletteTitle { get => paletteTitle; }
        public int PaletteSize { get => paletteSize; }
        public Color[] MissingPalette { get => missingPalette; }
        public string SpriteTitle { get => spriteTitle; }
        public int SpriteSize { get => spriteSize; }
        public string WeaponTitle { get => weaponTitle; }
        public Color WeaponColor { get => weaponColor; }
        public AnimationType WeaponAnimationType { get => weaponAnimationType; }
        public int WeaponFrameDuration { get => weaponFrameDuration; }
        public int WeaponFrameDurationMax { get => weaponFrameDurationMax; }
        public int WeaponBaseDamage { get => weaponBaseDamage; }
        public float WeaponUseDelay { get => weaponUseDelay; }
        public float WeaponUseCooldownMax { get => weaponUseCooldownMax; }
        public float WeaponKnockbackForceSelf { get => weaponKnockbackForceSelf; }
        public bool WeaponKnockbackLockDirectionSelf { get => weaponKnockbackLockDirectionSelf; }
        public float WeaponKnockbackForceOther { get => weaponKnockbackForceOther; }
        public bool WeaponKnockbackLockDirectionOther { get => weaponKnockbackLockDirectionOther; }
        public float WeaponKnockbackForceMax { get => weaponKnockbackForceMax; }
        public WeaponUseType WeaponUseType { get => weaponUseType; }
        public float WeaponUseDuration { get => weaponUseDuration; }
        public float WeaponUseDurationMax { get => weaponUseDurationMax; }
        public float WeaponUseStartDelay { get => weaponUseStartDelay; }
        public float WeaponUseStartDelayMax { get => weaponUseStartDelayMax; }
        public bool WeaponIsEvasive { get => weaponIsEvasive; }
        public bool WeaponFreezeUser { get => weaponFreezeUser; }
        public int WeaponProjectileMaxCount { get => weaponProjectileMaxCount; }
        public int WeaponProjectileAngleOffset { get => weaponProjectileAngleOffset; }
        public int WeaponProjectileAngleOffsetMax { get => weaponProjectileAngleOffsetMax; }
        public float WeaponProjectileSpawnDelay { get => weaponProjectileSpawnDelay; }
        public float WeaponProjectileSpawnDelayMax { get => weaponProjectileSpawnDelayMax; }
        public string ProjectileTitle { get => projectileTitle; }
        public Color ProjectileColor { get => projectileColor; }
        public AnimationType ProjectileAnimationType { get => projectileAnimationType; }
        public int ProjectileFrameDuration { get => projectileFrameDuration; }
        public int ProjectileFrameDurationMax { get => projectileFrameDurationMax; }
        public int ProjectileBaseDamage { get => projectileBaseDamage; }
        public float ProjectileLifetime { get => projectileLifetime; }
        public float ProjectileLifetimeMax { get => projectileLifetimeMax; }
        public float ProjectileKnockbackForceSelf { get => projectileKnockbackForceSelf; }
        public bool ProjectileKnockbackLockDirectionSelf { get => projectileKnockbackLockDirectionSelf; }
        public float ProjectileKnockbackForceOther { get => projectileKnockbackForceOther; }
        public bool ProjectileKnockbackLockDirectionOther { get => projectileKnockbackLockDirectionOther; }
        public float ProjectileKnockbackForceMax { get => projectileKnockbackForceMax; }
        public float ProjectileFlightSpeed { get => projectileFlightSpeed; }
        public float ProjectileAcceleration { get => projectileAcceleration; }
        public float ProjectileAccelerationMax { get => projectileAccelerationMax; }
        public float ProjectileBrakeForce { get => projectileBrakeForce; }
        public float ProjectileBrakeForceMax { get => projectileBrakeForceMax; }
        public PierceType ProjectilePierceType { get => projectilePierceType; }
        public string EnemyTitle { get => enemyTitle; }
        public Color EnemyColor { get => enemyColor; }
        public AnimationType EnemyAnimationType { get => enemyAnimationType; }
        public int EnemyFrameDuration { get => enemyFrameDuration; }
        public int EnemyFrameDurationMax { get => enemyFrameDurationMax; }
        public int EnemyBaseDamage { get => enemyBaseDamage; }
        public float EnemyInvincibilityTimeMax { get => enemyInvincibilityTimeMax; }
        public float EnemyAttackDelay { get => enemyAttackDelay; }
        public float EnemyAttackDelayMax { get => enemyAttackDelayMax; }
        public float EnemyKnockbackForceSelf { get => enemyKnockbackForceSelf; }
        public bool EnemyKnockbackLockDirectionSelf { get => enemyKnockbackLockDirectionSelf; }
        public float EnemyKnockbackForceOther { get => enemyKnockbackForceOther; }
        public bool EnemyKnockbackLockDirectionOther { get => enemyKnockbackLockDirectionOther; }
        public float EnemyKnockbackForceMax { get => enemyKnockbackForceMax; }
        public int EnemyMaxHealth { get => enemyMaxHealth; }
        public float EnemyAttackProbability { get => enemyAttackProbability; }
        public float EnemyInvincibilityTime { get => enemyInvincibilityTime; }
        public int EnemyWeaponMaxCount { get => enemyWeaponMaxCount; }
        public AIType EnemyAI { get => enemyAI; }
        public DirectionType EnemyStartingDirection { get => enemyStartingDirection; }
        public bool EnemySeamlessMovement { get => enemySeamlessMovement; }
        public float EnemyNextStepTime { get => enemyNextStepTime; }
        public float EnemyNextStepTimeMax { get => enemyNextStepTimeMax; }
        public string RoomTitle { get => roomTitle; }
        public Vector2Int RoomSize { get => roomSize; }
        public int RoomDifficulty { get => roomDifficulty; }
        public string[] RoomDifficultyTitles { get => roomDifficultyTitles; }
        public RoomType RoomType { get => roomType; }
        public int RoomLightness { get => roomLightness; }
        public Color RoomLightnessColor { get => roomLightnessColor; }
        public string TileTitle { get => tileTitle; }
        public TileType TileType { get => tileType; }
        public TileLayerType TileLayer { get => tileLayer; }
        public TerrainType TileTerrainType { get => tileTerrainType; }
        public float SoundPitchOffset { get => soundPitchOffset; }
        public string Author { get => author; }
        public string AuthorGame { get => authorGame; }
        public int PixelsPerUnit { get => pixelsPerUnit; }
        public Color EmptyGridColor { get => emptyGridColor; }
        public Color NoColor { get => noColor; }
        public Color MissingColor { get => missingColor; }
        public Color DefaultColor { get => defaultColor; }
        public Sprite EmptySprite { get => emptySprite; }
        public Sprite MissingSprite { get => missingSprite; }
        public Sprite PackIcon { get => packIcon; }
        public Sprite WeaponIcon { get => weaponIcon; }
        public Sprite ProjectileIcon { get => projectileIcon; }
        public Sprite EnemyIcon { get => enemyIcon; }
        public Sprite TileIcon { get => tileIcon; }
    }
}