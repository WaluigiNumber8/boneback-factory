using System;
using BoubakProductions.UI;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Enemies;
using UnityEngine;
using TMPro;

namespace Rogium.Editors.PropertyEditor.Builders
{
    /// <summary>
    /// Builds the property editor for <see cref="EnemyAsset"/>.
    /// </summary>
    public class PropertyEditorBuilderEnemy : PropertyEditorBuilderBase
    {
        private EnemyAsset asset;
        
        public PropertyEditorBuilderEnemy(Transform contentMain, Transform contentSecond) : base(contentMain, contentSecond) { }

        public void Build(EnemyAsset asset)
        {
            this.asset = asset;
            Clear();
            BuildImportant(contentMain);
            BuildProperty(contentSecond);
        }
        
        protected override void BuildImportant(Transform content)
        {
            b.BuildInputField("", asset.Title, content, asset.UpdateTitle);
            b.BuildAssetField("", AssetType.Sprite, asset, content, delegate(IAsset a) { asset.UpdateIcon(a.Icon);}, false, ThemeType.Red);
        }

        protected override void BuildProperty(Transform content)
        {
            b.BuildHeader("Life", content);
            b.BuildInputField("Max Health", asset.MaxHealth.ToString(), content, s => asset.UpdateMaxHealth(int.Parse(s)), false, TMP_InputField.CharacterValidation.Integer);
            b.BuildInputField("Invincibility Time", asset.InvincibilityTime.ToString(), content, s => asset.UpdateInvincibilityTime(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            
            b.BuildHeader("Damage", content);
            b.BuildInputField("Damage", asset.BaseDamage.ToString(), content, s => asset.UpdateBaseDamage(int.Parse(s)), false, TMP_InputField.CharacterValidation.Integer);
            b.BuildInputField("Attack Delay", asset.UseDelay.ToString(), content, s => asset.UpdateUseDelay(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildInputField("Knockback Self", asset.KnockbackForceSelf.ToString(), content, s => asset.UpdateKnockbackForceSelf(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildInputField("Knockback Self Time", asset.KnockbackTimeSelf.ToString(), content, s => asset.UpdateKnockbackTimeSelf(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildInputField("Knockback Other", asset.KnockbackForceOther.ToString(), content, s => asset.UpdateKnockbackForceOther(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildInputField("Knockback Other Time", asset.KnockbackTimeOther.ToString(), content, s => asset.UpdateKnockbackTimeOther(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);

            b.BuildHeader("Animation", content);
            b.BuildDropdown("Type", Enum.GetNames(typeof(AnimationType)), (int) asset.AnimationType, content, i => asset.UpdateAnimationType((AnimationType) i));
            b.BuildInputField("Frame Duration", asset.FrameDuration.ToString(), content, s => asset.UpdateFrameDuration(int.Parse(s)));
        }
    }
}