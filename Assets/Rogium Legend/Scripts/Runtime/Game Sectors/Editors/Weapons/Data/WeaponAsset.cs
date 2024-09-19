using System;
using System.Collections.Generic;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;

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

        private WeaponAsset() { }

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
        public void UpdateProjectileIDsPosID(int pos, string value) => projectileIDs[pos].UpdateID(value);

        public void UpdateProjectileIDsPosSpawnDelay(int pos, float value) => projectileIDs[pos].UpdateSpawnDelay(value);

        public void UpdateProjectileIDsPosAngleOffset(int pos, int value) => projectileIDs[pos].UpdateAngleOffset(value);
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

        public class Builder : EntityAssetBuilder<WeaponAsset, Builder>
        {
            public Builder()
            {
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
                Asset.GenerateID();
            }
            
            public override Builder AsClone(WeaponAsset asset)
            {
                AsCopy(asset);
                Asset.GenerateID();
                return This;
            }

            public override Builder AsCopy(WeaponAsset asset)
            {
                Asset.id = asset.ID;
                Asset.InitBase(asset.Title, asset.Icon, asset.Author, asset.CreationDate);
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
                Asset.useType = asset.UseType;
                Asset.useDuration = asset.UseDuration;
                Asset.useStartDelay = asset.useStartDelay;
                Asset.isEvasive = asset.IsEvasive;
                Asset.freezeUser = asset.FreezeUser;
                Asset.useSound = new AssetData(asset.UseSound);
                Asset.projectileIDs = new List<ProjectileDataInfo>(asset.ProjectileIDs);
                return This;
            }

            public Builder WithUseType(WeaponUseType useType)
            {
                Asset.useType = useType;
                return this;
            }
            
            public Builder WithUseDuration(float useDuration)
            {
                Asset.useDuration = useDuration;
                return this;
            }
            
            public Builder WithUseStartDelay(float useStartDelay)
            {
                Asset.useStartDelay = useStartDelay;
                return this;
            }
            
            public Builder WithIsEvasive(bool isEvasive)
            {
                Asset.isEvasive = isEvasive;
                return this;
            }
            
            public Builder WithFreezeUser(bool freezeUser)
            {
                Asset.freezeUser = freezeUser;
                return this;
            }
            
            public Builder WithProjectileIDs(IList<ProjectileDataInfo> projectileIDs)
            {
                Asset.projectileIDs.Clear();
                Asset.projectileIDs.AddRange(projectileIDs);
                return this;
            }
            
            public Builder WithUseSound(AssetData useSound)
            {
                Asset.useSound = new AssetData(useSound);
                return this;
            }

            protected sealed override WeaponAsset Asset { get; } = new();
        }
    }
}