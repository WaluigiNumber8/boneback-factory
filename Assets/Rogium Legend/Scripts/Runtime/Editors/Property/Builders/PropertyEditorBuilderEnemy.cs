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
            builder.BuildAssetField("", AssetType.Sprite, asset, content, delegate(AssetBase a) { asset.UpdateIcon(a.Icon);}, ThemeType.Red);
        }

        protected override void BuildProperty(Transform content)
        {
            builder.BuildHeader("General", content);
            builder.BuildInputField("Max Health", asset.MaxHealth.ToString(), content, s => asset.UpdateMaxHealth(int.Parse(s)), TMP_InputField.CharacterValidation.Integer);
            builder.BuildInputField("Invincibility Time", asset.InvincibilityTime.ToString(), content, s => asset.UpdateInvincibilityTime(float.Parse(s)), TMP_InputField.CharacterValidation.Decimal);
            builder.BuildInputField("Damage", asset.BaseDamage.ToString(), content, s => asset.UpdateBaseDamage(int.Parse(s)), TMP_InputField.CharacterValidation.Integer);
            builder.BuildInputField("Attack Delay", asset.UseDelay.ToString(), content, s => asset.UpdateUseDelay(float.Parse(s)), TMP_InputField.CharacterValidation.Decimal);
            builder.BuildInputField("Knockback Self", asset.KnockbackSelf.ToString(), content, s => asset.UpdateKnockbackSelf(float.Parse(s)), TMP_InputField.CharacterValidation.Decimal);
            builder.BuildInputField("Knockback Other", asset.KnockbackOther.ToString(), content, s => asset.UpdateKnockbackOther(float.Parse(s)), TMP_InputField.CharacterValidation.Decimal);
            
        }
    }
}