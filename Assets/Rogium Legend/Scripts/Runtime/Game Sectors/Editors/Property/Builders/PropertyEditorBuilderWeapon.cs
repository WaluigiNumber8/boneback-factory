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
            b.BuildInputField("", asset.Title, content, asset.UpdateTitle);
            
            animationBlock1Slot = b.CreateContentBlockVertical(content, (asset.AnimationType == AnimationType.SpriteSwap));
            b.BuildAssetField("", AssetType.Sprite, asset, animationBlock1Slot.GetTransform, a => asset.UpdateIcon(a.Icon), !currentPack.ContainsAnySprites, ThemeType.Green);
            
            animationBlock2Slot = b.CreateContentBlockColumn2(content, (asset.AnimationType != AnimationType.SpriteSwap));
            b.BuildAssetField("", AssetType.Sprite, asset, animationBlock2Slot.GetTransform, a => asset.UpdateIcon(a.Icon), !currentPack.ContainsAnySprites, ThemeType.Green);
            b.BuildAssetField("", AssetType.Sprite, asset, animationBlock2Slot.GetTransform, a => asset.UpdateIconAlt(a.Icon), !currentPack.ContainsAnySprites, ThemeType.Green);
            
            b.BuildDropdown("Use Type", Enum.GetNames(typeof(WeaponUseType)), (int)asset.UseType, content, asset.UpdateUseType);
        }

        protected override void BuildColumnProperty(Transform content)
        {
            b.BuildHeader("General", content);
            b.BuildInputField("Damage", asset.BaseDamage.ToString(), content, s => asset.UpdateBaseDamage(int.Parse(s)), false, TMP_InputField.CharacterValidation.Integer);
            b.BuildInputField("Attack Delay", asset.UseDelay.ToString(), content, s => asset.UpdateUseDelay(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildInputField("Attack Start Delay", asset.UseStartDelay.ToString(), content, s => asset.UpdateUseStartDelay(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildInputField("Attack Duration", asset.UseDuration.ToString(), content, s => asset.UpdateUseDuration(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildToggle("Is Evasive", asset.IsEvasive, content, asset.UpdateIsEvasive);
            b.BuildToggle("Freeze User", asset.FreezeUser, content, asset.UpdateFreezeUser);

            b.BuildHeader("Knockback", content);
            b.BuildInputField("Self Force", asset.KnockbackForceSelf.ToString(), content, s => asset.UpdateKnockbackForceSelf(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildInputField("Self Time", asset.KnockbackTimeSelf.ToString(), content, s => asset.UpdateKnockbackTimeSelf(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildToggle("Self Lock Direction", asset.KnockbackLockDirectionSelf, content, asset.UpdateKnockbackLockDirectionSelf);
            b.BuildInputField("Other Force", asset.KnockbackForceOther.ToString(), content, s => asset.UpdateKnockbackForceOther(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildInputField("Other Time", asset.KnockbackTimeOther.ToString(), content, s => asset.UpdateKnockbackTimeOther(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildToggle("Other Lock Direction", asset.KnockbackLockDirectionOther, content, asset.UpdateKnockbackLockDirectionOther);
            
            b.BuildHeader("Animation", content);
            b.BuildDropdown("Type", animationOptions, (int) asset.AnimationType, content, ProcessAnimationType);
            b.BuildInputField("Frame Duration", asset.FrameDuration.ToString(), content, s => asset.UpdateFrameDuration(int.Parse(s)));
            
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
            
            IAsset projectile = packProjectiles.FindValueFirstOrReturnFirst(asset.ProjectileIDs[0].ID);
            b.BuildAssetField("Projectile #1", AssetType.Projectile, projectile, projectileSlotsBlock.GetTransform, a => asset.UpdateProjectileIDsPosID(0, a.ID), !currentPack.ContainsAnyProjectiles);
            b.BuildInputField("Spawn Delay", asset.ProjectileIDs[0].SpawnDelay.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosSpawnDelay(0, float.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Decimal);
            b.BuildInputField("Angle Offset", asset.ProjectileIDs[0].AngleOffset.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosAngleOffset(0, int.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Integer);
            asset.UpdateProjectileIDsPosID(0, projectile.ID);

            if (amount >= 2)
            {
                projectile = packProjectiles.FindValueFirstOrReturnFirst(asset.ProjectileIDs[1].ID);
                b.BuildAssetField("Projectile #2", AssetType.Projectile, projectile, projectileSlotsBlock.GetTransform, a => asset.UpdateProjectileIDsPosID(1, a.ID), !currentPack.ContainsAnyProjectiles);
                b.BuildInputField("Spawn Delay", asset.ProjectileIDs[1].SpawnDelay.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosSpawnDelay(1, float.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Decimal);
                b.BuildInputField("Angle Offset", asset.ProjectileIDs[1].AngleOffset.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosAngleOffset(1, int.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Integer);
                asset.UpdateProjectileIDsPosID(1, projectile.ID);
            }
            
            if (amount >= 3)
            {
                projectile = packProjectiles.FindValueFirstOrReturnFirst(asset.ProjectileIDs[2].ID);
                b.BuildAssetField("Projectile #3", AssetType.Projectile, projectile, projectileSlotsBlock.GetTransform, a => asset.UpdateProjectileIDsPosID(2, a.ID), !currentPack.ContainsAnyProjectiles);
                b.BuildInputField("Spawn Delay", asset.ProjectileIDs[2].SpawnDelay.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosSpawnDelay(2, float.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Decimal);
                b.BuildInputField("Angle Offset", asset.ProjectileIDs[2].AngleOffset.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosAngleOffset(2, int.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Integer);
                asset.UpdateProjectileIDsPosID(2, projectile.ID);
            }
            
            if (amount >= 4)
            {
                projectile = packProjectiles.FindValueFirstOrReturnFirst(asset.ProjectileIDs[3].ID);
                b.BuildAssetField("Projectile #4", AssetType.Projectile, projectile, projectileSlotsBlock.GetTransform, a => asset.UpdateProjectileIDsPosID(3, a.ID), !currentPack.ContainsAnyProjectiles);
                b.BuildInputField("Spawn Delay", asset.ProjectileIDs[3].SpawnDelay.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosSpawnDelay(3, float.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Decimal);
                b.BuildInputField("Angle Offset", asset.ProjectileIDs[3].AngleOffset.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosAngleOffset(3, int.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Integer);
                asset.UpdateProjectileIDsPosID(3, projectile.ID);
            }
            
            if (amount >= 5)
            {
                projectile = packProjectiles.FindValueFirstOrReturnFirst(asset.ProjectileIDs[4].ID);
                b.BuildAssetField("Projectile #5", AssetType.Projectile, projectile, projectileSlotsBlock.GetTransform, a => asset.UpdateProjectileIDsPosID(4, a.ID), !currentPack.ContainsAnyProjectiles);
                b.BuildInputField("Spawn Delay", asset.ProjectileIDs[4].SpawnDelay.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosSpawnDelay(4, float.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Decimal);
                b.BuildInputField("Angle Offset", asset.ProjectileIDs[4].AngleOffset.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosAngleOffset(4, int.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Integer);
                asset.UpdateProjectileIDsPosID(4, projectile.ID);
            }
            
            if (amount >= 6)
            {
                projectile = packProjectiles.FindValueFirstOrReturnFirst(asset.ProjectileIDs[5].ID);
                b.BuildAssetField("Projectile #6", AssetType.Projectile, projectile, projectileSlotsBlock.GetTransform, a => asset.UpdateProjectileIDsPosID(5, a.ID), !currentPack.ContainsAnyProjectiles);
                b.BuildInputField("Spawn Delay", asset.ProjectileIDs[5].SpawnDelay.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosSpawnDelay(5, float.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Decimal);
                b.BuildInputField("Angle Offset", asset.ProjectileIDs[5].AngleOffset.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosAngleOffset(5, int.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Integer);
                asset.UpdateProjectileIDsPosID(5, projectile.ID);
            }
            
            if (amount >= 7)
            {
                projectile = packProjectiles.FindValueFirstOrReturnFirst(asset.ProjectileIDs[6].ID);
                b.BuildAssetField("Projectile #7", AssetType.Projectile, projectile, projectileSlotsBlock.GetTransform, a => asset.UpdateProjectileIDsPosID(6, a.ID), !currentPack.ContainsAnyProjectiles);
                b.BuildInputField("Spawn Delay", asset.ProjectileIDs[6].SpawnDelay.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosSpawnDelay(6, float.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Decimal);
                b.BuildInputField("Angle Offset", asset.ProjectileIDs[6].AngleOffset.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosAngleOffset(6, int.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Integer);
                asset.UpdateProjectileIDsPosID(6, projectile.ID);
            }
            
            if (amount >= 8)
            {
                projectile = packProjectiles.FindValueFirstOrReturnFirst(asset.ProjectileIDs[7].ID);
                b.BuildAssetField("Projectile #8", AssetType.Projectile, projectile, projectileSlotsBlock.GetTransform, a => asset.UpdateProjectileIDsPosID(7, a.ID), !currentPack.ContainsAnyProjectiles);
                b.BuildInputField("Spawn Delay", asset.ProjectileIDs[7].SpawnDelay.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosSpawnDelay(7, float.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Decimal);
                b.BuildInputField("Angle Offset", asset.ProjectileIDs[7].AngleOffset.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosAngleOffset(7, int.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Integer);
                asset.UpdateProjectileIDsPosID(7, projectile.ID);
            }
            
            if (amount >= 9)
            {
                projectile = packProjectiles.FindValueFirstOrReturnFirst(asset.ProjectileIDs[8].ID);
                b.BuildAssetField("Projectile #9", AssetType.Projectile, projectile, projectileSlotsBlock.GetTransform, a => asset.UpdateProjectileIDsPosID(8, a.ID), !currentPack.ContainsAnyProjectiles);
                b.BuildInputField("Spawn Delay", asset.ProjectileIDs[8].SpawnDelay.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosSpawnDelay(8, float.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Decimal);
                b.BuildInputField("Angle Offset", asset.ProjectileIDs[8].AngleOffset.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosAngleOffset(8, int.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Integer);
                asset.UpdateProjectileIDsPosID(8, projectile.ID);
            }
            
            if (amount >= 10)
            {
                projectile = packProjectiles.FindValueFirstOrReturnFirst(asset.ProjectileIDs[9].ID);
                b.BuildAssetField("Projectile #10", AssetType.Projectile, projectile, projectileSlotsBlock.GetTransform, a => asset.UpdateProjectileIDsPosID(9, a.ID), !currentPack.ContainsAnyProjectiles);
                b.BuildInputField("Spawn Delay", asset.ProjectileIDs[9].SpawnDelay.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosSpawnDelay(9, float.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Decimal);
                b.BuildInputField("Angle Offset", asset.ProjectileIDs[9].AngleOffset.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosAngleOffset(9, int.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Integer);
                asset.UpdateProjectileIDsPosID(9, projectile.ID);
            }
            
            if (amount >= 11)
            {
                projectile = packProjectiles.FindValueFirstOrReturnFirst(asset.ProjectileIDs[10].ID);
                b.BuildAssetField("Projectile #11", AssetType.Projectile, projectile, projectileSlotsBlock.GetTransform, a => asset.UpdateProjectileIDsPosID(10, a.ID), !currentPack.ContainsAnyProjectiles);
                b.BuildInputField("Spawn Delay", asset.ProjectileIDs[10].SpawnDelay.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosSpawnDelay(10, float.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Decimal);
                b.BuildInputField("Angle Offset", asset.ProjectileIDs[10].AngleOffset.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosAngleOffset(10, int.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Integer);
                asset.UpdateProjectileIDsPosID(10, projectile.ID);
            }
            
            if (amount >= 12)
            {
                projectile = packProjectiles.FindValueFirstOrReturnFirst(asset.ProjectileIDs[11].ID);
                b.BuildAssetField("Projectile #12", AssetType.Projectile, projectile, projectileSlotsBlock.GetTransform, a => asset.UpdateProjectileIDsPosID(11, a.ID), !currentPack.ContainsAnyProjectiles);
                b.BuildInputField("Spawn Delay", asset.ProjectileIDs[11].SpawnDelay.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosSpawnDelay(11, float.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Decimal);
                b.BuildInputField("Angle Offset", asset.ProjectileIDs[11].AngleOffset.ToString(), projectileSlotsBlock.GetTransform, s => asset.UpdateProjectileIDsPosAngleOffset(11, int.Parse(s)), !currentPack.ContainsAnyProjectiles, TMP_InputField.CharacterValidation.Integer);
                asset.UpdateProjectileIDsPosID(11, projectile.ID);
            }
        }
    }
}