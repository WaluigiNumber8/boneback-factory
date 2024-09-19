using System;
using Rogium.Core;
using Rogium.Editors.Packs;
using Rogium.Editors.Projectiles;
using UnityEngine;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Projectile Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderProjectile : ModalWindowPropertyBuilderBase
    {
        private readonly ProjectileEditorOverseer projectileEditor = ProjectileEditorOverseer.Instance;

        public override void OpenForCreate(Action whenConfirm = null) => OpenWindow(new ProjectileAsset.Builder().Build(), () => CreateAsset(whenConfirm), "Creating a new Projectile", AssetModificationType.Create);

        public override void OpenForUpdate(Action whenConfirm = null) => OpenWindow(new ProjectileAsset.Builder().AsCopy(projectileEditor.CurrentAsset).Build(), () => UpdateAsset(whenConfirm), $"Updating {projectileEditor.CurrentAsset.Title}", AssetModificationType.Update);

        public override void OpenForClone(Action whenConfirm = null) => OpenWindow(new ProjectileAsset.Builder().AsClone(projectileEditor.CurrentAsset).Build(), () => CloneAsset(whenConfirm), $"Creating a clone of {projectileEditor.CurrentAsset.Title}", AssetModificationType.Clone);
        
        private void OpenWindow(ProjectileAsset asset, Action onConfirm, string headerText, AssetModificationType modification)
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
            editor.CreateNewProjectile((ProjectileAsset)editedAssetBase);
            whenConfirm?.Invoke();
        }

        protected override void UpdateAsset(Action whenConfirm)
        {
            projectileEditor.UpdateAsset((ProjectileAsset)editedAssetBase);
            projectileEditor.CompleteEditing();
            whenConfirm?.Invoke();
        }

        protected override void CloneAsset(Action whenConfirm)
        {
            editor.CreateNewProjectile((ProjectileAsset) editedAssetBase);
            projectileEditor.AssignAsset((ProjectileAsset)editedAssetBase, PackEditorOverseer.Instance.CurrentPack.Projectiles.Count - 1);
            projectileEditor.CompleteEditing();
            whenConfirm?.Invoke();
        }
    }
}