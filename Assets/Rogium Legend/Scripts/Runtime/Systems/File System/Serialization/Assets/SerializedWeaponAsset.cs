using System;
using System.Collections.Generic;
using Rogium.Editors.Core;
using Rogium.Editors.Weapons;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="WeaponAsset"/>.
    /// </summary>
    [System.Serializable]
    public class SerializedWeaponAsset : SerializedEntityAssetBase<WeaponAsset>
    {
        private int useType;
        private float useDuration;
        private float useStartDelay;
        private bool isEvasive;
        private bool freezeUser;
        private List<ProjectileDataInfo> projectileIDs;

        public SerializedWeaponAsset(WeaponAsset asset) : base(asset)
        {
            useType = (int)asset.UseType;
            useDuration = asset.UseDuration;
            useStartDelay = asset.UseStartDelay;
            isEvasive = asset.IsEvasive;
            freezeUser = asset.FreezeUser;
            projectileIDs = asset.ProjectileIDs;
        }

        public override WeaponAsset Deserialize()
        {
            return new WeaponAsset(id,
                                   title,
                                   icon.Deserialize(),
                                   author,
                                   (AnimationType)animationType,
                                   frameDuration,
                                   iconAlt.Deserialize(),
                                   baseDamage,
                                   useDelay,
                                   knockbackForceSelf,
                                   knockbackTimeSelf,
                                   knockbackLockDirectionSelf,
                                   knockbackForceOther,
                                   knockbackTimeOther,
                                   knockbackLockDirectionOther,
                                   (WeaponUseType)useType,
                                   useDuration,
                                   useStartDelay,
                                   isEvasive,
                                   freezeUser,
                                   projectileIDs,
                                   DateTime.Parse(creationDate));
        }
    }
}