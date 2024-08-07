using System;
using System.Collections.Generic;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.Validation;

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
        private List<ProjectileDataInfo> projectileIDs;
        
        private AssetData useSound;

        #region Constructors
        public WeaponAsset()
        {
            InitBase(EditorDefaults.Instance.WeaponTitle, EditorDefaults.Instance.WeaponIcon, EditorDefaults.Instance.Author, DateTime.Now);
            GenerateID(EditorAssetIDs.WeaponIdentifier);
            color = EditorDefaults.Instance.WeaponColor;

            animationType = EditorDefaults.Instance.WeaponAnimationType;
            frameDuration = EditorDefaults.Instance.WeaponFrameDuration;
            iconAlt = EditorDefaults.Instance.EmptySprite;
            
            baseDamage = EditorDefaults.Instance.WeaponBaseDamage;
            useDelay = EditorDefaults.Instance.WeaponUseDelay;
            knockbackForceSelf = EditorDefaults.Instance.WeaponKnockbackForceSelf;
            knockbackLockDirectionSelf = EditorDefaults.Instance.WeaponKnockbackLockDirectionSelf;
            knockbackForceOther = EditorDefaults.Instance.WeaponKnockbackForceOther;
            knockbackLockDirectionOther = EditorDefaults.Instance.WeaponKnockbackLockDirectionOther;

            useType = EditorDefaults.Instance.WeaponUseType;
            useDuration = EditorDefaults.Instance.WeaponUseDuration;
            useStartDelay = EditorDefaults.Instance.WeaponUseStartDelay;
            isEvasive = EditorDefaults.Instance.WeaponIsEvasive;
            freezeUser = EditorDefaults.Instance.WeaponFreezeUser;
            
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

            ProjectileDataInfo data = new(EditorDefaults.EmptyAssetID, 
                                          EditorDefaults.Instance.WeaponProjectileSpawnDelay,
                                          EditorDefaults.Instance.WeaponProjectileAngleOffset);
            
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
            icon = EditorDefaults.Instance.WeaponIcon;
        }
        
        public WeaponUseType UseType { get => useType; }
        public float UseDuration { get => useDuration; }
        public float UseStartDelay { get => useStartDelay; }
        public bool IsEvasive { get => isEvasive; }
        public bool FreezeUser { get => freezeUser; }
        public List<ProjectileDataInfo> ProjectileIDs { get => projectileIDs; }
        public AssetData UseSound { get => useSound; }

        public class WeaponBuilder : EntityAssetBuilder<WeaponAsset, WeaponBuilder>
        {
            public WeaponBuilder()
            {
                Asset.GenerateID(EditorAssetIDs.WeaponIdentifier);
                Asset.title = EditorDefaults.Instance.WeaponTitle;
                Asset.icon = EditorDefaults.Instance.WeaponIcon;
                Asset.author = EditorDefaults.Instance.Author;
                Asset.creationDate = DateTime.Now;
                Asset.color = EditorDefaults.Instance.WeaponColor;

                Asset.animationType = EditorDefaults.Instance.WeaponAnimationType;
                Asset.frameDuration = EditorDefaults.Instance.WeaponFrameDuration;
                Asset.iconAlt = EditorDefaults.Instance.EmptySprite;
            
                Asset.baseDamage = EditorDefaults.Instance.WeaponBaseDamage;
                Asset.useDelay = EditorDefaults.Instance.WeaponUseDelay;
                Asset.knockbackForceSelf = EditorDefaults.Instance.WeaponKnockbackForceSelf;
                Asset.knockbackLockDirectionSelf = EditorDefaults.Instance.WeaponKnockbackLockDirectionSelf;
                Asset.knockbackForceOther = EditorDefaults.Instance.WeaponKnockbackForceOther;
                Asset.knockbackLockDirectionOther = EditorDefaults.Instance.WeaponKnockbackLockDirectionOther;

                Asset.useType = EditorDefaults.Instance.WeaponUseType;
                Asset.useDuration = EditorDefaults.Instance.WeaponUseDuration;
                Asset.useStartDelay = EditorDefaults.Instance.WeaponUseStartDelay;
                Asset.isEvasive = EditorDefaults.Instance.WeaponIsEvasive;
                Asset.freezeUser = EditorDefaults.Instance.WeaponFreezeUser;
            
                Asset.useSound = new AssetData(ParameterInfoConstants.ForSound);
            
                Asset.projectileIDs = new List<ProjectileDataInfo>();
            }
            
            public WeaponBuilder WithUseType(WeaponUseType useType)
            {
                Asset.useType = useType;
                return this;
            }
            
            public WeaponBuilder WithUseDuration(float useDuration)
            {
                Asset.useDuration = useDuration;
                return this;
            }
            
            public WeaponBuilder WithUseStartDelay(float useStartDelay)
            {
                Asset.useStartDelay = useStartDelay;
                return this;
            }
            
            public WeaponBuilder WithIsEvasive(bool isEvasive)
            {
                Asset.isEvasive = isEvasive;
                return this;
            }
            
            public WeaponBuilder WithFreezeUser(bool freezeUser)
            {
                Asset.freezeUser = freezeUser;
                return this;
            }
            
            public WeaponBuilder WithProjectileIDs(IList<ProjectileDataInfo> projectileIDs)
            {
                Asset.projectileIDs.Clear();
                Asset.projectileIDs.AddRange(projectileIDs);
                return this;
            }
            
            public WeaponBuilder WithUseSound(AssetData useSound)
            {
                Asset.useSound = new AssetData(useSound);
                return this;
            }
            
            public override WeaponAsset Build() => Asset;
            protected sealed override WeaponAsset Asset { get; } = new();
        }
    }
}