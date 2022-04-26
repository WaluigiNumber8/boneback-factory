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
        private WeaponUseType useType;
        private float useDuration;
        private bool isEvasive;
        
        #region Constructors
        public WeaponAsset()
        {
            title = EditorDefaults.WeaponTitle;
            icon = EditorDefaults.WeaponIcon;
            author = EditorDefaults.Author;
            creationDate = DateTime.Now;

            animationType = EditorDefaults.WeaponAnimationType;
            frameDuration = EditorDefaults.WeaponFrameDuration;
            iconAlt = EditorDefaults.WeaponIconAlt;
            
            baseDamage = EditorDefaults.WeaponBaseDamage;
            useDelay = EditorDefaults.WeaponUseDelay;
            knockbackForceSelf = EditorDefaults.WeaponKnockbackForceSelf;
            knockbackTimeSelf = EditorDefaults.WeaponKnockbackTimeSelf;
            knockbackForceOther = EditorDefaults.WeaponKnockbackForceOther;
            knockbackTimeOther = EditorDefaults.WeaponKnockbackTimeOther;

            useType = EditorDefaults.WeaponUseType;
            useDuration = EditorDefaults.WeaponUseDuration;
            isEvasive = EditorDefaults.WeaponIsEvasive;
            
            GenerateID(EditorAssetIDs.WeaponIdentifier);
        }

        public WeaponAsset(WeaponAsset asset)
        {
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
            knockbackForceOther = asset.KnockbackForceOther;
            knockbackTimeOther = asset.KnockbackTimeOther;

            useType = asset.UseType;
            useDuration = asset.UseDuration;
            isEvasive = asset.IsEvasive;
        }

        public WeaponAsset(string id, string title, Sprite icon, string author, AnimationType animationType, 
                           int frameDuration, Sprite iconAlt,int baseDamage, float useDelay, float knockbackForceSelf,
                           float knockbackTimeSelf, float knockbackForceOther, float knockbackTimeOther, WeaponUseType useType,
                           float useDuration, bool isEvasive, DateTime creationDate)
        {
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
            this.knockbackForceOther = knockbackForceOther;
            this.knockbackTimeOther = knockbackTimeOther;

            this.useType = useType;
            this.useDuration = useDuration;
            this.isEvasive = isEvasive;

        }
        #endregion

        #region Update Values
        public void UpdateUseType(WeaponUseType newUseType) => useType = newUseType;
        public void UpdateUseType(int newUseType) => useType = (WeaponUseType)newUseType;
        public void UpdateUseDuration(float newUseDuration) => useDuration = newUseDuration;
        public void UpdateIsEvasive(bool newIsEvasive) => isEvasive = newIsEvasive;
        #endregion

        public WeaponUseType UseType { get => useType; }
        public float UseDuration { get => useDuration; }
        public bool IsEvasive { get => isEvasive; }
    }
}