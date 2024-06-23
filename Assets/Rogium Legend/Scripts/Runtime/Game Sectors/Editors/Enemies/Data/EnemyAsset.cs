using System;
using System.Collections.Generic;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.Validation;
using UnityEngine;

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
        private readonly List<string> weaponIDs;
        
        private AIType ai;
        private DirectionType startingDirection;
        private bool seamlessMovement;
        private float nextStepTime;

        private AssetData hurtSound;
        private AssetData deathSound;
        private AssetData idleSound;

        #region Constructors
        public EnemyAsset()
        {
            InitBase(EditorDefaults.Instance.EnemyTitle, EditorDefaults.Instance.EnemyIcon, EditorDefaults.Instance.Author, DateTime.Now);
            color = EditorDefaults.Instance.EnemyColor;

            animationType = EditorDefaults.Instance.EnemyAnimationType;
            frameDuration = EditorDefaults.Instance.EnemyFrameDuration;
            iconAlt = EditorDefaults.Instance.EmptySprite;
            
            baseDamage = EditorDefaults.Instance.EnemyBaseDamage;
            useDelay = EditorDefaults.Instance.EnemyAttackDelay;
            knockbackForceSelf = EditorDefaults.Instance.EnemyKnockbackForceSelf;
            knockbackLockDirectionSelf = EditorDefaults.Instance.EnemyKnockbackLockDirectionSelf;
            knockbackForceOther = EditorDefaults.Instance.EnemyKnockbackForceOther;
            knockbackLockDirectionOther = EditorDefaults.Instance.EnemyKnockbackLockDirectionOther;

            maxHealth = EditorDefaults.Instance.EnemyMaxHealth;
            attackProbability = EditorDefaults.Instance.EnemyAttackProbability;
            invincibilityTime = EditorDefaults.Instance.EnemyInvincibilityTime;
            weaponIDs = new List<string>();
            
            ai = EditorDefaults.Instance.EnemyAI;
            nextStepTime = EditorDefaults.Instance.EnemyNextStepTime;
            seamlessMovement = EditorDefaults.Instance.EnemySeamlessMovement;
            startingDirection = EditorDefaults.Instance.EnemyStartingDirection;
            
            hurtSound = new AssetData(ParameterInfoConstants.ForSound);
            deathSound = new AssetData(ParameterInfoConstants.ForSound);
            idleSound = new AssetData(ParameterInfoConstants.ForSound);
            
            GenerateID(EditorAssetIDs.EnemyIdentifier);
        }

        public EnemyAsset(EnemyAsset asset)
        {
            AssetValidation.ValidateTitle(asset.title);
            
            id = asset.ID;
            InitBase(asset.Title, asset.Icon, asset.Author, asset.CreationDate);

            color = asset.Color;
            associatedSpriteID = asset.AssociatedSpriteID;
            
            animationType = asset.AnimationType;
            frameDuration = asset.FrameDuration;
            iconAlt = asset.IconAlt;
            
            baseDamage = asset.BaseDamage;
            useDelay = asset.UseDelay;
            knockbackForceSelf = asset.KnockbackForceSelf;
            knockbackLockDirectionSelf = asset.KnockbackLockDirectionSelf;
            knockbackForceOther = asset.KnockbackForceOther;
            knockbackLockDirectionOther = asset.KnockbackLockDirectionOther;

            maxHealth = asset.MaxHealth;
            attackProbability = asset.AttackProbability;
            invincibilityTime = asset.InvincibilityTime;
            
            ai = asset.AI;
            nextStepTime = asset.NextStepTime;
            startingDirection = asset.StartingDirection;
            seamlessMovement = asset.SeamlessMovement;
            
            hurtSound = new AssetData(asset.HurtSound);
            deathSound = new AssetData(asset.DeathSound);
            idleSound = new AssetData(asset.IdleSound);
            
            weaponIDs = new List<string>(asset.weaponIDs);
        }

        public EnemyAsset(string id, string title, Sprite icon, string author, Color color, string associatedSpriteID, 
                          AnimationType animationType, int frameDuration, Sprite iconAlt, int baseDamage, float useDelay, 
                          float knockbackForceSelf, bool knockbackLockDirectionSelf, float knockbackForceOther, 
                          bool knockbackLockDirectionOther, int maxHealth, float attackProbability, float invincibilityTime, 
                          IList<string> weaponIDs, AIType ai, float nextStepTime, DirectionType startingDirection, 
                          bool seamlessMovement, AssetData hurtSound, AssetData deathSound, AssetData idleSound, 
                          DateTime creationDate)
        {
            AssetValidation.ValidateTitle(title);
            
            this.id = id;
            InitBase(title, icon, author, creationDate);
            this.color = color;

            this.associatedSpriteID = associatedSpriteID;

            this.animationType = animationType;
            this.frameDuration = frameDuration;
            this.iconAlt = iconAlt;
            
            this.baseDamage = baseDamage;
            this.useDelay = useDelay;
            this.knockbackForceSelf = knockbackForceSelf;
            this.knockbackLockDirectionSelf = knockbackLockDirectionSelf;
            this.knockbackForceOther = knockbackForceOther;
            this.knockbackLockDirectionOther = knockbackLockDirectionOther;

            this.maxHealth = maxHealth;
            this.attackProbability = attackProbability;
            this.invincibilityTime = invincibilityTime;
            this.weaponIDs = new List<string>(weaponIDs);
            
            this.ai = ai;
            this.nextStepTime = nextStepTime;
            this.startingDirection = startingDirection;
            this.seamlessMovement = seamlessMovement;
            
            this.hurtSound = new AssetData(hurtSound);
            this.deathSound = new AssetData(deathSound);
            this.idleSound = new AssetData(idleSound);
        }
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
        public void UpdateAI(int newAI) => UpdateAI((AIType)newAI);
        public void UpdateAI(AIType newAI) => ai = newAI;
        public void UpdateNextStepTime(float newNextStepTime) => nextStepTime = newNextStepTime;
        public void UpdateStartingDirection(int newDirectionType) => UpdateStartingDirection((DirectionType)newDirectionType);
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
        
        public List<string> WeaponIDs { get => weaponIDs; }
        public int MaxHealth { get => maxHealth; }
        public float AttackProbability { get => attackProbability; }
        public float InvincibilityTime { get => invincibilityTime; }
        
        public AIType AI { get => ai; }
        public float NextStepTime { get => nextStepTime; }
        public DirectionType StartingDirection { get => startingDirection; }
        public bool SeamlessMovement { get => seamlessMovement; }
        
        public AssetData HurtSound { get => hurtSound; }
        public AssetData DeathSound { get => deathSound; }
        public AssetData IdleSound { get => idleSound; }
    }
}