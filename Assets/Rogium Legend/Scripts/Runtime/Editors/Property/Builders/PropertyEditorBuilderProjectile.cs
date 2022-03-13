using BoubakProductions.UI;
using Rogium.Core;
using Rogium.Editors.Core;
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
        
        public PropertyEditorBuilderProjectile(Transform importantContent, Transform propertyContent) : base(importantContent, propertyContent) { }

        public void Build(ProjectileAsset asset)
        {
            this.asset = asset;
            EmptyEditor();
            BuildImportant(importantContent);
            BuildProperty(propertyContent);
        }
        
        protected override void BuildImportant(Transform content)
        {
            builder.BuildInputField("", asset.Title, content, asset.UpdateTitle);
            builder.BuildAssetField("", AssetType.Sprite, asset, content, delegate(AssetBase a) { asset.UpdateIcon(a.Icon);}, ThemeType.Teal);
        }

        protected override void BuildProperty(Transform content)
        {
            builder.BuildHeader("General", content);
            builder.BuildInputField("Damage", asset.BaseDamage.ToString(), content, s => asset.UpdateBaseDamage(int.Parse(s)), TMP_InputField.CharacterValidation.Integer);
            builder.BuildInputField("Lifetime", asset.UseDelay.ToString(), content, s => asset.UpdateUseDelay(float.Parse(s)), TMP_InputField.CharacterValidation.Decimal);
            builder.BuildInputField("Knockback Self", asset.KnockbackForceSelf.ToString(), content, s => asset.UpdateKnockbackForceSelf(float.Parse(s)), TMP_InputField.CharacterValidation.Decimal);
            builder.BuildInputField("Knockback Other", asset.KnockbackForceOther.ToString(), content, s => asset.UpdateKnockbackForceOther(float.Parse(s)), TMP_InputField.CharacterValidation.Decimal);
            
        }
    }
}