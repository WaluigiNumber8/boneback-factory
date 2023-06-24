using System;
using System.Collections.Generic;
using RedRats.Core;
using RedRats.UI;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Enemies;
using Rogium.Editors.Objects;
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

        private readonly string[] aiOptions;

        private InteractablePropertyContentBlock aiLookInDirectionBlock;
        private InteractablePropertyContentBlock aiRotateTowardsBlock;
        
        private InteractablePropertyContentBlock weaponSlotsBlock;

        public PropertyEditorBuilderEnemy(Transform contentMain, Transform contentSecond) : base(contentMain, contentSecond)
        {
            aiOptions = new[] {"Look", "Rotate"};
        }

        public void Build(EnemyAsset asset)
        {
            this.asset = asset;
            currentPack = PackEditorOverseer.Instance.CurrentPack;
            packWeapons = currentPack.Weapons;
            
            Clear();
            BuildColumnImportant(contentMain);
            BuildColumnProperty(contentSecond);
        }
        
        protected override void BuildColumnImportant(Transform content)
        {
            b.BuildInputField("", asset.Title, content, asset.UpdateTitle);
            
            animationBlock1Slot = b.CreateContentBlockVertical(content, (asset.AnimationType == AnimationType.SpriteSwap));
            b.BuildAssetField("", AssetType.Sprite, asset, animationBlock1Slot.GetTransform, a => asset.UpdateIcon(a.Icon), !currentPack.ContainsAnySprites, ThemeType.Red);
            
            animationBlock2Slot = b.CreateContentBlockColumn2(content, (asset.AnimationType != AnimationType.SpriteSwap));
            b.BuildAssetField("", AssetType.Sprite, asset, animationBlock2Slot.GetTransform, a => asset.UpdateIcon(a.Icon), !currentPack.ContainsAnySprites, ThemeType.Red);
            b.BuildAssetField("", AssetType.Sprite, asset, animationBlock2Slot.GetTransform, a => asset.UpdateIconAlt(a.Icon), !currentPack.ContainsAnySprites, ThemeType.Red);

            b.BuildDropdown("AI", aiOptions, (int)asset.AI, content, ProcessAIType);
        }

        protected override void BuildColumnProperty(Transform content)
        {
            b.BuildHeader("General", content);
            b.BuildInputField("Max Health", asset.MaxHealth.ToString(), content, s => asset.UpdateMaxHealth(int.Parse(s)), false, TMP_InputField.CharacterValidation.Integer, 1);
            b.BuildInputField("Invincibility Time", asset.InvincibilityTime.ToString(), content, s => asset.UpdateInvincibilityTime(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildInputField("Damage", asset.BaseDamage.ToString(), content, s => asset.UpdateBaseDamage(int.Parse(s)), false, TMP_InputField.CharacterValidation.Integer);
            
            b.BuildHeader("Knockback", content);
            b.BuildInputField("Self Force", asset.KnockbackForceSelf.ToString(), content, s => asset.UpdateKnockbackForceSelf(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildInputField("Self Time", asset.KnockbackTimeSelf.ToString(), content, s => asset.UpdateKnockbackTimeSelf(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildToggle("Self Lock Direction", asset.KnockbackLockDirectionSelf, content, asset.UpdateKnockbackLockDirectionSelf);
            b.BuildInputField("Other Force", asset.KnockbackForceOther.ToString(), content, s => asset.UpdateKnockbackForceOther(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildInputField("Other Time", asset.KnockbackTimeOther.ToString(), content, s => asset.UpdateKnockbackTimeOther(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildToggle("Other Lock Direction", asset.KnockbackLockDirectionOther, content, asset.UpdateKnockbackLockDirectionOther);

            b.BuildHeader("AI", content);
            aiLookInDirectionBlock = b.CreateContentBlockVertical(content, (asset.AI == AIType.LookInDirection));
            b.BuildDropdown("Direction", Enum.GetNames(typeof(DirectionType)), (int)asset.StartingDirection, aiLookInDirectionBlock.GetTransform, asset.UpdateStartingDirection);

            aiRotateTowardsBlock = b.CreateContentBlockVertical(content, (asset.AI == AIType.RotateTowardsPlayer));
            b.BuildInputField("Next Rotation Time", asset.NextStepTime.ToString(), aiRotateTowardsBlock.GetTransform, s => asset.UpdateNextStepTime(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildToggle("Smooth Rotation", asset.SeamlessMovement, aiRotateTowardsBlock.GetTransform, asset.UpdateSeamlessMovement);
            
            b.BuildHeader("Animation", content);
            b.BuildDropdown("Type", animationOptions, (int) asset.AnimationType, content, ProcessAnimationType);
            b.BuildInputField("Frame Duration", asset.FrameDuration.ToString(), content, s => asset.UpdateFrameDuration(int.Parse(s)));
            
            ProcessAIType((int)asset.AI);
            BuildWeaponContent(content);
        }

        private void ProcessAnimationType(int animType)
        {
            AnimationType type = (AnimationType)animType;
            asset.UpdateAnimationType(type);
            SwitchAnimationSlots(type);
        }
        
        private void ProcessAIType(int newAIType)
        {
            asset.UpdateAI(newAIType);
            aiLookInDirectionBlock.SetDisabled((newAIType != (int)AIType.LookInDirection));
            aiRotateTowardsBlock.SetDisabled((newAIType != (int)AIType.RotateTowardsPlayer));
        }
        
        private void BuildWeaponContent(Transform content)
        {
            b.BuildHeader("Weapons", content);
            b.BuildInputField("Attack Delay", asset.UseDelay.ToString(), content, s => asset.UpdateUseDelay(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildSlider("Attack Probability", 0, 100, (int)(asset.AttackProbability * 100), content, f => asset.UpdateAttackProbability(f * 0.01f));
            b.BuildSlider("Weapon Amount", 0, EditorConstants.EnemyWeaponMaxCount, asset.WeaponIDs.Count, content, f => LoadWeaponSlots((int)f), !currentPack.ContainsAnyWeapons);
            weaponSlotsBlock = b.CreateContentBlockVertical(content, true);
            LoadWeaponSlots(asset.WeaponIDs.Count);
        }

        /// <summary>
        /// Loads Weapon Slots into the sub content.
        /// </summary>
        /// <param name="amount">The amount of weapon slots.</param>
        private void LoadWeaponSlots(int amount)
        {
            asset.UpdateWeaponIDsLength(amount);
            
            if (amount <= 0)
            {
                weaponSlotsBlock.SetDisabled(true);
                return;
            }

            weaponSlotsBlock.Clear();
            weaponSlotsBlock.SetDisabled(false);

            if (packWeapons == null || packWeapons.Count <= 0) return;
            
            IAsset weapon = packWeapons.FindValueFirstOrReturnFirst(asset.WeaponIDs[0]);
            b.BuildAssetField($"Weapon #1", AssetType.Weapon, weapon, weaponSlotsBlock.GetTransform, a => asset.UpdateWeaponIDPos(0, a.ID), !currentPack.ContainsAnyWeapons);
            asset.UpdateWeaponIDPos(0, weapon.ID);

            if (amount >= 2)
            {
                weapon = packWeapons.FindValueFirstOrReturnFirst(asset.WeaponIDs[1]);
                b.BuildAssetField($"Weapon #2", AssetType.Weapon, weapon, weaponSlotsBlock.GetTransform, a => asset.UpdateWeaponIDPos(1, a.ID), !currentPack.ContainsAnyWeapons);
                asset.UpdateWeaponIDPos(1, weapon.ID);
            }
            
            if (amount >= 3)
            {
                weapon = packWeapons.FindValueFirstOrReturnFirst(asset.WeaponIDs[2]);
                b.BuildAssetField($"Weapon #3", AssetType.Weapon, weapon, weaponSlotsBlock.GetTransform, a => asset.UpdateWeaponIDPos(2, a.ID), !currentPack.ContainsAnyWeapons);
                asset.UpdateWeaponIDPos(2, weapon.ID);
            }
            
            if (amount >= 4)
            {
                weapon = packWeapons.FindValueFirstOrReturnFirst(asset.WeaponIDs[3]);
                b.BuildAssetField($"Weapon #4", AssetType.Weapon, weapon, weaponSlotsBlock.GetTransform, a => asset.UpdateWeaponIDPos(3, a.ID), !currentPack.ContainsAnyWeapons);
                asset.UpdateWeaponIDPos(3, weapon.ID);
            }
        }
        
    }
}