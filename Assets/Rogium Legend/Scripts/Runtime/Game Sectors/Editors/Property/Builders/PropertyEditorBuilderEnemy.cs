using System;
using System.Collections.Generic;
using RedRats.Core;
using RedRats.UI;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Enemies;
using Rogium.Editors.Packs;
using Rogium.Editors.Sprites;
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
            
            animationBlock1Slot = b.CreateContentBlockVertical(content, (asset.AnimationType == AnimationType.SpriteSwap));
            b.BuildAssetField("", AssetType.Sprite, asset, animationBlock1Slot.GetTransform, a => asset.UpdateIcon(a), !currentPack.ContainsAnySprites, ThemeType.Red);
            
            animationBlock2Slot = b.CreateContentBlockColumn2(content, (asset.AnimationType != AnimationType.SpriteSwap));
            b.BuildAssetField("", AssetType.Sprite, asset, animationBlock2Slot.GetTransform, a => asset.UpdateIcon(a), !currentPack.ContainsAnySprites, ThemeType.Red);
            b.BuildAssetField("", AssetType.Sprite, asset, animationBlock2Slot.GetTransform, a => asset.UpdateIconAlt(a.Icon), !currentPack.ContainsAnySprites, ThemeType.Red);

            b.BuildInputField("", asset.Title, content, asset.UpdateTitle);
            b.BuildDropdown("AI", aiOptions, (int)asset.AI, content, ProcessAIType);
        }

        protected override void BuildColumnProperty(Transform content)
        {
            b.BuildHeader("General", content);
            b.BuildInputField("Max Health", asset.MaxHealth.ToString(), content, s => asset.UpdateMaxHealth(int.Parse(s)), false, false, TMP_InputField.CharacterValidation.Integer, 1);
            b.BuildInputField("Damage", asset.BaseDamage.ToString(), content, s => asset.UpdateBaseDamage(int.Parse(s)), false, false, TMP_InputField.CharacterValidation.Integer);
            b.BuildSlider("Invincibility Time", 0f, EditorConstants.EnemyInvincibilityTimeMax, asset.InvincibilityTime, content, f => asset.UpdateInvincibilityTime(f));
            
            b.BuildHeader("Knockback", content);
            b.BuildSlider("Self Force", -EditorConstants.EnemyKnockbackForceMax, EditorConstants.EnemyKnockbackForceMax, asset.KnockbackForceSelf, content, f => asset.UpdateKnockbackForceSelf(f));
            b.BuildSlider("Self Time", 0f, EditorConstants.EnemyKnockbackTimeMax, asset.KnockbackTimeSelf, content, f => asset.UpdateKnockbackTimeSelf(f));
            b.BuildToggle("Self Lock Direction", asset.KnockbackLockDirectionSelf, content, asset.UpdateKnockbackLockDirectionSelf);
            b.BuildSlider("Other Force", -EditorConstants.EnemyKnockbackForceMax, EditorConstants.EnemyKnockbackForceMax, asset.KnockbackForceOther, content, f => asset.UpdateKnockbackForceOther(f));
            b.BuildSlider("Other Time", 0f, EditorConstants.EnemyKnockbackTimeMax, asset.KnockbackTimeOther, content, f => asset.UpdateKnockbackTimeOther(f));
            b.BuildToggle("Other Lock Direction", asset.KnockbackLockDirectionOther, content, asset.UpdateKnockbackLockDirectionOther);

            b.BuildHeader("AI", content);
            aiLookInDirectionBlock = b.CreateContentBlockVertical(content, (asset.AI == AIType.LookInDirection));
            b.BuildDropdown("Direction", Enum.GetNames(typeof(DirectionType)), (int)asset.StartingDirection, aiLookInDirectionBlock.GetTransform, asset.UpdateStartingDirection);

            aiRotateTowardsBlock = b.CreateContentBlockVertical(content, (asset.AI == AIType.RotateTowardsPlayer));
            b.BuildSlider("Next Rotation Time", 0.01f, EditorConstants.EnemyNextStepTimeMax, asset.NextStepTime, aiRotateTowardsBlock.GetTransform, f => asset.UpdateNextStepTime(f));
            b.BuildToggle("Smooth Rotation", asset.SeamlessMovement, aiRotateTowardsBlock.GetTransform, asset.UpdateSeamlessMovement);
            
            b.BuildHeader("Animation", content);
            b.BuildDropdown("Type", animationOptions, (int) asset.AnimationType, content, ProcessAnimationType);
            b.BuildSlider("Frame Duration", 1, EditorConstants.EnemyFrameDurationMax, asset.FrameDuration, content, i => asset.UpdateFrameDuration((int)i));
            
            ProcessAIType((int)asset.AI);
            BuildWeaponSection(content);
            
            b.BuildHeader("Sound", content);
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
        
        private void BuildWeaponSection(Transform content)
        {
            b.BuildHeader("Weapons", content);
            b.BuildSlider("Attack Delay", 0f, EditorConstants.EnemyAttackDelayMax, asset.UseDelay, content, f => asset.UpdateUseDelay(f));
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

            for (int i = 0; i < amount; i++)
            {
                int index = i;
                IAsset w = packWeapons.FindValueFirstOrReturnFirst(asset.WeaponIDs[i]);
                b.BuildAssetField($"Weapon #{index+1}", AssetType.Weapon, w, weaponSlotsBlock.GetTransform, a => asset.UpdateWeaponIDPos(index, a.ID), !currentPack.ContainsAnyWeapons);
                asset.UpdateWeaponIDPos(i, w.ID);
            }
        }
        
    }
}