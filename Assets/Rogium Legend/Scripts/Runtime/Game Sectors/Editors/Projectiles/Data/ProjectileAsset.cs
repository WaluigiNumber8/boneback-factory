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
        private float flightSpeed;
        private float acceleration;
        private float brakeForce;
        private PierceType pierceType;
        
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

            flightSpeed = EditorDefaults.ProjectileFlightSpeed;
            acceleration = EditorDefaults.ProjectileAcceleration;
            brakeForce = EditorDefaults.ProjectileBrakeForce;
            pierceType = EditorDefaults.ProjectilePierceType;
            
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
            knockbackLockDirectionSelf = asset.KnockbackLockDirectionSelf;
            knockbackForceOther = asset.KnockbackForceOther;
            knockbackTimeOther = asset.KnockbackTimeOther;
            knockbackLockDirectionOther = asset.KnockbackLockDirectionOther;

            flightSpeed = asset.FlightSpeed;
            acceleration = asset.Acceleration;
            brakeForce = asset.BrakeForce;
            pierceType = asset.PierceType;
        }

        public ProjectileAsset(string id, string title, Sprite icon, string author, AnimationType animationType, 
                               int frameDuration, Sprite iconAlt, int baseDamage, float useDelay, float knockbackForceSelf, 
                               float knockbackTimeSelf, bool knockbackLockDirectionSelf, float knockbackForceOther,
                               float knockbackTimeOther, bool knockbackLockDirectionOther, float flightSpeed, float acceleration,
                               float brakeForce, PierceType pierceType, DateTime creationDate)
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

            this.flightSpeed = flightSpeed;
            this.acceleration = acceleration;
            this.brakeForce = brakeForce;
            this.pierceType = pierceType;
        }
        #endregion

        #region Update Values

        public void UpdateFlightSpeed(float newFlightSpeed) => flightSpeed = newFlightSpeed;
        public void UpdateAcceleration(float newAcceleration) => acceleration = newAcceleration;
        public void UpdateBrakeForce(float newBrakeForce) => brakeForce = newBrakeForce;
        public void UpdatePierceType(int newPierceType) => UpdatePierceType((PierceType)newPierceType);
        public void UpdatePierceType(PierceType newPierceType) => pierceType = newPierceType;

        #endregion
        
        public float FlightSpeed { get => flightSpeed; }
        public float Acceleration { get => acceleration; }
        public float BrakeForce { get => brakeForce; }
        public PierceType PierceType { get => pierceType; }

    }
}