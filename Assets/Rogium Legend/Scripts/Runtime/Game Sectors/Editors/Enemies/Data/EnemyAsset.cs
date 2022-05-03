using System;
using System.Collections.Generic;
using BoubakProductions.Core;
using BoubakProductions.Safety;
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

        #region Constructors
        public EnemyAsset()
        {
            title = EditorDefaults.EnemyTitle;
            icon = EditorDefaults.EnemyIcon;
            author = EditorDefaults.Author;
            creationDate = DateTime.Now;

            animationType = EditorDefaults.EnemyAnimationType;
            frameDuration = EditorDefaults.EnemyFrameDuration;
            iconAlt = EditorDefaults.EnemyIconAlt;
            
            baseDamage = EditorDefaults.EnemyBaseDamage;
            useDelay = EditorDefaults.EnemyAttackDelay;
            knockbackForceSelf = EditorDefaults.EnemyKnockbackForceSelf;
            knockbackTimeSelf = EditorDefaults.EnemyKnockbackTimeSelf;
            knockbackLockDirectionSelf = EditorDefaults.EnemyKnockbackLockDirectionSelf;
            knockbackForceOther = EditorDefaults.EnemyKnockbackForceOther;
            knockbackTimeOther = EditorDefaults.EnemyKnockbackTimeOther;
            knockbackLockDirectionOther = EditorDefaults.EnemyKnockbackLockDirectionOther;

            maxHealth = EditorDefaults.EnemyMaxHealth;
            attackProbability = EditorDefaults.EnemyAttackProbability;
            invincibilityTime = EditorDefaults.EnemyInvincibilityTime;
            weaponIDs = new List<string>();
            
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

            animationType = asset.AnimationType;
            frameDuration = asset.FrameDuration;
            iconAlt = asset.IconAlt;
            
            baseDamage = asset.BaseDamage;
            useDelay = asset.UseDelay;
            knockbackForceSelf = asset.KnockbackForceSelf;
            knockbackTimeSelf = asset.KnockbackTimeSelf;
            knockbackLockDirectionSelf = asset.knockbackLockDirectionSelf;
            knockbackForceOther = asset.KnockbackForceOther;
            knockbackTimeOther = asset.KnockbackTimeOther;
            knockbackLockDirectionOther = asset.knockbackLockDirectionOther;

            maxHealth = asset.MaxHealth;
            attackProbability = asset.AttackProbability;
            invincibilityTime = asset.InvincibilityTime;

            weaponIDs = new List<string>(asset.weaponIDs);
        }

        public EnemyAsset(string id, string title, Sprite icon, string author, AnimationType animationType, 
                          int frameDuration, Sprite iconAlt, int baseDamage, float useDelay, float knockbackForceSelf,
                          float knockbackTimeSelf, bool knockbackLockDirectionSelf, float knockbackForceOther, 
                          float knockbackTimeOther, bool knockbackLockDirectionOther, int maxHealth, float attackProbability, 
                          float invincibilityTime, List<string> weaponIDs, DateTime creationDate)
        {
            AssetValidation.ValidateTitle(title);
            
            this.id = id;
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;

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

        #endregion

        public List<string> WeaponIDs { get => weaponIDs; }
        public int MaxHealth { get => maxHealth; }
        public float AttackProbability { get => attackProbability; }
        public float InvincibilityTime { get => invincibilityTime; }
    }
}