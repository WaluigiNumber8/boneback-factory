using System;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;

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

        private ProjectileAsset() { }
        
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
            icon = EditorDefaults.Instance.ProjectileIcon;
        }
        
        public float FlightSpeed { get => flightSpeed; }
        public float Acceleration { get => acceleration; }
        public float BrakeForce { get => brakeForce; }
        public PierceType PierceType { get => pierceType; }

        public class Builder : EntityAssetBuilder<ProjectileAsset, Builder>
        {
            public Builder()
            {
                Asset.title = EditorDefaults.Instance.ProjectileTitle;
                Asset.icon = EditorDefaults.Instance.ProjectileIcon;
                Asset.author = EditorDefaults.Instance.Author;
                Asset.creationDate = DateTime.Now;
                Asset.GenerateID();
                Asset.color = EditorDefaults.Instance.ProjectileColor;
                
                Asset.animationType = EditorDefaults.Instance.ProjectileAnimationType;
                Asset.frameDuration = EditorDefaults.Instance.ProjectileFrameDuration;
                Asset.iconAlt = EditorDefaults.Instance.EmptySprite;
                
                Asset.baseDamage = EditorDefaults.Instance.ProjectileBaseDamage;
                Asset.useDelay = EditorDefaults.Instance.ProjectileLifetime;
                Asset.knockbackForceSelf = EditorDefaults.Instance.ProjectileKnockbackForceSelf;
                Asset.knockbackLockDirectionSelf = EditorDefaults.Instance.ProjectileKnockbackLockDirectionSelf;
                Asset.knockbackForceOther = EditorDefaults.Instance.ProjectileKnockbackForceOther;
                Asset.knockbackLockDirectionOther = EditorDefaults.Instance.ProjectileKnockbackLockDirectionOther;
                
                Asset.flightSpeed = EditorDefaults.Instance.ProjectileFlightSpeed;
                Asset.acceleration = EditorDefaults.Instance.ProjectileAcceleration;
                Asset.brakeForce = EditorDefaults.Instance.ProjectileBrakeForce;
                Asset.pierceType = EditorDefaults.Instance.ProjectilePierceType;
            }
            
            public Builder WithFlightSpeed(float flightSpeed)
            {
                Asset.flightSpeed = flightSpeed;
                return This;
            }
            
            public Builder WithAcceleration(float acceleration)
            {
                Asset.acceleration = acceleration;
                return This;
            }
            
            public Builder WithBrakeForce(float brakeForce)
            {
                Asset.brakeForce = brakeForce;
                return This;
            }
            
            public Builder WithPierceType(PierceType pierceType)
            {
                Asset.pierceType = pierceType;
                return This;
            }

            public override Builder AsClone(ProjectileAsset asset)
            {
                AsCopy(asset);
                Asset.GenerateID();
                return This;
            }

            public override Builder AsCopy(ProjectileAsset asset)
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
                Asset.flightSpeed = asset.FlightSpeed;
                Asset.acceleration = asset.Acceleration;
                Asset.brakeForce = asset.BrakeForce;
                Asset.pierceType = asset.PierceType;
                return This;
            }

            protected sealed override ProjectileAsset Asset { get; } = new();
        }
    
    }
}