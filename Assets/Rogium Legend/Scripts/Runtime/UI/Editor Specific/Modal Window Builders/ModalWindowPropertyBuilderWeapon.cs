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

        public override void OpenForCreate(Action whenConfirm = null) => OpenWindow(new WeaponAsset.Builder().Build(), () => CreateAsset(whenConfirm), "Creating a new Weapon");

        public override void OpenForUpdate(Action whenConfirm = null) => OpenWindow(new WeaponAsset.Builder().AsCopy(weaponEditor.CurrentAsset).Build(), () => UpdateAsset(whenConfirm), $"Updating {weaponEditor.CurrentAsset.Title}");

        private void OpenWindow(WeaponAsset weapon, Action onConfirm, string headerText)
        {
            OpenForColumns1(headerText, onConfirm, out Transform col1);
            
            b.BuildInputField("Title", weapon.Title, col1, weapon.UpdateTitle);
            b.BuildPlainText("Created by", weapon.Author, col1);
            b.BuildPlainText("Created on", weapon.CreationDate.ToString(), col1);
            
            editedAssetBase = weapon;
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
    }
}