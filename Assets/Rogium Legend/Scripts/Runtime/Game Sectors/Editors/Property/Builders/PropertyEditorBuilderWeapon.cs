using System;
using BoubakProductions.UI;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
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
            b.BuildAssetField("", AssetType.Sprite, asset, content, delegate(IAsset a) { asset.UpdateIcon(a?.Icon);}, !PackEditorOverseer.Instance.CurrentPack.ContainsAnyWeapons, ThemeType.Green);
            b.BuildDropdown("Use Type", Enum.GetNames(typeof(WeaponUseType)), (int)asset.UseType, content, asset.UpdateUseType);
        }

        protected override void BuildProperty(Transform content)
        {
            b.BuildHeader("General", content);
            b.BuildInputField("Damage", asset.BaseDamage.ToString(), content, s => asset.UpdateBaseDamage(int.Parse(s)), false, TMP_InputField.CharacterValidation.Integer);
            b.BuildInputField("Attack Delay", asset.UseDelay.ToString(), content, s => asset.UpdateUseDelay(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildInputField("Attack Duration", asset.UseDuration.ToString(), content, s => asset.UpdateUseDuration(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildInputField("Knockback Self", asset.KnockbackForceSelf.ToString(), content, s => asset.UpdateKnockbackForceSelf(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildInputField("Knockback Other", asset.KnockbackForceOther.ToString(), content, s => asset.UpdateKnockbackForceOther(float.Parse(s)), false, TMP_InputField.CharacterValidation.Decimal);
            b.BuildToggle("Is Evasive", asset.IsEvasive, content, asset.UpdateIsEvasive);
            
            b.BuildHeader("Animation", content);
            b.BuildDropdown("Type", Enum.GetNames(typeof(AnimationType)), (int) asset.AnimationType, content, i => asset.UpdateAnimationType((AnimationType) i));
            b.BuildInputField("Frame Duration", asset.FrameDuration.ToString(), content, s => asset.UpdateFrameDuration(int.Parse(s)));
            
        }
    }
}