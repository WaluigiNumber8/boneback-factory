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
        
        public AssetData useSound;
        
        public ProjectileDataInfo[] projectileIDs;

        public JSONWeaponAsset(WeaponAsset asset) : base(asset)
        {
            useType = (int)asset.UseType;
            useDuration = asset.UseDuration;
            useStartDelay = asset.UseStartDelay;
            isEvasive = asset.IsEvasive;
            freezeUser = asset.FreezeUser;
            useSound = new AssetData(asset.UseSound);
            projectileIDs = asset.ProjectileIDs.ToArray();
        }

        public override WeaponAsset Decode()
        {
            return new WeaponAsset.WeaponBuilder()
                .WithID(id)
                .WithTitle(title)
                .WithIcon(icon.Decode())
                .WithAuthor(author)
                .WithCreationDate(DateTime.Parse(creationDate))
                .WithAssociatedSpriteID(associatedSpriteID)
                .WithBaseDamage(baseDamage)
                .WithUseDelay(useDelay)
                .WithKnockbackForceSelf(knockbackForceSelf)
                .WithKnockbackLockDirectionSelf(knockbackLockDirectionSelf)
                .WithKnockbackForceOther(knockbackForceOther)
                .WithKnockbackLockDirectionOther(knockbackLockDirectionOther)
                .WithColor(color.Decode())
                .WithAnimationType((AnimationType)animationType)
                .WithFrameDuration(frameDuration)
                .WithIconAlt(iconAlt.Decode())
                .WithUseType((WeaponUseType)useType)
                .WithUseDuration(useDuration)
                .WithUseStartDelay(useStartDelay)
                .WithIsEvasive(isEvasive)
                .WithFreezeUser(freezeUser)
                .WithProjectileIDs(projectileIDs)
                .WithUseSound(useSound)
                .Build();
        }
    }
}