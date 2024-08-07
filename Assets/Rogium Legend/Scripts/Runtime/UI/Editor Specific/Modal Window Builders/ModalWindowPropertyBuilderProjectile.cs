using System;
using Rogium.Core;
using Rogium.Editors.Projectiles;
using UnityEngine;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Projectile Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderProjectile : ModalWindowPropertyBuilderBase
    {
        private readonly ProjectileEditorOverseer projectileEditor;

        public ModalWindowPropertyBuilderProjectile()
        {
            projectileEditor = ProjectileEditorOverseer.Instance;
        }
        
        public override void OpenForCreate() => OpenWindow(new ProjectileAsset.Builder().Build(), CreateAsset, "Creating a new Projectile");

        public override void OpenForUpdate() => OpenWindow(new ProjectileAsset.Builder().AsCopy(projectileEditor.CurrentAsset).Build(), UpdateAsset, $"Updating {projectileEditor.CurrentAsset.Title}");

        private void OpenWindow(ProjectileAsset projectile, Action onConfirm, string headerText)
        {
            OpenForColumns1(headerText, onConfirm, out Transform col1);
            b.BuildInputField("Title", projectile.Title, col1, projectile.UpdateTitle);
            b.BuildPlainText("Created by", projectile.Author, col1);
            b.BuildPlainText("Created on", projectile.CreationDate.ToString(), col1);
            
            editedAssetBase = projectile;
        }

        protected override void CreateAsset()
        {
            editor.CreateNewProjectile((ProjectileAsset)editedAssetBase);
            selectionMenu.Open(AssetType.Projectile);
        }

        protected override void UpdateAsset()
        {
            projectileEditor.UpdateAsset((ProjectileAsset)editedAssetBase);
            projectileEditor.CompleteEditing();
            selectionMenu.Open(AssetType.Projectile);
        }
    }
}