using BoubakProductions.UI;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Weapons;
using UnityEngine;
using TMPro;

namespace Rogium.Editors.PropertyEditor.Builders
{
    /// <summary>
    /// Builds the property editor for <see cref="WeaponAsset"/>.
    /// </summary>
    public class PropertyEditorBuilderWeapon : PropertyEditorBuilderBase
    {
        private WeaponAsset asset;
        
        public PropertyEditorBuilderWeapon(Transform contentMain, Transform contentSecond) : base(contentMain, contentSecond) { }

        public void Build(WeaponAsset asset)
        {
            this.asset = asset;
            Clear();
            BuildImportant(contentMain);
            BuildProperty(contentSecond);
        }
        
        protected override void BuildImportant(Transform content)
        {
            b.BuildInputField("", asset.Title, content, asset.UpdateTitle);
            b.BuildAssetField("", AssetType.Sprite, asset, content, delegate(IAsset a) { asset.UpdateIcon(a.Icon);}, false, ThemeType.Green);
        }

        protected override void BuildProperty(Transform content)
        {
            b.BuildHeader("General", content);
            b.BuildInputField("Damage", asset.BaseDamage.ToString(), content, s => asset.UpdateBaseDamage(int.Parse(s)), false, TMP_InputField.CharacterValidation.Integer);
            b.BuildInputField("Attack Delay", asset.UseDelay.ToString(), content, s => asset.UpdateUseDelay(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildInputField("Knockback Self", asset.KnockbackForceSelf.ToString(), content, s => asset.UpdateKnockbackForceSelf(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildInputField("Knockback Other", asset.KnockbackForceOther.ToString(), content, s => asset.UpdateKnockbackForceOther(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            
        }
    }
}