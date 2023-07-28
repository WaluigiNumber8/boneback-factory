using System;
using System.Collections.Generic;
using RedRats.UI;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Weapons;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;
using TMPro;

namespace Rogium.Editors.PropertyEditor.Builders
{
    /// <summary>
    /// Builds the property editor for <see cref="WeaponAsset"/>.
    /// </summary>
    public class PropertyEditorBuilderWeapon : PropertyEditorBuilderAnimationBase
    {
        private WeaponAsset asset;
        private PackAsset currentPack;
        private IList<ProjectileAsset> packProjectiles;
        
        private InteractablePropertyContentBlock projectileSlotsBlock;
        
        public PropertyEditorBuilderWeapon(Transform contentMain, Transform contentSecond) : base(contentMain, contentSecond) { }

        public void Build(WeaponAsset asset)
        {
            this.asset = asset;
            currentPack = PackEditorOverseer.Instance.CurrentPack;
            packProjectiles = currentPack.Projectiles;
            
            Clear();
            BuildColumnImportant(contentMain);
            BuildColumnProperty(contentSecond);
        }
        
        protected override void BuildColumnImportant(Transform content)
        {
            
            animationBlock1Slot = b.CreateContentBlockVertical(content, (asset.AnimationType == AnimationType.SpriteSwap));
            b.BuildAssetField("", AssetType.Sprite, asset, animationBlock1Slot.GetTransform, a => asset.UpdateIcon(a.Icon), !currentPack.ContainsAnySprites, ThemeType.Green);
            
            animationBlock2Slot = b.CreateContentBlockColumn2(content, (asset.AnimationType != AnimationType.SpriteSwap));
            b.BuildAssetField("", AssetType.Sprite, asset, animationBlock2Slot.GetTransform, a => asset.UpdateIcon(a.Icon), !currentPack.ContainsAnySprites, ThemeType.Green);
            b.BuildAssetField("", AssetType.Sprite, asset, animationBlock2Slot.GetTransform, a => asset.UpdateIconAlt(a.Icon), !currentPack.ContainsAnySprites, ThemeType.Green);
            
            b.BuildInputField("", asset.Title, content, asset.UpdateTitle);
            b.BuildDropdown("Use Type", Enum.GetNames(typeof(WeaponUseType)), (int)asset.UseType, content, asset.UpdateUseType);
        }

        protected override void BuildColumnProperty(Transform content)
        {
            b.BuildHeader("General", content);
            b.BuildInputField("Damage", asset.BaseDamage.ToString(), content, s => asset.UpdateBaseDamage(int.Parse(s)), false, false, TMP_InputField.CharacterValidation.Integer);
            b.BuildSlider("Attack Cooldown", 0, EditorConstants.WeaponUseCooldownMax, asset.UseDelay, content, f => asset.UpdateUseDelay(f));
            b.BuildSlider("Attack Start Delay", 0, EditorConstants.WeaponUseStartDelayMax, asset.UseStartDelay, content, f => asset.UpdateUseStartDelay(f));
            b.BuildSlider("Attack Duration", 0, EditorConstants.WeaponUseDurationMax, asset.UseDuration, content, f => asset.UpdateUseDuration(f));
            b.BuildToggle("Is Evasive", asset.IsEvasive, content, asset.UpdateIsEvasive);
            b.BuildToggle("Stops User's Movement", asset.FreezeUser, content, asset.UpdateFreezeUser);

            b.BuildHeader("Knockback", content);
            b.BuildSlider("Self Force", -EditorConstants.WeaponKnockbackForceMax, EditorConstants.WeaponKnockbackForceMax, asset.KnockbackForceSelf, content, f => asset.UpdateKnockbackForceSelf(f));
            b.BuildSlider("Self Time", 0, EditorConstants.WeaponKnockbackTimeMax, asset.KnockbackTimeSelf, content, f => asset.UpdateKnockbackTimeSelf(f));
            b.BuildToggle("Self Lock Direction", asset.KnockbackLockDirectionSelf, content, asset.UpdateKnockbackLockDirectionSelf);
            b.BuildSlider("Other Force", -EditorConstants.WeaponKnockbackForceMax, EditorConstants.WeaponKnockbackForceMax, asset.KnockbackForceOther, content, f => asset.UpdateKnockbackForceOther(f));
            b.BuildSlider("Other Time", 0, EditorConstants.WeaponKnockbackTimeMax, asset.KnockbackTimeOther, content, f => asset.UpdateKnockbackTimeOther(f));
            b.BuildToggle("Other Lock Direction", asset.KnockbackLockDirectionOther, content, asset.UpdateKnockbackLockDirectionOther);
            
            b.BuildHeader("Animation", content);
            b.BuildDropdown("Type", animationOptions, (int) asset.AnimationType, content, ProcessAnimationType);
            b.BuildSlider("Frame Duration", 1, EditorConstants.WeaponFrameDurationMax, asset.FrameDuration, content, f => asset.UpdateFrameDuration((int) f));
            
            BuildProjectileContent(content);
        }
        
        private void ProcessAnimationType(int animType)
        {
            AnimationType type = (AnimationType)animType;
            asset.UpdateAnimationType(type);
            SwitchAnimationSlots(type);
        }

        private void BuildProjectileContent(Transform content)
        {
            b.BuildHeader("Projectiles", content);
            b.BuildSlider("Projectile Amount", 0, EditorConstants.WeaponProjectileMaxCount, asset.ProjectileIDs.Count, content, f => LoadProjectileSlots((int)f), !currentPack.ContainsAnyProjectiles);
            projectileSlotsBlock = b.CreateContentBlockVertical(content, true);
            LoadProjectileSlots(asset.ProjectileIDs.Count);
        }

        /// <summary>
        /// Loads Weapon Slots into the sub content.
        /// </summary>
        /// <param name="amount">The amount of weapon slots.</param>
        private void LoadProjectileSlots(int amount)
        {
            asset.UpdateProjectileIDsLength(amount);
            
            if (amount <= 0)
            {
                projectileSlotsBlock.SetDisabled(true);
                return;
            }

            projectileSlotsBlock.Clear();
            projectileSlotsBlock.SetDisabled(false);

            if (packProjectiles == null || packProjectiles.Count <= 0) return;

            for (int i = 0; i < amount; i++)
            {
                int index = i;
                IAsset p = packProjectiles.FindValueFirstOrReturnFirst(asset.ProjectileIDs[index].ID);
                b.BuildAssetField($"Projectile #{index+1}", AssetType.Projectile, p, projectileSlotsBlock.GetTransform, a => asset.UpdateProjectileIDsPosID(index, a.ID), !currentPack.ContainsAnyProjectiles);
                b.BuildSlider("Spawn Delay", 0, EditorConstants.WeaponProjectileSpawnDelayMax, asset.ProjectileIDs[index].SpawnDelay, projectileSlotsBlock.GetTransform, f => asset.UpdateProjectileIDsPosSpawnDelay(index, f), !currentPack.ContainsAnyProjectiles);
                b.BuildSlider("Angle Offset", -EditorConstants.WeaponProjectileAngleOffsetMax, EditorConstants.WeaponProjectileAngleOffsetMax, asset.ProjectileIDs[index].AngleOffset, projectileSlotsBlock.GetTransform, f => asset.UpdateProjectileIDsPosAngleOffset(index, (int) f), !currentPack.ContainsAnyProjectiles);
                asset.UpdateProjectileIDsPosID(index, p.ID);
            }
        }
    }
}