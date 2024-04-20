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
    public class JSONWeaponAsset : JSONEntityAssetBase<WeaponAsset>
    {
        public int useType;
        public float useDuration;
        public float useStartDelay;
        public bool isEvasive;
        public bool freezeUser;
        public ProjectileDataInfo[] projectileIDs;

        public JSONWeaponAsset(WeaponAsset asset) : base(asset)
        {
            useType = (int)asset.UseType;
            useDuration = asset.UseDuration;
            useStartDelay = asset.UseStartDelay;
            isEvasive = asset.IsEvasive;
            freezeUser = asset.FreezeUser;
            projectileIDs = asset.ProjectileIDs.ToArray();
        }

        public override WeaponAsset Decode()
        {
            return new WeaponAsset(id,
                                   title,
                                   icon.Decode(),
                                   author,
                                   color.Decode(),
                                   associatedSpriteID,
                                   (AnimationType)animationType,
                                   frameDuration,
                                   iconAlt.Decode(),
                                   baseDamage,
                                   useDelay,
                                   knockbackForceSelf,
                                   knockbackLockDirectionSelf,
                                   knockbackForceOther,
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