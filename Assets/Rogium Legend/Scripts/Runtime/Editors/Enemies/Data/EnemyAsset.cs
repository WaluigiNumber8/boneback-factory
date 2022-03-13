using System;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
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
        
        #region Constructors
        public EnemyAsset()
        {
            title = EditorDefaults.EnemyTitle;
            icon = EditorDefaults.EnemyIcon;
            author = EditorDefaults.Author;
            creationDate = DateTime.Now;

            baseDamage = EditorDefaults.EnemyBaseDamage;
            useDelay = EditorDefaults.EnemyAttackDelay;
            knockbackForceSelf = EditorDefaults.EnemyKnockbackForceSelf;
            knockbackTimeSelf = EditorDefaults.EnemyKnockbackTimeSelf;
            knockbackForceOther = EditorDefaults.EnemyKnockbackForceOther;
            knockbackTimeOther = EditorDefaults.EnemyKnockbackTimeOther;

            maxHealth = EditorDefaults.EnemyMaxHealth;
            invincibilityTime = EditorDefaults.EnemyInvincibilityTime;
            
            GenerateID(EditorAssetIDs.EnemyIdentifier);
        }

        public EnemyAsset(EnemyAsset asset)
        {
            id = asset.ID;
            title = asset.Title;
            icon = asset.Icon;
            author = asset.Author;
            creationDate = asset.CreationDate;

            baseDamage = asset.BaseDamage;
            useDelay = asset.UseDelay;
            knockbackForceSelf = asset.KnockbackForceSelf;
            knockbackTimeSelf = asset.KnockbackTimeSelf;
            knockbackForceOther = asset.KnockbackForceOther;
            knockbackTimeOther = asset.KnockbackTimeOther;

            maxHealth = asset.MaxHealth;
            invincibilityTime = asset.InvincibilityTime;
        }

        public EnemyAsset(string id, string title, Sprite icon, string author, int baseDamage, float useDelay,
                          float knockbackForceSelf, float knockbackTimeSelf, float knockbackForceOther,
                          float knockbackTimeOther, int maxHealth, float invincibilityTime, DateTime creationDate)
        {
            this.id = id;
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;

            this.baseDamage = baseDamage;
            this.useDelay = useDelay;
            this.knockbackForceSelf = knockbackForceSelf;
            this.knockbackTimeSelf = knockbackTimeSelf;
            this.knockbackForceOther = knockbackForceOther;
            this.knockbackTimeOther = knockbackTimeOther;

            this.maxHealth = maxHealth;
            this.invincibilityTime = invincibilityTime;

        }
        #endregion

        #region Update Values
        public void UpdateMaxHealth(int newMaxHealth) => maxHealth = newMaxHealth;
        public void UpdateInvincibilityTime(float newInvincibilityTime) => invincibilityTime = newInvincibilityTime;

        #endregion

        public int MaxHealth { get => maxHealth; }
        public float InvincibilityTime { get => invincibilityTime; }
    }
}