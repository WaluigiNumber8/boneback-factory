using System;
using System.Collections.Generic;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;

namespace Rogium.Editors.Enemies
{
    /// <summary>
    /// Contains all data needed for an enemy.
    /// </summary>
    public class EnemyAsset : EntityAssetBase
    {
        private int maxHealth;
        private float attackProbability;
        private float invincibilityTime;
        private List<string> weaponIDs;

        private AIType ai;
        private DirectionType startingDirection;
        private bool seamlessMovement;
        private float nextStepTime;

        private AssetData hurtSound;
        private AssetData deathSound;
        private AssetData idleSound;

        private EnemyAsset() { }

        #region Constructors

        // public EnemyAsset()
        // {
        //     InitBase(EditorDefaults.Instance.EnemyTitle, EditorDefaults.Instance.EnemyIcon, EditorDefaults.Instance.Author, DateTime.Now);
        //     color = EditorDefaults.Instance.EnemyColor;
        //
        //     animationType = EditorDefaults.Instance.EnemyAnimationType;
        //     frameDuration = EditorDefaults.Instance.EnemyFrameDuration;
        //     iconAlt = EditorDefaults.Instance.EmptySprite;
        //     
        //     baseDamage = EditorDefaults.Instance.EnemyBaseDamage;
        //     useDelay = EditorDefaults.Instance.EnemyAttackDelay;
        //     knockbackForceSelf = EditorDefaults.Instance.EnemyKnockbackForceSelf;
        //     knockbackLockDirectionSelf = EditorDefaults.Instance.EnemyKnockbackLockDirectionSelf;
        //     knockbackForceOther = EditorDefaults.Instance.EnemyKnockbackForceOther;
        //     knockbackLockDirectionOther = EditorDefaults.Instance.EnemyKnockbackLockDirectionOther;
        //
        //     maxHealth = EditorDefaults.Instance.EnemyMaxHealth;
        //     attackProbability = EditorDefaults.Instance.EnemyAttackProbability;
        //     invincibilityTime = EditorDefaults.Instance.EnemyInvincibilityTime;
        //     weaponIDs = new List<string>();
        //     
        //     ai = EditorDefaults.Instance.EnemyAI;
        //     nextStepTime = EditorDefaults.Instance.EnemyNextStepTime;
        //     seamlessMovement = EditorDefaults.Instance.EnemySeamlessMovement;
        //     startingDirection = EditorDefaults.Instance.EnemyStartingDirection;
        //     
        //     hurtSound = new AssetData(ParameterInfoConstants.ForSound);
        //     deathSound = new AssetData(ParameterInfoConstants.ForSound);
        //     idleSound = new AssetData(ParameterInfoConstants.ForSound);
        //     
        //     GenerateID();
        // }
        //
        // public EnemyAsset(EnemyAsset asset)
        // {
        //     AssetValidation.ValidateTitle(asset.title);
        //     
        //     id = asset.ID;
        //     InitBase(asset.Title, asset.Icon, asset.Author, asset.CreationDate);
        //
        //     color = asset.Color;
        //     associatedSpriteID = asset.AssociatedSpriteID;
        //     
        //     animationType = asset.AnimationType;
        //     frameDuration = asset.FrameDuration;
        //     iconAlt = asset.IconAlt;
        //     
        //     baseDamage = asset.BaseDamage;
        //     useDelay = asset.UseDelay;
        //     knockbackForceSelf = asset.KnockbackForceSelf;
        //     knockbackLockDirectionSelf = asset.KnockbackLockDirectionSelf;
        //     knockbackForceOther = asset.KnockbackForceOther;
        //     knockbackLockDirectionOther = asset.KnockbackLockDirectionOther;
        //
        //     maxHealth = asset.MaxHealth;
        //     attackProbability = asset.AttackProbability;
        //     invincibilityTime = asset.InvincibilityTime;
        //     
        //     ai = asset.AI;
        //     nextStepTime = asset.NextStepTime;
        //     startingDirection = asset.StartingDirection;
        //     seamlessMovement = asset.SeamlessMovement;
        //     
        //     hurtSound = new AssetData(asset.HurtSound);
        //     deathSound = new AssetData(asset.DeathSound);
        //     idleSound = new AssetData(asset.IdleSound);
        //     
        //     weaponIDs = new List<string>(asset.weaponIDs);
        // }
        //
        // public EnemyAsset(string id, string title, Sprite icon, string author, Color color, string associatedSpriteID, 
        //                   AnimationType animationType, int frameDuration, Sprite iconAlt, int baseDamage, float useDelay, 
        //                   float knockbackForceSelf, bool knockbackLockDirectionSelf, float knockbackForceOther, 
        //                   bool knockbackLockDirectionOther, int maxHealth, float attackProbability, float invincibilityTime, 
        //                   IList<string> weaponIDs, AIType ai, float nextStepTime, DirectionType startingDirection, 
        //                   bool seamlessMovement, AssetData hurtSound, AssetData deathSound, AssetData idleSound, 
        //                   DateTime creationDate)
        // {
        //     AssetValidation.ValidateTitle(title);
        //     
        //     this.id = id;
        //     InitBase(title, icon, author, creationDate);
        //     this.color = color;
        //
        //     this.associatedSpriteID = associatedSpriteID;
        //
        //     this.animationType = animationType;
        //     this.frameDuration = frameDuration;
        //     this.iconAlt = iconAlt;
        //     
        //     this.baseDamage = baseDamage;
        //     this.useDelay = useDelay;
        //     this.knockbackForceSelf = knockbackForceSelf;
        //     this.knockbackLockDirectionSelf = knockbackLockDirectionSelf;
        //     this.knockbackForceOther = knockbackForceOther;
        //     this.knockbackLockDirectionOther = knockbackLockDirectionOther;
        //
        //     this.maxHealth = maxHealth;
        //     this.attackProbability = attackProbability;
        //     this.invincibilityTime = invincibilityTime;
        //     this.weaponIDs = new List<string>(weaponIDs);
        //     
        //     this.ai = ai;
        //     this.nextStepTime = nextStepTime;
        //     this.startingDirection = startingDirection;
        //     this.seamlessMovement = seamlessMovement;
        //     
        //     this.hurtSound = new AssetData(hurtSound);
        //     this.deathSound = new AssetData(deathSound);
        //     this.idleSound = new AssetData(idleSound);
        // }

        #endregion

        #region Update Values

        public void UpdateMaxHealth(int newMaxHealth) => maxHealth = newMaxHealth;
        public void UpdateAttackProbability(float newAttackProbability) => attackProbability = newAttackProbability;
        public void UpdateInvincibilityTime(float newInvincibilityTime) => invincibilityTime = newInvincibilityTime;

        public void UpdateWeaponIDsLength(int newLength)
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(newLength, 0, "New weapon IDs size");
            weaponIDs.Resize(newLength, EditorDefaults.EmptyAssetID);
        }

        public void UpdateWeaponIDPos(int pos, string value) => weaponIDs[pos] = value;
        public void UpdateAI(int newAI) => UpdateAI((AIType) newAI);
        public void UpdateAI(AIType newAI) => ai = newAI;
        public void UpdateNextStepTime(float newNextStepTime) => nextStepTime = newNextStepTime;
        public void UpdateStartingDirection(int newDirectionType) => UpdateStartingDirection((DirectionType) newDirectionType);
        public void UpdateStartingDirection(DirectionType newStartingDirection) => startingDirection = newStartingDirection;
        public void UpdateSeamlessMovement(bool newSeamlessMovementState) => seamlessMovement = newSeamlessMovementState;
        public void UpdateHurtSound(AssetData newHurtSound) => hurtSound = newHurtSound;
        public void UpdateDeathSound(AssetData newDeathSound) => deathSound = newDeathSound;
        public void UpdateIdleSound(AssetData newIdleSound) => idleSound = newIdleSound;

        #endregion

        public override void ClearAssociatedSprite()
        {
            base.ClearAssociatedSprite();
            icon = EditorDefaults.Instance.EnemyIcon;
        }

        public List<string> WeaponIDs
        {
            get => weaponIDs;
        }

        public int MaxHealth
        {
            get => maxHealth;
        }

        public float AttackProbability
        {
            get => attackProbability;
        }

        public float InvincibilityTime
        {
            get => invincibilityTime;
        }

        public AIType AI
        {
            get => ai;
        }

        public float NextStepTime
        {
            get => nextStepTime;
        }

        public DirectionType StartingDirection
        {
            get => startingDirection;
        }

        public bool SeamlessMovement
        {
            get => seamlessMovement;
        }

        public AssetData HurtSound
        {
            get => hurtSound;
        }

        public AssetData DeathSound
        {
            get => deathSound;
        }

        public AssetData IdleSound
        {
            get => idleSound;
        }

        public class Builder : EntityAssetBuilder<EnemyAsset, Builder>
        {
            public Builder()
            {
                Asset.title = EditorDefaults.Instance.EnemyTitle;
                Asset.icon = EditorDefaults.Instance.EnemyIcon;
                Asset.author = EditorDefaults.Instance.Author;
                Asset.creationDate = DateTime.Now;
                Asset.GenerateID();

                Asset.color = EditorDefaults.Instance.EnemyColor;
                Asset.animationType = EditorDefaults.Instance.EnemyAnimationType;
                Asset.frameDuration = EditorDefaults.Instance.EnemyFrameDuration;
                Asset.iconAlt = EditorDefaults.Instance.EmptySprite;

                Asset.baseDamage = EditorDefaults.Instance.EnemyBaseDamage;
                Asset.useDelay = EditorDefaults.Instance.EnemyAttackDelay;
                Asset.knockbackForceSelf = EditorDefaults.Instance.EnemyKnockbackForceSelf;
                Asset.knockbackLockDirectionSelf = EditorDefaults.Instance.EnemyKnockbackLockDirectionSelf;
                Asset.knockbackForceOther = EditorDefaults.Instance.EnemyKnockbackForceOther;
                Asset.knockbackLockDirectionOther = EditorDefaults.Instance.EnemyKnockbackLockDirectionOther;

                Asset.maxHealth = EditorDefaults.Instance.EnemyMaxHealth;
                Asset.attackProbability = EditorDefaults.Instance.EnemyAttackProbability;
                Asset.invincibilityTime = EditorDefaults.Instance.EnemyInvincibilityTime;
                Asset.weaponIDs = new List<string>();

                Asset.ai = EditorDefaults.Instance.EnemyAI;
                Asset.nextStepTime = EditorDefaults.Instance.EnemyNextStepTime;
                Asset.seamlessMovement = EditorDefaults.Instance.EnemySeamlessMovement;
                Asset.startingDirection = EditorDefaults.Instance.EnemyStartingDirection;

                Asset.hurtSound = new AssetData(ParameterInfoConstants.ForSound);
                Asset.deathSound = new AssetData(ParameterInfoConstants.ForSound);
                Asset.idleSound = new AssetData(ParameterInfoConstants.ForSound);
            }

            public Builder WithMaxHealth(int maxHealth)
            {
                Asset.maxHealth = maxHealth;
                return This;
            }

            public Builder WithAttackProbability(float attackProbability)
            {
                Asset.attackProbability = attackProbability;
                return This;
            }

            public Builder WithInvincibilityTime(float invincibilityTime)
            {
                Asset.invincibilityTime = invincibilityTime;
                return This;
            }

            public Builder WithWeaponIDs(IList<string> weaponIDs)
            {
                Asset.weaponIDs = new List<string>(weaponIDs);
                return This;
            }

            public Builder WithAI(AIType ai)
            {
                Asset.ai = ai;
                return This;
            }

            public Builder WithNextStepTime(float nextStepTime)
            {
                Asset.nextStepTime = nextStepTime;
                return This;
            }

            public Builder WithStartingDirection(DirectionType startingDirection)
            {
                Asset.startingDirection = startingDirection;
                return This;
            }

            public Builder WithSeamlessMovement(bool seamlessMovement)
            {
                Asset.seamlessMovement = seamlessMovement;
                return This;
            }

            public Builder WithHurtSound(AssetData hurtSound)
            {
                Asset.hurtSound = hurtSound;
                return This;
            }

            public Builder WithDeathSound(AssetData deathSound)
            {
                Asset.deathSound = deathSound;
                return This;
            }

            public Builder WithIdleSound(AssetData idleSound)
            {
                Asset.idleSound = idleSound;
                return This;
            }

            public override Builder AsClone(EnemyAsset asset)
            {
                AsCopy(asset);
                Asset.GenerateID();
                return This;
            }

            public override Builder AsCopy(EnemyAsset asset)
            {
                Asset.id = asset.ID;
                Asset.title = asset.Title;
                Asset.icon = asset.Icon;
                Asset.author = asset.Author;
                Asset.creationDate = asset.CreationDate;
                Asset.color = asset.Color;
                Asset.associatedSpriteID = asset.AssociatedSpriteID;
                Asset.animationType = asset.AnimationType;
                Asset.frameDuration = asset.FrameDuration;
                Asset.iconAlt = asset.IconAlt;
                Asset.baseDamage = asset.BaseDamage;
                Asset.useDelay = asset.UseDelay;
                Asset.knockbackForceSelf = asset.KnockbackForceSelf;
                Asset.knockbackLockDirectionSelf = asset.KnockbackLockDirectionSelf;
                Asset.knockbackForceOther = asset.KnockbackForceOther;
                Asset.knockbackLockDirectionOther = asset.KnockbackLockDirectionOther;
                Asset.maxHealth = asset.MaxHealth;
                Asset.attackProbability = asset.AttackProbability;
                Asset.invincibilityTime = asset.InvincibilityTime;
                Asset.ai = asset.AI;
                Asset.nextStepTime = asset.NextStepTime;
                Asset.startingDirection = asset.StartingDirection;
                Asset.seamlessMovement = asset.SeamlessMovement;
                Asset.hurtSound = new AssetData(asset.HurtSound);
                Asset.deathSound = new AssetData(asset.DeathSound);
                Asset.idleSound = new AssetData(asset.IdleSound);
                Asset.weaponIDs = new List<string>(asset.weaponIDs);
                return This;
            }

            protected sealed override EnemyAsset Asset { get; } = new();
        }
    }
}