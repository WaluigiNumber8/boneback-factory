using System;
using RedRats.UI;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using Rogium.Editors.Projectiles;
using UnityEngine;
using TMPro;

namespace Rogium.Editors.PropertyEditor.Builders
{
    /// <summary>
    /// Builds the property editor for <see cref="ProjectileAsset"/>.
    /// </summary>
    public class PropertyEditorBuilderProjectile : PropertyEditorBuilderAnimationBase
    {
        private ProjectileAsset asset;
        private PackAsset currentPack;
        
        public PropertyEditorBuilderProjectile(Transform contentMain, Transform contentSecond) : base(contentMain, contentSecond) { }

        public void Build(ProjectileAsset asset)
        {
            this.asset = asset;
            currentPack = PackEditorOverseer.Instance.CurrentPack;
            Clear();
            BuildColumnImportant(contentMain);
            BuildColumnProperty(contentSecond);
        }
        
        protected override void BuildColumnImportant(Transform content)
        {
            animationBlock1Slot = b.CreateContentBlockVertical(content, (asset.AnimationType == AnimationType.SpriteSwap));
            b.BuildAssetField("", AssetType.Sprite, asset, animationBlock1Slot.GetTransform, a => asset.UpdateIcon(a.Icon), !currentPack.ContainsAnySprites, ThemeType.Teal);
            
            animationBlock2Slot = b.CreateContentBlockColumn2(content, (asset.AnimationType != AnimationType.SpriteSwap));
            b.BuildAssetField("", AssetType.Sprite, asset, animationBlock2Slot.GetTransform, a => asset.UpdateIcon(a.Icon), !currentPack.ContainsAnySprites, ThemeType.Teal);
            b.BuildAssetField("", AssetType.Sprite, asset, animationBlock2Slot.GetTransform, a => asset.UpdateIconAlt(a.Icon), !currentPack.ContainsAnySprites, ThemeType.Teal);
            
            b.BuildInputField("", asset.Title, content, asset.UpdateTitle);
        }

        protected override void BuildColumnProperty(Transform content)
        {
            b.BuildHeader("General", content);
            b.BuildInputField("Damage", asset.BaseDamage.ToString(), content, s => asset.UpdateBaseDamage(int.Parse(s)),false, false, TMP_InputField.CharacterValidation.Integer);
            b.BuildInputField("Lifetime", asset.UseDelay.ToString(), content, s => asset.UpdateUseDelay(float.Parse(s)),false, false, TMP_InputField.CharacterValidation.Decimal, 0.01f, EditorConstants.ProjectileLifetimeMax);
            b.BuildDropdown("Pierce", Enum.GetNames(typeof(PierceType)), (int)asset.PierceType, content, asset.UpdatePierceType);

            b.BuildHeader("Movement", content);
            b.BuildInputField("Flight Speed", asset.FlightSpeed.ToString(), content, s => asset.UpdateFlightSpeed(float.Parse(s)));
            b.BuildSlider("Acceleration", 0.01f, EditorConstants.ProjectileMaxAcceleration, asset.Acceleration, content, asset.UpdateAcceleration);
            b.BuildSlider("Brake Force", 0.01f, EditorConstants.ProjectileMaxBrakeForce, asset.BrakeForce, content, asset.UpdateBrakeForce);
            
            b.BuildHeader("Knockback", content);
            b.BuildSlider("Self Force", -EditorConstants.ProjectileKnockbackForceMax, EditorConstants.ProjectileKnockbackForceMax, asset.KnockbackForceSelf, content, f => asset.UpdateKnockbackForceSelf(f));
            b.BuildSlider("Self Time", 0, EditorConstants.ProjectileKnockbackTimeMax, asset.KnockbackTimeSelf, content, f => asset.UpdateKnockbackTimeSelf(f));
            b.BuildToggle("Self Lock Direction", asset.KnockbackLockDirectionSelf, content, asset.UpdateKnockbackLockDirectionSelf);
            b.BuildSlider("Other Force", -EditorConstants.ProjectileKnockbackForceMax, EditorConstants.ProjectileKnockbackForceMax, asset.KnockbackForceOther, content, f => asset.UpdateKnockbackForceOther(f));
            b.BuildSlider("Other Time", 0, EditorConstants.ProjectileKnockbackTimeMax, asset.KnockbackTimeOther, content, f => asset.UpdateKnockbackTimeOther(f));
            b.BuildToggle("Other Lock Direction", asset.KnockbackLockDirectionOther, content, asset.UpdateKnockbackLockDirectionOther);
            
            b.BuildHeader("Animation", content);
            b.BuildDropdown("Type", animationOptions, (int) asset.AnimationType, content, i => asset.UpdateAnimationType((AnimationType) i));
            b.BuildSlider("Frame Duration", 0, EditorConstants.ProjectileFrameDurationMax, asset.FrameDuration, content, f => asset.UpdateFrameDuration((int) f));
        }
    }
}