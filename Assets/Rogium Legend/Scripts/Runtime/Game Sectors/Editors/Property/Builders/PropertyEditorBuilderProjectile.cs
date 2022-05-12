using System;
using BoubakProductions.UI;
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
    public class PropertyEditorBuilderProjectile : PropertyEditorBuilderBase
    {
        private ProjectileAsset asset;
        
        public PropertyEditorBuilderProjectile(Transform contentMain, Transform contentSecond) : base(contentMain, contentSecond) { }

        public void Build(ProjectileAsset asset)
        {
            this.asset = asset;
            Clear();
            BuildImportant(contentMain);
            BuildProperty(contentSecond);
        }
        
        protected override void BuildImportant(Transform content)
        {
            b.BuildInputField("", asset.Title, content, asset.UpdateTitle);
            b.BuildAssetField("", AssetType.Sprite, asset, content, delegate(IAsset a) { asset.UpdateIcon(a.Icon);}, !PackEditorOverseer.Instance.CurrentPack.ContainsAnyProjectiles, ThemeType.Teal);
        }

        protected override void BuildProperty(Transform content)
        {
            b.BuildHeader("General", content);
            b.BuildInputField("Damage", asset.BaseDamage.ToString(), content, s => asset.UpdateBaseDamage(int.Parse(s)),false, TMP_InputField.CharacterValidation.Integer);
            b.BuildInputField("Lifetime", asset.UseDelay.ToString(), content, s => asset.UpdateUseDelay(float.Parse(s)),false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildDropdown("Pierce", Enum.GetNames(typeof(PierceType)), (int)asset.PierceType, content, asset.UpdatePierceType);

            b.BuildHeader("Movement", content);
            b.BuildInputField("Flight Speed", asset.FlightSpeed.ToString(), content, s => asset.UpdateFlightSpeed(float.Parse(s)));
            b.BuildSlider("Acceleration", 0, EditorDefaults.ProjectileMaxAcceleration, asset.Acceleration, content, asset.UpdateAcceleration);
            b.BuildSlider("Brake Force", 0, EditorDefaults.ProjectileMaxBrakeForce, asset.BrakeForce, content, asset.UpdateBrakeForce);
            
            b.BuildHeader("Knockback", content);
            b.BuildInputField("Self Force", asset.KnockbackForceSelf.ToString(), content, s => asset.UpdateKnockbackForceSelf(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildInputField("Self Time", asset.KnockbackTimeSelf.ToString(), content, s => asset.UpdateKnockbackTimeSelf(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildToggle("Self Lock Direction", asset.KnockbackLockDirectionSelf, content, asset.UpdateKnockbackLockDirectionSelf);
            b.BuildInputField("Other Force", asset.KnockbackForceOther.ToString(), content, s => asset.UpdateKnockbackForceOther(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildInputField("Other Time", asset.KnockbackTimeOther.ToString(), content, s => asset.UpdateKnockbackTimeOther(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildToggle("Other Lock Direction", asset.KnockbackLockDirectionOther, content, asset.UpdateKnockbackLockDirectionOther);
            
            b.BuildHeader("Animation", content);
            b.BuildDropdown("Type", Enum.GetNames(typeof(AnimationType)), (int) asset.AnimationType, content, i => asset.UpdateAnimationType((AnimationType) i));
            b.BuildInputField("Frame Duration", asset.FrameDuration.ToString(), content, s => asset.UpdateFrameDuration(int.Parse(s)));
        }
    }
}