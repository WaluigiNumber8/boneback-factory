using System;
using System.Collections.Generic;
using System.Linq;
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
        private float invincibilityTime;
        private List<string> weaponIDs;

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
            knockbackForceOther = EditorDefaults.EnemyKnockbackForceOther;
            knockbackTimeOther = EditorDefaults.EnemyKnockbackTimeOther;

            maxHealth = EditorDefaults.EnemyMaxHealth;
            invincibilityTime = EditorDefaults.EnemyInvincibilityTime;
            weaponIDs = new List<string>();
            
            GenerateID(EditorAssetIDs.EnemyIdentifier);
        }

        public EnemyAsset(EnemyAsset asset)
        {
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
            knockbackForceOther = asset.KnockbackForceOther;
            knockbackTimeOther = asset.KnockbackTimeOther;

            maxHealth = asset.MaxHealth;
            invincibilityTime = asset.InvincibilityTime;

            weaponIDs = new List<string>(asset.weaponIDs);
        }

        public EnemyAsset(string id, string title, Sprite icon, string author, AnimationType animationType, 
                          int frameDuration, Sprite iconAlt, int baseDamage, float useDelay, float knockbackForceSelf,
                          float knockbackTimeSelf, float knockbackForceOther, float knockbackTimeOther, int maxHealth,
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
            this.knockbackForceOther = knockbackForceOther;
            this.knockbackTimeOther = knockbackTimeOther;

            this.maxHealth = maxHealth;
            this.invincibilityTime = invincibilityTime;

            this.weaponIDs = new List<string>(weaponIDs);
        }
        #endregion

        #region Update Values
        public void UpdateMaxHealth(int newMaxHealth)
        {
            newMaxHealth = Mathf.Clamp(newMaxHealth, 0, AssetValidation.MaxEnemyHealth);
            maxHealth = newMaxHealth;
        }

        public void UpdateInvincibilityTime(float newInvincibilityTime)
        {
            newInvincibilityTime = Mathf.Clamp(newInvincibilityTime, 0, AssetValidation.MaxEnemyInvincibilityTime);
            invincibilityTime = newInvincibilityTime;
        }

        public void UpdateWeaponIDsLength(int newLength)
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(newLength, 0, "New weapon IDs size");
            weaponIDs.Resize(newLength, EditorDefaults.EmptyAssetID);
        }
        public void UpdateWeaponIDPos(int pos, string value) => WeaponIDs[pos] = value;

        #endregion

        public List<string> WeaponIDs { get => weaponIDs; }
        public int MaxHealth { get => maxHealth; }
        public float InvincibilityTime { get => invincibilityTime; }
    }
}