using System;
using System.Collections.Generic;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Objects;
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

        #region Constructors
        public EnemyAsset()
        {
            title = EditorConstants.EnemyTitle;
            icon = EditorConstants.EnemyIcon;
            author = EditorConstants.Author;
            creationDate = DateTime.Now;

            animationType = EditorConstants.EnemyAnimationType;
            frameDuration = EditorConstants.EnemyFrameDuration;
            iconAlt = EditorConstants.EnemyIconAlt;
            
            baseDamage = EditorConstants.EnemyBaseDamage;
            useDelay = EditorConstants.EnemyAttackDelay;
            knockbackForceSelf = EditorConstants.EnemyKnockbackForceSelf;
            knockbackTimeSelf = EditorConstants.EnemyKnockbackTimeSelf;
            knockbackLockDirectionSelf = EditorConstants.EnemyKnockbackLockDirectionSelf;
            knockbackForceOther = EditorConstants.EnemyKnockbackForceOther;
            knockbackTimeOther = EditorConstants.EnemyKnockbackTimeOther;
            knockbackLockDirectionOther = EditorConstants.EnemyKnockbackLockDirectionOther;

            maxHealth = EditorConstants.EnemyMaxHealth;
            attackProbability = EditorConstants.EnemyAttackProbability;
            invincibilityTime = EditorConstants.EnemyInvincibilityTime;
            weaponIDs = new List<string>();
            
            ai = EditorConstants.EnemyAI;
            nextStepTime = EditorConstants.EnemyNextStepTime;
            seamlessMovement = EditorConstants.EnemySeamlessMovement;
            startingDirection = EditorConstants.EnemyStartingDirection;
            
            GenerateID(EditorAssetIDs.EnemyIdentifier);
        }

        public EnemyAsset(EnemyAsset asset)
        {
            AssetValidation.ValidateTitle(asset.title);
            
            id = asset.ID;
            title = asset.Title;
            icon = asset.Icon;
            author = asset.Author;
            creationDate = asset.CreationDate;

            associatedSpriteID = asset.AssociatedSpriteID;
            
            animationType = asset.AnimationType;
            frameDuration = asset.FrameDuration;
            iconAlt = asset.IconAlt;
            
            baseDamage = asset.BaseDamage;
            useDelay = asset.UseDelay;
            knockbackForceSelf = asset.KnockbackForceSelf;
            knockbackTimeSelf = asset.KnockbackTimeSelf;
            knockbackLockDirectionSelf = asset.KnockbackLockDirectionSelf;
            knockbackForceOther = asset.KnockbackForceOther;
            knockbackTimeOther = asset.KnockbackTimeOther;
            knockbackLockDirectionOther = asset.KnockbackLockDirectionOther;

            maxHealth = asset.MaxHealth;
            attackProbability = asset.AttackProbability;
            invincibilityTime = asset.InvincibilityTime;
            
            ai = asset.AI;
            nextStepTime = asset.NextStepTime;
            startingDirection = asset.StartingDirection;
            seamlessMovement = asset.SeamlessMovement;
            
            weaponIDs = new List<string>(asset.weaponIDs);
        }

        public EnemyAsset(string id, string title, Sprite icon, string author, string associatedSpriteID, AnimationType animationType, 
                          int frameDuration, Sprite iconAlt, int baseDamage, float useDelay, float knockbackForceSelf,
                          float knockbackTimeSelf, bool knockbackLockDirectionSelf, float knockbackForceOther, 
                          float knockbackTimeOther, bool knockbackLockDirectionOther, int maxHealth, float attackProbability, 
                          float invincibilityTime, IList<string> weaponIDs, AIType ai, float nextStepTime, 
                          DirectionType startingDirection,bool seamlessMovement, DateTime creationDate)
        {
            AssetValidation.ValidateTitle(title);
            
            this.id = id;
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;

            this.associatedSpriteID = associatedSpriteID;

            this.animationType = animationType;
            this.frameDuration = frameDuration;
            this.iconAlt = iconAlt;
            
            this.baseDamage = baseDamage;
            this.useDelay = useDelay;
            this.knockbackForceSelf = knockbackForceSelf;
            this.knockbackTimeSelf = knockbackTimeSelf;
            this.knockbackLockDirectionSelf = knockbackLockDirectionSelf;
            this.knockbackForceOther = knockbackForceOther;
            this.knockbackTimeOther = knockbackTimeOther;
            this.knockbackLockDirectionOther = knockbackLockDirectionOther;

            this.maxHealth = maxHealth;
            this.attackProbability = attackProbability;
            this.invincibilityTime = invincibilityTime;
            this.weaponIDs = new List<string>(weaponIDs);
            
            this.ai = ai;
            this.nextStepTime = nextStepTime;
            this.startingDirection = startingDirection;
            this.seamlessMovement = seamlessMovement;
        }
        #endregion

        #region Update Values
        public void UpdateMaxHealth(int newMaxHealth) => maxHealth = newMaxHealth;
        public void UpdateAttackProbability(float newAttackProbability) => attackProbability = newAttackProbability;
        public void UpdateInvincibilityTime(float newInvincibilityTime) => invincibilityTime = newInvincibilityTime;
        public void UpdateWeaponIDsLength(int newLength)
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(newLength, 0, "New weapon IDs size");
            weaponIDs.Resize(newLength, EditorConstants.EmptyAssetID);
        }
        public void UpdateWeaponIDPos(int pos, string value) => weaponIDs[pos] = value;
        public void UpdateAI(int newAI) => UpdateAI((AIType)newAI);
        public void UpdateAI(AIType newAI) => ai = newAI;
        public void UpdateNextStepTime(float newNextStepTime) => nextStepTime = newNextStepTime;
        public void UpdateStartingDirection(int newDirectionType) => UpdateStartingDirection((DirectionType)newDirectionType);
        public void UpdateStartingDirection(DirectionType newStartingDirection) => startingDirection = newStartingDirection;
        public void UpdateSeamlessMovement(bool newSeamlessMovementState) => seamlessMovement = newSeamlessMovementState;
        #endregion

        public override void ClearAssociatedSprite()
        {
            base.ClearAssociatedSprite();
            icon = EditorConstants.EnemyIcon;
        }
        
        public List<string> WeaponIDs { get => weaponIDs; }
        public int MaxHealth { get => maxHealth; }
        public float AttackProbability { get => attackProbability; }
        public float InvincibilityTime { get => invincibilityTime; }
        
        public AIType AI { get => ai; }
        public float NextStepTime { get => nextStepTime; }
        public DirectionType StartingDirection { get => startingDirection; }
        public bool SeamlessMovement { get => seamlessMovement; }
    }
}