using System;
using System.Linq;
using BoubakProductions.Core;
using BoubakProductions.UI;
using Rogium.Editors.Tiles;
using Rogium.Editors.Weapons;

namespace Rogium.UserInterface.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Weapon Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderWeapon : ModalWindowPropertyBuilder
    {
        private WeaponEditorOverseer weaponEditor;

        public ModalWindowPropertyBuilderWeapon()
        {
            weaponEditor = WeaponEditorOverseer.Instance;
        }
        
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
            b.BuildInputField("Title", weapon.Title, window.FirstColumnContent, weapon.UpdateTitle);
            b.BuildPlainText("Created by", weapon.Author, window.FirstColumnContent);
            b.BuildPlainText("Created on", weapon.CreationDate.ToString(), window.FirstColumnContent);
            
            editedAssetBase = weapon;
            window.OpenAsPropertiesColumn1(headerText, ThemeType.Green, "Done", "Cancel", onConfirmAction, true);
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