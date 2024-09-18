using System;
using Rogium.Core;
using Rogium.Editors.Weapons;
using UnityEngine;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Weapon Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderWeapon : ModalWindowPropertyBuilderBase
    {
        private readonly WeaponEditorOverseer weaponEditor = WeaponEditorOverseer.Instance;

        public override void OpenForCreate(Action whenConfirm = null) => OpenWindow(new WeaponAsset.Builder().Build(), () => CreateAsset(whenConfirm), "Creating a new Weapon", AssetModificationType.Create);

        public override void OpenForUpdate(Action whenConfirm = null) => OpenWindow(new WeaponAsset.Builder().AsCopy(weaponEditor.CurrentAsset).Build(), () => UpdateAsset(whenConfirm), $"Updating {weaponEditor.CurrentAsset.Title}", AssetModificationType.Update);

        public override void OpenForClone(Action whenConfirm = null) => OpenWindow(new WeaponAsset.Builder().AsClone(weaponEditor.CurrentAsset).Build(), () => CloneAsset(whenConfirm), $"Creating a clone of {weaponEditor.CurrentAsset.Title}", AssetModificationType.Clone);

        private void OpenWindow(WeaponAsset asset, Action onConfirm, string headerText, AssetModificationType modification)
        {
            asset.UpdateTitle(GetTitleByModificationType(asset, modification));
            OpenForColumns1(headerText, onConfirm, out Transform col1);
            
            b.BuildInputField("Title", asset.Title, col1, asset.UpdateTitle);
            b.BuildPlainText("Created by", asset.Author, col1);
            b.BuildPlainText("Created on", asset.CreationDate.ToString(), col1);
            
            editedAssetBase = asset;
        }

        protected override void CreateAsset(Action whenConfirm)
        {
            editor.CreateNewWeapon((WeaponAsset)editedAssetBase);
            whenConfirm?.Invoke();
        }

        protected override void UpdateAsset(Action whenConfirm)
        {
            weaponEditor.UpdateAsset((WeaponAsset)editedAssetBase);
            weaponEditor.CompleteEditing();
            whenConfirm?.Invoke();
        }
        
        protected override void CloneAsset(Action whenConfirm)
        {
            editor.CreateNewWeapon((WeaponAsset)editedAssetBase);
            whenConfirm?.Invoke();
        }
    }
}