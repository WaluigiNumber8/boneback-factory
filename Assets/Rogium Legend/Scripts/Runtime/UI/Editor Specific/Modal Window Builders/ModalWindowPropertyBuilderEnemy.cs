using System;
using Rogium.Core;
using Rogium.Editors.Enemies;
using UnityEngine;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Enemy Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderEnemy : ModalWindowPropertyBuilderBase
    {
        private readonly EnemyEditorOverseer enemyEditor = EnemyEditorOverseer.Instance;

        public override void OpenForCreate(Action whenConfirm = null) => OpenWindow(new EnemyAsset.Builder().Build(), () => CreateAsset(whenConfirm), "Creating a new Enemy", AssetModificationType.Create);

        public override void OpenForUpdate(Action whenConfirm = null) => OpenWindow(new EnemyAsset.Builder().AsCopy(enemyEditor.CurrentAsset).Build(), () => UpdateAsset(whenConfirm), $"Updating {enemyEditor.CurrentAsset.Title}", AssetModificationType.Update);

        public override void OpenForClone(Action whenConfirm = null) => OpenWindow(new EnemyAsset.Builder().AsClone(enemyEditor.CurrentAsset).Build(), () => CloneAsset(whenConfirm), $"Creating a clone of {enemyEditor.CurrentAsset.Title}", AssetModificationType.Clone);
        
        private void OpenWindow(EnemyAsset asset, Action onConfirm, string headerText, AssetModificationType modification)
        {
            asset.UpdateTitle(GetTitleByModificationType(asset, modification));
            OpenForColumns1(headerText, onConfirm, out Transform col);
            b.BuildInputField("Title", asset.Title, col, asset.UpdateTitle);
            b.BuildPlainText("Created by", asset.Author, col);
            b.BuildPlainText("Created on", asset.CreationDate.ToString(), col);
            
            editedAssetBase = asset;
        }

        protected override void CreateAsset(Action whenConfirm)
        {
            editor.CreateNewEnemy((EnemyAsset)editedAssetBase);
            whenConfirm?.Invoke();
        }

        protected override void UpdateAsset(Action whenConfirm)
        {
            enemyEditor.UpdateAsset((EnemyAsset)editedAssetBase);
            enemyEditor.CompleteEditing();
            whenConfirm?.Invoke();
        }
        
        protected override void CloneAsset(Action whenConfirm)
        {
            editor.CreateNewEnemy((EnemyAsset) editedAssetBase);
            enemyEditor.UpdateAsset((EnemyAsset)editedAssetBase);
            enemyEditor.CompleteEditing();
            whenConfirm?.Invoke();
        }
    }
}