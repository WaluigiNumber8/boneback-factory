using System;
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
            InitBase(EditorConstants.ProjectileTitle, EditorSpriteConstants.Instance.ProjectileIcon, EditorConstants.Author, DateTime.Now);
            GenerateID(EditorAssetIDs.ProjectileIdentifier);
            color = EditorConstants.ProjectileColor;

            animationType = EditorConstants.ProjectileAnimationType;
            frameDuration = EditorConstants.ProjectileFrameDuration;
            iconAlt = EditorSpriteConstants.Instance.EmptySprite;
            
            baseDamage = EditorConstants.ProjectileBaseDamage;
            useDelay = EditorConstants.ProjectileLifetime;
            knockbackForceSelf = EditorConstants.ProjectileKnockbackForceSelf;
            knockbackLockDirectionSelf = EditorConstants.ProjectileKnockbackLockDirectionSelf;
            knockbackForceOther = EditorConstants.ProjectileKnockbackForceOther;
            knockbackLockDirectionOther = EditorConstants.ProjectileKnockbackLockDirectionOther;

            flightSpeed = EditorConstants.ProjectileFlightSpeed;
            acceleration = EditorConstants.ProjectileAcceleration;
            brakeForce = EditorConstants.ProjectileBrakeForce;
            pierceType = EditorConstants.ProjectilePierceType;
            
        }
        public ProjectileAsset(string title, Sprite icon)
        {
            InitBase(title, icon, EditorConstants.Author, DateTime.Now);
            GenerateID(EditorAssetIDs.ProjectileIdentifier);
            color = EditorConstants.ProjectileColor;

            animationType = EditorConstants.ProjectileAnimationType;
            frameDuration = EditorConstants.ProjectileFrameDuration;
            iconAlt = EditorSpriteConstants.Instance.EmptySprite;
            
            baseDamage = EditorConstants.ProjectileBaseDamage;
            useDelay = EditorConstants.ProjectileLifetime;
            knockbackForceSelf = EditorConstants.ProjectileKnockbackForceSelf;
            knockbackLockDirectionSelf = EditorConstants.ProjectileKnockbackLockDirectionSelf;
            knockbackForceOther = EditorConstants.ProjectileKnockbackForceOther;
            knockbackLockDirectionOther = EditorConstants.ProjectileKnockbackLockDirectionOther;

            flightSpeed = EditorConstants.ProjectileFlightSpeed;
            acceleration = EditorConstants.ProjectileAcceleration;
            brakeForce = EditorConstants.ProjectileBrakeForce;
            pierceType = EditorConstants.ProjectilePierceType;
            
        }
        public ProjectileAsset(ProjectileAsset asset)
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

            flightSpeed = asset.FlightSpeed;
            acceleration = asset.Acceleration;
            brakeForce = asset.BrakeForce;
            pierceType = asset.PierceType;
        }

        public ProjectileAsset(string id, string title, Sprite icon, string author, Color color, string associatedSpriteID, 
                               AnimationType animationType, int frameDuration, Sprite iconAlt, int baseDamage, float useDelay, 
                               float knockbackForceSelf, bool knockbackLockDirectionSelf, float knockbackForceOther,
                               bool knockbackLockDirectionOther, float flightSpeed, float acceleration, float brakeForce, 
                               PierceType pierceType, DateTime creationDate)
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
        
        public override void ClearAssociatedSprite()
        {
            base.ClearAssociatedSprite();
            icon = EditorSpriteConstants.Instance.ProjectileIcon;
        }
        
        public float FlightSpeed { get => flightSpeed; }
        public float Acceleration { get => acceleration; }
        public float BrakeForce { get => brakeForce; }
        public PierceType PierceType { get => pierceType; }

    }
}