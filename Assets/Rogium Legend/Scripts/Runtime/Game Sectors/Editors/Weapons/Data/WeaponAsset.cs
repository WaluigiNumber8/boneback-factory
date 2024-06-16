using System;
using System.Collections.Generic;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.Validation;
using UnityEngine;

namespace Rogium.Editors.Weapons
{
    /// <summary>
    /// Contains all data needed for a weapon.
    /// </summary>
    public class WeaponAsset : EntityAssetBase
    {
        private WeaponUseType useType;
        private float useStartDelay;
        private float useDuration;
        private bool freezeUser;
        private bool isEvasive;
        private readonly List<ProjectileDataInfo> projectileIDs;
        
        private AssetData useSound;

        #region Constructors
        public WeaponAsset()
        {
            InitBase(EditorConstants.WeaponTitle, EditorConstants.WeaponIcon, EditorConstants.Author, DateTime.Now);
            GenerateID(EditorAssetIDs.WeaponIdentifier);
            color = EditorConstants.WeaponColor;

            animationType = EditorConstants.WeaponAnimationType;
            frameDuration = EditorConstants.WeaponFrameDuration;
            iconAlt = EditorConstants.WeaponIconAlt;
            
            baseDamage = EditorConstants.WeaponBaseDamage;
            useDelay = EditorConstants.WeaponUseDelay;
            knockbackForceSelf = EditorConstants.WeaponKnockbackForceSelf;
            knockbackLockDirectionSelf = EditorConstants.WeaponKnockbackLockDirectionSelf;
            knockbackForceOther = EditorConstants.WeaponKnockbackForceOther;
            knockbackLockDirectionOther = EditorConstants.WeaponKnockbackLockDirectionOther;

            useType = EditorConstants.WeaponUseType;
            useDuration = EditorConstants.WeaponUseDuration;
            useStartDelay = EditorConstants.WeaponUseStartDelay;
            isEvasive = EditorConstants.WeaponIsEvasive;
            freezeUser = EditorConstants.WeaponFreezeUser;
            
            useSound = new AssetData(ParameterInfoConstants.ForSound);
            
            projectileIDs = new List<ProjectileDataInfo>();
        }
        public WeaponAsset(string title, Sprite icon)
        {
            InitBase(title, icon, EditorConstants.Author, DateTime.Now);
            GenerateID(EditorAssetIDs.WeaponIdentifier);
            color = EditorConstants.WeaponColor;

            animationType = EditorConstants.WeaponAnimationType;
            frameDuration = EditorConstants.WeaponFrameDuration;
            iconAlt = EditorConstants.WeaponIconAlt;
            
            baseDamage = EditorConstants.WeaponBaseDamage;
            useDelay = EditorConstants.WeaponUseDelay;
            knockbackForceSelf = EditorConstants.WeaponKnockbackForceSelf;
            knockbackLockDirectionSelf = EditorConstants.WeaponKnockbackLockDirectionSelf;
            knockbackForceOther = EditorConstants.WeaponKnockbackForceOther;
            knockbackLockDirectionOther = EditorConstants.WeaponKnockbackLockDirectionOther;

            useType = EditorConstants.WeaponUseType;
            useDuration = EditorConstants.WeaponUseDuration;
            useStartDelay = EditorConstants.WeaponUseStartDelay;
            isEvasive = EditorConstants.WeaponIsEvasive;
            freezeUser = EditorConstants.WeaponFreezeUser;
            
            useSound = new AssetData(ParameterInfoConstants.ForSound);
            
            projectileIDs = new List<ProjectileDataInfo>();
        }

        public WeaponAsset(WeaponAsset asset)
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

            useType = asset.UseType;
            useDuration = asset.UseDuration;
            useStartDelay = asset.useStartDelay;
            isEvasive = asset.IsEvasive;
            freezeUser = asset.FreezeUser;
            
            useSound = new AssetData(asset.UseSound);
            
            projectileIDs = new List<ProjectileDataInfo>(asset.ProjectileIDs);
        }

        public WeaponAsset(string id, string title, Sprite icon, string author, Color color, string associatedSpriteID, 
                           AnimationType animationType, int frameDuration, Sprite iconAlt,int baseDamage, float useDelay, 
                           float knockbackForceSelf, bool knockbackLockDirectionSelf, float knockbackForceOther, 
                           bool knockbackLockDirectionOther, WeaponUseType useType, float useDuration, float useStartDelay, 
                           bool isEvasive, bool freezeUser, IList<ProjectileDataInfo> projectileIDs, AssetData useSound,
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

            this.useType = useType;
            this.useDuration = useDuration;
            this.useStartDelay = useStartDelay;
            this.isEvasive = isEvasive;
            this.freezeUser = freezeUser;
            
            this.useSound = new AssetData(useSound);
            
            this.projectileIDs = new List<ProjectileDataInfo>(projectileIDs);

        }
        #endregion

        #region Update Values
        public void UpdateUseType(WeaponUseType newUseType) => useType = newUseType;
        public void UpdateUseType(int newUseType) => useType = (WeaponUseType) newUseType;
        public void UpdateUseDuration(float newUseDuration) => useDuration = newUseDuration;
        public void UpdateUseStartDelay(float newUseStartDelay) => useStartDelay = newUseStartDelay;
        public void UpdateIsEvasive(bool newIsEvasive) => isEvasive = newIsEvasive;
        public void UpdateFreezeUser(bool newFreezeUser) => freezeUser = newFreezeUser;
        public void UpdateProjectileIDsLength(int newLength)
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(newLength, 0, "New weapon IDs size");

            ProjectileDataInfo data = new(EditorConstants.EmptyAssetID, 
                                          EditorConstants.WeaponProjectileSpawnDelay,
                                          EditorConstants.WeaponProjectileAngleOffset);
            
            projectileIDs.Resize(newLength, data);
        }
        public void UpdateProjectileIDsPosID(int pos, string value)
        {
            projectileIDs[pos].UpdateID(value);
        }

        public void UpdateProjectileIDsPosSpawnDelay(int pos, float value)
        {
            projectileIDs[pos].UpdateSpawnDelay(value);
        }

        public void UpdateProjectileIDsPosAngleOffset(int pos, int value)
        {
            projectileIDs[pos].UpdateAngleOffset(value);
        }
        public void UpdateUseSound(AssetData newUseSound) => useSound = new AssetData(newUseSound);
        #endregion

        public override void ClearAssociatedSprite()
        {
            base.ClearAssociatedSprite();
            icon = EditorConstants.WeaponIcon;
        }
        
        public WeaponUseType UseType { get => useType; }
        public float UseDuration { get => useDuration; }
        public float UseStartDelay { get => useStartDelay; }
        public bool IsEvasive { get => isEvasive; }
        public bool FreezeUser { get => freezeUser; }
        public List<ProjectileDataInfo> ProjectileIDs { get => projectileIDs; }
        public AssetData UseSound { get => useSound; }
    }
}