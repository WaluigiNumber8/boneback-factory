using System;
using BoubakProductions.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.Validation;
using UnityEngine;

namespace Rogium.Editors.Projectiles
{
    /// <summary>
    /// Contains all data needed for a projectile.
    /// </summary>
    public class ProjectileAsset : EntityAssetBase
    {

        #region Constructors
        public ProjectileAsset()
        {
            title = EditorDefaults.ProjectileTitle;
            icon = EditorDefaults.ProjectileIcon;
            author = EditorDefaults.Author;
            creationDate = DateTime.Now;

            animationType = EditorDefaults.ProjectileAnimationType;
            frameDuration = EditorDefaults.ProjectileFrameDuration;
            iconAlt = EditorDefaults.ProjectileIconAlt;
            
            baseDamage = EditorDefaults.ProjectileBaseDamage;
            useDelay = EditorDefaults.ProjectileLifetime;
            knockbackForceSelf = EditorDefaults.ProjectileKnockbackForceSelf;
            knockbackTimeSelf = EditorDefaults.ProjectileKnockbackTimeSelf;
            knockbackLockDirectionSelf = EditorDefaults.ProjectileKnockbackLockDirectionSelf;
            knockbackForceOther = EditorDefaults.ProjectileKnockbackForceOther;
            knockbackTimeOther = EditorDefaults.ProjectileKnockbackTimeOther;
            knockbackLockDirectionOther = EditorDefaults.ProjectileKnockbackLockDirectionOther;
            
            GenerateID(EditorAssetIDs.ProjectileIdentifier);
        }

        public ProjectileAsset(ProjectileAsset asset)
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
        }

        public ProjectileAsset(string id, string title, Sprite icon, string author, AnimationType animationType, 
                               int frameDuration, Sprite iconAlt, int baseDamage, float useDelay, float knockbackForceSelf, 
                               float knockbackTimeSelf, bool knockbackLockDirectionSelf, float knockbackForceOther,
                               float knockbackTimeOther, bool knockbackLockDirectionOther, DateTime creationDate)
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

        }
        #endregion

        #region Update Values



        #endregion

    }
}