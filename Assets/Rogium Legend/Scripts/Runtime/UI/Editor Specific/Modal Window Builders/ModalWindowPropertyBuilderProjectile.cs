using System;
using System.Linq;
using RedRats.Core;
using RedRats.UI;
using RedRats.UI.ModalWindows;
using Rogium.Editors.Core;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Tiles;
using Rogium.Editors.Weapons;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Projectile Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderProjectile : ModalWindowPropertyBuilder
    {
        private readonly ProjectileEditorOverseer projectileEditor;

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
            b.BuildInputField("Title", projectile.Title, windowColumn1, projectile.UpdateTitle);
            b.BuildPlainText("Created by", projectile.Author, windowColumn1);
            b.BuildPlainText("Created on", projectile.CreationDate.ToString(), windowColumn1);
            
            editedAssetBase = projectile;
            Open(new PropertyWindowInfo(headerText, PropertyLayoutType.Column1, ThemeType.Teal, "Done", "Cancel", onConfirmAction));
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