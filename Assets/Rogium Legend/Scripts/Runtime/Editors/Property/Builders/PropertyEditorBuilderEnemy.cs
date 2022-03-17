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
        
        public PropertyEditorBuilderEnemy(Transform importantContent, Transform propertyContent) : base(importantContent, propertyContent) { }

        public void Build(EnemyAsset asset)
        {
            this.asset = asset;
            EmptyEditor();
            BuildImportant(importantContent);
            BuildProperty(propertyContent);
        }
        
        protected override void BuildImportant(Transform content)
        {
            builder.BuildInputField("", asset.Title, content, asset.UpdateTitle);
            builder.BuildAssetField("", AssetType.Sprite, asset, content, delegate(IAsset a) { asset.UpdateIcon(a.Icon);}, ThemeType.Red);
        }

        protected override void BuildProperty(Transform content)
        {
            builder.BuildHeader("Life", content);
            builder.BuildInputField("Max Health", asset.MaxHealth.ToString(), content, s => asset.UpdateMaxHealth(int.Parse(s)), TMP_InputField.CharacterValidation.Integer);
            builder.BuildInputField("Invincibility Time", asset.InvincibilityTime.ToString(), content, s => asset.UpdateInvincibilityTime(float.Parse(s)), TMP_InputField.CharacterValidation.Decimal);
            
            builder.BuildHeader("Damage", content);
            builder.BuildInputField("Damage", asset.BaseDamage.ToString(), content, s => asset.UpdateBaseDamage(int.Parse(s)), TMP_InputField.CharacterValidation.Integer);
            builder.BuildInputField("Attack Delay", asset.UseDelay.ToString(), content, s => asset.UpdateUseDelay(float.Parse(s)), TMP_InputField.CharacterValidation.Decimal);
            builder.BuildInputField("Knockback Self", asset.KnockbackForceSelf.ToString(), content, s => asset.UpdateKnockbackForceSelf(float.Parse(s)), TMP_InputField.CharacterValidation.Decimal);
            builder.BuildInputField("Knockback Self Time", asset.KnockbackTimeSelf.ToString(), content, s => asset.UpdateKnockbackTimeSelf(float.Parse(s)), TMP_InputField.CharacterValidation.Decimal);
            builder.BuildInputField("Knockback Other", asset.KnockbackForceOther.ToString(), content, s => asset.UpdateKnockbackForceOther(float.Parse(s)), TMP_InputField.CharacterValidation.Decimal);
            builder.BuildInputField("Knockback Other Time", asset.KnockbackTimeOther.ToString(), content, s => asset.UpdateKnockbackTimeOther(float.Parse(s)), TMP_InputField.CharacterValidation.Decimal);
            
        }
    }
}