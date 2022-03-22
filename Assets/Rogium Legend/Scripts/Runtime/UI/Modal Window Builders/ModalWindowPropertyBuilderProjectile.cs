using System;
using System.Linq;
using BoubakProductions.Core;
using BoubakProductions.UI;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Tiles;
using Rogium.Editors.Weapons;

namespace Rogium.UserInterface.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Projectile Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderProjectile : ModalWindowPropertyBuilder
    {
        private ProjectileEditorOverseer projectileEditor;

        public ModalWindowPropertyBuilderProjectile()
        {
            projectileEditor = ProjectileEditorOverseer.Instance;
        }
        
        public override void OpenForCreate()
        {
            OpenWindow(new ProjectileAsset(), CreateAsset, "Creating a new Projectile");
        }

        public override void OpenForUpdate()
        {
            OpenWindow(new ProjectileAsset(projectileEditor.CurrentAsset), UpdateAsset, $"Updating {projectileEditor.CurrentAsset.Title}");
        }

        private void OpenWindow(ProjectileAsset projectile, Action onConfirmAction, string headerText)
        {
            b.BuildInputField("Title", projectile.Title, window.FirstColumnContent, projectile.UpdateTitle);
            b.BuildPlainText("Created by", projectile.Author, window.FirstColumnContent);
            b.BuildPlainText("Created on", projectile.CreationDate.ToString(), window.FirstColumnContent);
            
            editedAssetBase = projectile;
            window.OpenAsPropertiesColumn1(headerText, ThemeType.Teal, "Done", "Cancel", onConfirmAction, true);
        }

        protected override void CreateAsset()
        {
            editor.CreateNewProjectile((ProjectileAsset)editedAssetBase);
            selectionMenu.OpenForProjectiles();
        }

        protected override void UpdateAsset()
        {
            projectileEditor.UpdateAsset((ProjectileAsset)editedAssetBase);
            projectileEditor.CompleteEditing();
            selectionMenu.OpenForProjectiles();
        }
    }
}