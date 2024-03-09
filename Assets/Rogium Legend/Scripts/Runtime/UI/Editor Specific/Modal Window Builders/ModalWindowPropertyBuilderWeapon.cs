using System;
using RedRats.UI.ModalWindows;
using Rogium.Editors.Weapons;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Weapon Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderWeapon : ModalWindowPropertyBuilder
    {
        private readonly WeaponEditorOverseer weaponEditor;

        public ModalWindowPropertyBuilderWeapon() => weaponEditor = WeaponEditorOverseer.Instance;

        public override void OpenForCreate()
        {
            OpenWindow(new WeaponAsset(), CreateAsset, "Creating a new Weapon");
        }

        public override void OpenForUpdate()
        {
            OpenWindow(new WeaponAsset(weaponEditor.CurrentAsset), UpdateAsset, $"Updating {weaponEditor.CurrentAsset.Title}");
        }

        private void OpenWindow(WeaponAsset weapon, Action onConfirmAction, string headerText)
        {
            b.BuildInputField("Title", weapon.Title, windowColumn1, weapon.UpdateTitle);
            b.BuildPlainText("Created by", weapon.Author, windowColumn1);
            b.BuildPlainText("Created on", weapon.CreationDate.ToString(), windowColumn1);
            
            editedAssetBase = weapon;
            Open(new PropertyWindowInfo(headerText, PropertyLayoutType.Column1, "Done", "Cancel", onConfirmAction));
        }

        protected override void CreateAsset()
        {
            editor.CreateNewWeapon((WeaponAsset)editedAssetBase);
            selectionMenu.OpenForWeapons();
        }

        protected override void UpdateAsset()
        {
            weaponEditor.UpdateAsset((WeaponAsset)editedAssetBase);
            weaponEditor.CompleteEditing();
            selectionMenu.OpenForWeapons();
        }
    }
}