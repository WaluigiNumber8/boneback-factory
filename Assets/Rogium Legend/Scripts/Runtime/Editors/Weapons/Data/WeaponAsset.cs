using System;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using UnityEngine;

namespace Rogium.Editors.Weapons
{
    /// <summary>
    /// Contains all data needed for a weapon.
    /// </summary>
    public class WeaponAsset : EntityAssetBase
    {
        
        
        #region Constructors
        public WeaponAsset()
        {
            title = EditorDefaults.WeaponTitle;
            icon = EditorDefaults.WeaponIcon;
            author = EditorDefaults.Author;
            creationDate = DateTime.Now;

            baseDamage = EditorDefaults.WeaponBaseDamage;
            useDelay = EditorDefaults.WeaponUseDelay;
            knockbackForceSelf = EditorDefaults.WeaponKnockbackSelf;
            knockbackForceOther = EditorDefaults.WeaponKnockbackOther;
            
            GenerateID(EditorAssetIDs.WeaponIdentifier);
        }

        public WeaponAsset(WeaponAsset asset)
        {
            id = asset.ID;
            title = asset.Title;
            icon = asset.Icon;
            author = asset.Author;
            creationDate = asset.CreationDate;

            baseDamage = asset.BaseDamage;
            useDelay = asset.UseDelay;
            knockbackForceSelf = asset.KnockbackForceSelf;
            knockbackForceOther = asset.KnockbackForceOther;
        }

        public WeaponAsset(string id, string title, Sprite icon, string author, int baseDamage, float useDelay,
                           float knockbackForceSelf, float knockbackForceOther, DateTime creationDate)
        {
            this.id = id;
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;

            this.baseDamage = baseDamage;
            this.useDelay = useDelay;
            this.knockbackForceSelf = knockbackForceSelf;
            this.knockbackForceOther = knockbackForceOther;
            
        }
        #endregion

        #region Update Values



        #endregion
        
    }
}