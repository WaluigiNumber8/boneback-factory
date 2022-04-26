using System;
using System.Collections.Generic;
using BoubakProductions.UI;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Enemies;
using Rogium.Editors.Packs;
using Rogium.Editors.Weapons;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;
using TMPro;

namespace Rogium.Editors.PropertyEditor.Builders
{
    /// <summary>
    /// Builds the property editor for <see cref="EnemyAsset"/>.
    /// </summary>
    public class PropertyEditorBuilderEnemy : PropertyEditorBuilderAnimationBase
    {
        private EnemyAsset asset;
        private PackAsset currentPack;
        private IList<WeaponAsset> packWeapons;

        private InteractablePropertyContentBlock weaponSlotsBlock;

        public PropertyEditorBuilderEnemy(Transform contentMain, Transform contentSecond) : base(contentMain, contentSecond) { }

        public void Build(EnemyAsset asset)
        {
            this.asset = asset;
            currentPack = PackEditorOverseer.Instance.CurrentPack;
            packWeapons = currentPack.Weapons;
            
            Clear();
            BuildImportant(contentMain);
            BuildProperty(contentSecond);
        }
        
        protected override void BuildImportant(Transform content)
        {
            b.BuildInputField("", asset.Title, content, asset.UpdateTitle);
            
            animationBlock1Slot = b.CreateContentBlockVertical(content, (asset.AnimationType == AnimationType.SpriteSwap));
            b.BuildAssetField("", AssetType.Sprite, asset, animationBlock1Slot.GetTransform, a => asset.UpdateIcon(a.Icon), !currentPack.ContainsAnyEnemies, ThemeType.Red);
            
            animationBlock2Slot = b.CreateContentBlockColumn2(content, (asset.AnimationType != AnimationType.SpriteSwap));
            b.BuildAssetField("", AssetType.Sprite, asset, animationBlock2Slot.GetTransform, a => asset.UpdateIcon(a.Icon), !currentPack.ContainsAnyEnemies, ThemeType.Red);
            b.BuildAssetField("", AssetType.Sprite, asset, animationBlock2Slot.GetTransform, a => asset.UpdateIconAlt(a.Icon), !currentPack.ContainsAnyEnemies, ThemeType.Red);
        }

        protected override void BuildProperty(Transform content)
        {
            b.BuildHeader("Life", content);
            b.BuildInputField("Max Health", asset.MaxHealth.ToString(), content, s => asset.UpdateMaxHealth(int.Parse(s)), false, TMP_InputField.CharacterValidation.Integer);
            b.BuildInputField("Invincibility Time", asset.InvincibilityTime.ToString(), content, s => asset.UpdateInvincibilityTime(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            
            b.BuildHeader("Damage", content);
            b.BuildInputField("Damage", asset.BaseDamage.ToString(), content, s => asset.UpdateBaseDamage(int.Parse(s)), false, TMP_InputField.CharacterValidation.Integer);
            b.BuildInputField("Knockback Self", asset.KnockbackForceSelf.ToString(), content, s => asset.UpdateKnockbackForceSelf(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildInputField("Knockback Self Time", asset.KnockbackTimeSelf.ToString(), content, s => asset.UpdateKnockbackTimeSelf(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildInputField("Knockback Other", asset.KnockbackForceOther.ToString(), content, s => asset.UpdateKnockbackForceOther(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildInputField("Knockback Other Time", asset.KnockbackTimeOther.ToString(), content, s => asset.UpdateKnockbackTimeOther(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);

            BuildWeaponContent(content);
            
            b.BuildHeader("Animation", content);
            b.BuildDropdown("Type", Enum.GetNames(typeof(AnimationType)), (int) asset.AnimationType, content, ProcessAnimationType);
            b.BuildInputField("Frame Duration", asset.FrameDuration.ToString(), content, s => asset.UpdateFrameDuration(int.Parse(s)));
        }

        private void ProcessAnimationType(int animType)
        {
            AnimationType type = (AnimationType)animType;
            asset.UpdateAnimationType(type);
            SwitchAnimationSlots(type);
        }
        
        private void BuildWeaponContent(Transform content)
        {
            b.BuildHeader("Weapons", content);
            b.BuildInputField("Attack Delay", asset.UseDelay.ToString(), content, s => asset.UpdateUseDelay(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildSlider("Weapon Amount", 0, EditorDefaults.EnemyWeaponMaxCount, asset.WeaponIDs.Count, content, f => LoadWeaponSlots((int)f), !currentPack.ContainsAnyWeapons);
            weaponSlotsBlock = b.CreateContentBlockVertical(content, true);
            LoadWeaponSlots(asset.WeaponIDs.Count);
        }

        /// <summary>
        /// Loads Weapon Slots into the sub content.
        /// </summary>
        /// <param name="amount">The amount of weapon slots.</param>
        private void LoadWeaponSlots(int amount)
        {
            if (amount <= 0)
            {
                weaponSlotsBlock.SetDisabled(true);
                return;
            }
            asset.UpdateWeaponIDsLength(amount);
            weaponSlotsBlock.Clear();
            weaponSlotsBlock.SetDisabled(false);
            IAsset weapon;

            weapon = packWeapons.FindValueFirstOrReturnFirst(asset.WeaponIDs[0]);
            b.BuildAssetField($"Weapon #1", AssetType.Weapon, weapon, weaponSlotsBlock.GetTransform, a => asset.UpdateWeaponIDPos(0, a.ID), !currentPack.ContainsAnyWeapons);

            if (amount >= 2)
            {
                weapon = packWeapons.FindValueFirstOrReturnFirst(asset.WeaponIDs[1]);
                b.BuildAssetField($"Weapon #2", AssetType.Weapon, weapon, weaponSlotsBlock.GetTransform, a => asset.UpdateWeaponIDPos(1, a.ID), !currentPack.ContainsAnyWeapons);
            }
            
            if (amount >= 3)
            {
                weapon = packWeapons.FindValueFirstOrReturnFirst(asset.WeaponIDs[2]);
                b.BuildAssetField($"Weapon #3", AssetType.Weapon, weapon, weaponSlotsBlock.GetTransform, a => asset.UpdateWeaponIDPos(2, a.ID), !currentPack.ContainsAnyWeapons);
            }
            
            if (amount >= 4)
            {
                weapon = packWeapons.FindValueFirstOrReturnFirst(asset.WeaponIDs[3]);
                b.BuildAssetField($"Weapon #4", AssetType.Weapon, weapon, weaponSlotsBlock.GetTransform, a => asset.UpdateWeaponIDPos(3, a.ID), !currentPack.ContainsAnyWeapons);
            }
        }
        
    }
}